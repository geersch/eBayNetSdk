using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Util;
using Samples.Helper;
using eBayApi = eBay.Service;

namespace eBayPublisher
{
    public class EBayFileProcessor : FileProcessor
    {
        #region Fields

        private static readonly ApiContext _apiContext;
        private static readonly eBayDataContext _dataContext = new eBayDataContext();

        #endregion

        #region Constructor(s)

        static EBayFileProcessor()
        {
            _apiContext = AppSettingHelper.GetApiContext();
            _apiContext.ApiLogManager = new ApiLogManager();
            _apiContext.ApiLogManager.ApiLoggerList.Add(new FileLogger(AppSettingHelper.LOG_FILE_NAME, true, true, true));
            _apiContext.ApiLogManager.EnableLogging = true;
            _apiContext.Site = SiteCodeType.US;
        }

        #endregion

        #region Build Item

        private ReturnPolicyType GetPolicyForUS()
        {
            ReturnPolicyType policy =
                new ReturnPolicyType
                {
                    Refund = "MoneyBack",
                    ReturnsWithinOption = "Days_3",
                    ReturnsAcceptedOption = "ReturnsAccepted",
                    ShippingCostPaidByOption = "Buyer"
                };
            return policy;
        }

        private ItemType BuildItem(EBayProductModel product)
        {
            ItemType item = new ItemType();

            // Description of the item
            item.Title = product.ProductName;
            item.Description = product.ProductName;

            // Listing type and price
            item.ListingType = ListingTypeCodeType.FixedPriceItem;
            item.Currency = CurrencyUtility.GetDefaultCurrencyCodeType(_apiContext.Site);
            item.StartPrice = new AmountType 
            { currencyID = item.Currency, Value = Convert.ToDouble(product.UnitPrice) };

            // Listing is valid for X days
            item.ListingDuration = "Days_7"; 

            // Where is the item located?
            item.Location = "Fresno, California";
            item.Country = SiteUtility.GetCountryCodeType(_apiContext.Site);

            // List the item under the following leaf category
            CategoryType category = new CategoryType 
            { CategoryID = product.EBayCategoryId.ToString() };
            item.PrimaryCategory = category;
            //item.SecondaryCategory = ....;

            // Available quantity
            item.Quantity = product.Quantity;

            // Supported payment methods
            item.PaymentMethods = new BuyerPaymentMethodCodeTypeCollection();
            item.PaymentMethods.AddRange(new[] { BuyerPaymentMethodCodeType.PayPal });
            // Paypal e-mail address required if PayPal is listed as a payment method
            item.PayPalEmailAddress = "me@paypal.com"; 

            // Shipping details
            item.DispatchTimeMax = 1;
            item.ShippingDetails = GetShippingDetails();

            // Return policy
            item.ReturnPolicy = GetPolicyForUS();

            return item;
        }

        private ShippingDetailsType GetShippingDetails()
        {
            // Shipping details.
            ShippingDetailsType shippingDetails = new ShippingDetailsType();

            SalesTaxType salesTax = new SalesTaxType();
            salesTax.SalesTaxPercent = 0.0825f;
            salesTax.SalesTaxState = "CA";

            shippingDetails.ApplyShippingDiscount = true;
            AmountType amount = new AmountType 
            { Value = 2.8, currencyID = CurrencyUtility.GetDefaultCurrencyCodeType(_apiContext.Site) };
            shippingDetails.InsuranceFee = amount;
            shippingDetails.InsuranceOption = InsuranceOptionCodeType.Optional;
            shippingDetails.PaymentInstructions = "eBay DotNet SDK test instruction.";

            // Set calculated shipping.
            shippingDetails.ShippingType = ShippingTypeCodeType.Flat;
            ShippingServiceOptionsType shippingOptions = new ShippingServiceOptionsType();
            shippingOptions.ShippingService = ShippingServiceCodeType.ShippingMethodStandard.ToString();
            shippingOptions.ShippingServiceAdditionalCost = amount;
            shippingOptions.ShippingServiceCost = amount;
            shippingOptions.ShippingServicePriority = 1;
            shippingOptions.ShippingInsuranceCost = amount;

            shippingDetails.ShippingServiceOptions = 
                new ShippingServiceOptionsTypeCollection(new[] { shippingOptions });

            return shippingDetails;
        }

        #endregion

        private void RemoveListing(Listing listing)
        {
            // Remove the item from eBay
            EndItemCall endItemCall = new EndItemCall(_apiContext);
            endItemCall.EndItem(listing.ItemId, EndReasonCodeType.NotAvailable);
            
            // Remove the listing from our database
            _dataContext.Listings.DeleteOnSubmit(listing);
            _dataContext.SubmitChanges();

            Console.WriteLine("Listing no longer available. Ended at {0}.", endItemCall.EndTime);
            Console.WriteLine();
        }

        private void AddListing(EBayProductModel product)
        {
            ItemType item = BuildItem(product);

            AddItemCall addItemApiCall = new AddItemCall(_apiContext);
            FeeTypeCollection fees = addItemApiCall.AddItem(item);

            Listing listing = new Listing
            {
                ItemId = addItemApiCall.ItemID,
                ProductId = product.ProductID,
                StartDate = addItemApiCall.StartTime,
                EndDate = addItemApiCall.EndTime
            };
            _dataContext.Listings.InsertOnSubmit(listing);
            _dataContext.SubmitChanges();

            double cost = 0;
            foreach (FeeType fee in fees)
            {
                cost += fee.Fee.Value;
            }
            Console.WriteLine("Item added to eBay {0}. Cost price: {1:C}", listing.ItemId, cost);
        }

        private void UpdateListing(Listing listing, EBayProductModel product)
        {
            ItemType item = BuildItem(product);

            item.ItemID = listing.ItemId;
            ReviseItemCall reviseApiCall = new ReviseItemCall(_apiContext);
            reviseApiCall.ReviseItem(item, new StringCollection());
            FeeTypeCollection fees = reviseApiCall.FeeList;

            double cost = 0;
            foreach (FeeType fee in fees)
            {
                cost += fee.Fee.Value;
            }
            Console.WriteLine("Item {0} revised. Extra cost price: {1:C}", listing.ItemId, cost);
        }

        public override void ParseFile(string path)
        {
            try
            {
                // Deserialize
                FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                XmlSerializer serializer = new XmlSerializer(typeof(EBayProductModel));
                EBayProductModel product = (EBayProductModel)serializer.Deserialize(fileStream);
                fileStream.Close();

                Console.WriteLine("Processing product {0}", product.ProductName);

                // Is the item already listed on eBay?
                Listing listing = (from p in _dataContext.Listings
                                   where p.ProductId == product.ProductID &&
                                         p.StartDate <= DateTime.Now &&
                                         p.EndDate >= DateTime.Now
                                   select p).SingleOrDefault();

                // Do we need to remove the listing?
                if (listing != null && Path.GetFileName(path).StartsWith("remove_"))
                {
                    RemoveListing(listing);
                    return;
                }

                // Do we need to add a new listing?
                if (listing == null)
                {
                    AddListing(product);
                }
                else
                {
                    // Update the existing listing
                    UpdateListing(listing, product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
            }
        }

        public override void LogFailedParseAttempt(string path)
        {
            Console.WriteLine(String.Format("Could not parse file {0}.", path));
        }
    }
}
