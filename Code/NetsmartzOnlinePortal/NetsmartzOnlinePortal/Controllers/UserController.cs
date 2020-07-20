using NetsmartzOnlinePortal.Domain.Model;
using NetsmartzOnlinePortal.Domain.NetSmartzPortalEnum;
using NetsmartzOnlinePortal.Service.INetsmartzOnlinePortalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NetsmartzOnlinePortal.Controllers
{
    public class UserController : Controller
    {
        ICategoryService _categoryService;
        IProductService _productService;
        public UserController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }
        [HttpGet]
        // GET: User
        public ActionResult ManageCategory()
        {
            if (TempData["CatId"] != null)
            {
                int id = (int)TempData["CatId"];
                TempData["CatId"] = null;

                CategoryModel categoryModel = _categoryService.GetCategoryById(id);
                return View(categoryModel);
            }

            SetAlertMessage();

            return View();
        }

        [HttpPost]
        // GET: User
        public ActionResult ManageCategory(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                int result = _categoryService.AddUpdateCategory(categoryModel);
                if (result == (int)ResultStatus.Added)
                {
                    TempData["Added"] = "Category added successfuly";
                }
                else
                {
                    TempData["Updated"] = "Category updated successfuly";
                }
                return RedirectToAction("managecategory");
            }
            return View(categoryModel);
        }

        public ActionResult DeleteCategory(int id)
        {
            int result = _categoryService.DeleteCatById(id);
            if (result == (int)ResultStatus.Deleted)
            {
                TempData["Deleted"] = "Category Deleted Successfuly";
            }
            else
            {
                TempData["Failed"] = "Category you are trying to delete could not be found";
            }
            return RedirectToAction("manageproducts");
        }

        public ActionResult DeleteProduct(int id)
        {
            int result = _productService.DeleteProductById(id);
            if (result == (int)ResultStatus.Deleted)
            {
                TempData["Deleted"] = "Product Deleted Successfuly";
            }
            else
            {
                TempData["Failed"] = "Product you are trying to delete could not be found";
            }
            return RedirectToAction("managecategory");
        }

        public ActionResult ManageProducts()
        {
            ViewBag.Categories = _categoryService.GetAllActiveCategory();

            if (TempData["ProductId"] != null)
            {
                int id = (int)TempData["ProductId"];
                TempData["ProductId"] = null;

                ProductModel productModel = _productService.GetProductById(id);
                productModel.Category.PriceRange = _categoryService.GetPriceRangeByCatId(productModel.CategoryId);
                return View(productModel);
            }

            SetAlertMessage();
            return View();
        }

        [HttpPost]
        public ActionResult ManageProducts(ProductModel productModel)
        {

            if (ModelState.IsValid)
            {
                _productService.AddUpdateProduct(productModel);
                return RedirectToAction("manageproducts");
            }
            ViewBag.Categories = _categoryService.GetAllActiveCategory();
            productModel.Category = new CategoryModel();

            productModel.Category.PriceRange = _categoryService.GetPriceRangeByCatId(productModel.CategoryId);

            return View(productModel);
        }

        public ActionResult GetAllCategories(int draw, int start, int length, string criteria, string customSearch)
        {

            string search = Request.Form["search[value]"];
            if (String.IsNullOrEmpty(search) && !String.IsNullOrEmpty(customSearch))
            {
                search = customSearch;
            }

            // note: we only sort one column at a time
            int sortColumn = -1;
            string OrderColumnName = "";
            string sortColumnDir = "asc";
            if (Request.Form["order[0][column]"] != null)
            {
                sortColumn = int.Parse(Request.Form["order[0][column]"]);
                OrderColumnName = Request.Form["columns[" + sortColumn + "][data]"];
            }

            if (Request.Form["order[0][dir]"] != null)
            {
                sortColumnDir = Request.Form["order[0][dir]"];
            }

            if (Request.IsAjaxRequest())
            {
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                int recordFilteredTotal = 0;

                var lstCat = new List<CategoryModel>();
                lstCat = _categoryService.GetAllActiveCategory();

                lstCat.ForEach(x => x.PriceRange = x.MinPrice + "-" + x.MaxPrice);

                if (!string.IsNullOrEmpty(search))
                {
                    lstCat = lstCat.Where(c => (c.CategoryName != null && (c.CategoryName.ToLower()).Contains(search.ToLower()))
                              ||
                              (c.Description != null && (c.Description.ToLower()).Contains(search.ToLower()))
                              ||
                              (c.PriceRange != null && (c.PriceRange.ToLower()).Contains(search.ToLower()))).ToList();
                }

                var filteredData = lstCat as IEnumerable<CategoryModel>;
                filteredData.OrderByDescending(x => x.CategoryName);
                if (OrderColumnName != "")
                {
                    Func<CategoryModel, string> orderingFunction = (c => OrderColumnName == "CategoryName" ? c.CategoryName :
                                              OrderColumnName == "Description" ? c.Description :
                                              OrderColumnName == "PriceRange" ? c.PriceRange
                                              : c.CreatedOn.ToString());

                    if (sortColumnDir == "asc")
                    {
                        filteredData = filteredData.OrderBy(orderingFunction);
                    }
                    else
                    {
                        filteredData = filteredData.OrderByDescending(orderingFunction);
                    }
                }
                recordsTotal = filteredData.Count();
                filteredData = filteredData.Skip(skip).Take(pageSize).ToList();
                recordFilteredTotal = filteredData.Count();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordFilteredTotal, data = filteredData }, JsonRequestBehavior.AllowGet);
            }

            return View();
        }
        public ActionResult GetAllProducts(int draw, int start, int length, string criteria, string customSearch)
        {

            string search = Request.Form["search[value]"];
            if (String.IsNullOrEmpty(search) && !String.IsNullOrEmpty(customSearch))
            {
                search = customSearch;
            }

            // note: we only sort one column at a time
            int sortColumn = -1;
            string OrderColumnName = "";
            string sortColumnDir = "asc";
            if (Request.Form["order[0][column]"] != null)
            {
                sortColumn = int.Parse(Request.Form["order[0][column]"]);
                OrderColumnName = Request.Form["columns[" + sortColumn + "][data]"];
            }

            if (Request.Form["order[0][dir]"] != null)
            {
                sortColumnDir = Request.Form["order[0][dir]"];
            }

            if (Request.IsAjaxRequest())
            {
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                int recordFilteredTotal = 0;

                var lstProduct = new List<ProductModel>();
                lstProduct = _productService.GetAllActiveProduct();

                foreach (var item in lstProduct)
                {
                    item.strExpirationDate = item.ExpirationDate.Value.ToString("dd/MM/yyyy");
                    item.CategoryName = item.Category.CategoryName;
                }

                if (!string.IsNullOrEmpty(search))
                {
                    lstProduct = lstProduct.Where(c => (c.ProductName != null && (c.ProductName.ToLower()).Contains(search.ToLower()))
                              ||
                              (c.Description != null && (c.Description.ToLower()).Contains(search.ToLower()))
                              ||
                              (c.Quantity != null && (c.Quantity.ToString().ToLower()).Contains(search.ToLower()))
                              ||
                              (c.Price != null && (c.Price.ToString().ToLower()).Contains(search.ToLower()))
                              ||
                              (c.Discount != null && (c.Discount.ToString().ToLower()).Contains(search.ToLower()))
                              ||
                              (c.ProductName != null && (c.ProductName.ToString().ToLower()).Contains(search.ToLower()))
                              ||
                              (c.strExpirationDate != null && (c.strExpirationDate.ToString().ToLower()).Contains(search.ToLower()))
                              ||
                              (c.ExpirationDate != null && (c.ExpirationDate.Value.ToString("dd/MM/yyyy").ToLower()).Contains(search.ToLower()))).ToList();
                }

                var filteredData = lstProduct as IEnumerable<ProductModel>;
                filteredData.OrderByDescending(x => x.ProductName);
                if (OrderColumnName != "")
                {
                    Func<ProductModel, string> orderingFunction = (c => OrderColumnName == "ProductName" ? c.ProductName :
                                              OrderColumnName == "Description" ? c.Description :
                                              OrderColumnName == "Quantity" ? c.Quantity.ToString() :
                                              OrderColumnName == "Price" ? c.Price.ToString():
                                              OrderColumnName == "Discount" ? c.Discount.ToString():
                                              OrderColumnName == "ProductName" ? c.ProductName.ToString() :
                                              OrderColumnName == "strExpirationDate" ? c.strExpirationDate.ToString() :
                                              OrderColumnName == "ExpirationDate" ? c.ExpirationDate.ToString()
                                              : c.CreatedOn.Value.ToString());

                    if (sortColumnDir == "asc")
                    {
                        filteredData = filteredData.OrderBy(orderingFunction);
                    }
                    else
                    {
                        filteredData = filteredData.OrderByDescending(orderingFunction);
                    }
                }

                recordsTotal = filteredData.Count();
                filteredData = filteredData.Skip(skip).Take(pageSize).ToList();
                recordFilteredTotal = filteredData.Count();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordFilteredTotal, data = filteredData }, JsonRequestBehavior.AllowGet);
            }

            return View();
        }

        public void SetAlertMessage()
        {
            if (TempData["Added"] != null)
            {
                ViewBag.Message = TempData["Added"].ToString();
                TempData["Added"] = null;
            }
            else if (TempData["Updated"] != null)
            {
                ViewBag.Message = TempData["Updated"].ToString();
                TempData["Updated"] = null;
            }
            else if (TempData["Deleted"] != null)
            {
                ViewBag.Message = TempData["Deleted"].ToString();
                TempData["Deleted"] = null;
            }
            else if (TempData["Failed"] != null)
            {
                ViewBag.Message = TempData["Failed"].ToString();
                TempData["Failed"] = null;
            }
            else if (TempData["Failed"] != null)
            {
                ViewBag.Message = TempData["Failed"].ToString();
            }
        }

        [HttpGet]
        public ActionResult ManageCat(int id)
        {
            TempData["CatId"] = id;
            return RedirectToAction("managecategory");
        }

        [HttpGet]
        public ActionResult ManageProd(int id)
        {
            TempData["ProductId"] = id;
            return RedirectToAction("manageproducts");
        }

        [HttpGet]
        public ActionResult GetPriceRange(int categoryId)
        {
            string priceRange = _categoryService.GetPriceRangeByCatId(categoryId);

            return Json(priceRange, JsonRequestBehavior.AllowGet);
        }
    }
}