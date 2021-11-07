using System.Linq;
using System.Web.Mvc;
using Northwind.Web.UI.Models;

namespace Northwind.Web.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly NorthwindDataContext _context = new NorthwindDataContext();

        #region Index

        //
        // GET: /Product/

        public ActionResult Index()
        {
            var products = from p in _context.Products
                           select new ProductModel
                                      {
                                          ProductId = p.ProductID,
                                          Category = p.Category.CategoryName,
                                          Supplier = p.Supplier.CompanyName,
                                          ProductName = p.ProductName,
                                          UnitPrice = p.UnitPrice,
                                          UnitsInStock = p.UnitsInStock,
                                          UnitsOnOrder = p.UnitsOnOrder,
                                          QuantityPerUnit = p.QuantityPerUnit,
                                          ReorderLevel = p.ReorderLevel
                                      };

            return View(products.ToList());
        }

        #endregion

        #region Details

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id)
        {
            ProductModel product = (from p in _context.Products
                                    where p.ProductID == id
                                    select new ProductModel
                                               {
                                                   ProductId = p.ProductID,
                                                   Category = p.Category.CategoryName,
                                                   Supplier = p.Supplier.CompanyName,
                                                   ProductName = p.ProductName,
                                                   UnitPrice = p.UnitPrice,
                                                   UnitsInStock = p.UnitsInStock,
                                                   UnitsOnOrder = p.UnitsOnOrder,
                                                   QuantityPerUnit = p.QuantityPerUnit,
                                                   ReorderLevel = p.ReorderLevel
                                               }).SingleOrDefault();


            return View(product);
        }

        #endregion

        #region Create

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            this.ViewData["suppliers"] = new SelectList(_context.Suppliers.ToList(), "SupplierID", "CompanyName", null);
            this.ViewData["categories"] = new SelectList(_context.Categories.ToList(), "CategoryID", "CategoryName", null);

            return View();
        } 

        //
        // POST: /Product/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude="ProductId")] Product productToCreate)
        {
            this.ViewData["suppliers"] = new SelectList(_context.Suppliers.ToList(), "SupplierID", "CompanyName", null);
            this.ViewData["categories"] = new SelectList(_context.Categories.ToList(), "CategoryID", "CategoryName", null);

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _context.Products.InsertOnSubmit(productToCreate);
                _context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Edit

        //
        // GET: /Product/Edit/5
 
        public ActionResult Edit(int id)
        {
            this.ViewData["suppliers"] = new SelectList(_context.Suppliers.ToList(), "SupplierID", "CompanyName", null);
            this.ViewData["categories"] = new SelectList(_context.Categories.ToList(), "CategoryID", "CategoryName", null);

            var productToEdit = (from p in _context.Products
                                 where p.ProductID == id
                                 select p).SingleOrDefault();
            return View(productToEdit);
        }

        //
        // POST: /Product/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, Product productToEdit )
        {
            this.ViewData["suppliers"] = new SelectList(_context.Suppliers.ToList(), "SupplierID", "CompanyName", null);
            this.ViewData["categories"] = new SelectList(_context.Categories.ToList(), "CategoryID", "CategoryName", null);

            if (!ModelState.IsValid)
            {
                return View(productToEdit);
            }

            try
            {
                var product = (from p in _context.Products
                               where p.ProductID == id
                               select p).SingleOrDefault();

                product.ProductName = productToEdit.ProductName;
                product.ReorderLevel = productToEdit.ReorderLevel;
                product.UnitsInStock = productToEdit.UnitsInStock;
                product.UnitPrice = productToEdit.UnitPrice;
                product.QuantityPerUnit = productToEdit.QuantityPerUnit;
                product.CategoryID = productToEdit.CategoryID;
                product.SupplierID = productToEdit.SupplierID;

                _context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(productToEdit);
            }
        }

        #endregion

        #region Delete

        public ActionResult Delete(int id)
        {
            var productToDelete = (from p in _context.Products
                                   where p.ProductID == id
                                   select p).SingleOrDefault();
            return View(productToDelete);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var product = (from p in _context.Products
                                       where p.ProductID == id
                                       select p).SingleOrDefault();

                bool ordered = (from o in _context.Order_Details
                                where o.ProductID == id
                                select o.ProductID).Count() > 0;
                if (ordered)
                {
                    ModelState.AddModelError("Ordered", "Cannot delete product because it is ordered.");
                    return View(product);
                }

                EBayProductModel eBayProduct = new EBayProductModel(product);

                _context.Products.DeleteOnSubmit(product);
                _context.SubmitChanges();

                eBayProduct.RemoveFromEbay();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Publish On eBay

        public ActionResult PublishOnEBay(int id)
        {
            var product = (from p in _context.Products
                           where p.ProductID == id
                           select
                               new EBayProductModel
                                   {
                                       ProductID = p.ProductID,
                                       EBayCategoryId = p.Category.eBayCategoryId.Value,
                                       ProductName = p.ProductName,
                                       Quantity = p.UnitsInStock.Value,
                                       UnitPrice = p.UnitPrice.Value
                                   }).Single();

            product.PublishOnEBay();

            return RedirectToAction("Index");
        }


        #endregion
    }
}
