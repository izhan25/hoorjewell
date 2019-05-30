using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelleryShop.Models;

namespace JewelleryShop.Controllers
{
    public class HomeController : Controller
    {

        //Global Database Variable
        hoorjewellEntities database = new hoorjewellEntities();

        // GET: Home
        public ActionResult Index()
        {
            //categories
            var queryCateg = from tblCateg in database.ProductCategoryTables select tblCateg;
            ViewData["Categories"] = queryCateg.ToList();

            //products
            var queryProd = from tblProd in database.ProductsTables.Take(3) select tblProd;
            ViewData["threeProducts"] = queryProd.ToList();

            //products for slider
            var querySlide = (from tblProd in database.ProductsTables select tblProd).Distinct().OrderByDescending(x=>x.productId);
            ViewData["slider"] = querySlide.ToList();


            //for cart
            List<Cart> cartHome = (List<Cart>)Session["cart"];
            ViewBag.qty = 0;
            ViewBag.amount = 0;
            if(Session["cart"] != null)
            {
                foreach (var item in cartHome)
                {
                    ViewBag.qty = ViewBag.qty + 1;

                    var price = item.qty * item.productPrice;
                    ViewBag.amount = ViewBag.amount + price;
                }
            }
            

            return View();
        }


        // Log In Page
        [HttpGet]
        public ActionResult LogIn()
        {
            //categories
            var queryCateg = from tblCateg in database.ProductCategoryTables select tblCateg;
            ViewData["Categories"] = queryCateg.ToList();

            //for cart
            List<Cart> cartHome = (List<Cart>)Session["cart"];
            ViewBag.qty = 0;
            ViewBag.amount = 0;
            if (Session["cart"] != null)
            {
                foreach (var item in cartHome)
                {
                    ViewBag.qty = ViewBag.qty + 1;

                    var price = item.qty * item.productPrice;
                    ViewBag.amount = ViewBag.amount + price;
                }
            }

            if (Session["c_Name"] != null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpPost]
        public ActionResult LogIn(UsersTable loginObj)
        {
            //categories
            var queryCateg = from tblCateg in database.ProductCategoryTables select tblCateg;
            ViewData["Categories"] = queryCateg.ToList();

            //for cart
            List<Cart> cartHome = (List<Cart>)Session["cart"];
            ViewBag.qty = 0;
            ViewBag.amount = 0;
            if (Session["cart"] != null)
            {
                foreach (var item in cartHome)
                {
                    ViewBag.qty = ViewBag.qty + 1;

                    var price = item.qty * item.productPrice;
                    ViewBag.amount = ViewBag.amount + price;
                }
            }

            if (Request.Form["btnLogInUser"] != null)
            {
                //checking in UserTable for logging in
                var queryUser = from userTable in database.UsersTables select userTable;
                ViewBag.userTable = queryUser.ToList();


                foreach (var item in ViewBag.userTable)
                {
                    try
                    {
                        if (loginObj.userName.Equals(item.userName) && loginObj.userPassword.Equals(item.userPassword))
                        {
                            Session["c_Name"] = item.userName;
                            Session["c_Id"] = item.userId;

                            return RedirectToAction("Index");
                        }
                    }
                    catch(Exception ex)
                    {
                        ViewBag.logInMessage = "UserName or Password is not Valid";
                    }
                   
                }

            }



            return View();
        }
        
        

        //logout page
        public ActionResult logout()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }





        // Sign Up Page
        public ActionResult SignUp()
        {
            //categories
            var queryCateg = from tblCateg in database.ProductCategoryTables select tblCateg;
            ViewData["Categories"] = queryCateg.ToList();

            //for cart
            List<Cart> cartHome = (List<Cart>)Session["cart"];
            ViewBag.qty = 0;
            ViewBag.amount = 0;
            if (Session["cart"] != null)
            {
                foreach (var item in cartHome)
                {
                    ViewBag.qty = ViewBag.qty + 1;

                    var price = item.qty * item.productPrice;
                    ViewBag.amount = ViewBag.amount + price;
                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult SignUp(UsersTable user)
        {
            //categories
            var queryCateg = from tblCateg in database.ProductCategoryTables select tblCateg;
            ViewData["Categories"] = queryCateg.ToList();

            //for cart
            List<Cart> cartHome = (List<Cart>)Session["cart"];
            ViewBag.qty = 0;
            ViewBag.amount = 0;
            if (Session["cart"] != null)
            {
                foreach (var item in cartHome)
                {
                    ViewBag.qty = ViewBag.qty + 1;

                    var price = item.qty * item.productPrice;
                    ViewBag.amount = ViewBag.amount + price;
                }
            }

            if (Request.Form["btnSubmitUser"] != null)
            {
                try
                {
                    database.Entry(user).State = System.Data.Entity.EntityState.Added;
                    ViewBag.signup = "Congratulation ! You Are Now A Member !";
                    database.SaveChanges();
                }
                catch(Exception ex)
                {
                    ViewBag.signup = null;
                }
              
            }

            if (ModelState.IsValid)
            {
                // This will clear whatever form items have been populated
                ModelState.Clear();
                // Here I'm just returning the view I dont want a model being passed
                return View();
            }

            return View();
        }
        
        
        
        // Products Page
        public ActionResult Products(string id, string searchText)
        {
            //categories
            var queryCateg = from tblCateg in database.ProductCategoryTables select tblCateg;
            ViewData["Categories"] = queryCateg.ToList();

            //for cart
            List<Cart> cartHome = (List<Cart>)Session["cart"];
            ViewBag.qty = 0;
            ViewBag.amount = 0;
            if (Session["cart"] != null)
            {
                foreach (var item in cartHome)
                {
                    ViewBag.qty = ViewBag.qty + 1;

                    var price = item.qty * item.productPrice;
                    ViewBag.amount = ViewBag.amount + price;
                }
            }



            //code for search
            if (Request.Form["btnSearch"] != null)
            {
                var querySearch = from tblProd in database.ProductsTables.Where(x => x.productName.Contains(searchText)) select tblProd;
                
                if(!querySearch.Any())
                {
                    //if query is empty
                    ViewBag.search = "None";
                }
                else
                {
                    //if query is NOT empty
                    ViewData["Products"] = querySearch.ToList();
                }
                

                return View();

            }


            //code for category selection
            if (id == null)
            {
                //if category is not selected

                //products
                var queryProd = from tblProd in database.ProductsTables select tblProd;
                ViewData["Products"] = queryProd.ToList();
            }
            else
            {
                //if category is selected
                int categId = Convert.ToInt32(id);

                var query = from tbl in database.ProductsTables where tbl.categoryId == categId select tbl;
                ViewData["Products"] = query.ToList();
            }
            

            return View();
        }


        //single product page
        public ActionResult SingleProduct(string id)
        {
            //categories
            var queryCateg = from tblCateg in database.ProductCategoryTables select tblCateg;
            ViewData["Categories"] = queryCateg.ToList();


            //for cart
            List<Cart> cartHome = (List<Cart>)Session["cart"];
            ViewBag.qty = 0;
            ViewBag.amount = 0;
            if (Session["cart"] != null)
            {
                foreach (var item in cartHome)
                {
                    ViewBag.qty = ViewBag.qty + 1;

                    var price = item.qty * item.productPrice;
                    ViewBag.amount = ViewBag.amount + price;
                }
            }


            if (id == null)
            {
                //if no product is selected
                return RedirectToAction("Products");
            }
            else
            {
                //if product is selected

                int prodId = Convert.ToInt32(id);

                var queryItem = from tblProd in database.ProductsTables where tblProd.productId == prodId select tblProd;

                ViewData["SelectedProduct"] = queryItem.ToList();
            }

            return View();
        }

        //shopping cart
        [HttpGet]
        public ActionResult Cart()
        {
            //categories
            var queryCateg = from tblCateg in database.ProductCategoryTables select tblCateg;
            ViewData["Categories"] = queryCateg.ToList();

            //for cart
            List<Cart> cartHome = (List<Cart>)Session["cart"];
            ViewBag.qty = 0;
            ViewBag.amount = 0;
            if (Session["cart"] != null)
            {
                foreach (var item in cartHome)
                {
                    ViewBag.qty = ViewBag.qty + 1;

                    var price = item.qty * item.productPrice;
                    ViewBag.amount = ViewBag.amount + price;
                }
            }

            List<Cart> cart = (List<Cart>)Session["cart"];

            ViewData["Cart"] = cart;

            return View();
        }
        


        //add Item
        public ActionResult addItem(string id)
        {
            int myid = Convert.ToInt32(id);
            ProductsTable prod = new ProductsTable();
            prod = database.ProductsTables.Find(myid);

            Cart item = new Cart();

            item.productId = prod.productId;
            item.productName = prod.productName;
            item.productPrice = Convert.ToInt32(prod.productPrice);
            item.productImage = prod.productImage;
            item.qty = 1;



            List<Cart> cart = (List<Cart>)Session["cart"];

            if(Session["cart"] == null)
            {
                //if session cart is empty

                List<Cart> insert = new List<Cart>();
                insert.Add(item);

                Session["cart"] = insert;

            }
            else
            {
                //if session cart is not empty

                var isOk = true;

                foreach(var itemCart in cart)
                {
                    if(itemCart.productId == item.productId)
                    {
                        itemCart.qty = itemCart.qty + 1;
                        isOk = false;
                        break;
                    }
                }
                
                if(isOk)
                {
                    cart.Add(item);

                    Session["cart"] = cart;

                }
            
            }


            return RedirectToAction("Products");
            
        }

        public ActionResult removeItem(string id)
        {
            if(id == null)
            {
                //deleting all items from cart

                Session["cart"] = null;
            }
            else
            {
                //deleting specific item from cart

                int myid = Convert.ToInt32(id);

                List<Cart> cart = (List<Cart>)Session["cart"];

                foreach (var item in cart)
                {
                    if (item.productId == myid)
                    {
                        cart.Remove(item);
                        break;
                    }
                }
            }



            

            return RedirectToAction("Cart");
        }


        public ActionResult Order()
        {
            //categories
            var queryCateg = from tblCateg in database.ProductCategoryTables select tblCateg;
            ViewData["Categories"] = queryCateg.ToList();

            //for cart
            List<Cart> cartHome = (List<Cart>)Session["cart"];
            ViewBag.qty = 0;
            ViewBag.amount = 0;
            if (Session["cart"] != null)
            {
                foreach (var item in cartHome)
                {
                    ViewBag.qty = ViewBag.qty + 1;

                    var price = item.qty * item.productPrice;
                    ViewBag.amount = ViewBag.amount + price;
                }
            }





            if (Session["c_Name"] == null)
            {
                return RedirectToAction("LogIn");
            }
            
            if(Session["cart"] == null)
            {
                return RedirectToAction("Products");
            }

            //getting data from session as list
            List<Cart> cart = (List<Cart>)Session["cart"];

            //making a order object first
            OrdersTable order = new OrdersTable();

            //assigning order object its values from cart object
            order.userId = Convert.ToInt32(Session["c_Id"]);
            order.placementDate = DateTime.Now;
            order.deliveryData = DateTime.Now.AddDays(3);
            order.isDelievered = "false";

            //saving order object in database
            try
            {
                database.Entry(order).State = System.Data.Entity.EntityState.Added;
                database.SaveChanges();
            }
            catch(Exception ex)
            {
                ViewData["error"] = "An error occured in placing order";
            }


            //entring products of orderin orderedProducts table

            OrdersTable ordered = new OrdersTable();
            string orderId = Convert.ToString(Session["c_Id"]);
            var query = from tbl in database.OrdersTables.Where( m => m.userId.ToString().Contains(orderId) ) select tbl;
            ViewData["ordered"] = query.ToList();

            int orderedId = 0;

            foreach(var item in ViewData["ordered"] as IEnumerable<JewelleryShop.Models.OrdersTable>)
            {
                orderedId = item.orderId;
            }


            foreach (var itemCart in cart)
            {
                if(orderedId != 0)
                {
                    Ordered_Products op = new Ordered_Products();
                    op.orderId = orderedId;
                    op.productId = itemCart.productId;
                    op.Qty = itemCart.qty;
                    op.price = itemCart.qty * itemCart.productPrice;

                    try
                    {
                        database.Entry(op).State = System.Data.Entity.EntityState.Added;
                        database.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewData["error"] = "An error occured in placing order";
                    }
                }

                
            }
               

            return View();
        }






        //Search Box
        public ActionResult Search(string searchText)
        {
            if(Request.Form["btnSearch"] != null)
            {
                var querySearch = from tblProd in database.ProductsTables.Where(x => x.productName.Contains(searchText)) select tblProd;

                
            }

            return RedirectToAction("Products");
        }


        // About Us
        public ActionResult AboutUs()
        {
            //categories
            var queryCateg = from tblCateg in database.ProductCategoryTables select tblCateg;
            ViewData["Categories"] = queryCateg.ToList();

            //for cart
            List<Cart> cartHome = (List<Cart>)Session["cart"];
            ViewBag.qty = 0;
            ViewBag.amount = 0;
            if (Session["cart"] != null)
            {
                foreach (var item in cartHome)
                {
                    ViewBag.qty = ViewBag.qty + 1;

                    var price = item.qty * item.productPrice;
                    ViewBag.amount = ViewBag.amount + price;
                }
            }

            return View();
        }






        // Contact Page
        public ActionResult Contact()
        {
            //categories
            var queryCateg = from tblCateg in database.ProductCategoryTables select tblCateg;
            ViewData["Categories"] = queryCateg.ToList();

            //for cart
            List<Cart> cartHome = (List<Cart>)Session["cart"];
            ViewBag.qty = 0;
            ViewBag.amount = 0;
            if (Session["cart"] != null)
            {
                foreach (var item in cartHome)
                {
                    ViewBag.qty = ViewBag.qty + 1;

                    var price = item.qty * item.productPrice;
                    ViewBag.amount = ViewBag.amount + price;
                }
            }

            return View();
        }





    }//conttroller
}//namespace