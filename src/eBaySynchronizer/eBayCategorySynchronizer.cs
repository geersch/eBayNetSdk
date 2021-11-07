using System;
using System.Linq;
using eBay.Service.Call;
using eBay.Service.Core.Soap;

namespace eBaySynchronizer
{
    public class eBayCategorySynchronizer : eBaySynchronizer
    {
        #region Fields

        private readonly eBayDataContext _context = new eBayDataContext();

        #endregion

        private static GetCategoriesCall DownloadCategories()
        {
            GetCategoriesCall apiCall =
                new GetCategoriesCall(ApiContext)
                {
                    EnableCompression = true,
                    ViewAllNodes = true
                };
            apiCall.DetailLevelList.Add(DetailLevelCodeType.ReturnAll);
            apiCall.GetCategories();
            return apiCall;
        }

        public override void Synchronize()
        {
            _context.ebay_Categories.DeleteAllOnSubmit((from c in _context.ebay_Categories select c).ToList());
            _context.SubmitChanges();

            var categories = DownloadCategories();
            foreach (CategoryType category in categories.CategoryList)
            {
                int categoryId = Int32.Parse(category.CategoryID);
                int? parentId = Int32.Parse(category.CategoryParentID[0]);
                if (parentId == categoryId)
                {
                    parentId = null;
                }

                ebay_Category eBayCategory = new ebay_Category
                                                 {
                                                     CategoryID = categoryId,
                                                     ParentID = parentId,
                                                     Leaf = category.LeafCategory,
                                                     Level = category.CategoryLevel,
                                                     Name = category.CategoryName,
                                                     Virtual = category.Virtual,
                                                     Expired = category.Expired
                                                 };

                _context.ebay_Categories.InsertOnSubmit(eBayCategory);
            }
            _context.SubmitChanges();
        }
    }
}
