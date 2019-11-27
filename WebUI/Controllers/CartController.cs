using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Infrastructure.Abstract;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        // private EFDbContext context;
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;
        private IUserRepository userRepository;
        private IOrderSummaryRepository orderSummaryRepository;
        private IOrderDetailsRepository orderDetailsRepository;
        private IDimShippingRepository dimShippingRepository;

        //private readonly ILogger _logger;
        //private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public CartController(IProductRepository repo, IOrderProcessor proc, IUserRepository useRepo,
                              IOrderSummaryRepository orderSummaryRepository,
                              IOrderDetailsRepository orderDetailsRepository,
                              IDimShippingRepository dimShippingRepository/*, ILogger logger*/)
        {
            repository = repo;
            orderProcessor = proc;
            userRepository = useRepo;
            this.orderSummaryRepository = orderSummaryRepository;
            this.orderDetailsRepository = orderDetailsRepository;
            this.dimShippingRepository = dimShippingRepository;
            //_logger = logger;
        }

        public ActionResult Checkout(Cart cart)
        {
            var userAsync = userRepository.UsersInfo.FirstOrDefault(x=>x.Login==User.Identity.Name);
            ViewBag.OrderPrice = cart.Lines.Sum(x => x.Product.Price*x.Quantity);
            var dimShippingList = dimShippingRepository.DimShipping.Where(x => x.isActive).ToList();

            foreach (var dimShipping in dimShippingList)
            {
                dimShipping.ShippingType = dimShipping.ShippingType + " - " + dimShipping.ShippingPrice + " руб.";
            }

            if (cart.Lines.Count() == 0)
            {
                //  ViewBag.CartError = "Извините, но Ваша корзина пуста";
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    //userAsync.Wait();
                    User userInfo = userAsync;//.Result; //userRepository.UsersInfo.FirstOrDefault(x => x.Login == User.Identity.Name);
                    ShippingDatails shippingDatails = new ShippingDatails();
                    shippingDatails.ShippingEmail = userInfo.Email;
                    shippingDatails.ShippingPhone = userInfo.Phone;
                    shippingDatails.ShippingName = userInfo.UserName;
                    shippingDatails.DimShippings = dimShippingList;

                    return View(shippingDatails);
                }
                else
                {
                    ShippingDatails shippingDatails = new ShippingDatails();
                    shippingDatails.DimShippings = dimShippingList;
                    
                    //return View(new ShippingDatails());
                    return View(shippingDatails);
                }
            }
        }


        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDatails shippingDatails)
        {
            if (cart.Lines.Any()==false)
            {
                ModelState.AddModelError("", "Извините, но Ваша корзина пуста.");
                ViewBag.CartError = "Извините, но Ваша корзина пуста";
            }

            

            try
            {
                if (ModelState.IsValid)
                {
                    //var productList = repository.GetProductListAsync();
                    var productList = repository.Products;
                    //-------------------------------------------------
                    ///запуск системы загрузки в БД
                    int userID;
                    string login = "";
                    if (User.Identity.IsAuthenticated)
                    {
                        User user = userRepository.UsersInfo.FirstOrDefault(p => p.Login == User.Identity.Name);
                        userID = user.UserID;
                        login = user.Login;
                    }
                    else
                    {
                        userID = Domain.Constants.ANONYMOUS_ID;
                        login = Domain.Constants.ANONYMOUS_LOGIN;
                    }
                    var osAsync = orderSummaryRepository.CreateOrderSummary(shippingDatails, userID);

                    int newOrderNumber = orderSummaryRepository.NewOrderNumber();

                    //OrdersSummary os = orderSummaryRepository.CreateOrderSummary(shippingDatails, userID);
                    DimShipping ds =
                        dimShippingRepository.DimShipping.FirstOrDefault(x => x.ShippingID == shippingDatails.DimShippingID);

                    shippingDatails.DimShipping = ds;

                    //osAsync.Wait();

                    OrdersSummary os = osAsync;

                    var usersAsync = userRepository.UsersInfo;

                    os.ShippingPrice = ds.ShippingPrice;
                    os.ShippingType = ds.ShippingType;

                    decimal totalValue = 0;

                    //productList.Wait();
                    foreach (var p in cart.Lines)
                    {
                        Product product = productList.FirstOrDefault(x => x.ProductID == p.Product.ProductID);

                        //уменьшаем количество товаром на величину заказанных
                        product.Quantity = product.Quantity - p.Quantity;
                        //var savePrAsync = repository.SaveProductAsync(product);
                        repository.SaveProduct(product);

                        OrderDetails order = new OrderDetails();
                        order.Quantity = p.Quantity;
                        order.ProductID = p.Product.ProductID;
                        order.UserID = userID;
                        order.Price = p.Product.Price;
                        order.OrderSummaryID = os.OrderSummaryID;
                        orderDetailsRepository.CreateOrderDetails(order);
                        totalValue += p.Quantity * p.Product.Price;

                        /*savePrAsync.Wait();
                        var b = savePrAsync.Result;
                        if (b!=true)
                        {
                            repository.SaveProduct(product);  
                        }*/

                    }
                    //public void AddTotalValue(OrdersSummary os, decimal totalValue)
                    orderSummaryRepository.RefreshTotalValue(os);
                    //orderSummaryRepository.RefreshTotalValue(os);

                    //usersAsync.Wait();

                    var tmpEmails = from u in usersAsync.ToList()
                                    join ur in userRepository.UserRoles on u.UserID equals
                                        ur.UserID
                                    join r in userRepository.Roles on ur.RoleID equals
                                        r.RoleID
                                    where r.RoleName == "Content Manager" || r.RoleName == "ContentManager"
                                    select u.Email;
                    string[] contentManagersEmails = tmpEmails.ToArray();

                    //-------------------------------------------------
                    orderProcessor.ProcessOrder(cart, shippingDatails, os, "Заказ в магазине", "inserted", contentManagersEmails);
                    cart.Clear();
                    //logger.Warn("Клиент " + login + "сделал заказ");
                    return View("Completed");
                }
            }
            catch (Exception)
            {


                ModelState.AddModelError("", "Неудачная попытка отправки формы");
                return View(shippingDatails);
            }

            return View(shippingDatails);
            
        }


        public ActionResult AddToCart(Cart cart, int productId, string returnUrl, string login)
        {
            var userAsync = userRepository.UsersInfo.FirstOrDefault(x=>x.Login==login);
            
            if (returnUrl.Contains("X-Requested"))
            {
                returnUrl = returnUrl.Remove(returnUrl.IndexOf("X-Requested"));
            }
            
            Product product = repository.Products
                                        .FirstOrDefault(p => p.ProductID == productId);
            
            if (product != null)
            {
                var qnt = cart.Lines.FirstOrDefault(x => x.Product.ProductID == productId);
                int quantity = (qnt == null) ? 0 : qnt.Quantity;
              //  userAsync.Wait();
                User user = userAsync;
                if (quantity<product.Quantity)
                {   
                    cart.AddItem(product, 1, user.UserID); 
                }
                else
                {
                    cart.AddItem(product, 0, user.UserID); 
                }
            }

            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                                        .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new {returnUrl});
        }

        public ActionResult Index(Cart cart, int? productId, string returnUrl, string action, int userId = Domain.Constants.ANONYMOUS_ID, int quantity = 0)
        {
            if (productId!=null)
            {
                Product product = repository.Products
                                      .FirstOrDefault(p => p.ProductID == productId);
                CartLine cartLine = new CartLine();
                cartLine.Product = product;
                //cartLine.Quantity = 0;
                cartLine.Quantity = quantity;
                if (action == "+")
                {
                    if (cartLine.Quantity < cartLine.Product.Quantity)
                    {
                        cartLine.Quantity++;

                        if (product != null)
                        {
                            cart.AddItem(product, 1, userId);  
                            Session["UserID"] = userId;
                        }
                    }
                }
                else if (action == "-")
                {
                    if (cartLine.Quantity > 1)
                    {
                        cartLine.Quantity--;

                        if (product != null)
                        {
                            cart.RemoveItem(product, 1, userId); //это временный 
                            Session["UserID"] = userId;
                        }
                    }
                }

                ModelState.Clear();

                if (Request.IsAjaxRequest())
                {
                    Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                    return PartialView("CartDetailsPartial", new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
                }
                
                return View(new CartIndexViewModel
                {
                    Cart = cart,
                    ReturnUrl = returnUrl//,
                }
                    );
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                    return PartialView("CartDetailsPartial", new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
                }

                return View(new CartIndexViewModel
                {
                    Cart = cart,
                    ReturnUrl = returnUrl//,
                    // dimShipping = ds.ToList()
                }
                    );
            }
            
        }
    

    public PartialViewResult Summary(Cart cart)
        {
        if (Request.IsAjaxRequest())
        {
            return PartialView(cart);
        }
            return PartialView(cart);
        }

     /*   public ActionResult ChangeQuantity(Cart cart, int productId, string returnUrl, int quantity, int userId, string action)
        {
            Product product = repository.Products
                                        .FirstOrDefault(p => p.ProductID == productId);
            CartLine cartLine = new CartLine();
            cartLine.Product = product;
            cartLine.Quantity = quantity;
            if (action == "+")
            {
                if (cartLine.Quantity < 5000)
                {
                    cartLine.Quantity++;

                    if (product != null)
                    {
                        cart.AddItem(product, 1, (userId == null) ? Domain.Constants.ANONYMOUS_ID : userId); //это временный 
                        Session["UserID"] = (userId == null) ? Domain.Constants.ANONYMOUS_ID : userId;
                    }
                }
            }
            else if (action == "-")
            {
                if (cartLine.Quantity > 1)
                {
                    cartLine.Quantity--;

                    if (product != null)
                    {
                        cart.RemoveItem(product, 1, (userId == null) ? Domain.Constants.ANONYMOUS_ID : userId); //это временный 
                        Session["UserID"] = (userId == null) ? Domain.Constants.ANONYMOUS_ID : userId;
                    }
                }
            }


            ModelState.Clear();

            if (Request.IsAjaxRequest())
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                return PartialView("CartDetailsPartial", new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });



            }
            return RedirectToAction("Index", new { returnUrl });  
        }
      */
      
        /*public ActionResult ShippingInfo()
        {
            IEnumerable<DimShipping> ds = dimShippingRepository.DimShipping.Where(x => x.isActive == true);
            return PartialView(ds);
        }*/

    [HttpGet]
        public ActionResult SummaryPricePartial(int shippingId)
        {
            DimShipping ds = dimShippingRepository.DimShipping.FirstOrDefault(x => x.ShippingID == shippingId);
            string value = ds.ShippingPrice.ToString() + " руб.";
            //return Json(JsonStandardResponse.ErrorResponse(ds.ShippingPrice.ToString()), JsonRequestBehavior.AllowGet);   
        return Json(JsonStandardResponse.ErrorResponse(value), JsonRequestBehavior.AllowGet);
        //return Json(JsonStandardResponse.SuccessResponse(true), JsonRequestBehavior.AllowGet);
        //return ds.ShippingPrice;
        }


    }

}


/*
 * 
 * 
*/