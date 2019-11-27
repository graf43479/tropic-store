using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Xml.Linq;
using Domain;
using Domain.Entities;
using Newtonsoft.Json.Linq;
using WebUI.Infrastructure.Abstract;
using WebUI.Models;
using System.Web.Caching;


namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider authProvider;
        private DataManager dataManager;
        private readonly ILogger _logger;
        //private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public AccountController(IAuthProvider auth, DataManager dataManager, ILogger logger)
        {
            authProvider = auth;
            this.dataManager = dataManager;
            _logger = logger;


            //  public void TryToUpdateDimSettings(string settingTypeID, string settingTypeDesc, string settingsID, string settingsDesc, string settingValue)

        }

        public ViewResult LogOn()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authentificate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Actions", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Некорректное имя пользователя и пароль");
                    return View();
                }
            }
            else
            {
                //logger.Info("Пользователь вошел успешно");
                return View();
            }

        }

        public ActionResult RegistrateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrateUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //  StringBuilder pwd = new StringBuilder();
                //   pwd.Append(model.Password);
                //ViewBag.originPassword = model.Password;
                //string originPassword = String.Format(model.Password);
                //  string originPassword = String.Copy(model.Password);//String.Copy(model.Password); //String.Format(model.Login);
                model.PasswordSalt = CreateSalt();
                string pwd = CreatePasswordHash(model.Password, model.PasswordSalt);

                User socialUser =
                    dataManager.UsersRepository.UsersInfo.FirstOrDefault(
                        x => x.Email == model.Email && (x.Password == "FB_authentification" || x.Password == "VK_authentification"));
                if (socialUser != null)
                {
                    socialUser.UserName = model.UserName;
                    socialUser.Created = model.Created;
                    socialUser.IsActivated = model.IsActivated;
                    socialUser.IsActive = true;
                    socialUser.Login = model.Login;
                    socialUser.Mailing = model.Mailing;
                    socialUser.PasswordSalt = model.PasswordSalt;
                    //socialUser.NewEmailKey = model.Mailing;
                    socialUser.Password = pwd;
                    socialUser.Phone = model.Phone;

                    dataManager.UsersRepository.SaveUser(socialUser);

                    return RedirectToAction("List", "Product");
                }
                else
                {

                    //model.Password = CreatePasswordHash(model.Password, model.PasswordSalt);


                    MembershipCreateStatus status = dataManager.Provider.CreateUser(model.UserName, model.Login, pwd,
                                                                                    model.Email, model.Phone,
                                                                                    model.IsActivated, model.Mailing,
                                                                                    model.PasswordSalt);
                    //  var tmp1 = CreatePasswordHash(model.Password, model.PasswordSalt);
                    //   var tmp2 =model.Password;
                    if (status == MembershipCreateStatus.Success)
                    {
                        //FormsAuthentication.SetAuthCookie(model.Login, false);

                        dataManager.UsersRepository.GetMembershipUserByName(model.Login);

                        User userInfo = dataManager.UsersRepository.UsersInfo
                                                   .FirstOrDefault(p => p.Login == model.Login);

                        userInfo.Password = model.Password;


                        // userInfo.Password = originPassword; //ViewBag.originPassword;
                        string host = Request.Url.Host;
                        dataManager.OrdersProcessor.EmailActivation(userInfo, host);

                        return RedirectToAction("UserRole", new {login = model.Login});
                        // dataManager.UsersRepository.AddUserToRole(model.Login, "User");



                        //logger.Info("Новый пользователь: " + userInfo.Login);
                        //return View("Success", model);
                    }
                    else if (status == MembershipCreateStatus.DuplicateEmail)
                    {
                        RedirectToAction("List", "Product");
                    }

                    ModelState.AddModelError("", GetMembershipCreateStatusResultText(status));
                }
            }
            return View(model);
                
        }

        public ActionResult UserRole(string login)
        {
            dataManager.UsersRepository.AddUserToRole(login, "User");
            return View("Success");

        }

        public string GetMembershipCreateStatusResultText(MembershipCreateStatus status)
        {
            if (status == MembershipCreateStatus.DuplicateUserName)
                return "Пользователь с таким логином уже существует";
            if (status == MembershipCreateStatus.DuplicateEmail)
                return "Пользователь с таким email уже существует";
            return "Неизвестная ошибка";
        }


        public ActionResult UserEnter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserEnter(LoginViewModel model)
        {
            //TempData.Peek("attempt");
            if (Session["attempt"] == null)
            {
                Session["attempt"] = 0;
            }


            //validate captcha
            if ((Session["Captcha"] == null || Session["Captcha"].ToString() != model.Captcha) &&
                (int) Session["attempt"] > 3)
            {
                ModelState.AddModelError("Captcha", "Сумма введена неверно! Пожалуйста, повторите ещё раз!");
                Session["attempt"] = (int) Session["attempt"] + 1;
                //логгирование
                if ((int) Session["attempt"] == 4)
                {
                  //  logger.Info("Ошибка аутентификации у " + model.Login);
                }

                return View(model);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    User user = dataManager.UsersRepository.UsersInfo.FirstOrDefault(x => x.Login == model.Login);
                    /*if (user.Password.Length > 30 && user.IsActivated)
                    {
                        model.Password = CreatePasswordHash(model.Password, user.PasswordSalt);
                    }
                    else
                    {

                    }
                    */
                    //if (dataManager.Provider.ValidateUser(model.Login, model.Password))
                    if (dataManager.Provider.ValidateUser(model.Login,
                                                          CreatePasswordHash(model.Password, user.PasswordSalt)))
                    {
                        /*   User userInfo = dataManager.UsersRepository.UsersInfo
                                                      .FirstOrDefault(p => p.Login == model.Login);*/

                        /*     UserRole ur =
                                 dataManager.UsersRepository.UserRoles.FirstOrDefault(x => x.UserID == userInfo.UserID);
                         if (ur==null)
                         {
                             dataManager.UsersRepository.AddUserToRole(model.Login, "User");
                         }*/


                        //dataManager.UsersRepository.AddUserToRole(model.Login, "User");
                        FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);
                        
                        


                        //.UserName;
                        //ViewBag.UserNameInfo = userInfo.UserName;
                        //TempData["UserInfoMessage"] = userInfo.UserName;
                        //TempData.Keep("UserInfoMessage");
                        Session["UserName"] = user.UserName;
                        Session["UserID"] = user.UserID;
                        dataManager.UsersRepository.GetMembershipUserByName(model.Login);
                        Session["attempt"] = 0;
                        //logger.Info("Пользователь вошел удачно!");
                        return RedirectToAction("List", "Product");
                    }
                    ModelState.AddModelError("", "Неудачная попытка входа на сайт");
                    Session["attempt"] = (int) Session["attempt"] + 1;
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Неудачная попытка входа на сайт");
                    Session["attempt"] = (int) Session["attempt"] + 1;
                    return View(model);
                }
            }
            return View(model);
        }

        //-------

        public ActionResult UserProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                IEnumerable<User> users = dataManager.UsersRepository.UsersInfo.ToList();
                Session["UserID"] =
                    users.FirstOrDefault(x => x.Login == User.Identity.Name)
                               .UserID.ToString();
                string str = Session["UserID"].ToString();

                int s;
                Int32.TryParse(str, out s);

                User user = users.FirstOrDefault(p => p.UserID == s);

                RegisterViewModel viewModel = new RegisterViewModel()
                    {
                        UserName = user.UserName,
                        Login = user.Login,
                        Password = user.Password,
                        ConfirmPassword = "*****" /*user.Password*/,
                        Email = user.Email,
                        Phone = user.Phone,
                        UserID = s,
                        Created = user.Created,
                        Mailing = user.Mailing
                    };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("UserEnter");
            }
        }


        //-------


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserName"] = null;
            Session["UserID"] = null;
            HttpContext.Session.Clear();
            return RedirectToAction("UserEnter");

        }


        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult UserAccountEdit()
        {

            if (User.Identity.IsAuthenticated)
            {
                string str = User.Identity.Name; //Session["UserID"].ToString().Trim();

                //int s;
                //Int32.TryParse(str, out s);

                User user = dataManager.UsersRepository.UsersInfo.FirstOrDefault(p => p.Login == str);


                RegisterViewModel viewModel = new RegisterViewModel()
                    {
                        UserName = user.UserName,
                        Login = user.Login,
                        Password = user.Password,
                        ConfirmPassword = user.Password,
                        Email = user.Email,
                        Phone = user.Phone,
                        UserID = user.UserID,
                        Mailing = user.Mailing,
                        OldPassword = "***********",
                        Created = user.Created,
                        IsActivated = user.IsActivated,
                        PasswordSalt = user.PasswordSalt
                    };

                return PartialView(viewModel);
            }
            else
            {

                return RedirectToAction("UserEnter", "Account");
            }
        }

        [HttpPost]
        public ActionResult UserAccountEdit(RegisterViewModel model)
        {
            if (ModelState.IsValid & (User.Identity.Name != null))
            {
                FormsAuthentication.SetAuthCookie(model.Login, false);

                var userAsync = dataManager.UsersRepository.UsersInfo.FirstOrDefault(x=>x.UserID==model.UserID);

                //User userInfo = dataManager.UsersRepository.UsersInfo.FirstOrDefault(p => p.UserID == model.UserID);

                if (dataManager.UsersRepository.UsersInfo.Where(x => x.UserID != model.UserID)
                               .FirstOrDefault(x => x.Email == model.Email) != null)
                {
                    ModelState.AddModelError("", "Такой email уже существует");
                    return Json(JsonStandardResponse.ErrorResponse("Такой email уже существует"), JsonRequestBehavior.DenyGet);
                }


                if (dataManager.UsersRepository.UsersInfo.Where(x => x.UserID != model.UserID)
                               .FirstOrDefault(x => x.Login == model.Login)!=null)
                {
                    ModelState.AddModelError("", "Такой логин уже существует");
                    return Json(JsonStandardResponse.ErrorResponse("Такой логин уже существует"), JsonRequestBehavior.DenyGet);
                }
                //userAsync.Wait();
                User userInfo = userAsync;//.Result;
                userInfo.Login = model.Login;
                userInfo.Password = model.Password;
                userInfo.UserName = model.UserName;
                userInfo.Email = model.Email;
                userInfo.Phone = model.Phone;
                userInfo.Mailing = model.Mailing;
                userInfo.PasswordSalt = model.PasswordSalt;
                userInfo.Password = model.Password;
                userInfo.Created = model.Created;

                dataManager.UsersRepository.SaveUser(userInfo);
                TempData["Message"] = string.Format("профиль {0} изменен", model.Login);
                TempData["messageType"] = "information-msg";
                Session["UserName"] = model.UserName;
                //return JavaScript("window.location.replace(http://localhost:57600/Account/UserProfile);");
                //  var df = ModelState.IsValid;
                return Json(JsonStandardResponse.SuccessResponse(), JsonRequestBehavior.DenyGet);
                //return Json(JsonStandardResponse.SuccessResponse());
                // return Json(JsonStandardResponse.SuccessResponse("На указанный адрес был выслан пароль"), JsonRequestBehavior.DenyGet);
                //return Content(Boolean.TrueString);
                //return RedirectToAction("List", "Product");
                //return Json(JsonStandardResponse.ErrorResponse("Пожалуйста проверьте форму"), JsonRequestBehavior.DenyGet);
            }
            else
                ModelState.AddModelError("", "Неудачная попытка изменения учетной записи");
            //return View(model);
            //return Content("Пожалуйста проверьте форму");
            return Json(JsonStandardResponse.ErrorResponse("Пожалуйста проверьте форму"), JsonRequestBehavior.DenyGet);

        }

        public ActionResult ChangePassword()
        {
            if (User.Identity.IsAuthenticated)
            {

                User user = dataManager.UsersRepository.UsersInfo.FirstOrDefault(p => p.Login == User.Identity.Name);


                RegisterViewModel viewModel = new RegisterViewModel()
                    {
                        UserName = user.UserName,
                        Login = user.Login,
                        Password = "", //user.Password,
                        ConfirmPassword = "", // user.Password,
                        Email = user.Email,
                        Phone = user.Phone,
                        UserID = user.UserID,
                        Mailing = user.Mailing,
                        IsActivated = user.IsActivated
                    };
                //if (Request.IsAjaxRequest())
                //{
                    return PartialView(viewModel);   
                //}
                
            }
            else
            {
                return RedirectToAction("UserEnter", "Account");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(RegisterViewModel model)
        {


            if (ModelState.IsValid && (model.Password == model.ConfirmPassword))
            {
                User userInfo = dataManager.UsersRepository.UsersInfo.FirstOrDefault(p => p.UserID == model.UserID);
                string tmp = CreatePasswordHash(model.OldPassword, userInfo.PasswordSalt);
                if (userInfo.Password != CreatePasswordHash(model.OldPassword, userInfo.PasswordSalt) &&
                    (userInfo.Password.Length > 35))
                {
                    //return PartialView(model); 
                    ModelState.AddModelError("", "Неудачная попытка изменения учетной записи");
                    //return Content("Пожалуйста проверьте форму");
                    return Json(JsonStandardResponse.ErrorResponse("Пожалуйста проверьте форму"),
                                JsonRequestBehavior.DenyGet);
                }

                if (userInfo.Password != model.OldPassword && userInfo.Password.Length < 35)
                {
                    //return PartialView(model);
                    ModelState.AddModelError("", "Неудачная попытка изменения учетной записи");
                    return Json(JsonStandardResponse.ErrorResponse("Пожалуйста проверьте форму"),
                                JsonRequestBehavior.DenyGet);
                }

                userInfo.Password = CreatePasswordHash(model.Password, userInfo.PasswordSalt);
                FormsAuthentication.SetAuthCookie(model.Login, false);

                dataManager.UsersRepository.SaveUser(userInfo);
                TempData["Message"] = string.Format("Пароль для учетной записи {0} изменен", model.Login);
                TempData["messageType"] = "information-msg";
                Session["UserName"] = model.UserName;
                //return RedirectToAction("List", "Product");
                //return Content(Boolean.TrueString); ; //JavaScript("window.location.replace('http://localhost:57600/Account/UserAccountEdit');");
                return Json(JsonStandardResponse.SuccessResponse(true), JsonRequestBehavior.DenyGet);
            }
            else
                ModelState.AddModelError("", "Неудачная попытка изменения пароля");
            return Json(JsonStandardResponse.ErrorResponse("Пожалуйста проверьте форму"), JsonRequestBehavior.DenyGet);
            //return Content("Пожалуйста проверьте форму"); //Content("Пожалуйста проверьте форму");
            // return View(model);
          //  return PartialView(model);
        }

        public ActionResult ForgottenPassword()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ForgottenPassword(User user)
        {

            if (dataManager.UsersRepository.GetUserNameByEmail(user.Email) != "")
            {

                User userInfo = dataManager.UsersRepository.UsersInfo.FirstOrDefault(
                    x => x.Email == user.Email);
                string pwdOrigin = CreatePassword(6);
                userInfo.Password = CreatePasswordHash(pwdOrigin, userInfo.PasswordSalt);
                dataManager.UsersRepository.SaveUser(userInfo);
                userInfo.Password = pwdOrigin;
                dataManager.OrdersProcessor.EmailRecovery(userInfo);


                ViewBag.UserInfo = "На указанный адрес был выслан пароль";
                //return View("Success");
                //return Content(Boolean.TrueString);
                return Json(JsonStandardResponse.SuccessResponse("На указанный адрес был выслан пароль"),
                            JsonRequestBehavior.DenyGet);
            }

            else
            {
                if (user.Email == null)
                {
                    Session["UserEmail"] = "";
                    //ModelState.AddModelError("", "Неудачная попытка изменения пароля");
                    return Json(JsonStandardResponse.ErrorResponse("Пожалуйста проверьте форму"),
                                JsonRequestBehavior.DenyGet);
                    //return Content("Пожалуйста проверьте форму");
                    //return View(user);
                }
                else
                {
                    //Session["UserEmail"] = "Пользователя с таким email не существует!";
                    //return Content("Пользователя с таким email не существует!");
                    return Json(JsonStandardResponse.ErrorResponse("Пользователя с таким email не существует!"),
                                JsonRequestBehavior.DenyGet);
                    //return View(user);
                }


            }
        }

        //--Просмотр статусов заказа
        public ActionResult AccountOrdersList(int userId)
        {
           // User user = dataManager.UsersRepository.UsersInfo.Where(x => x.UserID == userId).Single();

            IEnumerable<OrdersSummary> ordersSummary =
                dataManager.OrderSummaryRepository.OrdersSummaryInfo.Where(x => x.UserID == userId)
                           .OrderByDescending(x => x.TransactionDate).ToList();

            return PartialView(ordersSummary);
        }

        public ActionResult AccountOrdersDetails(int? orderId)
        {
            IEnumerable<OrderDetails> orderDetails =
                dataManager.OrderDetailsRepository.OrdersDetails.Where(x => x.OrderSummaryID == orderId).ToList();

            if (orderId != null)
            {
               /* IEnumerable<OrderStatus> orderStatuses =
                    dataManager.OrderStatusRepository.OrderStatuses.Where(x => x.OrderStatusID == orderId);*/
                PopulateAssignedStatus(orderId);
                TempData["OrderNumber"] =
                    dataManager.OrderSummaryRepository.OrdersSummaryInfo.FirstOrDefault(x => x.OrderSummaryID == orderId)
                               .OrderNumber.ToString();
            }
            else
            {
                PopulateAssignedStatus(orderId);
            }

            return PartialView(orderDetails);
        }

        //формирует модель с отмеченными статусами заказа
        private void PopulateAssignedStatus(int? orderSummaryId)
        {
            //OrdersSummary os = repositoryOrderSummary.OrdersSummaryInfo.FirstOrDefault(x=>x.OrderSummaryID==orderSummaryId);

            var allStatuses = dataManager.DimOrderStatusRepository.DimOrderStatuses.ToList();

            if (orderSummaryId == null)
            {
                //var allStatuses = dataManager.DimOrderStatusRepository.DimOrderStatuses.ToList();
                var viewModel = new List<AssignedStatusViewModel>();
                foreach (var status in allStatuses)
                {
                    viewModel.Add(new AssignedStatusViewModel
                        {
                            Assigned = false,
                            DimOrderStatusID = status.DimOrderStatusID,
                            DimOrderStatusDesc = status.OrderStatusDesc
                        });
                }
                TempData["Statuses"] = viewModel;
                TempData["Visible"] = false;
            }
            else
            {
                OrdersSummary os =
                    dataManager.OrderSummaryRepository.OrdersSummaryInfo.FirstOrDefault(
                        x => x.OrderSummaryID == orderSummaryId);
                //var allStatuses = dataManager.DimOrderStatusRepository.DimOrderStatuses;
                //var orderSummaryStatuses = new HashSet<int>(os.DimOrderStatuses.Select(c => c.DimOrderStatusID));
                var orderSummaryStatuses = new HashSet<int>(os.OrderStatuses.Select(c => c.DimOrderStatusID)).ToList();
                var viewModel = new List<AssignedStatusViewModel>();
                foreach (var status in allStatuses)
                {
                    viewModel.Add(new AssignedStatusViewModel
                        {
                            Assigned = orderSummaryStatuses.Contains(status.DimOrderStatusID),
                            DimOrderStatusID = status.DimOrderStatusID,
                            DimOrderStatusDesc = status.OrderStatusDesc
                        });
                }
                // ViewBag.Statuses = viewModel;
                TempData["Statuses"] = viewModel;
                TempData["Visible"] = true;
            }

        }


        public ActionResult Activate(string username, string key)
        {

            if (dataManager.UsersRepository.ActivateUser(username, key) == false)
            {
                TempData["message"] =
                    string.Format("При активации произошла проблема. Возможно вы уже активировались ранее!");
                TempData["messageType"] = "warning-msg";
                return RedirectToAction("RegistrateUser", "Account");
            }
            else
            {
                TempData["message"] =
                    string.Format(
                        "Поздравляем! Активация прошла успешно! Введите свой логин и пароль, чтобы авторизироваться на сайте! ");
                TempData["messageType"] = "confirmation-msg";
                //TempData.Keep("message");
                //logger.Info("Пользователь " + username + " активировался");
                return RedirectToAction("UserEnter", "Account");
            }
        }


        //генерация хэша
        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
            return hashedPwd;
        }

        //Генерация соли
        private static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        //генерация пароля
        public string CreatePassword(int length)
        {
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += valid[rnd.Next(valid.Length)];
            return res;
        }

        //Капча
        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int) DateTime.Now.Ticks);

            //generate new question
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);

            //store answer
            Session["Captcha" + prefix] = a + b;

            //image stream
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image) bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                            (rand.Next(0, 255)),
                            (rand.Next(0, 255)),
                            (rand.Next(0, 255)));

                        r = rand.Next(0, (130/3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                //render as Jpeg
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }


        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult AjaxUserEnter()
        {
            LoginViewModel viewModel = new LoginViewModel();
            // Thread.Sleep(2000); // Fake processing time
            if (Request.IsAjaxRequest())
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache); 
                return PartialView(viewModel);    
            }
            return RedirectToAction("UserEnter");

        }

        [HttpPost]
        public ActionResult AjaxUserEnter(LoginViewModel model)
        {
            //Thread.Sleep(10000);
            if (Session["attempt"] == null)
            {
                Session["attempt"] = 0;
            }
            else if ((int) Session["attempt"] > 3)
            {
                return Json(JsonStandardResponse.ErrorResponse("Capcha"), JsonRequestBehavior.DenyGet);
            }


            //validate captcha
            if ((Session["Captcha"] == null || Session["Captcha"].ToString() != model.Captcha) &&
                (int) Session["attempt"] > 3)
            {
                ModelState.AddModelError("Captcha", "Сумма введена неверно! Пожалуйста, повторите ещё раз!");
                Session["attempt"] = (int) Session["attempt"] + 1;
                return Json(JsonStandardResponse.ErrorResponse("Capcha"), JsonRequestBehavior.DenyGet);
                // return JavaScript("window.location.replace('http://localhost:57600/Account/UserEnter');");
                //return RedirectToAction("UserEnter","Account");
                //View("_LogOnError"); //PartialView("UserEnter", model); //RedirectToAction("UserEnter", model);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    User user = dataManager.UsersRepository.UsersInfo.FirstOrDefault(x => x.Login == model.Login);
                    /*if (user.Password.Length > 30 && user.IsActivated)
                    {
                        model.Password = CreatePasswordHash(model.Password, user.PasswordSalt);
                    }
                    else
                    {

                    }
                    */
                    //if (dataManager.Provider.ValidateUser(model.Login, model.Password))
                    if (dataManager.Provider.ValidateUser(model.Login,
                                                          CreatePasswordHash(model.Password, user.PasswordSalt)))
                    {
                        /*   User userInfo = dataManager.UsersRepository.UsersInfo
                                                      .FirstOrDefault(p => p.Login == model.Login);*/

                        /*     UserRole ur =
                                 dataManager.UsersRepository.UserRoles.FirstOrDefault(x => x.UserID == userInfo.UserID);
                         if (ur==null)
                         {
                             dataManager.UsersRepository.AddUserToRole(model.Login, "User");
                         }*/


                        //dataManager.UsersRepository.AddUserToRole(model.Login, "User");
                        FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);



                        //.UserName;
                        //ViewBag.UserNameInfo = userInfo.UserName;
                        //TempData["UserInfoMessage"] = userInfo.UserName;
                        //TempData.Keep("UserInfoMessage");
                        Session["UserName"] = user.UserName;
                        Session["UserID"] = user.UserID;
                        dataManager.UsersRepository.GetMembershipUserByName(model.Login);
                        Session["attempt"] = 0;
                       // logger.Info("Пользователь вошел удачно!");
                        
                        return Json(JsonStandardResponse.SuccessResponse(true), JsonRequestBehavior.DenyGet);
                        //return View("_OK");
                    }
                    ModelState.AddModelError("", "Неудачная попытка входа на сайт");
                    Session["attempt"] = (int) Session["attempt"] + 1;
                    //          return Json(JsonStandardResponse.ErrorResponse("Пожалуйста проверьте форму"), JsonRequestBehavior.DenyGet);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Неудачная попытка входа на сайт");
                    Session["attempt"] = (int) Session["attempt"] + 1;
                    //return PartialView(model);
                    //return Content("Пожалуйста проверьте форму");
                    return Json(JsonStandardResponse.ErrorResponse("Пожалуйста проверьте форму"),
                                JsonRequestBehavior.DenyGet);
                    //return View("_OK");
                }

            }

            //return PartialView(model);
            //return Content("Пожалуйста проверьте форму");
            return Json(JsonStandardResponse.ErrorResponse("Пожалуйста проверьте форму"), JsonRequestBehavior.DenyGet);
        }


        public ActionResult AdminSetup()
        {
            if (dataManager.UsersRepository.AdminExists())
                return RedirectToAction("UserEnter");
            return View();
        }

        [HttpPost]
        public ActionResult AdminSetup(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(model.Password);
                string originPassword = sb.ToString();
                string salt = CreateSalt();
                model.Password = CreatePasswordHash(model.Password, model.PasswordSalt);


                MembershipCreateStatus status = dataManager.Provider.CreateUser(model.UserName, model.Login,
                                                                                model.Password, model.Email, model.Phone,
                                                                                model.IsActivated, model.Mailing,
                                                                                salt);

                if (status == MembershipCreateStatus.Success)
                {
                    //FormsAuthentication.SetAuthCookie(model.Login, false);

                    User userInfo = dataManager.UsersRepository.UsersInfo
                                               .FirstOrDefault(p => p.Login == model.Login);

                    dataManager.UsersRepository.GetMembershipUserByName(model.Login);

                    // Session["UserID"] = userInfo.UserID;
                    // Session["UserName"] = userInfo.UserName;
                    userInfo.Password = originPassword;
                    string host = Request.Url.Host;
                    dataManager.OrdersProcessor.EmailActivation(userInfo, host);
                    ///Session["UserID"] = model;
                    dataManager.UsersRepository.CreateRole("Admin");
                    dataManager.UsersRepository.AddUserToRole(model.Login, "Admin");
                //    logger.Info("Пользователю " + userInfo.Login + " назначена роль Admin");
                    //RoleService.AddUsersToRoles(new string[] { model.UserName }, new string[] { "Admin" });
                    //FormsService.SignIn(model.UserName, true);
                    return View("Success", model);
                }
                ModelState.AddModelError("", GetMembershipCreateStatusResultText(status));
            }
            return View(model);
        }


        [HttpGet]
        [OutputCache(Duration = 10000)]
        public ActionResult Contacts()
        {
            //FeedBack feedBack = new FeedBack();
            return View();
        }

        [HttpPost]
        public ActionResult Contacts(int mySelect)
        {
             //   return JavaScriptResult("window.location.replace(http://localhost:57600/Account/UserProfile);");
         
            //FeedBack feedBack = new FeedBack();
            return View();
        }

        [HttpGet]
        public ActionResult FeedBackModal()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View("FeedBack", new FeedBack());

        }

        [HttpPost]
        public ActionResult FeedBackModal(FeedBack viewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    dataManager.OrdersProcessor.FeedBackRequest(viewModel);

                                   
                    //         ModelState.AddModelError("", "Неудачная попытка входа на сайт");

                    //          return Json(JsonStandardResponse.ErrorResponse("Пожалуйста проверьте форму"), JsonRequestBehavior.DenyGet);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Неудачная попытка отправки формы");

                    if (Request.IsAjaxRequest())
                    {
                        return Json(JsonStandardResponse.ErrorResponse("Пожалуйста проверьте форму"),
                                    JsonRequestBehavior.DenyGet);    
                    }
                    return View("FeedBack", viewModel);
                }

                var tmpEmails = from u in dataManager.UsersRepository.UsersInfo
                                join ur in dataManager.UsersRepository.UserRoles on u.UserID equals
                                    ur.UserID
                                join r in dataManager.UsersRepository.Roles on ur.RoleID equals
                                    r.RoleID
                                where r.RoleName == "Content Manager" || r.RoleName == "ContentManager"
                                select u.Email;
                string[] contentManagersEmails = tmpEmails.ToArray();
                try
                {
                    dataManager.OrdersProcessor.FeedBackRequestForContentManagers(viewModel, contentManagersEmails);
                    if (Request.IsAjaxRequest())
                    {
                        return Json(JsonStandardResponse.SuccessResponse(true), JsonRequestBehavior.DenyGet);    
                    }
                TempData["Message"] = string.Format("Уважаемый {0}! Ваш вопрос был отправлен! В ближайшее время ответ на Ваш запрос прийдет на указанную почту", viewModel.Name);
                TempData["messageType"] = "information-msg";
                    return RedirectToAction("List", "Product");

                }
                catch (Exception)
                {
                    if (Request.IsAjaxRequest())
                    {
                        return Json(JsonStandardResponse.SuccessResponse(true), JsonRequestBehavior.DenyGet);
                    }
                    return View("FeedBack", viewModel);
                }

 
            }
            if (Request.IsAjaxRequest())
            {
                return Json(JsonStandardResponse.ErrorResponse("Пожалуйста проверьте форму"), JsonRequestBehavior.DenyGet);
            }
            return View("FeedBack", viewModel);

        }



#region Errors
        
                                     // GET: /Error/HttpError404
        public ActionResult HttpError404(string message)
        {
            ViewBag.ErrorCode = message;
            return View("HttpError404");
        }

        public ActionResult HttpError500(string message)
        {
            ViewBag.ErrorCode = message;
            return View("HttpError500");
        }


        
#endregion


/*
        public ActionResult Sitemap()
        {
            SitemapBuilder builder = new SitemapBuilder();

            builder.AppendUrl(Url.Action("Index", "Home", null, this.Request.Url.Scheme), ChangefreqEnum.weekly);
            builder.AppendUrl(Url.Action("About", "Home", null, this.Request.Url.Scheme));
            builder.AppendUrl("http://example.com/Home/Contact", ChangefreqEnum.never, 0.9d);

            return new XmlViewResult(builder.XmlDocument);
        }
        */



        public ActionResult FbLogin(string code)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string JsonResult = client.DownloadString(string.Concat("https://graph.facebook.com/oauth/access_token?client_id=", 698808393488449, "&redirect_uri=http://tropic-store.ru/Account/FbLogin/fblogin/&client_secret=", "2f8d87353b440dda7334aee145afd89c", "&code=", code));

            JsonResult = JsonResult.Substring(JsonResult.IndexOf("=") + 1, JsonResult.IndexOf("&") - JsonResult.IndexOf("=")-1);
            
            
            //JObject jsonUserInfo = JObject.Parse(JsonResult);
            JsonResult = client.DownloadString(string.Concat("https://graph.facebook.com/me?access_token=", JsonResult));
            JObject jsonUserInfo = JObject.Parse(JsonResult);
            
            UInt64 facebook_userID = jsonUserInfo.Value<UInt64>("id");
            string username = jsonUserInfo.Value<string>("username");
            string fb_name = jsonUserInfo.Value<string>("first_name");
            string fb_lastname = jsonUserInfo.Value<string>("last_name");
            string email = jsonUserInfo.Value<string>("email");
            //можем сохранить эту информацию

            if (email != null)
            {
                User user = dataManager.UsersRepository.UsersInfo.FirstOrDefault(x => x.Email == email);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Login, false);

                    Session["UserName"] = user.UserName;
                    Session["UserID"] = user.UserID;
                    dataManager.UsersRepository.GetMembershipUserByName(user.Login);
                    Session["attempt"] = 0;
                    //logger.Info("Пользователь вошел удачно через fb!");

                    string p = User.Identity.Name;
                    return RedirectToAction("List", "Product");
                    //return Json(JsonStandardResponse.SuccessResponse(true), JsonRequestBehavior.DenyGet);
                }
                else
                {
                    RegisterViewModel model = new RegisterViewModel()
                        {
                            Email = email,
                            Phone = "FB_authentification",
                            UserName = fb_name + " " + fb_lastname,
                            ConfirmPassword = "FB_authentification",
                            Created = DateTime.Now,
                            Login = email, //facebook_userID.ToString(),
                            Mailing = true,
                            OldPassword = "",
                            Password = "FB_authentification",
                            PasswordSalt = "",
                            IsActivated = true
                        };

                    //----
                    MembershipCreateStatus status = dataManager.Provider.CreateUser(model.UserName, model.Login,
                                                                                    model.Password,
                                                                                    model.Email, model.Phone,
                                                                                    model.IsActivated, model.Mailing,
                                                                                    model.PasswordSalt);
                    //  var tmp1 = CreatePasswordHash(model.Password, model.PasswordSalt);
                    //   var tmp2 =model.Password;
                    if (status == MembershipCreateStatus.Success)
                    {

                        if (dataManager.Provider.ValidateUser(model.Login,
                                                              "FB_authentification"))
                        {
                        
                        FormsAuthentication.SetAuthCookie(model.Login, false);

                        

                        string p = User.Identity.Name;

                           Session["UserName"] = model.UserName;
                        Session["UserID"] = model.UserID;
                        dataManager.UsersRepository.GetMembershipUserByName(model.Login);
                        Session["attempt"] = 0;
                        //logger.Info("Пользователь вошел удачно!");
                        return RedirectToAction("List", "Product");
                            }
                    }


                    


                        //---- 
                    

                }
            }
            else
            {
                //Надо продумывать
                Exception ex;
            }




            
            return RedirectToAction("UserEnter", "Account");
        }


        public ActionResult VkAuth(string code)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string JsonResult = client.DownloadString(string.Concat("https://oauth.vk.com/access_token?client_id=4522845&client_secret=xMfwf6TJIPSmBCjUsYpO&code=", code, "&redirect_uri=http://tropic-store.ru/Account/VkAuth/vk-auth/"));
            JObject jsonUserInfo = JObject.Parse(JsonResult);
            UInt64 userID = jsonUserInfo.Value<UInt64>("user_id");
            string access_token = jsonUserInfo.Value<string>("access_token");
            string email = jsonUserInfo.Value<string>("email");

            JsonResult = client.DownloadString(string.Concat("https://api.vk.com/method/users.get?uids=", userID, "&scope=email&fields=email,uid,first_name,last_name,nickname,screen_name,sex,bdate,city,country,timezone,photo&access_token=", access_token));
            jsonUserInfo = JObject.Parse(JsonResult);
            var m = jsonUserInfo["response"];


            dynamic root = JObject.Parse(JsonResult);
            int uid = Convert.ToInt32((string)root["response"][0]["uid"]);
            string name = Convert.ToString((string)root["response"][0]["first_name"]);




            string lastname = Convert.ToString((string)root["response"][0]["last_name"]);
            string nickname = Convert.ToString((string)root["response"][0]["nickname"]);

            //    var email = m.Value<string>("email");
            //    var nickname = m.Value<string>("nickname");
            //System.Text.Encoding.ASCII.GetString

            if (email != null)
            {
                User user = dataManager.UsersRepository.UsersInfo.FirstOrDefault(x => x.Email == email);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Login, false);

                    Session["UserName"] = user.UserName;
                    Session["UserID"] = user.UserID;
                    dataManager.UsersRepository.GetMembershipUserByName(user.Login);
                    Session["attempt"] = 0;
              //      logger.Info("Пользователь вошел удачно через vk.com!");

                    string p = User.Identity.Name;
                    return RedirectToAction("List", "Product");
                    //return Json(JsonStandardResponse.SuccessResponse(true), JsonRequestBehavior.DenyGet);
                }
                else
                {
                    RegisterViewModel model = new RegisterViewModel()
                    {
                        Email = email,
                        Phone = "VK_authentification",
                        UserName = name + " " + lastname,
                        ConfirmPassword = "VK_authentification",
                        Created = DateTime.Now,
                        Login = email, //uid.ToString(),
                        Mailing = true,
                        OldPassword = "",
                        Password = "VK_authentification",
                        PasswordSalt = "",
                        IsActivated = true
                    };

                    //----
                    MembershipCreateStatus status = dataManager.Provider.CreateUser(model.UserName, model.Login,
                                                                                    model.Password,
                                                                                    model.Email, model.Phone,
                                                                                    model.IsActivated, model.Mailing,
                                                                                    model.PasswordSalt);
                    //  var tmp1 = CreatePasswordHash(model.Password, model.PasswordSalt);
                    //   var tmp2 =model.Password;
                    if (status == MembershipCreateStatus.Success)
                    {

                        if (dataManager.Provider.ValidateUser(model.Login,
                                                              "VK_authentification"))
                        {

                            FormsAuthentication.SetAuthCookie(model.Login, false);



                            string p = User.Identity.Name;

                            Session["UserName"] = model.UserName;
                            Session["UserID"] = model.UserID;
                            dataManager.UsersRepository.GetMembershipUserByName(model.Login);
                            Session["attempt"] = 0;
                    //        logger.Info("Пользователь вошел удачно!");
                            return RedirectToAction("List", "Product");
                        }
                    }





                    //---- 


                }
            }
            else
            {
                //Надо продумывать
                Exception ex;
            }

            return RedirectToAction("List", "Product");
        }
        



        public ActionResult Robots()
        {
            Response.ContentType = "text/plain";
            return View();
        }


        public ActionResult Sitemap()
        {
            string host = Request.Url.Host;
            string port = Request.Url.Port.ToString();
            Response.ContentType = "text/xml";
            IEnumerable<Category> categoryList = dataManager.Categories.Categories.OrderBy(x => x.ShortName).Where(x => x.IsDeleted != true);
            IEnumerable<Product> productList = dataManager.Products.Products.OrderBy(x=>x.Category.ShortName).Where(x=>x.IsDeleted!=true);
            IEnumerable<Article> articleList = dataManager.ArticleRepository.Articles.OrderBy(x => x.ArticleDate);
            IEnumerable<NewsTape> newsList = dataManager.NewsTapeRepository.NewsTapes.OrderByDescending(x=>x.NewsDate);
            List<SitemapAttributes> nodes = new List<SitemapAttributes>();

            //url.Add();

            foreach (var product in productList)
            {
                nodes.Add(new SitemapAttributes
                {
                    Loc = "http://" + host + ":" + port + "/" + product.Category.ShortName + "/" + product.ShortName + "''" + "\n",
                    Lastmod = product.UpdateDate.ToString("yyyy-MM-dd"),
                    Changefreq = "weekly",
                    Priority = "0.8"
                });
            }

            foreach (var category in categoryList)
            {
                nodes.Add(new SitemapAttributes
                {
                    Loc = "http://" + host + ":" + port + "/" + category.ShortName + "\n",
                    Lastmod = category.UpdateDate.ToString("yyyy-MM-dd"),
                    Changefreq = "daily",
                    Priority = "0.8"
                });
            }

            
            
            
            
            //
            foreach (var article in articleList)
            {
                nodes.Add(new SitemapAttributes
                {
                    Loc = "http://" + host + ":" + port + "/Articles/" + article.ShortLink + "\n",
                    Lastmod = article.UpdateDate.ToString("yyyy-MM-dd"),
                    Changefreq = "weekly",
                    Priority = "0.8"
                });
            }
            //
            
            
            
            
            
            
            
            foreach (var news in newsList)
            {
                nodes.Add(new SitemapAttributes
                {
                    Loc = "http://" + host + ":" + port + "/" + "News/NewsArticle?newsId=" + news.NewsID + "\n",
                    Lastmod = news.UpdateDate.ToString("yyyy-MM-dd"),
                    Changefreq = "daily",
                    Priority = "0.7"
                });
            }

            nodes.Add(new SitemapAttributes
            {
                Loc = "http://" + host + ":" + port + "/" + "Contacts" + "\n",
                Lastmod = DateTime.Now.ToString("yyyy-MM-dd"),
                Changefreq = "monthly",
                Priority = "0.5"
            });

            nodes.Add(new SitemapAttributes
            {
                Loc = "http://" + host + ":" + port + "/" + "Delivery" + "\n",
                Lastmod = DateTime.Now.ToString("yyyy-MM-dd"),
                Changefreq = "monthly",
                Priority = "0.3"
            });

            nodes.Add(new SitemapAttributes
            {
                Loc = "http://" + host + ":" + port + "/" + "News" + "\n",
                Lastmod = DateTime.Now.ToString("yyyy-MM-dd"),
                Changefreq = "daily",
                Priority = "0.5"
            });

            XElement data = new XElement("urlset", nodes.Select(x => new XElement("url",
                                                                                  new XElement("loc", x.Loc),
                                                                                  new XElement("lastmod", x.Lastmod),
                                                                                  new XElement("changefreq", x.Changefreq),
                                                                                  new XElement("priority", x.Priority))));
            data.Add(new XAttribute("id", "1"));
            return Content(data.ToString(), "text/xml");
        }






        [OutputCache(Duration = 1000)]
        public ActionResult Delivery()
        {
            return View();
        }


        public ActionResult BannerPreLoad()
        {
            if (Request.IsAjaxRequest())
            {
              //  Thread.Sleep(5000);
                return PartialView();
            }
            return PartialView();
        }


        public ActionResult BannerPostLoad()
        {
            //Thread.Sleep(5000);
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return PartialView();
        }

        [HttpGet]
        public ActionResult FooterPartial()
        {
           //  Thread.Sleep(5000);
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return PartialView();
        }




    }


    public class SitemapAttributes
    {
        public string Loc { get; set; }
        public string Lastmod { get; set; }
        public string Changefreq { get; set; }
        public string Priority { get; set; }

    }
}

        
