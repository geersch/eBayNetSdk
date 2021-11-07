using System;
using System.Linq;
using System.Web.Mvc;
using Northwind.Web.UI.Models;

namespace Northwind.Web.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly NorthwindDataContext _context = new NorthwindDataContext();

        #region Index

        public ActionResult Index()
        {
            var categories = from c in _context.Categories
                           select new CategoryModel
                           {
                               CategoryId = c.CategoryID,
                               CategoryName = c.CategoryName,
                               Description = c.Description,
                               EBayCategoryName = c.ebay_Category != null ? c.ebay_Category.Name : String.Empty
                           };

            return View(categories.ToList());
        }

        #endregion

        #region Link To eBay Category

        //[OutputCache(Duration = 3600, VaryByParam = "None")]
        public ActionResult EBayCategories(int id)
        {
            ViewData["id"] = id;

            var categories = from c in _context.ebay_Categories
                             where c.ParentID == null
                             orderby c.Name
                             select c;

            return View(categories.ToList());
        }

        public ActionResult LinkToeBayCategory(int id, int ebaycategoryid)
        {
            Category category = (from c in _context.Categories 
                                 where c.CategoryID == id
                                 select c).Single();

            category.eBayCategoryId = ebaycategoryid;
            _context.SubmitChanges();

            return RedirectToAction("Index");
        }

        #endregion
    }
}
