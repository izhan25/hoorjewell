using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelleryShop.Models;
using System.IO;

namespace JewelleryShop.Controllers
{
    public class AdminController : Controller
    {

        //Global Database Variable
        JewelleryShopEntities database = new JewelleryShopEntities();



        // GET: Admin
        public ActionResult Index()
        {
            if(Session["u_Name"] == null)
            {
                return RedirectToAction("logIn");
            }

            ViewBag.breadcrum = "Dashboard";

            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(EmployeeTable loginObj)
        {

            if(Request.Form["btnLogIn"] != null)
            {
                //checking in EmployyeTable for logging in
                var queryEmp = from empTable in database.EmployeeTables select empTable;
                ViewBag.empTable = queryEmp.ToList();


                foreach (var item in ViewBag.empTable)
                {
                    if (loginObj.EmployeeUserName.Equals(item.EmployeeUserName) && loginObj.EmployeePwd.Equals(item.EmployeePwd))
                    {
                        Session["u_Role"] = item.roleId;
                        Session["u_Name"] = item.EmployeeUserName;
                        Session["u_Contact"] = item.EmployeeContact;
                        Session["u_Id"] = item.Employee_Id;
                        Session["u_Image"] = item.EmployeeImage;


                        //assigning value to Role from EmployeeTable to RoleTable
                        var queryRole = from roleTable in database.RolesTables select roleTable;
                        ViewBag.roleTable = queryRole.ToList();
                        foreach (var itemRole in ViewBag.roleTable)
                        {
                            if(item.roleId.Equals(itemRole.roleId))
                            {
                                Session["u_Role"] = itemRole.roleName;
                            }
                        }




                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.logInMessage = "Invalid UserName or Password";
                    }
                }

            }

            

            return View();
        }

        //log out code
        public ActionResult logOut()
        {
            Session["u_Name"] = null;

            return RedirectToAction("logIn");
        }

        //Profile Page Code
        public ActionResult myProfile()
        {
            //authenticating
            if(Session["u_Name"] == null)
            {
                return RedirectToAction("logIn");
            }

            ViewBag.breadcrum = "Dashboard / Profile";

            return View();
        }

        [HttpGet]
        public ActionResult profileEdit(string id)
        {
            if (Session["u_Name"] == null)
            {
                return RedirectToAction("logIn");
            }

            if (id != null)
            {
                int myId = Convert.ToInt32(id);
                EmployeeTable emp = new EmployeeTable();
                emp = database.EmployeeTables.Find(myId);

                ViewBag.img = emp.EmployeeImage;

                
                ViewBag.breadcrum = "Dashboard / Profile / Edit";

                return View(emp);
            }
            else
            {
                ViewBag.breadcrum = "Dashboard / Profile / Edit";

                return RedirectToAction("myProfile");
            }

        }

        [HttpPost]
        public ActionResult profileEdit(EmployeeTable emp, HttpPostedFileBase EmployeeImage, string id)
        {
            if (Request.Form["btnSubmitProfile"] != null)
            {
                if (id != null)
                {
                    //update

                    if (EmployeeImage == null)
                    {
                        //do nothing
                    }
                    else
                    {
                        string filename = Path.GetFileName(EmployeeImage.FileName);
                        string ext = Path.GetExtension(EmployeeImage.FileName);
                        if (ext == ".jpg" || ext == ".gif" || ext == ".jpeg" || ext == ".png")
                        {
                            EmployeeImage.SaveAs(Server.MapPath("~/Design/images/employees/" + filename));
                            emp.EmployeeImage = filename;

                        }
                        else
                        {
                            ViewBag.addEmpMsg = "Image extension is not valid";
                        }

                    }

                    int myid = Convert.ToInt32(id);
                    emp.Employee_Id = myid;
                    database.Entry(emp).State = System.Data.Entity.EntityState.Modified;

                }
                else
                {
                    //insert

                    string filename = Path.GetFileName(EmployeeImage.FileName);
                    string ext = Path.GetExtension(EmployeeImage.FileName);
                    if (ext == ".jpg" || ext == ".gif" || ext == ".jpeg" || ext == ".png")
                    {
                        EmployeeImage.SaveAs(Server.MapPath("~/Design/images/employees/" + filename));
                        emp.EmployeeImage = filename;

                    }
                    else
                    {
                        ViewBag.addEmpMsg = "Image extension is not valid";
                    }

                    database.Entry(emp).State = System.Data.Entity.EntityState.Added;
                }

            }

            try
            {
                database.SaveChanges();
            }
            catch(Exception ex)
            {
                ViewBag.error = ex.Message;
            }

            Session.Clear();
            return RedirectToAction("LogIn");
        }


        //Roles Page
        public ActionResult Roles()
        {
            if (Session["u_Name"] == null)
            {
                return RedirectToAction("logIn");
            }

            var queryEmployees = from tblEmployees in database.EmployeeTables select tblEmployees;
            ViewData["employees"] = queryEmployees.ToList();

            var queryRole = from tblRoles in database.RolesTables select tblRoles;
            ViewData["Roles"] = queryRole.ToList();

            ViewBag.breadcrum = "Dashboard / Roles ";

            return View();
        }
        [HttpGet]
        public ActionResult addEmployee(string id)
        {
            var queryRoles = from tblRoles in database.RolesTables where tblRoles.status == "active" select tblRoles;
            ViewData["Roles"] = queryRoles.ToList();

            if (id != null)
            {
                ViewBag.btnEmpValue = "Update";
                ViewBag.btnEmpColor = "btn btn-primary btn-lg btn-block";

                int empId = Convert.ToInt32(id);
                EmployeeTable empTable = new EmployeeTable();
                empTable = database.EmployeeTables.Find(empId);

                ViewBag.img = empTable.EmployeeImage;

                return View(empTable);
            }
            else
            {
                ViewBag.btnEmpColor = "btn btn-info btn-lg btn-block";
                ViewBag.btnEmpValue = "Insert";
            }


            ViewBag.breadcrum = "Dashboard / Roles / Add Employee";
            return View();
        }
        [HttpPost]
        public ActionResult addEmployee(EmployeeTable employee, HttpPostedFileBase EmployeeImage, string id, string PrevemployeeImage)
        {
            if (Request.Form["btnAddEmployee"] != null)
            {
                if (id != null)
                {
                    //update

                    if (EmployeeImage == null)
                    {
                        //do nothing
                        int myid = Convert.ToInt32(id);
                        employee.Employee_Id = myid;
                        database.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        string filename = Path.GetFileName(EmployeeImage.FileName);
                        string ext = Path.GetExtension(EmployeeImage.FileName);
                        if (ext == ".jpg" || ext == ".gif" || ext == ".jpeg" || ext == ".png")
                        {
                            EmployeeImage.SaveAs(Server.MapPath("~/Design/images/employees/" + filename));
                            employee.EmployeeImage = filename;

                        }
                        else
                        {
                            ViewBag.addEmployeeMsg = "Image extension is not valid";
                        }

                        employee.Employee_Id = Convert.ToInt32(id);
                        database.Entry(employee).State = System.Data.Entity.EntityState.Modified;


                    }

                }
                else
                {
                    //insert

                    try
                    {
                        string filename = Path.GetFileName(EmployeeImage.FileName);
                        string ext = Path.GetExtension(EmployeeImage.FileName);
                        if (ext == ".jpg" || ext == ".gif" || ext == ".jpeg" || ext == ".png")
                        {
                            EmployeeImage.SaveAs(Server.MapPath("~/Design/images/employees/" + filename));
                            employee.EmployeeImage = filename;

                        }
                        else
                        {
                            ViewBag.addEmployeeMsg = "Image extension is not valid";
                        }

                        database.Entry(employee).State = System.Data.Entity.EntityState.Added;
                    }
                    catch (Exception ex)
                    {
                        //do nothing
                    }


                }

            }




            try
            {
                database.SaveChanges();
            }
            catch (Exception ex)
            {
                //do nothing
            }


            return RedirectToAction("Roles");
        }

        public ActionResult deleteEmployee()
        {
            string empId = Request.RequestContext.RouteData.Values["id"].ToString();

            EmployeeTable empObj = new EmployeeTable();
            empObj.Employee_Id = Convert.ToInt32(empId);
            database.Entry(empObj).State = System.Data.Entity.EntityState.Deleted;
            database.SaveChanges();

            return RedirectToAction("Roles");
        }

        public ActionResult allRoles()
        {
            var queryRoles = from tblRoles in database.RolesTables select tblRoles;
            ViewData["Roles"] = queryRoles.ToList();

            ViewBag.breadcrum = "Dashboard / Products / Roles";

            return View();
        }

        public ActionResult disableRole(string id)
        {
            int myid = Convert.ToInt32(id);
            RolesTable roles = database.RolesTables.Find(myid);
            roles.status = "disable";
            database.Entry(roles).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("allRoles");
        }
        public ActionResult activateRole(string id)
        {
            int myid = Convert.ToInt32(id);
            RolesTable roles = database.RolesTables.Find(myid);
            roles.status = "active";
            database.Entry(roles).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("allRoles");
        }


        [HttpGet]
        public ActionResult addRole(string id)
        {
            if (id != null)
            {
                ViewBag.btnRoleValue = "Update";
                ViewBag.btnRoleColor = "btn btn-primary btn-lg btn-block";

                int roleId = Convert.ToInt32(id);
                RolesTable rolesTable = new RolesTable();
                rolesTable = database.RolesTables.Find(roleId);


                return View(rolesTable);
            }
            else
            {
                ViewBag.btnRoleColor = "btn btn-info btn-lg btn-block";
                ViewBag.btnRoleValue = "Add";
            }

            ViewBag.breadcrum = "Dashboard / Roles / Add Role";

            return View();
        }
        [HttpPost]
        public ActionResult addRole(RolesTable role, string id)
        {
            if (Request.Form["btnAddRole"] != null)
            {
                if (id != null)
                {
                    //update
                    role.roleId = Convert.ToInt32(id);
                    database.Entry(role).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    //insert
                    database.Entry(role).State = System.Data.Entity.EntityState.Added;

                }
            }

            database.SaveChanges();
            return RedirectToAction("allRoles");
        }

        public ActionResult deleteRole()
        {
            string roleId = Request.RequestContext.RouteData.Values["id"].ToString();

            RolesTable roleObj = new RolesTable();
            roleObj.roleId = Convert.ToInt32(roleId);
            database.Entry(roleObj).State = System.Data.Entity.EntityState.Deleted;
            database.SaveChanges();

            return RedirectToAction("allRoles");
        }

        //Users Page
        public ActionResult Users()
        {
            if (Session["u_Name"] == null)
            {
                return RedirectToAction("logIn");
            }

            var queryUsers = from tblUsers in database.UsersTables select tblUsers;
            ViewData["Users"] = queryUsers.ToList();

            ViewBag.breadcrum = "Dashboard / Users ";

            return View();
        }

        public ActionResult deleteUser()
        {
            string userId = Request.RequestContext.RouteData.Values["id"].ToString();

            UsersTable userObj = new UsersTable();
            userObj.userId = Convert.ToInt32(userId);
            database.Entry(userObj).State = System.Data.Entity.EntityState.Deleted;
            database.SaveChanges();

            return RedirectToAction("Users");
        }

        //Products Page
        public ActionResult Products()
        {
            if (Session["u_Name"] == null)
            {
                return RedirectToAction("logIn");
            }
            var queryProducts = from tblProducts in database.ProductsTables select tblProducts;
            ViewData["Products"] = queryProducts.ToList();

            var queryCateg = from tblCateg in database.ProductCategoryTables select tblCateg;
            ViewData["ProdCategs"] = queryCateg.ToList();

            ViewBag.breadcrum = "Dashboard / Products ";

            return View();
        }

        [HttpGet]
        public ActionResult addProduct(string id)
        {
            var queryCategory = from tblCategory in database.ProductCategoryTables select tblCategory;
            ViewData["ProductCategories"] = queryCategory.ToList();

            if (id != null)
            {
                ViewBag.btnProdValue = "Update";
                ViewBag.btnProdColor = "btn btn-primary btn-lg btn-block";

                int prodId = Convert.ToInt32(id);
                ProductsTable prodTable = new ProductsTable();
                prodTable = database.ProductsTables.Find(prodId);

                ViewBag.img = prodTable.productImage;

                return View(prodTable);
            }
            else
            {
                ViewBag.btnProdColor = "btn btn-info btn-lg btn-block";
                ViewBag.btnProdValue = "Insert";
            }


            ViewBag.breadcrum = "Dashboard / Products / Add Product";
            return View();
        }
        [HttpPost]
        public ActionResult addProduct(ProductsTable product, HttpPostedFileBase productImage, string id, string prevImage)
        {
            if(Request.Form["btnAddProduct"] != null)
            {
                if (id != null)
                {
                    //update

                    if (productImage == null)
                    {
                        //do nothing
                    }
                    else
                    {
                        string filename = Path.GetFileName(productImage.FileName);
                        string ext = Path.GetExtension(productImage.FileName);
                        if (ext == ".jpg" || ext == ".gif" || ext == ".jpeg" || ext == ".png")
                        {
                            productImage.SaveAs(Server.MapPath("~/Design/images/products/" + filename));
                            product.productImage = filename;

                        }
                        else
                        {
                            ViewBag.addProductMsg = "Image extension is not valid";
                        }

                    }
                    product.productId = Convert.ToInt32(id);
                    database.Entry(product).State = System.Data.Entity.EntityState.Modified;

                }
                else
                {
                    //insert

                    string filename = Path.GetFileName(productImage.FileName);
                    string ext = Path.GetExtension(productImage.FileName);
                    if (ext == ".jpg" || ext == ".gif" || ext == ".jpeg" || ext == ".png")
                    {
                        productImage.SaveAs(Server.MapPath("~/Design/images/products/" + filename));
                        product.productImage = filename;

                    }
                    else
                    {
                        ViewBag.addProductMsg = "Image extension is not valid";
                    }

                    database.Entry(product).State = System.Data.Entity.EntityState.Added;
                }

            }

            database.SaveChanges();
            return RedirectToAction("Products");
        }

        public ActionResult deleteProduct()
        {
            string prodId = Request.RequestContext.RouteData.Values["id"].ToString();

            ProductsTable prodObj = new ProductsTable();
            prodObj.productId = Convert.ToInt32(prodId);
            database.Entry(prodObj).State = System.Data.Entity.EntityState.Deleted;
            database.SaveChanges();

            return RedirectToAction("Products");
        }


        public ActionResult Categories()
        {
            var queryCategogies = from tblcategories in database.ProductCategoryTables select tblcategories;
            ViewData["Categories"] = queryCategogies.ToList();

            ViewBag.breadcrum = "Dashboard / Products / Categories";

            return View();
        }

        [HttpGet]
        public ActionResult addCategory(string id)
        {
            if (id != null)
            {
                ViewBag.btnCategValue = "Update";
                ViewBag.btnCategColor = "btn btn-primary btn-lg btn-block";

                int categId = Convert.ToInt32(id);
                ProductCategoryTable categTable = new ProductCategoryTable();
                categTable = database.ProductCategoryTables.Find(categId);


                return View(categTable);
            }
            else
            {
                ViewBag.btnCategColor = "btn btn-info btn-lg btn-block";
                ViewBag.btnCategValue = "Add";
            }

            ViewBag.breadcrum = "Dashboard / Products / Categories / Category Actions";
            return View();
        }
        [HttpPost]
        public ActionResult addCategory(ProductCategoryTable category, string id)
        {
            if (Request.Form["btnAddCategory"] != null)
            {
                if (id != null)
                {
                    //update
                    category.categoryId = Convert.ToInt32(id);
                    database.Entry(category).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    //insert
                    database.Entry(category).State = System.Data.Entity.EntityState.Added;

                }
            }

            database.SaveChanges();
            return RedirectToAction("Categories");
        }

        public ActionResult deleteCategory()
        {
            string categId = Request.RequestContext.RouteData.Values["id"].ToString();

            ProductCategoryTable categObj = new ProductCategoryTable();
            categObj.categoryId = Convert.ToInt32(categId);
            database.Entry(categObj).State = System.Data.Entity.EntityState.Deleted;
            database.SaveChanges();

            return RedirectToAction("Categories");
        }

        //Orders Page
        public ActionResult Orders()
        {
            if (Session["u_Name"] == null)
            {
                return RedirectToAction("logIn");
            }

            var queryOrder = from tblOrders in database.OrderViews.Where(m=>m.isDelievered.Contains("false")) select tblOrders;
            ViewData["Orders"] = queryOrder.ToList();

            ViewBag.breadcrum = "Dashboard / Orders ";

            return View();
        }

        public ActionResult orderDelivered(string id)
        {
            //Aquiring order by id
            int myid = Convert.ToInt32(id);
            OrdersTable odObj = new OrdersTable();
            odObj = database.OrdersTables.Find(myid);

            //changing state of order
            odObj.isDelievered = "true";

            //Modifying order in OrderTable
            database.Entry(odObj).State = System.Data.Entity.EntityState.Modified;

            //saving database
            database.SaveChanges();

            return RedirectToAction("Orders");
        }

        //order details page
        public ActionResult orderDetails(string id)
        {
            //if user is not logged in
            if (Session["u_Name"] == null)
            {
                return RedirectToAction("logIn");
            }

            //if id is not selected
            if(id == null)
            {
                return RedirectToAction("Orders");
            }

            //getting order views into a list
            int myid = Convert.ToInt32(id);
            var query = from tbl in database.OrderViews where tbl.orderId == myid select tbl;
            var query1 = database.GetOrders(myid);

            List<GetOrders_Result> orders = new List<GetOrders_Result>();
            orders = query1.ToList();

            //sending data to view
            ViewData["Details"] = orders;


            //getting user details
            ViewBag.UserName = orders.First().userName;
            ViewBag.UserContact = orders.First().userContact;
            ViewBag.UserAddress = orders.First().userAddress;

            //getting order details
            ViewBag.Amount = 0;
            foreach (var item in orders)
            {
                ViewBag.Amount = ViewBag.Amount + item.price;
            }
            ViewBag.Place = orders.First().placementDate.ToString().Substring(0, 9);
            ViewBag.Deliver = orders.First().deliveryData.ToString().Substring(0, 9);
            ViewBag.Status = "Undefined";
            if (orders.First().isDelievered.ToString() == "false")
            {
                ViewBag.Status = "Not Delivered";
            }


            ViewBag.breadcrum = "Dashboard / Orders / Details ";

            return View();
        }


    }//controller
}//namespace