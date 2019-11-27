using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Ninject;
using WebUI.Infrastructure.Concrete;
using WebUI.Infrastructure.ExtensionMethods;
using WebUI.Models;
using MvcContrib.UI.Grid;
using MvcContrib.Sorting;
using Calabonga.Xml.Exports;


namespace WebUI.Controllers
{
    //[Authorize(Users = "admin")]
      [Authorize(Roles = "admin, contentManager, Admin, ContentManager, SEO")]
    public class AdminController : Controller
    {
        

        private IProductRepository repositoryProduct;
        private ISuperCategoryRepository repositorySuperCategory;
        private ICategoryRepository repositoryCategory;
        private IUserRepository repositoryUser;
        private IOrderProcessor repositoryOrder;
        private IOrderSummaryRepository repositoryOrderSummary;
        private IOrderDetailsRepository repositoryOrderDetails;
        private IProductImageRepository repositoryProductImages;
        private IDimOrderStatusRepository repositoryDimOrderStatus;
        private IOrderStatusRepository repositoryOrderStatus;
        private IDimSettingTypeRepository repositoryDimSettingType;
        private IDimSettingRepository repositoryDimSetting;
        private IDimShippingRepository repositoryDimShipping;
        private INewsTapeRepository repositoryNewsTape;
        private IArticleRepository repositoryArticle;
        private INLogRepository repositoryNLog;
        private IMailingRepository repositoryMailing;


        //  private EFDbContext context;  
        //private DataManager dataManager;

        public int PageSize = 5;


        //-----------------------------------------------------
        public AdminController(IProductRepository repoProduct, ISuperCategoryRepository repoSuperCategory, ICategoryRepository repoCategory,
                               IUserRepository repoUser, IOrderProcessor repoOrder, /*DataManager dataManager*/
                               IOrderSummaryRepository repoOrderSummary, IOrderDetailsRepository repoOrderDetails,
                               IProductImageRepository repoProductImages, IDimOrderStatusRepository repoDimOrderStatus,
                               IOrderStatusRepository repoOrderStatus, IDimSettingTypeRepository repoDimSettingType,
                               IDimSettingRepository repoDimSetting, IDimShippingRepository repoDimShipping,
                               INewsTapeRepository repoNewsTape, IArticleRepository repoArticle, INLogRepository repoNLog, IOrderProcessor repoOrderProcessor,
                IMailingRepository repoMailing)
        {
            repositoryProduct = repoProduct;
            repositorySuperCategory = repoSuperCategory;
            repositoryCategory = repoCategory;
            repositoryUser = repoUser;
            repositoryOrder = repoOrder;
            repositoryOrderSummary = repoOrderSummary;
            repositoryOrderDetails = repoOrderDetails;
            repositoryProductImages = repoProductImages;
            repositoryDimOrderStatus = repoDimOrderStatus;
            repositoryOrderStatus = repoOrderStatus;
            repositoryDimSettingType = repoDimSettingType;
            repositoryDimSetting = repoDimSetting;
            repositoryNewsTape = repoNewsTape;
            repositoryArticle = repoArticle;
            repositoryNLog = repoNLog;
            repositoryOrder = repoOrderProcessor;
            repositoryDimShipping = repoDimShipping;
            repositoryMailing = repoMailing;
            //  this.dataManager = dataManager;

            
            /*
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_TO_ADDRESS", "Email адресата", Constants.MAIL_TO_ADDRESS);
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FROM_ADDRESS", "Email сервера отправки", Constants.MAIL_FROM_ADDRESS);
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_USE_SSL", "Использовать SSL", Constants.USE_SSL.ToString());
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_USER_NAME", "Логин почты", Constants.USERNAME);
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_PASSWORD", "Пароль", Constants.PASSWORD);
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_NAME", "Имя сервера", Constants.SERVERNAME);
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_WRITE_AS_FILE", "Записывать как файл", Constants.WRITE_AS_FILE.ToString());
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_PORT", "Порт", Constants.SERVER_PORT.ToString());



            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_TO_ADDRESS", "Email адресата", Constants.MAIL_TO_ADDRESS);
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FROM_ADDRESS", "Email сервера отправки", Constants.MAIL_FROM_ADDRESS);
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_USE_SSL", "Использовать SSL", Constants.USE_SSL.ToString());
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_USER_NAME", "Логин почты", Constants.USERNAME);
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_PASSWORD", "Пароль", Constants.PASSWORD);
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_NAME", "Имя сервера", Constants.SERVERNAME);
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_WRITE_AS_FILE", "Записывать как файл", Constants.WRITE_AS_FILE.ToString());
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_PORT", "Порт", Constants.SERVER_PORT.ToString());
            */


        }
        NLogLogger logger = new NLogLogger();

         // [OutputCache(Duration = 1000)]
          public void LetsTry()
          {
              IEnumerable<DimSetting> ds = repositoryDimSetting.DimSettings.ToList();
              IEnumerable<DimSettingType> dst = repositoryDimSettingType.DimSettingTypes.ToList();
              
              try
              {
                  IEnumerable<DimSetting> cont = ds.Where(x => x.SettingTypeID == "ADMIN_EMAIL_SETTINGS").ToList();

                  if (cont.Count()==0)
                  {
                      DimSettingType dst1 =
                          dst.FirstOrDefault(
                              x => x.SettingTypeID == "ADMIN_EMAIL_SETTINGS");

                      if (dst1==null)
                      {
                          DimSettingType dst2 = new DimSettingType()
                              {
                                  SettingTypeID = "ADMIN_EMAIL_SETTINGS",
                                  SettingTypeDesc = "Настройки рассылки"
                              };
                          repositoryDimSettingType.SaveDimSettingType(dst2, true);
                      }
                  }


                  try
                  {
                      EmailOrderProcessor.EmailSettings es = new EmailOrderProcessor.EmailSettings
                      {
                          MailFromAddress = cont.FirstOrDefault(x => x.SettingsID == "MAIL_FROM_ADDRESS").SettingsValue,
                          MailToAddress = cont.FirstOrDefault(x => x.SettingsID == "MAIL_TO_ADDRESS").SettingsValue,
                          UseSsl = Boolean.Parse(cont.FirstOrDefault(x => x.SettingsID == "MAIL_USE_SSL").SettingsValue),
                          UserName = cont.FirstOrDefault(x => x.SettingsID == "MAIL_SERVER_USER_NAME").SettingsValue,
                          Password = cont.FirstOrDefault(x => x.SettingsID == "MAIL_SERVER_PASSWORD").SettingsValue,
                          ServerName = cont.FirstOrDefault(x => x.SettingsID == "MAIL_SERVER_NAME").SettingsValue,
                          ServerPort =
                              Int32.Parse(cont.FirstOrDefault(x => x.SettingsID == "MAIL_SERVER_PORT").SettingsValue),
                          WriteAsFile =
                              Boolean.Parse(cont.FirstOrDefault(x => x.SettingsID == "MAIL_WRITE_AS_FILE").SettingsValue),
                          FileLocation = @"c:/sportstore"
                      };

                      if (String.IsNullOrEmpty(es.MailFromAddress))
                      {
                          TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FROM_ADDRESS",
                                                 "Email сервера отправки", Constants.MAIL_FROM_ADDRESS);
                      }
                      if (String.IsNullOrEmpty(es.MailToAddress))
                      {
                          TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_TO_ADDRESS",
                                                 "Email адресата", Constants.MAIL_TO_ADDRESS);
                      }
                      if ((es.UseSsl == null))
                      {
                          TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_USE_SSL",
                                                 "Использовать SSL", Constants.USE_SSL.ToString());
                      }
                      if (String.IsNullOrEmpty(es.UserName))
                      {
                          TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_USER_NAME",
                                                 "Логин почты", Constants.USERNAME);
                      }

                      if (String.IsNullOrEmpty(es.Password))
                      {
                          TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_PASSWORD",
                                                 "Пароль", Constants.PASSWORD);
                      }
                      if (String.IsNullOrEmpty(es.ServerName))
                      {
                          TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_NAME",
                                                 "Имя сервера", Constants.SERVERNAME);
                      }

                      if (es.WriteAsFile == null)
                      {
                          TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_WRITE_AS_FILE",
                                                 "Записывать как файл", Constants.WRITE_AS_FILE.ToString());
                      }

                      if (String.IsNullOrEmpty(es.FileLocation))
                      {
                          TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FILE_LOCATION",
                                                 "Местонахождение файла", Constants.FILE_LOCATION);
                      }

                      if (es.ServerPort == null)
                      {
                          TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_PORT", "Порт",
                                                 Constants.SERVER_PORT.ToString());
                      }
                  }
                  catch (Exception)
                  {
                   TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_TO_ADDRESS", "Email адресата", Constants.MAIL_TO_ADDRESS);
                  TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FROM_ADDRESS", "Email сервера отправки", Constants.MAIL_FROM_ADDRESS);
                  TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_USE_SSL", "Использовать SSL", Constants.USE_SSL.ToString());
                  TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_USER_NAME", "Логин почты", Constants.USERNAME);
                  TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_PASSWORD", "Пароль", Constants.PASSWORD);
                  TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_NAME", "Имя сервера", Constants.SERVERNAME);
                  TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_WRITE_AS_FILE", "Записывать как файл", Constants.WRITE_AS_FILE.ToString());
                  TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
                  TryToUpdateDimSettingsNow("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_PORT", "Порт", Constants.SERVER_PORT.ToString());
                  }
                  
                  
                  
              }
              catch (Exception)
              {
              /*    TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_TO_ADDRESS", "Email адресата", Constants.MAIL_TO_ADDRESS);
                  TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FROM_ADDRESS", "Email сервера отправки", Constants.MAIL_FROM_ADDRESS);
                  TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_USE_SSL", "Использовать SSL", Constants.USE_SSL.ToString());
                  TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_USER_NAME", "Логин почты", Constants.USERNAME);
                  TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_PASSWORD", "Пароль", Constants.PASSWORD);
                  TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_NAME", "Имя сервера", Constants.SERVERNAME);
                  TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_WRITE_AS_FILE", "Записывать как файл", Constants.WRITE_AS_FILE.ToString());
                  TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
                  //TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
                  TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_PORT", "Порт", Constants.SERVER_PORT.ToString());*/
              }


              //===============================================


              try
              {
                  IEnumerable<DimSetting> cont = ds.Where(x => x.SettingTypeID == "OPERATOR_EMAIL_SETTINGS");

                  if (cont.Count() == 0)
                  {
                      DimSettingType dst1 =
                          dst.FirstOrDefault(
                              x => x.SettingTypeID == "OPERATOR_EMAIL_SETTINGS");

                      if (dst1 == null)
                      {
                          DimSettingType dst2 = new DimSettingType()
                          {
                              SettingTypeID = "OPERATOR_EMAIL_SETTINGS",
                              SettingTypeDesc = "Настройки рассылки оператору"
                          };
                          repositoryDimSettingType.SaveDimSettingType(dst2, true);
                      }
                  }


                  try
                  {
                      EmailOrderProcessor.EmailSettings es = new EmailOrderProcessor.EmailSettings
                      {
                          MailFromAddress = cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_FROM_ADDRESS").SettingsValue,
                          MailToAddress = cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_TO_ADDRESS").SettingsValue,
                          UseSsl = Boolean.Parse(cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_USE_SSL").SettingsValue),
                          UserName = cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_SERVER_USER_NAME").SettingsValue,
                          Password = cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_SERVER_PASSWORD").SettingsValue,
                          ServerName = cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_SERVER_NAME").SettingsValue,
                          ServerPort =
                              Int32.Parse(cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_SERVER_PORT").SettingsValue),
                          WriteAsFile =
                              Boolean.Parse(cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_WRITE_AS_FILE").SettingsValue),
                          FileLocation = @"c:/sportstore"
                      };

                      if (String.IsNullOrEmpty(es.MailFromAddress))
                      {
                          TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FROM_ADDRESS",
                                                 "Email сервера отправки", Constants.MAIL_FROM_ADDRESS);
                      }
                      if (String.IsNullOrEmpty(es.MailToAddress))
                      {
                          TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_TO_ADDRESS",
                                                 "Email адресата", Constants.MAIL_TO_ADDRESS);
                      }
                      if ((es.UseSsl == null))
                      {
                          TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_USE_SSL",
                                                 "Использовать SSL", Constants.USE_SSL.ToString());
                      }
                      if (String.IsNullOrEmpty(es.UserName))
                      {
                          TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_SERVER_USER_NAME",
                                                 "Логин почты", Constants.USERNAME);
                      }

                      if (String.IsNullOrEmpty(es.Password))
                      {
                          TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_SERVER_PASSWORD",
                                                 "Пароль", Constants.PASSWORD);
                      }
                      if (String.IsNullOrEmpty(es.ServerName))
                      {
                          TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_SERVER_NAME",
                                                 "Имя сервера", Constants.SERVERNAME);
                      }

                      if (es.WriteAsFile == null)
                      {
                          TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_WRITE_AS_FILE",
                                                 "Записывать как файл", Constants.WRITE_AS_FILE.ToString());
                      }

                      if (String.IsNullOrEmpty(es.FileLocation))
                      {
                          TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_FILE_LOCATION",
                                                 "Местонахождение файла", Constants.FILE_LOCATION);
                      }

                      if (es.ServerPort == null)
                      {
                          TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_SERVER_PORT", "Порт",
                                                 Constants.SERVER_PORT.ToString());
                      }
                  }
                  catch (Exception)
                  {
                      TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_TO_ADDRESS", "Email адресата", Constants.MAIL_TO_ADDRESS);
                      TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_FROM_ADDRESS", "Email сервера отправки", Constants.MAIL_FROM_ADDRESS);
                      TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_USE_SSL", "Использовать SSL", Constants.USE_SSL.ToString());
                      TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_SERVER_USER_NAME", "Логин почты", Constants.USERNAME);
                      TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_SERVER_PASSWORD", "Пароль", Constants.PASSWORD);
                      TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_SERVER_NAME", "Имя сервера", Constants.SERVERNAME);
                      TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_WRITE_AS_FILE", "Записывать как файл", Constants.WRITE_AS_FILE.ToString());
                      TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
                      TryToUpdateDimSettingsNow("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки", "OPERATOR_MAIL_SERVER_PORT", "Порт", Constants.SERVER_PORT.ToString());
                  }
              }
              catch (Exception)
              {
              }
                 
              /*
              try
              {
                  IEnumerable<DimSetting> cont = ds.Where(x => x.SettingTypeID == "OPERATOR_EMAIL_SETTINGS");
                     EmailOrderProcessor.EmailSettings es = new EmailOrderProcessor.EmailSettings()
                  {
                      MailFromAddress = cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_FROM_ADDRESS").SettingsValue,
                      UseSsl = Boolean.Parse(cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_USE_SSL").SettingsValue),
                      UserName = cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_SERVER_USER_NAME").SettingsValue,
                      Password = cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_SERVER_PASSWORD").SettingsValue,
                      ServerName = cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_SERVER_NAME").SettingsValue,
                      ServerPort = Int32.Parse(cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_SERVER_PORT").SettingsValue),
                      WriteAsFile = Boolean.Parse(cont.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_WRITE_AS_FILE").SettingsValue),
                      FileLocation = @"c:/sportstore"
                  };

                     if (String.IsNullOrEmpty(es.MailFromAddress))
                     {
                         TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FROM_ADDRESS",
                                                "Email сервера отправки", Constants.MAIL_FROM_ADDRESS);
                     }
                     if (String.IsNullOrEmpty(es.MailToAddress))
                     {
                         TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_TO_ADDRESS",
                                                "Email адресата", Constants.MAIL_TO_ADDRESS);
                     }
                     if ((es.UseSsl == null))
                     {
                         TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_USE_SSL",
                                                "Использовать SSL", Constants.USE_SSL.ToString());
                     }
                     if (String.IsNullOrEmpty(es.UserName))
                     {
                         TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_USER_NAME",
                                                "Логин почты", Constants.USERNAME);
                     }

                     if (String.IsNullOrEmpty(es.Password))
                     {
                         TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_PASSWORD",
                                                "Пароль", Constants.PASSWORD);
                     }
                     if (String.IsNullOrEmpty(es.ServerName))
                     {
                         TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_NAME",
                                                "Имя сервера", Constants.SERVERNAME);
                     }

                     if (es.WriteAsFile == null)
                     {
                         TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_WRITE_AS_FILE",
                                                "Записывать как файл", Constants.WRITE_AS_FILE.ToString());
                     }

                     if (String.IsNullOrEmpty(es.FileLocation))
                     {
                         TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FILE_LOCATION",
                                                "Местонахождение файла", Constants.FILE_LOCATION);
                     }

                     if (es.ServerPort == null)
                     {
                         TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_PORT", "Порт",
                                                Constants.SERVER_PORT.ToString());
                     }

              }
              catch (Exception)
              {
                   TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_TO_ADDRESS", "Email адресата", Constants.MAIL_TO_ADDRESS);
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FROM_ADDRESS", "Email сервера отправки", Constants.MAIL_FROM_ADDRESS);
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_USE_SSL", "Использовать SSL", Constants.USE_SSL.ToString());
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_USER_NAME", "Логин почты", Constants.USERNAME);
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_PASSWORD", "Пароль", Constants.PASSWORD);
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_NAME", "Имя сервера", Constants.SERVERNAME);
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_WRITE_AS_FILE", "Записывать как файл", Constants.WRITE_AS_FILE.ToString());
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_PORT", "Порт", Constants.SERVER_PORT.ToString());
              }
              /*TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_TO_ADDRESS", "Email адресата", Constants.MAIL_TO_ADDRESS);
              TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FROM_ADDRESS", "Email сервера отправки", Constants.MAIL_FROM_ADDRESS);
              TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_USE_SSL", "Использовать SSL", Constants.USE_SSL.ToString());
              TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_USER_NAME", "Логин почты", Constants.USERNAME);
              TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_PASSWORD", "Пароль", Constants.PASSWORD);
              TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_NAME", "Имя сервера", Constants.SERVERNAME);
              TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_WRITE_AS_FILE", "Записывать как файл", Constants.WRITE_AS_FILE.ToString());
              TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
              TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
              TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_PORT", "Порт", Constants.SERVER_PORT.ToString());



              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_TO_ADDRESS", "Email адресата", Constants.MAIL_TO_ADDRESS);
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FROM_ADDRESS", "Email сервера отправки", Constants.MAIL_FROM_ADDRESS);
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_USE_SSL", "Использовать SSL", Constants.USE_SSL.ToString());
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_USER_NAME", "Логин почты", Constants.USERNAME);
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_PASSWORD", "Пароль", Constants.PASSWORD);
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_NAME", "Имя сервера", Constants.SERVERNAME);
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_WRITE_AS_FILE", "Записывать как файл", Constants.WRITE_AS_FILE.ToString());
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
              TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_PORT", "Порт", Constants.SERVER_PORT.ToString());*/
              
          }

          #region Product       
        
         public ActionResult Products(string searchWord, GridSortOptions gridSortOptions, int? categoryId, int? page, string quantity = "-- Все --")
        {
            //   var tmp = repositoryProduct.Products;
            int pageItemsCount;

            TryToUpdateDimSettings("ADMIN_PAGE_SIZE",
                                   "Количество элементов на странице в админке",
                                   "ADMIN_PAGE_SIZE_Product",
                                   "Количество элементов на странице в административном разделе Товар");


            try
            {
                pageItemsCount =
                    Int32.Parse(
                        repositoryDimSetting.DimSettings.Where(x => x.SettingsID == "ADMIN_PAGE_SIZE_Product")
                                            .Select(x => x.SettingsValue).Single());
            }
            catch (Exception)
            {
                logger.Warn("Отсутствует значение параметра ADMIN_PAGE_SIZE_Product в БД");
                pageItemsCount = 0;
            }



            var query = from a in repositoryProduct.Products
                        select new ProductEditViewModel
                            {
                                SelectedCategoryID = a.CategoryID,
                                ProductID = a.ProductID,
                                CategoryName = a.Category.Name,
                                Name = a.Name,
                                Price = a.Price,
                                ArticleNumber = a.ArticleNumber,
                                Quantity = a.Quantity,
                                ShortName = a.ShortName,
                                Description = a.Description,
                                Sequence = a.Sequence,
                                IsInStock = false,
                                IsActive = a.IsActive,
                                IsDeleted = a.IsDeleted,
                                StartDate = a.StartDate,
                                UpdateDate = a.UpdateDate,
                                LastPriceChangeDate = a.LastPriceChangeDate,
                                Keywords = a.Keywords,
                                Snippet = a.Snippet,
                                OldPrice = a.OldPrice
                            };

            var pagedViewModel = new PagedViewModel<ProductEditViewModel>
                {
                    ViewData = ViewData,
                    Query = query, //repositoryProduct.Products, //repositoryProduct.Products,
                    GridSortOptions = gridSortOptions,
                    DefaultSortColumn = "Name",
                    Page = page,
                    PageSize = (pageItemsCount == 0) ? Domain.Constants.ADMIN_PAGE_SIZE : pageItemsCount,
                };
            int s = 0;
            if (quantity == "В асортименте")
            {
                pagedViewModel.Query = pagedViewModel.Query.Where(x => x.Quantity > 20);
            }
            else if (quantity == "Мало")
            {
                pagedViewModel.Query = pagedViewModel.Query.Where(x => x.Quantity > 0 & x.Quantity <= 20);
            }
            else if (quantity == "Нет")
            {
                pagedViewModel.Query = pagedViewModel.Query.Where(x => x.Quantity == 0);
            }

            pagedViewModel
                .AddFilter("searchWord", searchWord,
                           a =>
                           a.Name.Contains(searchWord) 
                           || a.ShortName.Contains(searchWord)
                           || a.CategoryName.Contains(searchWord)
                           || a.ArticleNumber.Contains(searchWord) 
                           || a.Description.Contains(searchWord))
                .AddFilter("categoryId", categoryId, a => a.SelectedCategoryID == categoryId,
                           repositoryCategory.Categories.OrderBy(x=>x.Name), "Name")
                .Setup();

            if (Request.IsAjaxRequest())
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                return PartialView("ProductGridPartial", pagedViewModel);
            }

            return View(pagedViewModel);
            //return View(repositoryProduct.Products);

        }



          
          public ViewResult EditProduct(int productId)
        {
            IEnumerable<Category> categoryList = repositoryCategory.Categories.Where(x=>x.IsDeleted==false).OrderBy(x=>x.Name);
            IEnumerable<ProductImage> productImagesList = repositoryProductImages.ProductImages;
            Product product = repositoryProduct.Products.FirstOrDefault(p => p.ProductID == productId);

            
            ProductEditViewModel viewModel = new ProductEditViewModel()
                {
                    ProductID = product.ProductID,
                    Name = product.Name,
                    SelectedCategoryID = product.CategoryID,
                    Price = product.Price,
                    ArticleNumber = product.ArticleNumber,
                    Quantity = product.Quantity,
                    Description = product.Description,
                    Categories = categoryList,
                    ShortName = product.ShortName,
                    ProductImages = productImagesList,
                    StartDate = product.StartDate,
                    UpdateDate = product.UpdateDate,
                    Sequence = product.Sequence,
                    IsActive = product.IsActive,
                    IsDeleted = product.IsDeleted,
                    OldPrice = product.OldPrice,
                    LastPriceChangeDate = product.LastPriceChangeDate,
                    Keywords = product.Keywords,
                    Snippet = product.Snippet,
                };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProduct(ProductEditViewModel productViewModel)
        {
            int selectedCategory = productViewModel.SelectedCategoryID;

          /*  Product product = new Product()
                {
                    ProductID = productViewModel.ProductID,
                    Name = productViewModel.Name,
                    Price = productViewModel.Price,
                    Quantity = productViewModel.Quantity,
                    Description = productViewModel.Description,
                    ShortName = productViewModel.ShortName,
                    StartDate = productViewModel.StartDate,
                    UpdateDate = productViewModel.UpdateDate,
                    Sequence = productViewModel.Sequence,
                    IsActive = productViewModel.IsActive,
                    IsDeleted = productViewModel.IsDeleted,
                    CategoryID = productViewModel.SelectedCategoryID
                };*/

            
            //int tmp3 = repositoryProduct.Products.Count();
            

            if (ModelState.IsValid)
            {
                productViewModel.UpdateDate = DateTime.Now;
                //productViewModel.CategoryName = productViewModel.CategoryName.Trim();
                
                if (productViewModel.ProductID == 0)
                {
                    productViewModel.StartDate = DateTime.Now;
                    repositoryProduct.SaveProduct(productViewModel.ToDomainProduct());
                    repositoryProduct.RefreshProductShortName(productViewModel.ToDomainProduct());
                    
                    TempData["message"] = string.Format("{0} сохранен", productViewModel.Name);
                    TempData["messageType"] = "confirmation-msg";

                    logger.Warn(User.Identity.Name + ". Товар " + productViewModel.Name + "  сохранен " );
                    return RedirectToAction("EditProduct", "Admin",new {productId = repositoryProduct.Products.Max(x => x.ProductID)});
                }
                else
                {
                    ///проверка, если у товара изменилась категория, то пересчитать величину Sequence для обеих категорий
                 /*   int oldProdCategoryId =
                    repositoryProduct.Products.FirstOrDefault(x => x.ProductID == productViewModel.ProductID).CategoryID;*/


                    Product product = repositoryProduct.Products.FirstOrDefault(x => x.ProductID == productViewModel.ProductID);
                    Product productOriginal = repositoryProduct.GetProductOrigin(product);

                    productViewModel.Sequence = 10000;


                    if (productViewModel.Price!=productOriginal.Price)
                    {
                        productViewModel.OldPrice = productOriginal.Price;
                        productViewModel.LastPriceChangeDate = DateTime.Now;
                    }

                    repositoryProduct.SaveProduct(productViewModel.ToDomainProduct());

                    if (productOriginal.CategoryID != productViewModel.SelectedCategoryID)
                     {
                         int[] categoryIdArray = new int[2];
                         categoryIdArray[0] = productOriginal.CategoryID;
                         categoryIdArray[1] = selectedCategory;
                         repositoryProduct.RefreshEveryProductSequence(categoryIdArray);
                     }

                    if (productOriginal.IsActive!=productViewModel.IsActive)
                    {
                        int[] categoryIdArray = new int[1];
                        categoryIdArray[0] = productViewModel.SelectedCategoryID;
                        repositoryProduct.RefreshEveryProductSequence(categoryIdArray);
                        //RefreshEveryProductSequence
                        //repositoryProduct.SetActiveStatus((product.IsActive==true) ? true : false, product);
                    }

                    logger.Warn(User.Identity.Name + ". Товар " + productViewModel.Name + "  изменен ");
                    return RedirectToAction("EditProduct", "Admin", new {productId = productViewModel.ProductID});
                }
            }
            else
            {
                IEnumerable<Category> categoryList = repositoryCategory.Categories;
                productViewModel.Categories = categoryList;
                // there is something wrong with the data values
                return View(productViewModel);
            }
        }

          [HttpGet]
          public ActionResult RefreshProductShortLink(int productId)
          {
              Product product = repositoryProduct.Products.FirstOrDefault(x => x.ProductID == productId);
              product.UpdateDate = DateTime.Now;
              repositoryProduct.RefreshProductShortName(product);
              return RedirectToAction("EditProduct","Admin", new {productId});
          }

          //public void RefreshProductShortName(Product product)

          public ActionResult ProductSequenceUpdater()
          {
              int[] categoryIdArray = repositoryCategory.Categories.Select(x => x.CategoryID).ToArray();
              repositoryProduct.RefreshEveryProductSequence(categoryIdArray);
              return RedirectToAction("Actions");
          }

          [Authorize(Roles = "Admin, ContentManager")]
          public ViewResult CreateProduct()
        {
            IEnumerable<Category> categoryList = repositoryCategory.Categories;
            return View("EditProduct", new ProductEditViewModel() {Categories = categoryList});
        }

          /*public ActionResult DeleteProductPreView(int productId)
          {
              return View(productId);
          }*/



          [HttpPost]
          [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult DeleteProduct(int? productId, int[] resubmit)
        {
              DeleteProductRelationships(productId, resubmit);
              if (productId == null)
              {
                  //IEnumerable<Product> productList = repositoryProduct.Products.Where(x=>x.ProductID==)
                  foreach (var p in resubmit)
                  {
                      IEnumerable<ProductImage> productImages = repositoryProductImages.ProductImages.Where(x => x.ProductID == p);
                      if (productImages != null)
                      {
                          foreach (var productImage in productImages)
                          {
                              productImage.Product = null;

                              string strSaveFileName = productId.ToString() + "_" + productImage.ProductImageID.ToString() + productImage.ImageExt;
                              //repositoryProductImages.DeleteProductImage(productImage);
                            //  TempData["Message"] = string.Format("Изображение {0}_{1}.{2} было удалено", productImage.ProductID, productImage.ProductImageID, productImage.ImageExt);
                            //  TempData["messageType"] = "warning-msg";
                              string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                              Constants.PRODUCT_IMAGE_FOLDER, strSaveFileName);
                              string strSavePreviewFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                                     Constants.PRODUCT_IMAGE_FOLDER,
                                                                                     Constants.PRODUCT_IMAGE_PREVIEW_FOLDER,
                                                                                     strSaveFileName);
                              if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);
                              if (System.IO.File.Exists(strSavePreviewFullPath)) System.IO.File.Delete(strSavePreviewFullPath);
                          }
                      }
                      repositoryProductImages.DeleteProductImageBulk(productImages);

                      Product prod = repositoryProduct.Products.FirstOrDefault(x => x.ProductID == p);

                      if (prod != null)
                      {
                          try
                          {
                              repositoryProduct.DeleteProduct(prod);
                              logger.Warn(User.Identity.Name + ". Товар " + prod.Name + " удален.");
                          }
                          catch (Exception)
                          {
                              repositoryProduct.SetDeletedStatus(true, prod);
                              repositoryProduct.SetActiveStatus(false, prod);
                             //Тут будет код о пометке deleted
                              //repositoryProduct.DeleteProduct(prod);
                             logger.Warn(User.Identity.Name + ". Товар " + prod.Name + " был помечен как удаленный");
                          }
                          
                      }
                      
                  }
                  TempData["message"] = string.Format("Товары были удалены или помечены как удаленные");
                  TempData["messageType"] = "warning-msg";
                  return RedirectToAction("Products");
              }
              else
              {
                      IEnumerable<ProductImage> productImages = repositoryProductImages.ProductImages.Where(x => x.ProductID == productId);
                    if (productImages != null)
                    {
                        foreach (var productImage in productImages)
                        {
                            productImage.Product = null;
                    
                            string strSaveFileName = productId.ToString() + "_" + productImage.ProductImageID.ToString() + productImage.ImageExt;
                            //repositoryProductImages.DeleteProductImage(productImage);
                            TempData["Message"] = string.Format("Изображение {0}_{1}.{2} было удалено", productImage.ProductID, productImage.ProductImageID, productImage.ImageExt);
                            TempData["messageType"] = "warning-msg";
                            string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                            Constants.PRODUCT_IMAGE_FOLDER, strSaveFileName);
                            string strSavePreviewFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                                   Constants.PRODUCT_IMAGE_FOLDER,
                                                                                   Constants.PRODUCT_IMAGE_PREVIEW_FOLDER,
                                                                                   strSaveFileName);
                            if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);
                            if (System.IO.File.Exists(strSavePreviewFullPath)) System.IO.File.Delete(strSavePreviewFullPath);
                        }
                    }
                    repositoryProductImages.DeleteProductImageBulk(productImages);
                    Product prod = repositoryProduct.Products.FirstOrDefault(p => p.ProductID == productId);

                    if (prod != null)
                    {
                        try
                        {
                            repositoryProduct.DeleteProduct(prod);
                            logger.Warn(User.Identity.Name + ". Товар " + prod.Name + " удален.");
                            TempData["message"] = string.Format("Товар  " + prod.Name + " был удален");
                            TempData["messageType"] = "warning-msg";
                        }
                        catch (Exception)
                        {
                            repositoryProduct.SetDeletedStatus(true, prod);
                            repositoryProduct.SetActiveStatus(false, prod);
                            //Тут будет код о пометке deleted
                            //repositoryProduct.DeleteProduct(prod);
                            logger.Warn(User.Identity.Name + ". Товар " + prod.Name + " был помечен как удаленный");
                            TempData["message"] = string.Format("Товар  " + prod.Name + " был помечен как удаленный");
                            TempData["messageType"] = "warning-msg";
                        }
                    }
                    return RedirectToAction("Products");
              }
           return RedirectToAction("Products");
        }


        public ActionResult RefreshAllShortNamesInProducts()
        {
            repositoryProduct.RefreshAllShortNames();
            logger.Warn(User.Identity.Name + ". FriendlyUrl товаров обновлен ");
            return RedirectToAction("Products");
        }

        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult ProductSequence(int productId, string actionType)
        {
            Product product = repositoryProduct.Products.FirstOrDefault(x => x.ProductID == productId);

            try
            {
                Exception ex = new Exception();
                int[] sequence =
                    repositoryProduct.Products.Where(x => x.CategoryID == product.CategoryID)
                                     .Select(x => x.Sequence)
                                     .ToArray();
                Array.Sort(sequence);
                //sequence.OrderBy(x => x.ToString());

                for (int i = 0; i < sequence.Count(); i++)
                {
                    if (sequence[i] == i + 1)
                    {

                    }
                    else
                    {
                        logger.Error(User.Identity.Name + ". Ошибка при переборе последовательности товаров " + ex.Message);
                        throw (ex);
                    }
                }

                repositoryProduct.ProductSequence(productId, actionType);
            }

            catch (Exception ex)
            {
                TempData["message"] = string.Format("Нарушена последовательность! Список был пересчитан!");
                TempData["messageType"] = "error-msg"; 
                repositoryProduct.UpdateProductSequence(product.CategoryID, false);
                repositoryProduct.ProductSequence(productId, actionType);
                logger.Error(User.Identity.Name + ". Нарушена последовательность! Приоритетность товаров пересчитана!" + ex.Message);
            }

            //---Определение пустых категорий 
            /*    IEnumerable<string> productCategories = from c in repositoryCategory.Categories.ToList()
                                                    join p in repositoryProduct.Products.ToList() on c.CategoryID
                                                        equals
                                                        p.CategoryID
                                                    group c by new { c.CategoryID, c.ShortName }
                                                        into tmp
                                                        select tmp.Key.ShortName;

            IEnumerable<string> categoriesExists = from c in repositoryCategory.Categories.ToList()
                                                   select c.ShortName;


            IQueryable<string> difference = categoriesExists.Except(productCategories).AsQueryable();



            var z = from j in repositoryCategory.Categories.ToList()
                    where j.ShortName == difference.FirstOrDefault(x => x.ToString() == j.ShortName)
                    select j.CategoryID;

            ViewBag.EmptyCategories = z.ToArray();  */
            //--------------------------


            GridSortOptions gridSortOptions = new GridSortOptions()
                {
                    Column = "Sequence",
                    Direction = SortDirection.Ascending
                };


            var pagedViewModel = new PagedViewModel<Product>
                {
                    ViewData = ViewData,
                    Query = repositoryProduct.Products.Where(x => x.CategoryID == product.CategoryID),
                    //_service.GetAlbumsView(),
                    GridSortOptions = gridSortOptions,
                    DefaultSortColumn = "Sequence",
                    Page = 1,
                    PageSize = 100 //Domain.Constants.ADMIN_PAGE_SIZE
                }
                .Setup();

            if (Request.IsAjaxRequest())
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                return PartialView("ProductSequenceGridPartial", pagedViewModel);    
            }

            return PartialView("ProductSequenceGridPartial", pagedViewModel);

        }



        public ActionResult ProductSequenceView(int categoryId)
        {
            //IQueryable<Category> categoryType = repositoryCategory.Categories;

            GridSortOptions gridSortOptions = new GridSortOptions()
                {
                    Direction = SortDirection.Ascending,
                    Column = "Sequence"
                };

            var pagedViewModel = new PagedViewModel<Product>
                {
                    ViewData = ViewData,
                    Query = repositoryProduct.Products.Where(x => x.CategoryID == categoryId),
                    GridSortOptions = gridSortOptions,
                    DefaultSortColumn = "Sequence",
                    Page = 1,
                    PageSize = 100,
                }
                .Setup();

            return View("ProductSequence", pagedViewModel);
        }
        
          #endregion


        #region SuperCategory

          public ActionResult SuperCategories(string searchWord, GridSortOptions gridSortOptions, int? page,
                                         string categoryActivity = "Активные")
          {
              //  public ActionResult UsersView(string searchWord, int? artistId, GridSortOptions gridSortOptions, int? page, string userActivity = "Активные")

              //var categoryListAsync = repositoryCategory.GetCategoryListAsync();
              var categoryListAsync = repositoryCategory.Categories;
              IEnumerable<SuperCategory> superCategories = repositorySuperCategory.SuperCategories.ToList();

              
              

              if (superCategories.Any(x => x.ShortName == null))
              {
                  repositorySuperCategory.RefreshAllShortNames();
              }
              int pageItemsCount;

              TryToUpdateDimSettings("ADMIN_PAGE_SIZE",
                                     "Количество элементов на странице в админке",
                                     "ADMIN_PAGE_SIZE_SuperCategory",
                                     "Количество элементов на странице в административном разделе Надкатегории");

              try
              {
                  pageItemsCount =
                      Int32.Parse(
                          repositoryDimSetting.DimSettings.Where(x => x.SettingsID == "ADMIN_PAGE_SIZE_SuperCategory")
                                              .Select(x => x.SettingsValue).Single());
              }
              catch (Exception ex)
              {
                  pageItemsCount = 0;
                  logger.Warn(User.Identity.Name + ". Отсутствует значение параметра ADMIN_PAGE_SIZE_SuperCategory в БД" +
                              ex.Message);
              }

              IQueryable<SuperCategory> superCategoryType = repositorySuperCategory.SuperCategories;
              //categoryListAsync.Wait();
              IEnumerable<Category> categories = categoryListAsync;//.Result;
              if (categoryActivity == "Пустые")
              {
                  IEnumerable<string> categorySuperCategories = from c in superCategories
                                                          join p in categories on c.SuperCategoryID
                                                              equals
                                                              p.SuperCategoryID
                                                          group c by new {c.SuperCategoryID, c.ShortName}
                                                          into tmp
                                                          select tmp.Key.ShortName;

                  IEnumerable<string> categoriesExists = from c in superCategories
                                                         select c.ShortName;


                  IEnumerable<string> difference = categoriesExists.Except(categorySuperCategories);



                  IEnumerable<SuperCategory> d = from j in superCategories
                                            where
                                                j.ShortName ==
                                                difference.FirstOrDefault(x => x.ToString() == j.ShortName)
                                            select new SuperCategory()
                                                {
                                                    SuperCategoryID = j.SuperCategoryID,
                                                    Name = j.Name,
                                                    ShortName = j.ShortName,
                                                    ImageExt = j.ImageExt //,
                                                    // Sequence = j.Sequence
                                                };
                  superCategoryType = d.AsQueryable();
              }
              else if (categoryActivity == "Активные")
              {
                  IEnumerable<string> categorySuperCategories = from c in superCategories
                                                          join p in categories on c.SuperCategoryID
                                                              equals
                                                              p.SuperCategoryID
                                                          group c by new {c.SuperCategoryID, c.ShortName}
                                                          into tmp
                                                          select tmp.Key.ShortName;

                  IEnumerable<SuperCategory> d = from j in superCategories
                                            where
                                                j.ShortName ==
                                                categorySuperCategories.FirstOrDefault(x => x.ToString() == j.ShortName)
                                            select new SuperCategory()
                                                {
                                                    SuperCategoryID = j.SuperCategoryID,
                                                    Name = j.Name,
                                                    ShortName = j.ShortName,
                                                    ImageExt = j.ImageExt
                                                };
                  superCategoryType = d.AsQueryable();
              }
              else
              {

              }

            
              var pagedViewModel = new PagedViewModel<SuperCategory>
                  {
                      ViewData = ViewData,
                      Query = superCategoryType.AsQueryable(),
                      //repositoryCategory.Categories.Except(categoryType), //categoryType, //repositoryCategory.Categories.Where(x=>x.CategoryID==),  //Except(categoryType), //categoryType,//repositoryCategory.Categories, //_service.GetAlbumsView(),
                      GridSortOptions = gridSortOptions,
                      DefaultSortColumn = "Name",
                      Page = page,
                      PageSize = (pageItemsCount == 0) ? Domain.Constants.ADMIN_PAGE_SIZE : pageItemsCount,
                  }
                  .AddFilter("searchWord", searchWord,
                             a => a.Name.Contains(searchWord) || a.ShortName.Contains(searchWord))
                  .Setup();


              if (Request.IsAjaxRequest())
              {
                  Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                  return PartialView("SuperCategoryGridPartial", pagedViewModel);
              }

              return View(pagedViewModel);

          }

          [Authorize(Roles = "Admin, ContentManager")]
          public ViewResult CreateSuperCategory()
          {
              return View("EditSuperCategory", new SuperCategory());
          }



          public ViewResult EditSuperCategory(int superCategoryId)
          {

              SuperCategory superCategory = repositorySuperCategory.SuperCategories
                                                    .FirstOrDefault(p => p.SuperCategoryID == superCategoryId);
              return View(superCategory);

          }


          [HttpPost]
          public ActionResult EditSuperCategory(SuperCategory superCategory)
          {
              if ((ModelState.IsValid) &&
                  (repositorySuperCategory.SuperCategories.FirstOrDefault(x => x.Name.TrimEnd() == superCategory.Name.TrimEnd()) == null))
              {
                  /*         if (image != null)
                                   {
                                       product.ImageMimeType = image.ContentType;
                                       product.ImageData = new byte[image.ContentLength];
                                       image.InputStream.Read(product.ImageData, 0, image.ContentLength);
         
                                   }
                                   // save the product
                           */
                  repositorySuperCategory.SaveSuperCategory(superCategory);
                  repositorySuperCategory.RefreshAllShortNames();
                  // add a message to the viewbag
                  
                  if (superCategory.SuperCategoryID == 0)
                  {
                      TempData["message"] = string.Format("Суперкатегория {0} создана", superCategory.Name);
                      TempData["messageType"] = "confirmation-msg"; 
                      logger.Warn(User.Identity.Name + ".  Суперкатегория " + superCategory.Name + " создана  ");
                  }
                  else
                  {
                      TempData["message"] = string.Format("Суперкатегория {0} изменена", superCategory.Name);
                      logger.Warn(User.Identity.Name + ".  Надкатегория " + superCategory.Name + " изменена ");
                      TempData["messageType"] = "warning-msg";
                  }


                  // return the user to the list
                  //return RedirectToAction("Categories");
                  return RedirectToAction("EditSuperCategory", new { superCategoryId = superCategory.SuperCategoryID });
              }
              else
              {
                  // there is something wrong with the data values
                  TempData["message"] = string.Format("Суперкатегория '{0}' уже существует в базе! Изменения не внесены", superCategory.Name);
                  TempData["messageType"] = "warning-msg";
                  return View(superCategory);
              }
          }

          [HttpPost]
          [Authorize(Roles = "Admin, ContentManager")]
          public ActionResult DeleteSuperCategory(int superCategoryId)
          {
              SuperCategory superCategory = repositorySuperCategory.SuperCategories.FirstOrDefault(p => p.SuperCategoryID == superCategoryId);

              if (superCategory != null)
              {
                  repositorySuperCategory.DeleteSuperCategory(superCategory);
                  TempData["message"] = string.Format("Суперкатегория {0} удалена", superCategory.Name);
                  TempData["messageType"] = "warning-msg"; 
                  logger.Warn(User.Identity.Name + ".  Категория " + superCategory.Name + " удалена ");
              }
              return RedirectToAction("SuperCategories");
          }



          public ActionResult UpdateSuperCategorySequence()
          {
              repositorySuperCategory.UpdateSuperCategorySequence();

              return RedirectToAction("SuperCategories");
          }

          public ActionResult SuperCategorySequence(int superCategoryId, string actionType)
          {
              try
              {
                  Exception ex = new Exception();
                  int[] sequence =
                      repositorySuperCategory.SuperCategories.Select(x => x.Sequence).ToArray();
                  Array.Sort(sequence);
                  //sequence.OrderBy(x => x.ToString());

                  for (int i = 0; i < sequence.Count(); i++)
                  {
                      if (sequence[i] == i + 1)
                      {

                      }
                      else
                      {
                          logger.Error(User.Identity.Name + ". Проблема обновления списка в суперкатегории" + ex.Message);
                          throw (ex);
                      }
                  }

                  repositorySuperCategory.SuperCategorySequence(superCategoryId, actionType);
              }

              catch (Exception ex)
              {
                  TempData["message"] = string.Format("Нарушена последовательность! Список был пересчитан!");
                  TempData["messageType"] = "error-msg"; 
                  logger.Error(User.Identity.Name + ". Нарушена последовательность в суперкатегориях! Список был пересчитан!" + ex.Message);
                  repositorySuperCategory.UpdateSuperCategorySequence(); 
                  repositorySuperCategory.SuperCategorySequence(superCategoryId, actionType);
              }

              GridSortOptions gridSortOptions = new GridSortOptions()
              {
                  Column = "Sequence",
                  Direction = SortDirection.Ascending
              };


              var pagedViewModel = new PagedViewModel<SuperCategory>()
              {
                  ViewData = ViewData,
                  Query = repositorySuperCategory.SuperCategories, //_service.GetAlbumsView(),
                  GridSortOptions = gridSortOptions,
                  DefaultSortColumn = "Sequence",
                  Page = 1,
                  PageSize = 100 //Domain.Constants.ADMIN_PAGE_SIZE
              }
                  .Setup();

              return PartialView("SuperCategoryGridPartial", pagedViewModel);

          }
          [Authorize(Roles = "Admin, ContentManager")]
          public ActionResult UploadSuperCategoryImage(HttpPostedFileBase imagefile, int superCategoryId)
          {
              // Получаем объект, для которого загружаем картинку
              if (imagefile == null)
              {
                  return RedirectToAction("EditSuperCategory", new { superCategoryId });
              }

              SuperCategory obj = repositorySuperCategory.SuperCategories.FirstOrDefault(x => x.SuperCategoryID == superCategoryId);
              // service.Get(objID);
              if (obj == null) return Content("NotFound"); //View("NotFound");

              try
              {
                  if (imagefile != null)
                  {
                      // Определяем название конечного графического файла вместе с полным путём.
                      // Название файла должно быть такое же, как ID объекта. Это гарантирует уникальность названия.
                      // Расширение должно быть такое же, как расширение у исходного графического файла.
                      string strExtension = System.IO.Path.GetExtension(imagefile.FileName);
                      string strSaveFileName = superCategoryId + strExtension;
                      string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                      Constants.SUPER_CATEGORY_MINI_IMAGES_FOLDER,
                                                                      strSaveFileName);

                      // Если файл с таким названием имеется, удаляем его.
                      if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);

                      // Сохраняем картинку, изменив её размеры.
                      imagefile.ResizeAndSave(Constants.SUPER_CATEGORY_MINI_IMAGE_HEIGHT, Constants.SUPER_CATEGORY_MINI_IMAGE_WIDTH,
                                              strSaveFullPath);

                      // Расширение файла записываем в базу данных в поле ImageExt.
                      obj.ImageExt = strExtension;

                      repositorySuperCategory.SaveSuperCategory(obj);
                      //service.Save();
                  }
              }
              catch (Exception ex)
              {
                  string strErrorMessage = ex.Message;
                  if (ex.InnerException != null)
                      strErrorMessage = string.Format("{0} --- {1}", strErrorMessage, ex.InnerException.Message);
                  logger.Error(User.Identity.Name + ". Ошибка при сохранении изображения " + ex.Message);
                  ViewBag.ErrorMessage = strErrorMessage;
                  return View("Error");
              }
              return RedirectToAction("EditSuperCategory", "Admin", new { superCategoryId });
          }





          #endregion


        #region Category

        //==========================================

        //-----------------------------------------------------
        public ViewResult Actions()
        {
            LetsTry();
            TempData["rejectProduct"] = TempData["rejectProduct"];
            /*TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_TO_ADDRESS", "Email адресата", Constants.MAIL_TO_ADDRESS);
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FROM_ADDRESS", "Email сервера отправки", Constants.MAIL_FROM_ADDRESS);
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_USE_SSL", "Использовать SSL", Constants.USE_SSL.ToString());
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_USER_NAME", "Логин почты", Constants.USERNAME);
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_PASSWORD", "Пароль", Constants.PASSWORD);
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_NAME", "Имя сервера", Constants.SERVERNAME);
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_WRITE_AS_FILE", "Записывать как файл", Constants.WRITE_AS_FILE.ToString());
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
            TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_PORT", "Порт", Constants.SERVER_PORT.ToString());



            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_TO_ADDRESS", "Email адресата", Constants.MAIL_TO_ADDRESS);
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FROM_ADDRESS", "Email сервера отправки", Constants.MAIL_FROM_ADDRESS);
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_USE_SSL", "Использовать SSL", Constants.USE_SSL.ToString());
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_USER_NAME", "Логин почты", Constants.USERNAME);
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_PASSWORD", "Пароль", Constants.PASSWORD);
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_NAME", "Имя сервера", Constants.SERVERNAME);
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_WRITE_AS_FILE", "Записывать как файл", Constants.WRITE_AS_FILE.ToString());
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_FILE_LOCATION", "Местонахождение файла", Constants.FILE_LOCATION);
            TryToUpdateDimSettings("OPERATOR_EMAIL_SETTINGS", "Настройки рассылки оператору", "OPERATOR_MAIL_SERVER_PORT", "Порт", Constants.SERVER_PORT.ToString());
            // ViewBag.rejectProducts = ViewBag.rejectProducts;*/
            return View();
        }


        public ActionResult Categories(string searchWord, GridSortOptions gridSortOptions, int? superCategoryId, int? page,
                                       string categoryActivity = "Активные")
        {
            //  public ActionResult UsersView(string searchWord, int? artistId, GridSortOptions gridSortOptions, int? page, string userActivity = "Активные")
            var productsListAsync = repositoryProduct.Products;// GetProductListAsync();

            int pageItemsCount;

            TryToUpdateDimSettings("ADMIN_PAGE_SIZE",
                                   "Количество элементов на странице в админке",
                                   "ADMIN_PAGE_SIZE_Category",
                                   "Количество элементов на странице в административном разделе Категории");


            try
            {
                pageItemsCount =
                    Int32.Parse(
                        repositoryDimSetting.DimSettings.Where(x => x.SettingsID == "ADMIN_PAGE_SIZE_Category")
                                            .Select(x => x.SettingsValue).Single());
            }
            catch (Exception ex)
            {
                pageItemsCount = 0;
                logger.Warn(User.Identity.Name + ". Отсутствует значение параметра ADMIN_PAGE_SIZE_Product в БД" + ex.Message);
            }
            var superCategoryListAsync = repositorySuperCategory.SuperCategories.ToList();//GetSuperCategoryListAsync();
            
            IEnumerable<Category> categoryType = repositoryCategory.Categories.ToList();
            //productsListAsync.Wait();
            //IEnumerable<Product> products = productsListAsync.Result.ToList();
            IEnumerable<Product> products = productsListAsync;//.Result.ToList();

            if (categoryActivity == "Пустые")
            {
                IEnumerable<string> productCategories = from c in categoryType
                                                        join p in products on c.CategoryID
                                                            equals
                                                            p.CategoryID
                                                            where p.IsActive==true 
                                                        group c by new {c.CategoryID, c.ShortName}
                                                        into tmp
                                                        select tmp.Key.ShortName;

                IEnumerable<string> categoriesExists = from c in categoryType
                                                       select c.ShortName;


                IEnumerable<string> difference = categoriesExists.Except(productCategories);



                IEnumerable<Category> d = from j in categoryType
                                          where
                                              j.ShortName == difference.FirstOrDefault(x => x.ToString() == j.ShortName)
                                          select new Category
                                              {
                                                  CategoryID = j.CategoryID,
                                                  Name = j.Name,
                                                  ShortName = j.ShortName,
                                                  ImageExt = j.ImageExt,
                                                  SuperCategoryID = j.SuperCategoryID,
                                                  Sequence = j.Sequence,
                                                  SuperCategory = j.SuperCategory,
                                                  Description = j.Description,
                                                  CategoryKeywords = j.CategoryKeywords,
                                                  CategorySnippet = j.CategorySnippet,
                                                  IsDeleted = j.IsDeleted,
                                                  UpdateDate = j.UpdateDate
                                              };
                categoryType = d;
            }
            else if (categoryActivity == "Активные")
            {
                IEnumerable<string> productCategories = from c in categoryType
                                                        join p in products on c.CategoryID
                                                            equals
                                                            p.CategoryID
                                                            where p.IsActive==true 
                                                        group c by new {c.CategoryID, c.ShortName}
                                                        into tmp
                                                        select tmp.Key.ShortName;

                /*
                IEnumerable<string> categoriesExists = from c in repositoryCategory.Categories.ToList()
                                                       select c.ShortName;


                IEnumerable<string> difference = categoriesExists.Except(productCategories);

                */
                IEnumerable<Category> d = from j in categoryType
                                          where
                                              j.ShortName ==
                                              productCategories.FirstOrDefault(x => x.ToString() == j.ShortName)
                                          select new Category
                                              {
                                                  CategoryID = j.CategoryID,
                                                  Name = j.Name,
                                                  ShortName = j.ShortName,
                                                  ImageExt = j.ImageExt,
                                                  SuperCategoryID = j.SuperCategoryID,
                                                  Sequence = j.Sequence,
                                                  SuperCategory = j.SuperCategory,
                                                  Description = j.Description,
                                                  CategoryKeywords = j.CategoryKeywords,
                                                  CategorySnippet = j.CategorySnippet,
                                                  IsDeleted = j.IsDeleted,
                                                  UpdateDate = j.UpdateDate
                                              };
                categoryType = d;
            }
            else
            {

            }

            /*
             IEnumerable<Category> s = from j in repositoryCategory.Categories.ToList()
                                          where j.ShortName == difference.FirstOrDefault(x=>x.ToString()==j.ShortName)
                                            select new Category()
                                              {
                                                  CategoryID = j.CategoryID,
                                                  Name = j.Name,
                                                  ShortName = j.ShortName,
                                                  ImageExt = j.ImageExt
                                             };
             */
            var query = from a in categoryType
                        select new CategoryViewModel()
                            {
                                CategoryID = a.CategoryID,
                                Name = a.Name,
                                ImageExt = a.ImageExt,
                                Sequence = a.Sequence,
                                ShortName = a.ShortName,
                                SelectedSuperCategoryID = a.SuperCategoryID,
                                SuperCategoryID = a.SuperCategoryID,
                                SuperCategoryName = a.SuperCategory.Name,
                                IsDeleted = a.IsDeleted,
                                Description = a.Description,
                                CategoryKeywords = a.CategoryKeywords,
                                CategorySnippet = a.CategorySnippet,
                                UpdateDate = a.UpdateDate
                            };
            //superCategoryListAsync.Wait();
            
            //IEnumerable<SuperCategory> superCategories = superCategoryListAsync.Result.ToList();
            IEnumerable<SuperCategory> superCategories = superCategoryListAsync;
            
            var pagedViewModel = new PagedViewModel<CategoryViewModel>
                {
                    ViewData = ViewData,
                    Query = query.AsQueryable(),//categoryType.AsQueryable(),
                    //repositoryCategory.Categories.Except(categoryType), //categoryType, //repositoryCategory.Categories.Where(x=>x.CategoryID==),  //Except(categoryType), //categoryType,//repositoryCategory.Categories, //_service.GetAlbumsView(),
                    GridSortOptions = gridSortOptions,
                    DefaultSortColumn = "Name",
                    Page = page,
                    PageSize = (pageItemsCount == 0) ? Domain.Constants.ADMIN_PAGE_SIZE : pageItemsCount,
                }
                .AddFilter("searchWord", searchWord,
                           a => a.Name.Contains(searchWord) || a.ShortName.Contains(searchWord))
                .AddFilter("superCategoryId", superCategoryId, a => a.SelectedSuperCategoryID == superCategoryId,
                           superCategories.AsQueryable(), "Name")
                .Setup();


            if (Request.IsAjaxRequest())
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                return PartialView("CategoryGridPartial", pagedViewModel);
            }

            return View(pagedViewModel);



            /*
                 
                    var pagedViewModel = new PagedViewModel<User>
                    {
                        ViewData = ViewData,
                        Query = repositoryUser.UsersInfo,//_service.GetAlbumsView(),
                        GridSortOptions = gridSortOptions,
                        DefaultSortColumn = "Login",
                        Page = page,
                        PageSize = 10,
                    }
                    
                    

                    .AddFilter("searchWord", searchWord, a => a.UserName.Contains(searchWord) || a.Phone.Contains(searchWord) || a.Email.Contains(searchWord))
                    //.AddFilter("userActivity", userActivity, a => a.IsActive == IsActive) //,  _service.GetGenres(), "Name")
                    .AddFilter("userActivity", (userActivity == "Активные") ? IsActive = true : IsActive = false , a => a.IsActive == IsActive) //,  _service.GetGenres(), "Name")
                    /*.AddFilter("artistId", artistId, a => a.ArtistId == artistId, _service.GetArtists(), "Name")*/
            //.Setup();
            //return View(pagedViewModel);




            /*
                    
                   
                    .AddFilter("userActivity", (userActivity == "Активные") ? IsActive = true : IsActive = false , a => a.IsActive == IsActive) //,  _service.GetGenres(), "Name")
                   
                    .Setup();
                    return View(pagedViewModel);
             */
            /*Обязательно реализовать
            if (all!=true)
            {

                IEnumerable<string> productCategories = from c in repositoryCategory.Categories.ToList()
                                                        join p in repositoryProduct.Products.ToList() on c.CategoryID
                                                            equals
                                                            p.CategoryID
                                                        group c by new {c.CategoryID, c.ShortName}
                                                        into tmp
                                                        select tmp.Key.ShortName;

                IEnumerable<string> categoriesExists = from c in repositoryCategory.Categories.ToList()
                                                       select c.ShortName;


                IEnumerable<string> difference = categoriesExists.Except(productCategories);



                IEnumerable<Category> s = from j in repositoryCategory.Categories.ToList()
                                          where j.ShortName == difference.FirstOrDefault(x=>x.ToString()==j.ShortName)
                                            select new Category()
                                              {
                                                  CategoryID = j.CategoryID,
                                                  Name = j.Name,
                                                  ShortName = j.ShortName,
                                                  ImageExt = j.ImageExt
                                             };

                 return View(s);
            }
            else
            {
                return View(repositoryCategory.Categories);    
            } */

        }

          [Authorize(Roles = "Admin, ContentManager")]
        public ViewResult CreateCategory()
        {
            IEnumerable<SuperCategory> superCategoryList = repositorySuperCategory.SuperCategories;
            return View("EditCategory", new CategoryViewModel(){SuperCategories = superCategoryList});
        }



        public ViewResult EditCategory(int categoryId)
        {

            Category category = repositoryCategory.Categories
                                                  .FirstOrDefault(p => p.CategoryID == categoryId);
            var superCategories = repositorySuperCategory.SuperCategories.OrderBy(x=>x.Sequence).ToList();
            CategoryViewModel viewModel = new CategoryViewModel()
                {
                    CategoryID = categoryId,
                    Name = category.Name,
                    SuperCategoryID = category.SuperCategoryID,
                    SuperCategoryName = category.SuperCategory.Name,
                    ImageExt = category.ImageExt,
                    Sequence = category.Sequence,
                    ShortName = category.ShortName,
                    SelectedSuperCategoryID = category.SuperCategoryID,
                    SuperCategories = superCategories,
                    IsDeleted = category.IsDeleted,
                    Description = category.Description,
                    CategoryKeywords = category.CategoryKeywords,
                    CategorySnippet = category.CategorySnippet,
                    UpdateDate = category.UpdateDate
                };

            return View(viewModel);

        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCategory(CategoryViewModel viewModel)
        {
            var superCategoryListAsync = repositorySuperCategory.SuperCategories;//.GetSuperCategoryListAsync();
            Category category = new Category()
                {
                    CategoryID = viewModel.CategoryID,
                    SuperCategoryID = Convert.ToInt32(viewModel.SelectedSuperCategoryID),
                    ImageExt = viewModel.ImageExt,
                    Name = viewModel.Name,
                    Sequence = viewModel.Sequence,
                    ShortName = viewModel.ShortName,
                    IsDeleted = viewModel.IsDeleted,
                    Description = viewModel.Description,
                    CategoryKeywords = viewModel.CategoryKeywords,
                    CategorySnippet = viewModel.CategorySnippet
                };

            if ((ModelState.IsValid))//                && (repositoryCategory.Categories.FirstOrDefault(x => x.Name.TrimEnd() == category.Name.TrimEnd()) == null))
            {
                /*         if (image != null)
                                 {
                                     product.ImageMimeType = image.ContentType;
                                     product.ImageData = new byte[image.ContentLength];
                                     image.InputStream.Read(product.ImageData, 0, image.ContentLength);
         
                                 }
                                 // save the product
                         */
                
                
                if ((repositoryCategory.Categories.FirstOrDefault(x => x.Name.TrimEnd() == category.Name.TrimEnd()) != null) && (category.CategoryID==0))
                {
                    //superCategoryListAsync.Wait();
                    IEnumerable<SuperCategory> superCategoryList = superCategoryListAsync.ToList();
                    //IEnumerable<SuperCategory> superCategoryList = superCategoryListAsync.Result.ToList();
                    viewModel.SuperCategories = superCategoryList;
                    TempData["message"] = string.Format("{0} уже существует в базе! Изменения не внесены", category.Name);
                    TempData["messageType"] = "warning-msg";
                    return View(viewModel);
                }
                Debug.WriteLine("Admin поток №{0}", Thread.CurrentThread.ManagedThreadId);
                repositoryCategory.SaveCategory(category);
                //repositoryCategory.SaveCategory(category);
                repositoryCategory.RefreshAllShortNames();
                // add a message to the viewbag

                Debug.WriteLine("Admin2 поток №{0}", Thread.CurrentThread.ManagedThreadId);
                if (category.CategoryID == 0)
                {
                    TempData["message"] = string.Format("Категория '{0}' создана", category.Name);
                    TempData["messageType"] = "confirmation-msg";
                    logger.Warn(User.Identity.Name + ".  Категория " + category.Name + " создана  ");
                }
                else
                {
                    TempData["message"] = string.Format("Категория '{0}' изменена", category.Name);
                    TempData["messageType"] = "information-msg"; 
                    logger.Warn(User.Identity.Name + ".  Категория " + category.Name + " изменена ");
                }
                // return the user to the list
                //return RedirectToAction("Categories");
                
                
                
                return RedirectToAction("EditCategory", new {categoryId = category.CategoryID});
            }
            else
            {
                // there is something wrong with the data values
                TempData["message"] = string.Format("Категория '{0}' уже существует в базе! Изменения не внесены", category.Name);
                TempData["messageType"] = "warning-msg";
                return View(viewModel);
            }
        }




        //--------------------

          public void DeleteProductRelationships(int? productId, int[] resubmit)
          {
              //if ((productId == null) && (resubmit!=null)) 
              if ((productId == null) && (resubmit != null)) 
              {
                  //IEnumerable<Product> productList = repositoryProduct.Products.Where(x=>x.ProductID==)
                  foreach (var p in resubmit)
                  {

                      //var productAsync = repositoryProduct.GetProductByIDAsync(p);
                      var productAsync = repositoryProduct.Products.FirstOrDefault(x => x.ProductID == p);//GetProductByIDAsync(p);
                      
                      IEnumerable<ProductImage> productImages =
                          repositoryProductImages.ProductImages.Where(x => x.ProductID == p);
                      if (productImages != null)
                      {
                          foreach (var productImage in productImages)
                          {
                              productImage.Product = null;

                              string strSaveFileName = productImage.ProductID.ToString() + "_" +
                                                       productImage.ProductImageID.ToString() + productImage.ImageExt;
                              //repositoryProductImages.DeleteProductImage(productImage);
                              //  TempData["Message"] = string.Format("Изображение {0}_{1}.{2} было удалено", productImage.ProductID, productImage.ProductImageID, productImage.ImageExt);
                              //  TempData["messageType"] = "warning-msg";
                              string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                              Constants.PRODUCT_IMAGE_FOLDER,
                                                                              strSaveFileName);
                              string strSavePreviewFullPath =
                                  System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                         Constants.PRODUCT_IMAGE_FOLDER,
                                                         Constants.PRODUCT_IMAGE_PREVIEW_FOLDER,
                                                         strSaveFileName);
                              if (System.IO.File.Exists(strSaveFullPath))
                              {
                                  System.IO.File.Delete(strSaveFullPath);
                              }
                              else
                              {
                                  Exception ex;
                              }
                              if (System.IO.File.Exists(strSavePreviewFullPath))
                              {
                                  System.IO.File.Delete(strSavePreviewFullPath);
                              }
                          }
                      }
                      repositoryProductImages.DeleteProductImageBulk(productImages);

                     // productAsync.Wait();
                      Product prod = productAsync; //productAsync.Result;

                      if (prod != null)
                      {
                          OrderDetails od = repositoryOrderDetails.OrdersDetails.FirstOrDefault(x => x.ProductID == p);
                          if (od == null)
                          {
                              repositoryProduct.DeleteProduct(prod);
                              logger.Warn(User.Identity.Name + ". Товар " + prod.Name + " удален.");
                          }
                          else
                          {
                              repositoryProduct.SetDeletedStatus(true, prod);
                             // repositoryProduct.SetDeletedStatus(true, prod);
                              repositoryProduct.SetActiveStatus(false, prod);
                              //Тут будет код о пометке deleted
                              //repositoryProduct.DeleteProduct(prod);
                              logger.Warn(User.Identity.Name + ". Товар " + prod.Name + " был помечен как удаленный");
                          }
                      }

                  }
                  TempData["message"] = string.Format("Товары были удалены или помечены как удаленные");
                  TempData["messageType"] = "warning-msg";

              }
              else if ((productId != null) && (resubmit == null)) 
              {
                  IEnumerable<ProductImage> productImages =
                      repositoryProductImages.ProductImages.Where(x => x.ProductID == productId);
                  if (productImages != null)
                  {
                      foreach (var productImage in productImages)
                      {
                          productImage.Product = null;

                          string strSaveFileName = productId.ToString() + "_" + productImage.ProductImageID.ToString() +
                                                   productImage.ImageExt;
                          //repositoryProductImages.DeleteProductImage(productImage);
                          TempData["Message"] = string.Format("Изображение {0}_{1}.{2} было удалено",
                                                              productImage.ProductID, productImage.ProductImageID,
                                                              productImage.ImageExt);
                          TempData["messageType"] = "warning-msg";
                          string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                          Constants.PRODUCT_IMAGE_FOLDER,
                                                                          strSaveFileName);
                          string strSavePreviewFullPath =
                              System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                     Constants.PRODUCT_IMAGE_FOLDER,
                                                     Constants.PRODUCT_IMAGE_PREVIEW_FOLDER,
                                                     strSaveFileName);
                          if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);
                          if (System.IO.File.Exists(strSavePreviewFullPath))
                              System.IO.File.Delete(strSavePreviewFullPath);
                      }
                  }
                  repositoryProductImages.DeleteProductImageBulk(productImages);
                  Product prod = repositoryProduct.Products.FirstOrDefault(p => p.ProductID == productId);

                  if (prod != null)
                  {
                      try
                      {
                          repositoryProduct.DeleteProduct(prod);
                          logger.Warn(User.Identity.Name + ". Товар " + prod.Name + " удален.");
                          TempData["message"] = string.Format("Товар  " + prod.Name + " был удален");
                          TempData["messageType"] = "warning-msg";
                      }
                      catch (Exception)
                      {
                          repositoryProduct.SetDeletedStatus(true, prod);
                          repositoryProduct.SetActiveStatus(false, prod);
                          //Тут будет код о пометке deleted
                          //repositoryProduct.DeleteProduct(prod);
                          logger.Warn(User.Identity.Name + ". Товар " + prod.Name + " был помечен как удаленный");
                          TempData["message"] = string.Format("Товар  " + prod.Name + " был помечен как удаленный");
                          TempData["messageType"] = "warning-msg";
                      }
                  }

              }
          }


//--------------------
        [HttpPost]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult DeleteCategory(int categoryId)
        {
            Category category = repositoryCategory.Categories.FirstOrDefault(p => p.CategoryID == categoryId);

            int[] productsIdForDelete = repositoryProduct.Products.Where(x => x.CategoryID == categoryId).Select(x=>x.ProductID).ToArray();

            DeleteProductRelationships(null, productsIdForDelete);

            if (category != null)
            {
                string strSaveFileName = categoryId.ToString() + category.ImageExt;
                string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                               Constants.CATEGORY_MINI_IMAGES_FOLDER, strSaveFileName);

                if (System.IO.File.Exists(strSaveFullPath))
                {
                    System.IO.File.Delete(strSaveFullPath);
                }

                Product pr = repositoryProduct.Products.FirstOrDefault(x => x.CategoryID == category.CategoryID);

                if (pr == null)
                {
                    repositoryCategory.DeleteCategory(category);
                    TempData["message"] = string.Format("Категория '{0}' была удалена", category.Name);
                    TempData["messageType"] = "warning-msg";
                    logger.Warn(User.Identity.Name + ".  Категория " + category.Name + " удалена ");
                }
                else
                {
                    repositoryCategory.SetDeletedStatus(true, category);
                    TempData["message"] = string.Format("Категория '{0}' была помечена как удаленная", category.Name);
                    TempData["messageType"] = "warning-msg";
                    logger.Warn(User.Identity.Name + ".  Категория " + category.Name + " помечена как удаленная");
                }
                
            }
            return RedirectToAction("Categories");
        }


        public ActionResult RefreshAllShortNamesInCategories()
        {
            repositoryCategory.RefreshAllShortNames();
            logger.Warn(User.Identity.Name + ". FriendlyUrl всех категорий обновлен ");
            return RedirectToAction("Categories");
        }

        //========================================== 



        /// <summary>
        ///Добавление картинки 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult UploadCatalogImage(HttpPostedFileBase imagefile, int categoryId)
        {
            if (imagefile == null)
            {
                return RedirectToAction("EditCategory", new { categoryId });
            }
            // Получаем объект, для которого загружаем картинку
            Category obj = repositoryCategory.Categories.FirstOrDefault(x => x.CategoryID == categoryId);
                // service.Get(objID);
            if (obj == null) return Content("NotFound"); //View("NotFound");

            try
            {
                if (imagefile != null)
                {
                    // Определяем название конечного графического файла вместе с полным путём.
                    // Название файла должно быть такое же, как ID объекта. Это гарантирует уникальность названия.
                    // Расширение должно быть такое же, как расширение у исходного графического файла.
                    string strExtension = System.IO.Path.GetExtension(imagefile.FileName);
                    string strSaveFileName = categoryId + strExtension;
                    string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                    Constants.CATEGORY_MINI_IMAGES_FOLDER,
                                                                    strSaveFileName);

                    // Если файл с таким названием имеется, удаляем его.
                    if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);

                    // Сохраняем картинку, изменив её размеры.
                    imagefile.ResizeAndSave(Constants.CATEGORY_MINI_IMAGE_HEIGHT, Constants.CATEGORY_MINI_IMAGE_WIDTH,
                                            strSaveFullPath);

                    // Расширение файла записываем в базу данных в поле ImageExt.
                    obj.ImageExt = strExtension;

                    repositoryCategory.SaveCategory(obj);
                    //service.Save();
                }
            }
            catch (Exception ex)
            {
                string strErrorMessage = ex.Message;
                if (ex.InnerException != null)
                    strErrorMessage = string.Format("{0} --- {1}", strErrorMessage, ex.InnerException.Message);
                logger.Error(User.Identity.Name + ". Ошибка при сохранении изображения " + ex.Message);
                ViewBag.ErrorMessage = strErrorMessage;
                return View("Error");
            }

            //return View("", ""); //ReturnToObject(obj);
            //return RedirectToAction("Categories", "Admin");
            return RedirectToAction("EditCategory", "Admin", new {categoryId});
        }




        public ActionResult UpdateCategorySequence()
        {
            repositoryCategory.UpdateCategorySequence();

            return RedirectToAction("Categories");
        }

        public ActionResult CategorySequence(int categoryId, string actionType)
        {
            try
            {
                Exception ex = new Exception();
                int[] sequence =
                    repositoryCategory.Categories.Select(x => x.Sequence).ToArray();
                Array.Sort(sequence);
                //sequence.OrderBy(x => x.ToString());

                for (int i = 0; i < sequence.Count(); i++)
                {
                    if (sequence[i] == i + 1)
                    {

                    }
                    else
                    {
                        logger.Error(User.Identity.Name + ". Проблема обновления списка " + ex.Message);
                        throw (ex);
                        
                    }
                }

                repositoryCategory.CategorySequence(categoryId, actionType);
            }

            catch (Exception ex)
            {
                TempData["message"] = string.Format("Нарушена последовательность! Список был пересчитан!");
                TempData["messageType"] = "error-msg"; 
                logger.Error(User.Identity.Name + ". Нарушена последовательность в категориях! Список был пересчитан!" + ex.Message);
                repositoryCategory.UpdateCategorySequence();
                repositoryCategory.CategorySequence(categoryId, actionType);
            }

            GridSortOptions gridSortOptions = new GridSortOptions()
                {
                    Column = "Sequence",
                    Direction = SortDirection.Ascending
                };


            var pagedViewModel = new PagedViewModel<Category>()
                {
                    ViewData = ViewData,
                    Query = repositoryCategory.Categories, //_service.GetAlbumsView(),
                    GridSortOptions = gridSortOptions,
                    DefaultSortColumn = "Sequence",
                    Page = 1,
                    PageSize = 100 //Domain.Constants.ADMIN_PAGE_SIZE
                }
                .Setup();

            return PartialView("CategorySequenceGridPartial", pagedViewModel);

        }



        public ActionResult CategorySequenceView()
        {

            var productListAsync = repositoryProduct.Products;//GetProductListAsync();
            IQueryable<Category> categoryType = repositoryCategory.Categories;



            GridSortOptions gridSortOptions = new GridSortOptions()
                {
                    Direction = SortDirection.Ascending,
                    Column = "Sequence"
                };

            var pagedViewModel = new PagedViewModel<Category>
                {
                    ViewData = ViewData,
                    Query = categoryType.AsQueryable(),
                    //repositoryCategory.Categories.Except(categoryType), //categoryType, //repositoryCategory.Categories.Where(x=>x.CategoryID==),  //Except(categoryType), //categoryType,//repositoryCategory.Categories, //_service.GetAlbumsView(),
                    GridSortOptions = gridSortOptions,
                    DefaultSortColumn = "Sequence",
                    Page = 1,
                    PageSize = 100,
                }
                .Setup();


            /* if (Request.IsAjaxRequest())
            {
                return PartialView("CategoryGridPartial", pagedViewModel);
            }
            */


            //productListAsync.Wait();
            //IEnumerable<Product> products = productListAsync.Result.ToList();
            IEnumerable<Product> products = productListAsync;

            IEnumerable<string> productCategories = from c in repositoryCategory.Categories.ToList()
                                                    join p in products on c.CategoryID
                                                        equals
                                                        p.CategoryID
                                                        where p.IsActive==true && p.IsDeleted == false
                                                    group c by new {c.CategoryID, c.ShortName}
                                                    into tmp
                                                    select tmp.Key.ShortName;

            IEnumerable<string> categoriesExists = from c in repositoryCategory.Categories.ToList()
                                                   select c.ShortName;


            IQueryable<string> difference = categoriesExists.Except(productCategories).AsQueryable();



            var z = from j in repositoryCategory.Categories.ToList()
                    where j.ShortName == difference.FirstOrDefault(x => x.ToString() == j.ShortName)
                    select j.CategoryID;
            TempData["EmptyCategories"] = z.ToArray();




            var categoryGroupping = from n in products
                                    group n by n.CategoryID
                                    into g
                                    select new {CategoryID = g.Key, ProductCount = g.Count()};

            var singleProductCategory = from m in categoryGroupping
                                        where m.ProductCount == 1
                                        select m.CategoryID;
            TempData["SingProductCategories"] = singleProductCategory.ToArray();

            /*
            
            IEnumerable<string> productCategories2 = from c in repositoryCategory.Categories.ToList()
                                                    join p in repositoryProduct.Products.ToList() on c.CategoryID
                                                        equals
                                                        p.CategoryID
                                           //             where 
                                                    group c by new { c.CategoryID, c.ShortName }
                                                        into tmp
                                                        select tmp.Key.ShortName;

            IEnumerable<string> categoriesExists2 = from c in repositoryCategory.Categories.ToList()
                                                   select c.ShortName;


            IQueryable<string> difference2 = categoriesExists.Except(productCategories).AsQueryable();



            var z = from j in repositoryCategory.Categories.ToList()
                    where j.ShortName == difference.FirstOrDefault(x => x.ToString() == j.ShortName)
                    select j.CategoryID;

            */




            return View("CategorySequence", pagedViewModel);
        }


        #endregion Category




        #region User
          [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult UsersView(string searchWord, GridSortOptions gridSortOptions, int? page,
                                      string userActivity = "Активные")
          {
              var userListAsync = repositoryUser.UsersInfo.ToList();//GetUserListAsync();
            bool IsActive;

            //"-- Все --", "Активные", "Неактивные"
            // from c in repositoryCategory.Categories.ToList() select c.ShortName;
            //IEnumerable<string> s1 = repositoryDimSetting.DimSettings.Select(x => x.SettingsID).ToList();
            //repositoryDimSetting.DimSettings.ToList();
            int pageItemsCount;

            TryToUpdateDimSettings("ADMIN_PAGE_SIZE",
                                   "Количество элементов на странице в админке",
                                   "ADMIN_PAGE_SIZE_User",
                                   "Количество элементов на странице в административном разделе Клиенты");


            try
            {
                pageItemsCount =
                    Int32.Parse(
                        repositoryDimSetting.DimSettings.Where(x => x.SettingsID == "ADMIN_PAGE_SIZE_User")
                                            .Select(x => x.SettingsValue).Single());
            }
            catch (Exception)
            {
                pageItemsCount = 0;
            }



            //  userListAsync.Wait();
            var pagedViewModel = new PagedViewModel<User>
                {
                    ViewData = ViewData,
                    //Query = userListAsync.Result.AsQueryable(), //_service.GetAlbumsView(),
                    Query = userListAsync.AsQueryable(), //_service.GetAlbumsView(),
                    GridSortOptions = gridSortOptions,
                    DefaultSortColumn = "Login",
                    Page = page,
                    PageSize = (pageItemsCount == 0) ? Domain.Constants.ADMIN_PAGE_SIZE : pageItemsCount
                    // Domain.Constants.ADMIN_PAGE_SIZE,
                }

                .AddFilter("searchWord", searchWord,
                           a =>
                           a.UserName.Contains(searchWord) || a.Phone.Contains(searchWord) ||
                           a.Email.Contains(searchWord))
                //.AddFilter("userActivity", userActivity, a => a.IsActive == IsActive) //,  _service.GetGenres(), "Name")
                .AddFilter("userActivity", (userActivity == "Активные") ? IsActive = true : IsActive = false,
                           a => a.IsActive == IsActive) //,  _service.GetGenres(), "Name")
                /*.AddFilter("artistId", artistId, a => a.ArtistId == artistId, _service.GetArtists(), "Name")*/
                .Setup();
            //  ViewBag.ServName = Server.MachineName;

            if (Request.IsAjaxRequest())
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                return PartialView("UserGridPartial", pagedViewModel);
            }

            return View(pagedViewModel);
            //return View(repositoryUser.UsersInfo);
        }



        /*
                  public ActionResult Index(string searchWord, int? genreId, int? artistId, GridSortOptions gridSortOptions, int? page)
                {
                    var pagedViewModel = new PagedViewModel<AlbumViewModel>
                    {
                        ViewData = ViewData,
                        Query = _service.GetAlbumsView(),
                        GridSortOptions = gridSortOptions,
                        DefaultSortColumn = "AlbumId",
                        Page = page,
                        PageSize = 10,
                    }
                    .AddFilter("searchWord", searchWord, a => a.AlbumTitle.Contains(searchWord) || a.Artist.Contains(searchWord) || a.Genre.Contains(searchWord))
                    .AddFilter("genreId", genreId, a => a.GenreId == genreId, _service.GetGenres(), "Name")
                    .AddFilter("artistId", artistId, a => a.ArtistId == artistId, _service.GetArtists(), "Name")
                    .Setup();

                    return View(pagedViewModel);
                }
                 */


          [Authorize(Roles = "Admin, ContentManager")]
        public ViewResult CreateUser()
        {
            return View("EditUser", new User());
        }

          [Authorize(Roles = "Admin, ContentManager")]
        public ViewResult EditUser(int userId)
        {

            User user = repositoryUser.UsersInfo.FirstOrDefault(p => p.UserID == userId);
            return View(user);

        }


        [HttpPost]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult EditUser(User user)
        {
            user.Email = user.Email.TrimEnd();
            user.Login = user.Login.TrimEnd();
            user.Password = user.Password.TrimEnd();
            user.UserName = user.UserName.TrimEnd();

            if (ModelState.IsValid)
            {
                repositoryUser.SaveUser(user);
                
                // return the user to the list

                if (user.UserID==0)
                {
                    // add a message to the viewbag
                    TempData["Message"] = string.Format("Пользователь '{0}' создан", user.Login);
                    TempData["messageType"] = "confirmation-msg"; 
                    logger.Warn(User.Identity.Name + ". Пользователь "+user.Login+" создан ");
                }
                else
                {
                    TempData["Message"] = string.Format("Пользователь '{0}' изменен", user.Login);
                    logger.Warn(User.Identity.Name + ". Пользователь " + user.Login + " изменен ");
                    TempData["messageType"] = "information-msg"; 
                }

                return RedirectToAction("UsersView");
            }
            else
            {
                // there is something wrong with the data values
                return View(user);
            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult DeleteUser(int? userID, int?[] resubmit)
        {
            if (userID!=null)
            {
                User user = repositoryUser.UsersInfo.FirstOrDefault(p => p.UserID == userID);

                if (user != null)
                {
                    try
                    {
                        repositoryUser.DeleteUser(user);
                                    TempData["Message"] = string.Format("Пользователь '{0}' был удален", user.Login);
                                    TempData["messageType"] = "warning-msg"; 
                                    logger.Warn(User.Identity.Name + ". Пользователь " + user.Login + " удален ");
                    }
                    catch (Exception)
                    {
                        user.IsActive = false;
                        repositoryUser.SaveUser(user);
                        TempData["Message"] = string.Format("Пользователь '{0}' деактивирован", user.Login);
                                    TempData["messageType"] = "warning-msg"; 
                                    logger.Warn(User.Identity.Name + ". Пользователь " + user.Login + " деактивирован ");
                    }
                    
                }    
            }
            else if (resubmit!=null)
            {
                foreach (var p in resubmit)
                {
                    User user = repositoryUser.UsersInfo.FirstOrDefault(x => x.UserID == p);
                    if (user != null)
                    {
                        try
                        {
                            repositoryUser.DeleteUser(user);
                            TempData["Message"] = string.Format("Пользователь '{0}' был удален", user.Login);
                            TempData["messageType"] = "warning-msg";
                            logger.Warn(User.Identity.Name + ". Пользователь " + user.Login + " удален ");
                        }
                        catch (Exception)
                        {
                            user.IsActive = false;
                            repositoryUser.SaveUser(user);
                            TempData["Message"] = string.Format("Пользователь '{0}' деактивирован", user.Login);
                            TempData["messageType"] = "warning-msg";
                            logger.Warn(User.Identity.Name + ". Пользователь " + user.Login + " деактивирован ");
                        }

                    }    
                }   
            }
            
            return RedirectToAction("UsersView");
        }


        #endregion


        #region OrderSummary
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult Orders(string searchWord, GridSortOptions gridSortOptions, int? page, string startDate, string endDate, bool isActive = true)
        {
            int sw;
           
            int pageItemsCount;

            TryToUpdateDimSettings("ADMIN_PAGE_SIZE",
                                   "Количество элементов на странице в админке",
                                   "ADMIN_PAGE_SIZE_OrderSummary",
                                   "Количество элементов на странице в административном разделе Заказы");
            try
            {
                Int32.TryParse(searchWord, out sw);
            }
            catch (Exception)
            {
                sw = 0;
            }

            if (startDate == null)
            {
                startDate = String.Format("{0:dd.mm.yyyy}", (DateTime.Now.Date.AddDays(-5)).ToShortDateString());
            }
            else
            {
                startDate = String.Format("{0:dd.mm.yyyy}", startDate);
            }

            if (endDate == null)
            {
                endDate = (DateTime.Now.Date.ToShortDateString());
            }
            else
            {
                endDate = String.Format("{0:dd.mm.yyyy}", endDate);
            }

            DateTime dStart = Convert.ToDateTime(startDate);
            DateTime dEnd = Convert.ToDateTime(endDate).AddDays(1);
            
            
            //сохранение данных в сессию, чтобы не сбраслывались фильтры в таблице
            /*
                 var searchWord = TempData.Peek("SearchWord");
    var gridSortOptions = TempData.Peek("GridSortOptions");
    var page = TempData.Peek("Page");
    var startDate = TempData.Peek("StartDate");
    var endDate = TempData.Peek("EndDate");
    var isActive = TempData.Peek("IsActive");*@
             */
            Session["isActive"] = isActive;
            Session["searchWord"] = searchWord;
            Session["StartDate"] = dStart.ToShortDateString();
            Session["EndDate"] = dEnd.ToShortDateString();
            Session["page"] = page;


            var pagedViewModel = new PagedViewModel<OrdersSummary>
                {
                    ViewData = ViewData,
                    Query = repositoryOrderSummary.OrdersSummaryInfo, //_service.GetAlbumsView(),
                    GridSortOptions = gridSortOptions,
                    DefaultSortColumn = "OrderNumber",
                    Page = page,
                    PageSize = Domain.Constants.ADMIN_PAGE_SIZE,
                }
                .AddFilter("searchWord", searchWord,
                           a =>
                           a.OrderNumber == sw
                           /*a.OrderNumber == ((Int32.TryParse(searchWord, out d)) ? d : 2121212121212121) */||
                           a.Phone.Contains(searchWord) || a.UserName.Contains(searchWord) ||
                           a.UserAddress.Contains(searchWord))
                .AddFilter("startDate", Convert.ToDateTime(startDate), a => a.TransactionDate >= dStart)
                .AddFilter("endDate", Convert.ToDateTime(endDate), a => a.TransactionDate <= dEnd)
                .AddFilter("isActive", isActive, a => a.IsActive == isActive)
                .Setup();

            if (Request.IsAjaxRequest())
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                return PartialView("OrdersGridPartial", pagedViewModel);
            }

            return View(pagedViewModel);


            //  return View(repositoryOrderSummary.OrdersSummaryInfo);
        }


          [HttpPost]
        public ActionResult CheckboxOrder(int? orderSummaryID, int?[] resubmit)
          {
              return null;
          }


        [HttpPost]
        [Authorize(Roles = "Admin, ContentManager")]
          public ActionResult DeleteOrderSummary(int? orderSummaryID, int?[] resubmit)
        {
            if (orderSummaryID != null)
            {
                //var orderSummaryAsync = repositoryOrderSummary.GetOrderSummaryByIDAsync((int)orderSummaryID);
                var orderSummaryAsync = repositoryOrderSummary.OrdersSummaryInfo.FirstOrDefault(x=>x.OrderSummaryID==(int)orderSummaryID);

                

                IEnumerable orderDetails =
                    repositoryOrderDetails.OrdersDetails.Where(x => x.OrderSummaryID == orderSummaryID);

                //восстановление количества товаров на складе при удалении заказа
                foreach (OrderDetails orderDetail in orderDetails)
                {
                    Product product =
                        repositoryProduct.Products.FirstOrDefault(x => x.ProductID == orderDetail.ProductID);
                    product.Quantity = product.Quantity + orderDetail.Quantity;
                    repositoryProduct.SaveProduct(product);
                }
                //orderSummaryAsync.Wait();
                OrdersSummary ordersSummary = orderSummaryAsync;//.Result;
                if (ordersSummary != null)
                {
                    repositoryOrderSummary.DeleteOrderSummary(ordersSummary);
                    TempData["message"] = string.Format("Заказ №{0} был удален", ordersSummary.OrderSummaryID);
                    TempData["messageType"] = "warning-msg";
                    logger.Warn(string.Format("{0}. Заказ №{1} был удален", User.Identity.Name, ordersSummary.OrderSummaryID));
                }    
            }
            else if(resubmit!=null)
            {
                foreach (var p in resubmit)
                {
                    OrdersSummary ordersSummary =
                    repositoryOrderSummary.OrdersSummaryInfo.FirstOrDefault(x => x.OrderSummaryID == p);
                    
                    //восстановление количества товаров на складе при удалении заказа
                    IEnumerable orderDetails = repositoryOrderDetails.OrdersDetails.Where(x => x.OrderSummaryID == ordersSummary.OrderSummaryID);
                    foreach (OrderDetails orderDetail in orderDetails)
                    {
                        Product product =
                            repositoryProduct.Products.FirstOrDefault(x => x.ProductID == orderDetail.ProductID);
                        product.Quantity = product.Quantity + orderDetail.Quantity;
                        repositoryProduct.SaveProduct(product);
                    }
                    //---

                    if (ordersSummary != null)
                    {
                        repositoryOrderSummary.DeleteOrderSummary(ordersSummary);
                        logger.Warn(string.Format("{0}. Заказ №{1} был удален", User.Identity.Name, ordersSummary.OrderSummaryID));
                    }
                }
                TempData["message"] = string.Format("Выбранные заказы были удалены");
                TempData["messageType"] = "warning-msg";
                
            }
            
            return RedirectToAction("Orders");
        }

        [HttpGet]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult EditOrderSummary(int orderSummaryID)
        {

          

            OrdersSummary os =
                repositoryOrderSummary.OrdersSummaryInfo.FirstOrDefault(p => p.OrderSummaryID == orderSummaryID);
           
            return View("EditOrderSummary", os);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult EditOrderSummary(OrdersSummary ordersSummary)
        {

            //--Tempdata searchOptions

            string searchWord = (string) TempData["SearchWord"];
            int page;
            try
            {
                page = (int) TempData["Page"];
            }
            catch (Exception)
            {
                page = 1;
            }


            string startDate = (string)Session["StartDate"];//(string) TempData["StartDate"];
            string endDate = (string)Session["EndDate"];
            bool isActive = (bool)Session["IsActive"];
            

            if (ModelState.IsValid)
            {
                repositoryOrderSummary.SaveOrder(ordersSummary);
                TempData["message"] = string.Format("Заказ №{0} изменен", ordersSummary.OrderNumber);
                TempData["messageType"] = "information-msg"; 
                logger.Warn(string.Format("{0}. Заказ №{1} изменен", User.Identity.Name, ordersSummary.OrderNumber));
                return RedirectToAction("Orders", new {searchWord, page, startDate, endDate, isActive});
            }
            else
            {
                return View(ordersSummary);
            }
        }

        #endregion


        #region OrderDetails

        //Перенести куда-нить
        public static string[] InfoAboutField<T>()
        {
            Type t = typeof (T);
            var fieldsName = t.GetProperties();
            // Ну и выводим в консоль
            string[] ar = new string[fieldsName.Length];
            int i = 0;
            string s = "";

            foreach (var f in fieldsName)
            {
                ar[i] = f.Name;
                i++;
            }
            return ar;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult OrdersDetails(string searchWord, GridSortOptions gridSortOptions, int? page,
                                          int orderSummaryID)
        {

            string[] s = InfoAboutField<OrdersSummary>();
            if ((from x in s where x.ToString() == gridSortOptions.Column select x).Count() == 0)
            {
                gridSortOptions.Column = gridSortOptions.Column;
            }
            else
            {
                gridSortOptions.Column = "OrderSummaryID";
            }

            IEnumerable<OrderDetails> orderDetails = repositoryOrderDetails.OrdersDetails.ToList();

            var query = from o in orderDetails.Where(x => x.OrderSummaryID == orderSummaryID)
                        select new OrderDetailsViewModel
                            {
                                OrderDetailsID = o.OrderDetailsID,
                                OrderSummaryID = o.OrderSummaryID,
                                ProductID = o.ProductID,
                                UserID = o.UserID,
                                CategoryName = o.Product.Category.Name,
                                ProductName = o.Product.Name,
                                PriceInOrder = o.Price,
                                QuantityInOrder = o.Quantity,
                                TotalPrice = o.Price*o.Quantity,
                                QuantityInStore = o.Product.Quantity,
                                PriceNow = o.Product.Price,
                                OrdersSummary = o.OrdersSummary
                            };
            var pagedViewModel = new PagedViewModel<OrderDetailsViewModel>
                {
                    ViewData = ViewData,
                    Query = query.AsQueryable(),
                    GridSortOptions = gridSortOptions,
                    DefaultSortColumn = "OrderSummaryID",
                    Page = page,
                    PageSize = Domain.Constants.ADMIN_PAGE_SIZE
                };
            pagedViewModel
                .Setup();

            

            OrdersSummary os =
                repositoryOrderSummary.OrdersSummaryInfo.FirstOrDefault(x => x.OrderSummaryID == orderSummaryID);
            /*ViewBag.OrderSummaryOrderNumber = os.OrderNumber;
            ViewBag.OrderSummaryUserName = os.UserName;
            ViewBag.OrderSummaryAddress = os.UserAddress;
            ViewBag.OrderSummaryPhone = os.Phone;
            ViewBag.OrderSummaryEmail = os.Email;
            ViewBag.OrderSummaryTransactionDate = os.TransactionDate.ToShortDateString();
            ViewBag.OrderSummaryTransactionTime = os.TransactionDate.ToShortTimeString();
            ViewBag.OrderSummaryTotalValue = os.TotalValue;
            ViewBag.OrderSummaryIsActive = os.IsActive;
            ViewBag.OrderSummaryID = os.OrderSummaryID;
            ViewBag.ShippingPrice = os.ShippingPrice;
            ViewBag.ShippingType = os.ShippingType;*/
            ViewBag.TotalSum = os.ShippingPrice+os.TotalValue;
            /*TempData["OrderSummaryID"] = os.OrderSummaryID;*/
            string whole = ((int) os.TotalValue).ToString();
            PopulateAssignedStatus(orderSummaryID);




            ViewBag.OrderSummaryTotalValueFloor = string.Format("{0:### ###}", os.TotalValue);
            string cent = (os.TotalValue - (int) os.TotalValue).ToString();
            ViewBag.OrderSummaryTotalValueMod = (cent).Remove(0, cent.LastIndexOf(",") + 1);
            if (Request.IsAjaxRequest())
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                return PartialView("OrderDetailsGridPartial", pagedViewModel);
            }

            return View(pagedViewModel);
        }

        //
        // [HttpGet]
          [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult OrderStatus(int orderSummaryID)
        {
            //PopulateAssignedStatus(orderSummaryID);
            OrdersSummary os =
                repositoryOrderSummary.OrdersSummaryInfo.Where(x => x.OrderSummaryID == orderSummaryID).Single();
            ViewBag.OrderSummaryID = os.OrderSummaryID;

            return PartialView(os);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult OrdersDetails(int orderSummaryID, FormCollection formCollection, string[] selectedStatuses)
        {
            int statusDesc =
                repositoryDimOrderStatus.DimOrderStatuses.Where(x => x.OrderStatusDesc == "Заказ выполнен")
                                        .Select(x => x.DimOrderStatusID).Single();



            var orderSummaryToUpdate =
                repositoryOrderSummary.OrdersSummaryInfo.Where(x => x.OrderSummaryID == orderSummaryID).Single();

            // selectedOrderStatus, OrdersSummary orderStatusToUpdate

            //string searchWord = (string) TempData["SearchWord"];
            int page;
            try
            {
                page = (int) TempData["Page"];
            }
            catch (Exception)
            {
                page = 1;
            }


            string startDate;  
            string endDate; 
             
            

            

            //DateTime dStart = Session["startDate"];
            //DateTime dEnd = Convert.ToDateTime(Session["endDate"]);
             

            if (TryUpdateModel(orderSummaryToUpdate, "", null, new string[] {"OrderStatuses"}))
            {
                try
                {
                    UpdateModel(orderSummaryToUpdate, "", null, new string[] {"OrderStatuses"});

                    UpdateOrderStatus(selectedStatuses, orderSummaryToUpdate);

                    repositoryOrderSummary.SaveOrder(orderSummaryToUpdate);

                    //изменение общего статуса заказа
                    try
                    {

                        if (selectedStatuses.Contains(statusDesc.ToString()))
                        {
                            orderSummaryToUpdate.IsActive = false;
                        }
                        else
                        {
                            orderSummaryToUpdate.IsActive = true;
                        }
                    }
                    catch (Exception)
                    {
                        orderSummaryToUpdate.IsActive = true;
                    }

                    repositoryOrderSummary.SaveOrder(orderSummaryToUpdate);

                    
                    
                    //RedirectToAction("Orders", new {searchWord, page, startDate, endDate, isActive});

                    /*
                     
            Session["StartDate"] = dStart;
            Session["EndDate"] = dEnd;
                     */

                    if (Session["startDate"].ToString() == null)
                        Session["startDate"] = DateTime.Now.ToShortDateString();
                    if (Session["endDate"].ToString()==null)
                    {
                        Session["endDate"] = DateTime.Now.ToShortDateString();
                    }

                    // public ActionResult Orders(string searchWord, GridSortOptions gridSortOptions, int? page, string startDate,string endDate, bool isActive = true)
                    return RedirectToAction("Orders", new { searchWord = Session["searchWord"], startDate = Session["startDate"].ToString(), endDate = Session["endDate"].ToString(), isActive = Session["isActive"], page = Session["page"] });

                }
                catch (DataException ex)
                {
                    ModelState.AddModelError("", "Невозможно внести изменения, обратитесь к администратору");
                    logger.Warn(string.Format("{0}. Невозможно внести изменения в статус заказа №{1}", User.Identity.Name, orderSummaryToUpdate.OrderNumber));
                }
            }
            PopulateAssignedStatus(orderSummaryToUpdate.OrderSummaryID);


           // return RedirectToAction("Orders", new {searchWord, page, startDate, endDate, isActive});
            //return RedirectToAction("Orders", new { searchWord });
            //return RedirectToAction("Orders", new { searchWord, startDate = DateTime.Parse(Session["StartDate"].ToString()), endDate = Session["EndDate"].ToString() });
            //return RedirectToAction("Orders", new { searchWord, startDate = "01.01.2013", endDate = "01.01.2014" });
            return RedirectToAction("Orders", new { searchWord = Session["searchWord"], startDate = Session["startDate"].ToString(), endDate = Session["endDate"].ToString(), isActive = Session["isActive"], page = Session["page"] });
        }


          
        private void UpdateOrderStatus(string[] selectedOrderStatus, OrdersSummary orderStatusToUpdate)
        {
            if (selectedOrderStatus == null)
            {

                repositoryOrderStatus.DeleteAllOrderStatuses(
                    repositoryOrderStatus.OrderStatuses.Where(
                        x => x.OrderSummaryID == orderStatusToUpdate.OrderSummaryID));
                return;
            }

            var selectedStatusesHS = new HashSet<string>(selectedOrderStatus);
            var orderSummaryStatus = new HashSet<int>(orderStatusToUpdate.OrderStatuses.Select(c => c.DimOrderStatusID));

            foreach (var status in repositoryDimOrderStatus.DimOrderStatuses)
            {
                if (selectedStatusesHS.Contains(status.DimOrderStatusID.ToString()))
                {
                    if (!orderSummaryStatus.Contains(status.DimOrderStatusID))
                    {
                        OrderStatus orderStatus = new OrderStatus
                            {
                                DimOrderStatusID = status.DimOrderStatusID,
                                OrderSummaryID = orderStatusToUpdate.OrderSummaryID
                            };

                        orderStatusToUpdate.OrderStatuses.Add(orderStatus);
                    }
                }
                else
                {
                    if (orderSummaryStatus.Contains(status.DimOrderStatusID))
                    {
                        int orderStatusId = orderStatusToUpdate.OrderStatuses
                                                               .Where(
                                                                   x =>
                                                                   x.DimOrderStatusID == status.DimOrderStatusID &&
                                                                   x.OrderSummaryID ==
                                                                   orderStatusToUpdate.OrderSummaryID)
                                                               .Select(x => x.OrderStatusID)
                                                               .Single();
                        repositoryOrderStatus.DeleteOrderStatus(orderStatusId);
                    }
                }
            }
        }


          [HttpGet]
          [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult ChangeOrderStatus(int orderId, string status, string header)
          {
              OrdersSummary os =
                  repositoryOrderSummary.OrdersSummaryInfo.FirstOrDefault(x => x.OrderSummaryID == orderId);
              //void ProcessOrder(Cart cart, ShippingDatails shippingDatails, OrdersSummary os, string subject);
              /*
                 public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int UserID {get; set;}
    }
               */
              //Cart cart = new Cart();
              Cart cart = new Cart();
              IEnumerable<CartLine> cartLine = from p in repositoryOrderDetails.OrdersDetails.Where(x => x.OrderSummaryID == orderId)
                           select new CartLine()
                               {
                                   Product = p.Product,
                                   Quantity = p.Quantity,
                                   UserID = p.UserID
                               };
              foreach (var line in cartLine)
              {
                    cart.AddItem(line.Product, line.Quantity, line.UserID);    
              }
              ShippingDatails shipping = new ShippingDatails()
                  {
                      ShippingAddress = os.UserAddress,
                      ShippingEmail = os.Email,
                      ShippingName = os.UserName,
                      ShippingPhone = os.Phone,
                      
                  };
              string subject = (header=="updated") ? "Изменение статуса заказа" : "Заказ выполнен" ;
              repositoryOrder.ProcessOrder(cart, shipping, os, subject, header, null);
              TempData["message"] = string.Format("Клиенту {0} было отправлено уведомление", shipping.ShippingName);
              TempData["messageType"] = "warning-msg";
              return RedirectToAction("Actions");
          }


          [HttpPost]
          [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult DeleteOrderDetail(int orderDetailsID, int orderSummaryID)
        {
            OrdersSummary os =
                repositoryOrderSummary.OrdersSummaryInfo.FirstOrDefault(p => p.OrderSummaryID == orderSummaryID);

            OrderDetails ord =
                repositoryOrderDetails.OrdersDetails.FirstOrDefault(p => p.OrderDetailsID == orderDetailsID);
            //int orderSummaryID = ord.OrderID;
            //ПЕресчет суммы покупки
            if (ord != null)
            {
                //возврат товара на склад
                Product product = repositoryProduct.Products.FirstOrDefault(x => x.ProductID == ord.ProductID);
                product.Quantity = product.Quantity + ord.Quantity;
                repositoryProduct.SaveProduct(product);

                repositoryOrderDetails.DeleteOrderDetail(ord);
                repositoryOrderSummary.RefreshTotalValue(os);
                TempData["message"] = string.Format("Подзаказ №{0} был удален", ord.OrderDetailsID);
                TempData["messageType"] = "warning-msg";
                logger.Warn(string.Format("{0}. Подзаказ №{1} был удален", User.Identity.Name, ord.OrderDetailsID));
            }

            if (repositoryOrderDetails.OrdersDetails.FirstOrDefault(p => p.OrderSummaryID == os.OrderSummaryID) == null)
            {
                repositoryOrderSummary.DeleteOrderSummary(os);
                return RedirectToAction("Orders");
            }
            return RedirectToAction("OrdersDetails", new {ord.OrderSummaryID});
        }





          public ActionResult AddProductIntoOrder(string searchWord, GridSortOptions gridSortOptions, int orderSummaryID, int? page)
          {
              string request = (string)Session["Requests"];

            //  Session["Requests"] = null;
              /*if (Request.IsAjaxRequest())
              {
                  return PartialView(request);
              }
              return PartialView(request);*/

            
            //   var tmp = repositoryProduct.Products;
            int pageItemsCount;

            try
            {
                pageItemsCount =
                    Int32.Parse(
                        repositoryDimSetting.DimSettings.Where(x => x.SettingsID == "ADMIN_PAGE_SIZE_Product")
                                            .Select(x => x.SettingsValue).Single());
            }
            catch (Exception)
            {
                logger.Warn("Отсутствует значение параметра ADMIN_PAGE_SIZE_Product в БД");
                pageItemsCount = 0;
            }



            var query = from a in repositoryProduct.Products
                        select new ProductOrderViewModel()
                            {
                                SelectedCategoryID = a.CategoryID,
                                ProductID = a.ProductID,
                                CategoryName = a.Category.Name,
                                Name = a.Name,
                                Price = a.Price,
                                Quantity = a.Quantity,
                                ShortName = a.ShortName,
                                Description = a.Description,
                                Sequence = a.Sequence,
                                IsInStock = false,
                                IsActive = a.IsActive,
                                IsDeleted = a.IsDeleted,
                                StartDate = a.StartDate,
                                UpdateDate = a.UpdateDate,
                                OldPrice = a.OldPrice,
                                OrderSummaryID = orderSummaryID
                            };

            var pagedViewModel = new PagedViewModel<ProductOrderViewModel>
                {
                    ViewData = ViewData,
                    Query = query, //repositoryProduct.Products, //repositoryProduct.Products,
                    GridSortOptions = gridSortOptions,
                    DefaultSortColumn = "Name",
                    Page = page,
                    PageSize = (pageItemsCount == 0) ? Domain.Constants.ADMIN_PAGE_SIZE : pageItemsCount,
                };
            int s = 0;
            pagedViewModel
                .AddFilter("searchWord", searchWord,
                           a =>
                           a.Name.Contains(searchWord) || a.ShortName.Contains(searchWord) ||
                           a.CategoryName.Contains(searchWord) || a.Description.Contains(searchWord))
                
                .Setup();

            if (Request.IsAjaxRequest())
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                return PartialView("AddProductIntoOrderPartial", pagedViewModel);
            }

            return View(pagedViewModel);
            //return View(repositoryProduct.Products);

        }

          public ActionResult AddProductIntoOrderAtLast(int orderId, int productId)
          {
              Product pr = repositoryProduct.Products.FirstOrDefault(x => x.ProductID == productId);
              OrdersSummary os =
                  repositoryOrderSummary.OrdersSummaryInfo.FirstOrDefault(x => x.OrderSummaryID == orderId);

              IEnumerable<OrderDetails> ods =
                  repositoryOrderDetails.OrdersDetails.Where(x => x.OrderSummaryID == orderId && x.ProductID==productId);

              if (ods.Count()==0)
              {
                  OrderDetails od = new OrderDetails()
                  {
                      OrderSummaryID = orderId,
                      Price = pr.Price,
                      ProductID = productId,
                      Quantity = 1,
                      UserID = os.UserID
                  };

                  pr.Quantity--;
                  repositoryProduct.SaveProduct(pr);
                  repositoryOrderDetails.CreateOrderDetails(od);    
              }
              

              

              return RedirectToAction("OrdersDetails",
                                      new {orderSummaryID = orderId});
          }

          /*    
          [HttpPost]
          public ActionResult AddProductIntoOrder(string request)
          {

              IEnumerable<Product> prs = repositoryProduct.Products.Where(x => x.Quantity == 10);
           //   return RedirectToAction("ProductSearchChoiceIntoOrderModal", request);

              Session["Requests"] = request;
              

              
              if (Request.IsAjaxRequest())
              {
                  return PartialView("AddProductIntoOrder");
              }
              return RedirectToAction("AddProductIntoOrder", request);

          }
          */
         // [HttpPost]
        /*  public ActionResult ProductSearchChoiceIntoOrderModal(string request)
          {
              IEnumerable<Product> prs = repositoryProduct.Products.Where(x => x.Quantity == 10);
              return PartialView(prs);

          }
          */
          /*[HttpPost]
          public ActionResult ProductSearchChoiceIntoOrderModal(Product product)
          {
              return null;

          }*/



          //-------------------------
        [HttpGet]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult EditOrderDetail(int orderDetailsID, int quantity)
        {
            OrderDetails od =
                repositoryOrderDetails.OrdersDetails.FirstOrDefault(p => p.OrderDetailsID == orderDetailsID);
            TempData["BaseQuantity"] = quantity;
            return View("EditOrderDetail", od);
        }


        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult EditOrderDetail(OrderDetails orderDetails)
        {
            //var orderSummaryAsync = repositoryOrderSummary.GetOrderSummaryByIDAsync(orderDetails.OrderSummaryID);
            var orderSummaryAsync = repositoryOrderSummary.OrdersSummaryInfo.FirstOrDefault(x=>x.OrderSummaryID==orderDetails.OrderSummaryID);
            //количество товаров изначально
            int orderDetailsBasic = (int)TempData["BaseQuantity"];
                /*repositoryOrderDetails.OrdersDetails.FirstOrDefault(x => x.OrderDetailsID == orderDetails.OrderDetailsID)
                                      .OrderDetailsID;*/
           
            
            /* int orderDetailsBasic =
                repositoryOrderDetails.OrdersDetails.FirstOrDefault(x => x.OrderDetailsID == orderDetails.OrderDetailsID).Quantity;*/

            //количество товаров на складе
            Product product =
                repositoryProduct.Products.FirstOrDefault(x => x.ProductID == orderDetails.ProductID);

            if (product.Quantity + orderDetailsBasic < orderDetails.Quantity)
            {
                //ModelState.AddModelError("", "Количество указанных товаров превышает количество на складе");
                TempData["message"] = string.Format("Превышено количество товара на складе");
                TempData["messageType"] = "warning-msg"; 
               // return View("",orderDetails);
                return RedirectToAction("OrdersDetails", new { orderDetails.OrderSummaryID });
            }

            //orderSummaryAsync.Wait();
            OrdersSummary os = orderSummaryAsync;//.Result;
            /*orderDetails.OrdersSummary = new OrdersSummary()
                {
                    OrderNumber = os.OrderNumber,
                    Email = os.Email,
                    IsActive = os.IsActive,
                    OrderSummaryID = os.OrderSummaryID,
                    OrdersDetails = os.OrdersDetails,
                    Phone = os.Phone,
                    ShippingPrice = os.ShippingPrice,
                    ShippingType = os.ShippingType,
                    TotalValue = os.TotalValue,
                    TransactionDate = os.TransactionDate,
                    UserAddress = os.UserAddress,
                    UserID = os.UserID,
                    UserName = os.UserName
                };
            orderDetails.Product = ViewBag.Product;*/

       /*     OrderDetails od =
                repositoryOrderDetails.OrdersDetails.FirstOrDefault(p => p.OrderDetailsID == orderDetails.OrderDetailsID);

            //orderDetails.OrdersSummary = od.OrdersSummary;
            orderDetails.Product = od.Product;*/

            /*ModelState.SetModelValue(); IsValidField("OrdersSummary") == true;
            ModelState.Add("OrdersSummary", new ModelState());
            ModelState.Add("Product", new ModelState());
            ModelState.SetModelValue("Product", null);
            ModelState.SetModelValue("OrdersSummary", null);*/

            OrderDetails od = new OrderDetails()
                {
                    OrderDetailsID = orderDetails.OrderDetailsID,
                    OrderSummaryID = orderDetails.OrderSummaryID,
                    UserID = orderDetails.UserID,
                    Price = orderDetails.Price,
                    ProductID = orderDetails.ProductID,
                    Quantity = orderDetails.Quantity
                };
            
            if (ModelState.IsValid)
            {
                product.Quantity = product.Quantity + orderDetailsBasic - orderDetails.Quantity; //od.Quantity;
                repositoryProduct.SaveProduct(product);
                //int orderSummaryID = orderDetails.OrderSummaryID;
                repositoryOrderDetails.SaveOrderDetails(orderDetails);
                repositoryOrderSummary.RefreshTotalValue(os);

                
                                    //od.Quantity

                TempData["message"] = string.Format("Подзаказ Заказа №{0} изменен", os.OrderNumber);
                TempData["messageType"] = "information-msg"; 
                logger.Warn(string.Format("{0}. Подзаказ Заказа №{1} был удален", User.Identity.Name, os.OrderNumber));
                return RedirectToAction("OrdersDetails", new {orderDetails.OrderSummaryID});
            }
            else
            {
                return View(orderDetails);
            }
        }


        #endregion




        #region ProductImage

          [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult Upload(HttpPostedFileBase imagefile, int ProductID)
        {
            try
            {
                if (imagefile == null)
                {
                    return RedirectToAction("EditProduct", new { ProductID });
                }

                if (imagefile != null)
                {
                    string strExtension = System.IO.Path.GetExtension(imagefile.FileName);

                    if (strExtension.ToLower() == ".gif" || strExtension.ToLower() == ".jpg" ||
                        strExtension.ToLower() == ".jpeg" || strExtension.ToLower() == ".png")
                    {



                        ProductImage obj = new ProductImage();
                        obj.ProductID = ProductID;
                        try
                        {
                            obj.Sequence =
                                ((repositoryProductImages.ProductImages.Where(x => x.ProductID == ProductID)
                                                         .Select(x => x.Sequence)
                                                         .Max() + 1) == null
                                     ? 1
                                     : repositoryProductImages.ProductImages.Where(x => x.ProductID == ProductID)
                                                              .Select(x => x.Sequence)
                                                              .Max() + 1);
                        }
                        catch (Exception)
                        {
                            obj.Sequence = 1;
                        }

                        obj.ImageExt = strExtension;
                        repositoryProductImages.SaveProductImage(obj);

                        string strSaveFileName = obj.ProductID.ToString() + "_" + obj.ProductImageID.ToString() +
                                                 strExtension;
                        string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                        Constants.PRODUCT_IMAGE_FOLDER, strSaveFileName);
                        string strSavePreviewFullPath = System.IO.Path.Combine(
                            Server.MapPath(Url.Content("~/Content")), Constants.PRODUCT_IMAGE_FOLDER,
                            Constants.PRODUCT_IMAGE_PREVIEW_FOLDER, strSaveFileName);

                        if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);
                        if (System.IO.File.Exists(strSavePreviewFullPath))
                            System.IO.File.Delete(strSavePreviewFullPath);

                        imagefile.ResizeAndSave(Constants.PRODUCT_IMAGE_HEIGHT, Constants.PRODUCT_IMAGE_WIDTH,
                                                strSaveFullPath);
                        imagefile.ResizeAndSave(Constants.PRODUCT_IMAGE_PREVIEW_HEIGHT,
                                                Constants.PRODUCT_IMAGE_PREVIEW_WIDTH, strSavePreviewFullPath);
                    }
                }
                Product product = repositoryProduct.Products.FirstOrDefault(x => x.ProductID == ProductID);
                return RedirectToAction("EditProduct", "Admin", new {productID = product.ProductID});
            }
            catch (Exception ex)
            {
                logger.Warn(User.Identity.Name + ex.Message);
                return null;
            }
        }

        // Удаление объекта без перенаправления на страницу подтверждения
        //[Authorize(Roles = Constants.ROLES_ADMIN_CONTENT_MANAGER)]
          [Authorize(Roles = "Admin, ContentManager")]
        public virtual ActionResult DeleteExpress(int id, int productId)
        {
              var productAsync = repositoryProduct.Products.FirstOrDefault(x=>x.ProductID==productId);
            ProductImage productImage = repositoryProductImages.ProductImages.FirstOrDefault(x => x.ProductImageID == id);
            if (productImage == null)
                return View("Error");
            else
            {
                

                string strSaveFileName = productId.ToString() + "_" + id.ToString() +
                                         repositoryProductImages.ProductImages.FirstOrDefault(
                                             x => x.ProductImageID == id).ImageExt;
                repositoryProductImages.DeleteProductImage(productImage);
                TempData["Message"] = string.Format("Изображение {0}_{1}.{2} было удалено", productImage.ProductID, productImage.ProductImageID, productImage.ImageExt);
                TempData["messageType"] = "warning-msg";
                string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                Constants.PRODUCT_IMAGE_FOLDER, strSaveFileName);
                string strSavePreviewFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                       Constants.PRODUCT_IMAGE_FOLDER,
                                                                       Constants.PRODUCT_IMAGE_PREVIEW_FOLDER,
                                                                       strSaveFileName);

                if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);
                if (System.IO.File.Exists(strSavePreviewFullPath)) System.IO.File.Delete(strSavePreviewFullPath);

                UpdateImageSequence(productId, false);
            }
            if (Request.IsAjaxRequest())
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                //productAsync.Wait();
                Product product = productAsync;//.Result;
                IEnumerable<Category> categoryList = repositoryCategory.Categories;
                IEnumerable<ProductImage> productImagesList = repositoryProductImages.ProductImages;
                ProductEditViewModel productEditViewModel = new ProductEditViewModel()
                    {
                        ProductID = product.ProductID,
                        Name = product.Name,
                        SelectedCategoryID = product.CategoryID,
                        Price = product.Price,
                        ArticleNumber = product.ArticleNumber,
                        Quantity = product.Quantity,
                        Description = product.Description,
                        Categories = categoryList,
                        ShortName = product.ShortName,
                        ProductImages = productImagesList,
                        StartDate = product.StartDate,
                        UpdateDate = product.UpdateDate,
                        Sequence = product.Sequence,
                        IsActive = product.IsActive,
                        IsDeleted = product.IsDeleted,
                        Keywords = product.Keywords,
                        Snippet = product.Snippet,
                        LastPriceChangeDate = product.LastPriceChangeDate,
                        OldPrice = product.OldPrice
                        //repositoryProductImages.ProductImages.FirstOrDefault(x => x.ProductID == product.ProductID)
                    };
                return PartialView("EditProductImages", productEditViewModel);
            }
            return RedirectToAction("EditProduct", "Admin", new {productID = productImage.ProductID});

        }


        public ActionResult UpdateImageSequence(int productId, bool every)
        {
            repositoryProductImages.UpdateSequence(productId, every);
            logger.Warn(string.Format("{0}. Последовательность изображений к товарам пересчитана", User.Identity.Name));
            return RedirectToAction("EditProduct", new {productId = productId});
        }

        public PartialViewResult ProductImageSequence(int productImageID, int productID, string actionType)
        {

            /*int productID =
                repositoryProductImages.ProductImages.FirstOrDefault(x => x.ProductImageID == productImageID).ProductID; */
            repositoryProductImages.ProductImageSequence(productImageID, productID, actionType);
            // return RedirectToAction("EditProduct", "Admin", new {productID = productID});
            //return RedirectToAction("EditProductImages"); 

            IEnumerable<Category> categoryList = repositoryCategory.Categories;
            IEnumerable<ProductImage> productImagesList =
                repositoryProductImages.ProductImages.Where(x => x.ProductID == productID);
            Product product = repositoryProduct.Products.FirstOrDefault(p => p.ProductID == productID);

            /*
             IEnumerable<ProductImage> productImagesList = repositoryProductImages.ProductImages;
            Product product = repositoryProduct.Products.FirstOrDefault(p => p.ProductID == productId);
             */
            ProductEditViewModel productEditViewModel = new ProductEditViewModel()
                {
                    ProductID = product.ProductID,
                    Name = product.Name,
                    SelectedCategoryID = product.CategoryID,
                    Price = product.Price,
                    ArticleNumber = product.ArticleNumber,
                    Quantity = product.Quantity,
                    Description = product.Description,
                    Categories = categoryList,
                    ShortName = product.ShortName,
                    ProductImages = productImagesList,
                    StartDate = product.StartDate,
                    UpdateDate = product.UpdateDate,
                    Sequence = product.Sequence,
                    IsActive = product.IsActive,
                    IsDeleted = product.IsDeleted,
                    Keywords = product.Keywords,
                    Snippet = product.Snippet,
                    LastPriceChangeDate = product.LastPriceChangeDate,
                    OldPrice = product.OldPrice
                    //repositoryProductImages.ProductImages.FirstOrDefault(x => x.ProductID == product.ProductID)
                };



            return PartialView("EditProductImages", productEditViewModel);

            //return PartialView("EditProductImages", productEditViewModel);
            //return RedirectToAction("EditProductImages", productEditViewModel);
        }

        /*productImageID = image.ProductImageID, @*productID = image.ProductID*@ editProductViewModel = @Model, actionType = "Up"*/

        public PartialViewResult EditProductImages(ProductEditViewModel productEditViewModel)
        {
            // Thread.Sleep(2000);
            return PartialView(productEditViewModel);
        }


        #endregion

        #region DimsRolesShippingsEtc

        public ActionResult DimsParameters()
        {
            return View();
        }

        #endregion

          #region DimOrderStatusRepository

        public PartialViewResult DimOrderStatus(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            IEnumerable<DimOrderStatus> ds = repositoryDimOrderStatus.DimOrderStatuses.ToList();
            try
            {
                int tmp =
                    ds.FirstOrDefault(x => x.OrderStatusDesc == "Заказ выполнен")
                                            .DimOrderStatusID;
            }
            catch (Exception ex)
            {
                DimOrderStatus dimOrderStatus = new DimOrderStatus()
                    {
                        OrderStatusDesc = "Заказ выполнен"
                    };
                repositoryDimOrderStatus.SaveDimOrderStatus(dimOrderStatus);
                logger.Warn(string.Format("{0}. Статус 'Заказ выполнен' пересоздан", User.Identity.Name));
            }

            var pagedViewModel = new PagedViewModel<DimOrderStatus>
                {
                    ViewData = ViewData,
                    Query = ds.AsQueryable(), //_service.GetAlbumsView(),
                    GridSortOptions = gridSortOptions,
                    DefaultSortColumn = "Sequence",
                    Page = page,
                    PageSize = 200 //Domain.Constants.ADMIN_PAGE_SIZE,
                }
                .Setup();
            return PartialView(pagedViewModel);
        }

          [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult CreateDimOrderStatus()
        {
            return PartialView("EditDimOrderStatus", new DimOrderStatus());
        }

          [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult EditDimOrderStatus(int dimOrderStatusId)
        {

            DimOrderStatus dimOrderStatus = repositoryDimOrderStatus.DimOrderStatuses
                                                                    .FirstOrDefault(
                                                                        p => p.DimOrderStatusID == dimOrderStatusId);
            return PartialView(dimOrderStatus);

        }



        [HttpPost]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult EditDimOrderStatus(DimOrderStatus dimOrderStatus)
        {
            if ((ModelState.IsValid) &&
                (repositoryDimOrderStatus.DimOrderStatuses.FirstOrDefault(
                    x => x.OrderStatusDesc.TrimEnd() == dimOrderStatus.OrderStatusDesc.TrimEnd()) == null))
            {

                repositoryDimOrderStatus.SaveDimOrderStatus(dimOrderStatus);
                TempData["message"] = string.Format("Статус заказа '{0}' сохранен", dimOrderStatus.OrderStatusDesc);
                TempData["messageType"] = "information-msg"; 
                logger.Warn(string.Format("{0}. Статус заказа {1} сохранен", User.Identity.Name, dimOrderStatus.OrderStatusDesc));
                // return the user to the list
                //return RedirectToAction("Categories");
                //return RedirectToAction("EditDimOrderStatus", new { dimOrderStatusId = dimOrderStatus.DimOrderStatusID });
                //return RedirectToAction("DimOrderStatus");
                //return JavaScript("window.location.replace('http://localhost:57600/Admin/DimOrderStatus');");
                //return Json(JsonStandardResponse.SuccessResponse(true));
                return RedirectToAction("DimsParameters");
            }
            else
            {
                // there is something wrong with the data values
                TempData["message"] = string.Format("{0} уже существует в базе! Изменения не внесены",
                                                    dimOrderStatus.OrderStatusDesc);
                TempData["messageType"] = "warning-msg"; 
                //return View(dimOrderStatus);
                return Json(JsonStandardResponse.ErrorResponse(string.Format("Статус {0}  - уже существует в базе! Изменения не внесены",
                                                    dimOrderStatus.OrderStatusDesc)), JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult DeleteDimOrderStatus(int dimOrderStatusId)
        {
            DimOrderStatus dimOrderStatus =
                repositoryDimOrderStatus.DimOrderStatuses.FirstOrDefault(p => p.DimOrderStatusID == dimOrderStatusId);

            if (dimOrderStatus != null)
            {
                repositoryDimOrderStatus.DeleteDimOrderStatus(dimOrderStatus);
                TempData["message"] = string.Format("Статус заказа '{0}' был удален", dimOrderStatus.OrderStatusDesc);
                TempData["messageType"] = "warning-msg"; 
                logger.Warn(string.Format("{0}. Статус заказа {1} удален", User.Identity.Name, dimOrderStatus.OrderStatusDesc));
            }
            return RedirectToAction("DimsParameters");
        }


        public ActionResult UpdateDimOrderStatusSequence()
        {
            repositoryDimOrderStatus.UpdateDimOrderStatusSequence();

            return RedirectToAction("DimOrderStatus");
        }

        public ActionResult DimOrderStatusSequence(int dimOrderStatusId, string actionType)
        {
            try
            {
                Exception ex = new Exception();
                int[] sequence =
                    repositoryDimOrderStatus.DimOrderStatuses.Select(x => x.Sequence).ToArray();
                Array.Sort(sequence);
                //sequence.OrderBy(x => x.ToString());

                for (int i = 0; i < sequence.Count(); i++)
                {
                    if (sequence[i] == i + 1)
                    {

                    }
                    else
                    {
                        throw (ex);
                    }
                }

                repositoryDimOrderStatus.DimOrderStatusSequence(dimOrderStatusId, actionType);
            }

            catch (Exception)
            {
                TempData["message"] = string.Format("Нарушена последовательность! Список был пересчитан!");
                TempData["messageType"] = "error-msg"; 
                repositoryDimOrderStatus.UpdateDimOrderStatusSequence();
                repositoryDimOrderStatus.DimOrderStatusSequence(dimOrderStatusId, actionType);

            }

            GridSortOptions gridSortOptions = new GridSortOptions()
                {
                    Column = "Sequence",
                    Direction = SortDirection.Ascending
                };


            var pagedViewModel = new PagedViewModel<DimOrderStatus>()
                {
                    ViewData = ViewData,
                    Query = repositoryDimOrderStatus.DimOrderStatuses, //_service.GetAlbumsView(),
                    GridSortOptions = gridSortOptions,
                    DefaultSortColumn = "Sequence",
                    Page = 1,
                    PageSize = Domain.Constants.ADMIN_PAGE_SIZE
                }
                .Setup();

            return PartialView("DimOrderStatusPartial", pagedViewModel);

        }





        /*
         void DimOrderStatusSequence(int dimOrderStatusId, string actionType);

        void UpdateSequence(int dimOrderId);  
                
                [HttpPost]
                public ActionResult EditCategory(Category category)
                {
                    if ((ModelState.IsValid) && (repositoryCategory.Categories.FirstOrDefault(x => x.Name.TrimEnd() == category.Name.TrimEnd()) == null))
                    {
                    
                     repositoryCategory.SaveCategory(category);
                     repositoryCategory.RefreshAllShortNames();
                        // add a message to the viewbag
                        TempData["message"] = string.Format("{0} сохранен", category.Name);
                        // return the user to the list
                        //return RedirectToAction("Categories");
                        return RedirectToAction("EditCategory", new {categoryId=category.CategoryID});
                    }
                    else
                    {
                        // there is something wrong with the data values
                        TempData["message"] = string.Format("{0} уже существует в базе! Изменения не внесены", category.Name);
                        return View(category);
                    }
                }
        
                [HttpPost]
                public ActionResult DeleteCategory(int categoryId)
                {
                    Category category = repositoryCategory.Categories.FirstOrDefault(p => p.CategoryID == categoryId);

                    if (category != null)
                    {
                        repositoryCategory.DeleteCategory(category);
                        TempData["message"] = string.Format("{0} был удален", category.Name);
                    }
                    return RedirectToAction("Categories");
                }


                public ActionResult RefreshAllShortNamesInCategories()
                {
                    repositoryCategory.RefreshAllShortNames();
                    return RedirectToAction("Categories");
                }

         */



        #endregion


        #region NewsTape
          [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult CreateNews(int newsId=0)
        {
            /*
            var pagedViewModel = new PagedViewModel<NewsTape>
            {
                ViewData = ViewData,
                Query = repositoryNewsTape.NewsTapes.OrderByDescending(x=>x.NewsDate),
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "NewsDate",
                Page = page,
                PageSize = Domain.Constants.ADMIN_PAGE_SIZE,
            }*/
            /*.AddFilter("searchWord", searchWord,
                       a =>
                       a.OrderNumber == sw
                           ||
                       a.Phone.Contains(searchWord) || a.UserName.Contains(searchWord) ||
                       a.UserAddress.Contains(searchWord))
            .AddFilter("startDate", Convert.ToDateTime(startDate), a => a.TransactionDate >= dStart)
            .AddFilter("endDate", Convert.ToDateTime(endDate), a => a.TransactionDate <= dEnd)
            .AddFilter("isActive", isActive, a => a.IsActive == isActive)
            .Setup();*/


            if (newsId == 0)
            {
                NewsTape newsTape = new NewsTape();
                newsTape.NewsDate = DateTime.Now;
                return PartialView(newsTape);    
            }
            else
            {
                NewsTape newsTape = repositoryNewsTape.NewsTapes.FirstOrDefault(x => x.NewsID==newsId);
                return PartialView(newsTape);
            }
           
        }


        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult CreateNews(NewsTape newsTape)
        {
            if (ModelState.IsValid)
            {
                repositoryNewsTape.SaveNewsTape(newsTape);
                logger.Warn(string.Format("{0}. Новость {1} сохранена", User.Identity.Name, newsTape.NewsID ));
                return View("NewsList");
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("CreateNews", newsTape);
                }
                return RedirectToAction("NewsList");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult DeleteNews(int? NewsID)
        {
            if (NewsID==0 || NewsID==null)
            {
                return RedirectToAction("NewsList");
            }
            NewsTape newsTape = repositoryNewsTape.NewsTapes.FirstOrDefault(x => x.NewsID == NewsID);
            if (newsTape == null)
            {
                return RedirectToAction("NewsList");
            }
            if (newsTape.NewsID==0)
            {
                return View("NewsList");
            }
            repositoryNewsTape.DeleteNewsTape(newsTape);
            logger.Warn(string.Format("{0}. Новость {1} удалена", User.Identity.Name, newsTape.NewsID));
            string strExtension = System.IO.Path.GetExtension(newsTape.ImgPath);
            string strSaveFileName = newsTape.NewsID + strExtension;
            string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                   Constants.NEWS_MINI_IMAGES_FOLDER,
                                                                   strSaveFileName);
            /*
             * string strExtension = System.IO.Path.GetExtension(imagefile.FileName);
                    string strSaveFileName = newsId + strExtension;
                    string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                    Constants.NEWS_MINI_IMAGES_FOLDER,
                                                                    strSaveFileName);
             */

            if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);

            return RedirectToAction("NewsList");
        }

          [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult NewsList(string searchWord, GridSortOptions gridSortOptions, int? page, bool isActive = true)
        {
            TryToUpdateDimSettings("ADMIN_PAGE_SIZE",
                       "Количество элементов на странице в админке",
                       "ADMIN_PAGE_SIZE_NewsTape",
                       "Количество элементов на странице в административном разделе Новости");

            var pagedViewModel = new PagedViewModel<NewsTape>
            {
                ViewData = ViewData,
                Query = repositoryNewsTape.NewsTapes,
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "NewsDate",
                Page = page,
                PageSize = Domain.Constants.ADMIN_PAGE_SIZE,
            }
    /*.AddFilter("searchWord", searchWord,
               a =>
               a.OrderNumber == sw
                   ||
               a.Phone.Contains(searchWord) || a.UserName.Contains(searchWord) ||
               a.UserAddress.Contains(searchWord))
    .AddFilter("startDate", Convert.ToDateTime(startDate), a => a.TransactionDate >= dStart)
    .AddFilter("endDate", Convert.ToDateTime(endDate), a => a.TransactionDate <= dEnd)
    .AddFilter("isActive", isActive, a => a.IsActive == isActive)*/
    .Setup();
            
            if (Request.IsAjaxRequest())
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                return PartialView("NewsListGridPartial",pagedViewModel); 
            }

            return View(pagedViewModel);

            /*IEnumerable<NewsTape> allNews = repositoryNewsTape.NewsTapes.OrderByDescending(x => x.NewsDate);
            return PartialView(allNews);*/
        }

[Authorize(Roles = "Admin, ContentManager")]
        public ActionResult UploadNewsImage(HttpPostedFileBase imagefile, int newsId)
        {
            if (imagefile == null)
            {
                return RedirectToAction("CreateNews", new { newsId });
            }

            NewsTape obj = repositoryNewsTape.NewsTapes.FirstOrDefault(x => x.NewsID == newsId);
            if (obj == null) return Content("NotFound"); //View("NotFound");
            try
            {
                if (imagefile != null)
                {
                    // Определяем название конечного графического файла вместе с полным путём.
                    // Название файла должно быть такое же, как ID объекта. Это гарантирует уникальность названия.
                    // Расширение должно быть такое же, как расширение у исходного графического файла.
                    string strExtension = System.IO.Path.GetExtension(imagefile.FileName);
                    string strSaveFileName = newsId + strExtension;
                    string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                    Constants.NEWS_MINI_IMAGES_FOLDER,
                                                                    strSaveFileName);

                    // Если файл с таким названием имеется, удаляем его.
                    if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);
                    
                    imagefile.ResizeAndSave(Constants.NEWS_MINI_IMAGE_HEIGHT, Constants.NEWS_MINI_IMAGE_WIDTH,
                                            strSaveFullPath);
                    obj.ImgPath = strExtension;

                    repositoryNewsTape.SaveNewsTape(obj);
                    
                }
            }
            catch (Exception ex)
            {
                string strErrorMessage = ex.Message;
                if (ex.InnerException != null)
                    strErrorMessage = string.Format("{0} --- {1}", strErrorMessage, ex.InnerException.Message);
                logger.Error(string.Format("{0}. Ошибка при загрузке изображения. {1} ", User.Identity.Name, ex.Message));
                ViewBag.ErrorMessage = strErrorMessage;
                return View("Error");
            }

            //return View("", ""); //ReturnToObject(obj);
            //return RedirectToAction("Categories", "Admin");
            return RedirectToAction("NewsList", "Admin", new { newsId });
        }




        #endregion


        #region Roles
          [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult UserRoles(string searchWord, GridSortOptions gridSortOptions, int? page, int? roleId)
          {

              var query = from ur in repositoryUser.UserRoles
                          select new UserRoleViewModel
                              {
                                  Login = ur.User.Login,
                                  RoleName = ur.Role.RoleName,
                                  UserName = ur.User.UserName,
                                  Email = ur.User.Email,
                                  Phone = ur.User.Phone,
                                  Created = ur.User.Created,
                                  IsActive = ur.User.IsActive,
                                  UserRoleID = ur.UserRoleID,
                                  SelectedRoleID = ur.RoleID //,
                                  //RoleID = ur.RoleID
                              };

             
              var pagedViewModel = new PagedViewModel<UserRoleViewModel>
              {
                  ViewData = ViewData,
                  Query = query, //repositoryUser.UserRoles, //_service.GetAlbumsView(),
                  GridSortOptions = gridSortOptions,
                  DefaultSortColumn = "RoleName",
                  Page = page,
                  PageSize = 20 //(pageItemsCount == 0) ? Domain.Constants.ADMIN_PAGE_SIZE : pageItemsCount
                  // Domain.Constants.ADMIN_PAGE_SIZE,
              }

                .AddFilter("searchWord", searchWord,
                           a =>
                           a.UserName.Contains(searchWord) || a.Email.Contains(searchWord))
                
                  //.AddFilter("userActivity", userActivity, a => a.IsActive == IsActive) //,  _service.GetGenres(), "Name")
                  //.AddFilter("roleId", roleId, a => a.UserRoleID == roleId)  
                 .AddFilter("roleId", roleId, a => a.SelectedRoleID == roleId,
                           repositoryUser.Roles, "RoleName")
                .Setup();
              /*
              if (Request.IsAjaxRequest())
              {
                  return PartialView("UserGridPartial", pagedViewModel);
              }*/
              if (Request.IsAjaxRequest())
              {
                  Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                  return PartialView("UserRolesGridPartial", pagedViewModel);
              }

              return View(pagedViewModel);
          }


          public ActionResult Roles(GridSortOptions gridSortOptions, int? page)
          {

              var pagedViewModel = new PagedViewModel<Role>
              {
                  ViewData = ViewData,
                  Query = repositoryUser.Roles, //_service.GetAlbumsView(),
                  GridSortOptions = gridSortOptions,
                  DefaultSortColumn = "RoleName",
                  Page = page,
                  PageSize = 20 //(pageItemsCount == 0) ? Domain.Constants.ADMIN_PAGE_SIZE : pageItemsCount
                  // Domain.Constants.ADMIN_PAGE_SIZE,
              }
              
                .Setup();
              if (Request.IsAjaxRequest())
              {
                  Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                  return PartialView("RolesGridPartial", pagedViewModel);
              }
              return PartialView(pagedViewModel);
          }

          [HttpPost]
          [Authorize(Roles = "Admin, ContentManager")]
          public ActionResult DeleteRole(int roleId)
          {
              Role r = repositoryUser.Roles.FirstOrDefault(x => x.RoleID == roleId);

              if (r.RoleName == "User" || r.RoleName == "Admin")
              {
                  TempData["message"] = string.Format("Данную роль нельзя удалять");
                  TempData["messageType"] = "error-msg"; 
                  return RedirectToAction("DimsParameters");
              }
              else
              {
                  string[] ur = repositoryUser.UserRoles.Where(x => x.RoleID == roleId).Select(x=>x.User.Login).ToArray();
                  foreach (var userRole in ur)
                  {
                      repositoryUser.AddUserToRole(userRole, "User");
                  }
                  repositoryUser.DeleteRole(r);
                  TempData["message"] = string.Format("Роль {0} была удалена", r.RoleName);
                  TempData["messageType"] = "warning-msg";
                  logger.Warn(string.Format("{0}. Роль {1} удалена", User.Identity.Name, r.RoleName));
              }
              //return RedirectToAction("Roles");

              GridSortOptions gridSortOptions = new GridSortOptions()
              {
                  Column = "RoleName",
                  Direction = SortDirection.Ascending
              };

              var pagedViewModel = new PagedViewModel<Role>
              {
                  ViewData = ViewData,
                  Query = repositoryUser.Roles, //_service.GetAlbumsView(),
                  GridSortOptions = gridSortOptions,
                  DefaultSortColumn = "RoleName",
                  Page = 1,
                  PageSize = 20 //(pageItemsCount == 0) ? Domain.Constants.ADMIN_PAGE_SIZE : pageItemsCount
                  // Domain.Constants.ADMIN_PAGE_SIZE,
              }
          .Setup();
              if (Request.IsAjaxRequest())
              {
                  Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                  return PartialView("RolesGridPartial", pagedViewModel);    
              }
              return View("DimsParameters");
          }

          [Authorize(Roles = "Admin, ContentManager")]
          public ActionResult EditRole(int? roleId)
          {
              if (roleId!=null)
              {
                  Role role = repositoryUser.Roles.FirstOrDefault(x => x.RoleID == roleId);

                  return PartialView(role);
              }
              return PartialView();
          }


          [HttpPost]
          [Authorize(Roles = "Admin, ContentManager")]
          public ActionResult EditRole(Role role)
          {
              if (ModelState.IsValid)
              {
                  if (role.RoleID!=0)
                  {
                      var existedRole = repositoryUser.Roles.FirstOrDefault(x => x.RoleName == role.RoleName);
                      if (existedRole==null)
                      {
                          repositoryUser.SaveRole(role);    
                      }
                      
                  }
                  else
                  {
                      repositoryUser.CreateRole(role.RoleName);
                      logger.Warn(string.Format("{0}. Заказ №{1} создана", User.Identity.Name, role.RoleName));
                  }
                  /*GridSortOptions gridSortOptions = new GridSortOptions()
                      {
                          Column = "RoleName",
                          Direction = SortDirection.Ascending
                      };

                  var pagedViewModel = new PagedViewModel<Role>
                  {
                      ViewData = ViewData,
                      Query = repositoryUser.Roles, //_service.GetAlbumsView(),
                      GridSortOptions = gridSortOptions,
                      DefaultSortColumn = "RoleName",
                      Page = 1,
                      PageSize = 20 //(pageItemsCount == 0) ? Domain.Constants.ADMIN_PAGE_SIZE : pageItemsCount
                      // Domain.Constants.ADMIN_PAGE_SIZE,
                  }

              .Setup();*/
                  //return Json(JsonStandardResponse.SuccessResponse(true), JsonRequestBehavior.DenyGet);
                  // return PartialView("RolesGridPartial", pagedViewModel);
                  return View("DimsParameters");
                  // return RedirectToAction("DimsParameters", gridSortOptions) ;
              }
              return Json(JsonStandardResponse.ErrorResponse("Херакс"), JsonRequestBehavior.DenyGet);
              //return View(role);
          }

          public ActionResult EditUserRole(int userRoleId)
          {
              UserRole ur = repositoryUser.UserRoles.FirstOrDefault(x => x.UserRoleID == userRoleId);

              IEnumerable<Role> roleList = repositoryUser.Roles;
              
              UserRoleViewModel viewModel = new UserRoleViewModel()
                  {
                      Login = ur.User.Login,
                      RoleName = ur.Role.RoleName,
                      Created = ur.User.Created,
                      Phone = ur.User.Phone,
                      IsActive = ur.User.IsActive,
                      Email = ur.User.Email,
                      UserName = ur.User.UserName,
                      UserRoleID = ur.UserRoleID,
                      SelectedRoleID = ur.RoleID,
                      Roles = roleList//,
                    //  RoleID = ur.RoleID,
                   //   UserID = ur.UserID
                  };

              return PartialView(viewModel);
          }

          [HttpPost]
          [Authorize(Roles = "Admin, ContentManager")]
          public ActionResult EditUserRole(UserRoleViewModel viewModel)
          {
              if (ModelState.IsValid)
              {
            //      UserRole ur = repositoryUser.UserRoles.FirstOrDefault(x => x.UserRoleID == viewModel.UserRoleID);
                  string role = repositoryUser.Roles.FirstOrDefault(x => x.RoleID == viewModel.SelectedRoleID).RoleName;
                  //ur.RoleID = viewModel.RoleID;
                  //repositoryUser.RemoveUserFromRole(viewModel.Login, ur.Role.RoleName);
                  repositoryUser.AddUserToRole(viewModel.Login, role);
                  logger.Warn(string.Format("{0}. Пользователь '{1}' добавлен в роль {2}", User.Identity.Name, viewModel.Login, role));
                  TempData["Message"] = string.Format("Пользователь '{0}' добавлен в роль {1}", viewModel.Login, role);
                  TempData["messageType"] = "information-msg";
                  //return Json(JsonStandardResponse.SuccessResponse(true)); 
                  //return View("UserRoles"); //RedirectToAction("UserRoles", "Admin");
                  //return RedirectToAction("Actions");
                  //return JavaScript("window.location.replace('http://localhost:57600/Admin/DimOrderStatus');");
                  return JavaScript("window.location.reload();");
              }
              //return PartialView(viewModel);
              return Json(JsonStandardResponse.ErrorResponse(string.Format("Возникла ошибка! Изменения не внесены")), JsonRequestBehavior.DenyGet);
          }





          public ActionResult UpdateUserRole()
          {
              Role roleExists = repositoryUser.Roles.FirstOrDefault(x => x.RoleName == "User");
              if (roleExists == null)
              {
                  repositoryUser.CreateRole("User");
              }


              int[] tmp = repositoryUser.UserRoles.Select(x => x.UserID).ToArray();
              int[] tmp2 = repositoryUser.UsersInfo.Select(x => x.UserID).ToArray();
              tmp2 = tmp2.Except(tmp).ToArray();

              foreach (var userId in tmp2)
              {
                  string login = repositoryUser.UsersInfo.FirstOrDefault(x => x.UserID == userId).Login;
                   repositoryUser.AddUserToRole(login, "User");
              }
              
              


              return RedirectToAction("UserRoles");
          }
          /*
           * 
           * 
           *  IEnumerable<string> productCategories = from c in repositoryCategory.Categories.ToList()
                                                        join p in repositoryProduct.Products.ToList() on c.CategoryID
                                                            equals
                                                            p.CategoryID
                                                        group c by new {c.CategoryID, c.ShortName}
                                                        into tmp
                                                        select tmp.Key.ShortName;

                IEnumerable<string> categoriesExists = from c in repositoryCategory.Categories.ToList()
                                                       select c.ShortName;


                IQueryable<string> difference = categoriesExists.Except(productCategories).AsQueryable();


           * 
           var role = context.Roles.FirstOrDefault(x => x.RoleName == "admin");
                if (role != null)
                {
                    var userRole = context.UserRoles.FirstOrDefault(x => x.RoleID == role.RoleID);
                    var user = context.Users.Where(x => x.UserID == userRole.UserID);
                    
                    if (user.Count() == 0)
                    {
                        return false;
                    }
                    return true;
           */
          /*
           public ActionResult UsersView(string searchWord, GridSortOptions gridSortOptions, int? page,
                                      string userActivity = "Активные")
        {
            bool IsActive;

            //"-- Все --", "Активные", "Неактивные"
            // from c in repositoryCategory.Categories.ToList() select c.ShortName;
            //IEnumerable<string> s1 = repositoryDimSetting.DimSettings.Select(x => x.SettingsID).ToList();
            //repositoryDimSetting.DimSettings.ToList();
            int pageItemsCount;

            TryToUpdateDimSettings("ADMIN_PAGE_SIZE",
                                   "Количество элементов на странице в админке",
                                   "ADMIN_PAGE_SIZE_User",
                                   "Количество элементов на странице в административном разделе Клиенты");


            try
            {
                pageItemsCount =
                    Int32.Parse(
                        repositoryDimSetting.DimSettings.Where(x => x.SettingsID == "ADMIN_PAGE_SIZE_User")
                                            .Select(x => x.SettingsValue).Single());
            }
            catch (Exception)
            {
                pageItemsCount = 0;
            }




            var pagedViewModel = new PagedViewModel<User>
                {
                    ViewData = ViewData,
                    Query = repositoryUser.UsersInfo, //_service.GetAlbumsView(),
                    GridSortOptions = gridSortOptions,
                    DefaultSortColumn = "Login",
                    Page = page,
                    PageSize = (pageItemsCount == 0) ? Domain.Constants.ADMIN_PAGE_SIZE : pageItemsCount
                    // Domain.Constants.ADMIN_PAGE_SIZE,
                }

                .AddFilter("searchWord", searchWord,
                           a =>
                           a.UserName.Contains(searchWord) || a.Phone.Contains(searchWord) ||
                           a.Email.Contains(searchWord))
                //.AddFilter("userActivity", userActivity, a => a.IsActive == IsActive) //,  _service.GetGenres(), "Name")
                .AddFilter("userActivity", (userActivity == "Активные") ? IsActive = true : IsActive = false,
                           a => a.IsActive == IsActive) //,  _service.GetGenres(), "Name")
                
                .Setup();
            

            if (Request.IsAjaxRequest())
            {
                return PartialView("UserGridPartial", pagedViewModel);
            }

            return View(pagedViewModel);
            //return View(repositoryUser.UsersInfo);
        }

           */


        #endregion

        /*
        public ActionResult OrderStatus(int orderSummaryId)
        {

            PopulateAssignedStatus(orderSummaryId);

            return PartialViewResult();
        }
        */

        //формирует модель с отмеченными статусами заказа
        private void PopulateAssignedStatus(int orderSummaryId)
        {
            //OrdersSummary os = repositoryOrderSummary.OrdersSummaryInfo.FirstOrDefault(x=>x.OrderSummaryID==orderSummaryId);
            OrdersSummary os = repositoryOrderSummary.OrdersSummaryInfo.FirstOrDefault(x => x.OrderSummaryID == orderSummaryId);
            var allStatuses = repositoryDimOrderStatus.DimOrderStatuses.ToList();
            //var orderSummaryStatuses = new HashSet<int>(os.DimOrderStatuses.Select(c => c.DimOrderStatusID));
            var orderSummaryStatuses = new HashSet<int>(os.OrderStatuses.Select(c => c.DimOrderStatusID));
            var viewModel = new List<AssignedStatusViewModel>();
            foreach (var status in allStatuses)
            {
                viewModel.Add(new AssignedStatusViewModel
                    {
                     Assigned   = orderSummaryStatuses.Contains(status.DimOrderStatusID),
                     DimOrderStatusID = status.DimOrderStatusID,
                     DimOrderStatusDesc = status.OrderStatusDesc
                    });
            }
           // ViewBag.Statuses = viewModel;
            TempData["Statuses"] = viewModel;
        }

          
#region Settings
        /*
        [HttpGet]
        public ActionResult Settings(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            var pagedViewModel = new PagedViewModel<DimSetting>
            {
                ViewData = ViewData,
                Query = repositoryDimSetting.DimSettings,//_service.GetAlbumsView(),
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "SettingTypeID",
                Page = page,
                PageSize = Domain.Constants.ADMIN_PAGE_SIZE,
            }
            .Setup();

            ViewBag.Model = pagedViewModel;

            if (Request.IsAjaxRequest())
            {
                return PartialView("DimSettingPartial", pagedViewModel);
            }
            return View(pagedViewModel);
        }

        [HttpPost]
        public ActionResult EditSettings(DimSetting dr)
        {
            DimSetting dr2 = repositoryDimSetting.DimSettings.Where(x => x.SettingsID == dr.SettingsID).Single();

            dr2.SettingsValue = dr.SettingsValue;

            repositoryDimSetting.SaveDimSetting(dr2, false);


            GridSortOptions gridSortOptions = new GridSortOptions()
                {
                    Column = "SettingTypeID",
                    Direction = SortDirection.Ascending
                };


            var pagedViewModel = new PagedViewModel<DimSetting>
            {
                ViewData = ViewData,
                Query = repositoryDimSetting.DimSettings,//_service.GetAlbumsView(),
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "SettingTypeID",
                Page = 1,
                PageSize =1000 //Domain.Constants.ADMIN_PAGE_SIZE,
            }
            .Setup();


            if (Request.IsAjaxRequest())
            {
                return PartialView("DimSettingPartial", pagedViewModel);
            }
            return RedirectToAction("Settings");
        }

*/

        public ActionResult DimSettings()
        {
            IEnumerable<DimSettingType> ds = repositoryDimSettingType.DimSettingTypes;
            return PartialView(ds);
        }



        public ActionResult Options(string settingTypeId)
        {
            var settings = repositoryDimSetting.DimSettings.Where(x => x.SettingTypeID.TrimEnd() == settingTypeId.TrimEnd());
            return View(settings);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult Options(DimSetting setting)
        {
            DimSetting ds = repositoryDimSetting.DimSettings.Where(x => x.SettingsID == setting.SettingsID).Single();
            ds.SettingsValue = setting.SettingsValue;
            repositoryDimSetting.SaveDimSetting(ds, false);
            logger.Warn(string.Format("{0}. Настройка {1} модифицирован", User.Identity.Name, ds.SettingsDesc));
            return RedirectToAction("Options", new { ds.SettingTypeID });
        }








        #endregion Settings


         public void TryToUpdateDimSettings(string settingTypeID, string settingTypeDesc, string settingsID, string settingsDesc)
        {
            if (String.IsNullOrEmpty(
                repositoryDimSetting.DimSettings.Where(x => x.SettingsID == settingsID)
                                    .Select(x => x.SettingsValue)
                                    .SingleOrDefault()))
            {
            DimSetting ds = new DimSetting();    
                        if (String.IsNullOrEmpty(repositoryDimSettingType.DimSettingTypes.Where(x => x.SettingTypeID == settingTypeID).
                            Select(x=>x.SettingTypeID).SingleOrDefault()))
                        {
                            DimSettingType dt = new DimSettingType()
                                {
                                    SettingTypeID = settingTypeID,
                                    SettingTypeDesc = settingTypeDesc
                                };
                        //    ViewBag.dt = dt;
                            repositoryDimSettingType.SaveDimSettingType(dt, true);
                        
                        }
                        
                        ds.SettingsID = settingsID;
                        ds.SettingTypeID = settingTypeID;
                        ds.SettingsDesc = settingsDesc;
                        ds.SettingsValue = Constants.ADMIN_PAGE_SIZE.ToString();
                            repositoryDimSetting.SaveDimSetting(ds, true);
            }
        }


        //import из Excel


        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult UploadExcel()
        {
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult UploadExcel(HttpPostedFileBase uploadFile)
        {
            StringBuilder strValidations = new StringBuilder(string.Empty);
            try
            {
                if (uploadFile.ContentLength > 0)
                {
                    string filePath = Path.Combine(HttpContext.Server.MapPath("../Content/Upload"),
                   Path.GetFileName(uploadFile.FileName));
                    uploadFile.SaveAs(filePath);
                    DataSet ds = new DataSet();
                    string fileExt = uploadFile.FileName;

                    fileExt = fileExt.Remove(0, fileExt.LastIndexOf(".")+1);
                    string ConnectionString = "";
                    if (fileExt.ToUpper() == "XLSX")
                    {
                        ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;";
                    }
                    else if (fileExt.ToUpper() == "XLS")
                    {
                        ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=Excel 8.0;"; 
                    }
                    //if (fileName.split(".")[1].toUpperCase() == "XLS" || fileName.split(".")[1].toUpperCase() == "XLSX")
                    //A 32-bit provider which enables the use of
                    /*
                                     if (fileName == "") {
                            alert("Browse to upload a valid File with xls / xlsx extension");
                            return false;
                        }
                        else if (fileName.split(".")[1].toUpperCase() == "XLS" || fileName.split(".")[1].toUpperCase() == "XLSX")
                            return true;
                        else {
                            alert("File with " + fileName.split(".")[1] + " is invalid. Upload a validfile with xls / xlsx extensions");
                            return false;
                        }
                     */
                    //string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;";
                    //string ConnectionString ="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=""Excel 8.0;"; 
                    using (OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ConnectionString))
                    {
                        conn.Open();
                        using (DataTable dtExcelSchema = conn.GetSchema("Tables"))
                        {
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            string query = "SELECT * FROM [" + sheetName + "]";
                            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                            //DataSet ds = new DataSet();
                            adapter.Fill(ds, "Items");

                            int succesInsertedProduct = 0;
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    string[] s = new string[ds.Tables[0].Rows.Count];

                                    string[] rejectProducts = new string[ds.Tables[0].Rows.Count];
                                   for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                   {
                                       
                                        //Now we can insert this data to database...
                                       
                                       s[i] = Convert.ToString(ds.Tables[0].Rows[i][0]);
                                       //поиск и добавление новой категории 
                                       string[] allCategories = repositoryCategory.Categories.Select(x => x.Name).ToArray();
                                       Category category = new Category();
                                       
                                       if (allCategories.Contains(s[i]) || (s[i] == "" )|| (s[i]==null))
                                       {

                                       }
                                       else
                                       {
                                           category.Name = s[i];
                                           category.ImageExt = ".jpg";
                                           repositoryCategory.SaveCategory(category);
                                       }
                                    }

                                   for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        Product product = new Product();
                                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                        {
                                            if (String.IsNullOrEmpty(ds.Tables[0].Rows[i][j].ToString()))
                                            {
                                                break;
                                            }
                                            switch (j)
                                            {
                                                    //Название товара 
                                                case 0:
                                                    //Идентификатор категории
                                                    //product.CategoryID =
                                                    var categoryName = ds.Tables[0].Rows[i][j];

                                                      product.CategoryID =  (int)repositoryCategory.Categories.FirstOrDefault(
                                                            x => x.Name == categoryName).CategoryID;
                                                    break;
                                                case 1:
                                                    product.Name = Convert.ToString(ds.Tables[0].Rows[i][j]);
                                                    
                                                    break;
                                                    //Цена
                                                case 2:
                                                    var price = Convert.ToDecimal(ds.Tables[0].Rows[i][j]);
                                                    product.Price = price;
                                                    break;
                                                    //Количество
                                                case 3:
                                                    int quantity = Convert.ToInt32(ds.Tables[0].Rows[i][j]);
                                                    product.Quantity = quantity;
                                                    break;
                                                default: break;
                                            };
                                        }
                                      
                                       if (String.IsNullOrEmpty(ds.Tables[0].Rows[i][0].ToString()))
                                        {
                                            break;
                                        }

                                        product.Sequence=0;
                                        product.ProductID = 0;
                                        product.Description = "Нет данных";
                                        try
                                        {
                                            if ((product.Name!= null) || (product.Name!=""))
                                            {
                                                repositoryProduct.Products.Where(x => x.Name == product.Name);
                                                rejectProducts[i] =
                                                    repositoryProduct.Products.FirstOrDefault(x => x.Name == product.Name)
                                                                     .Name;   
                                            }
                                            
                                            
                                        //    TempData["message"] = string.Format("Не все строки вставлены во избежание дублей");
                                        }
                                        catch (Exception)
                                        {
                                            repositoryProduct.SaveProduct(product);
                                            succesInsertedProduct++;
                                        }
                                    }
                                    //добавление товара
                                    //rejectProducts = rejectProducts;

                                 //  ViewBag.rejectProducts = rejectProducts.Where(x => x.ToString() != ""); 

                                    try
                                    {
                                        if (rejectProducts.Where(x => x != null).Count() > 0)
                                        {
                                            TempData["rejectProduct"] = rejectProducts.Where(x => x != null);
                                            TempData["message"] = string.Format("Не все строки вставлены во избежание дублирования названий товаров!");
                                            TempData["messageType"] = "error-msg"; 
                                        }
                                        else
                                        {
                                            TempData["rejectProduct"] = null;
                                            
                                        }
                                        
                                    }
                                    catch (Exception)
                                    {

                                        TempData["rejectProduct"] = null;
                                    }

                                    
                                    TempData["ProductToInsert"] = rejectProducts;
                                    TempData["succesInsertedProduct"] = succesInsertedProduct;
                                //    TempData["message"] = string.Format("Не все строки вставлены во избежание дублирования названий товаров!");

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

            return RedirectToAction("Actions");
        }

 /*         [HttpGet]
        public ActionResult Export2(PagedViewModel<ProductEditViewModel> viewModel)
        {

            var p = viewModel;
            return null;
        }
          */
        
        public ActionResult Export2(string query)
        {
            
        //    viewModel = viewModel.OrderBy(x => new {x.CategoryName, x.Name}).ToList();

            Workbook wb = new Workbook();
            // properties
            wb.Properties.Author = "Calabonga";
            wb.Properties.Created = DateTime.Today;
            wb.Properties.LastAutor = "Calabonga";
            wb.Properties.Version = "14";
            // options sheets
            wb.ExcelWorkbook.ActiveSheet = 1;
            wb.ExcelWorkbook.DisplayInkNotes = false;
            wb.ExcelWorkbook.FirstVisibleSheet = 1;
            wb.ExcelWorkbook.ProtectStructure = false;
            wb.ExcelWorkbook.WindowHeight = 800;
            wb.ExcelWorkbook.WindowTopX = 0;
            wb.ExcelWorkbook.WindowTopY = 0;
            wb.ExcelWorkbook.WindowWidth = 600;
            // create style s1 for header
                    Column column = new Column()
                    {
                        Width = 100,
                        Hidden = false,
                    };

                    Border border = new Border()
                    {
                        LineStyle = LineStyle.Continuous,
                        Color = "red",
                        Weight = Weight.Medium,
                        Position = Position.Top
                    };
            Font[] fonts = new Font[3];
            for (int i = 0; i < fonts.Count(); i++)
            {
                Font font = new Font();
                font.Size = 12;
                fonts[i] = font;
            }
            fonts[0].Bold = true;
            fonts[1].Bold = false;
            fonts[2].Bold = true;

            fonts[0].Color = "white";
            fonts[1].Color = "#237FBE";
            fonts[2].Color = "white";

            fonts[0].Size = fonts[1].Size=fonts[2].Size = 12;
            
            
           

            Alignment[] alignments = new Alignment[3];
            for (int i = 0; i < alignments.Count(); i++)
            {
                Alignment alignment = new Alignment();
                alignment.Horizontal = Horizontal.Center;
                alignment.Vertical = Vertical.Center;
                alignment.WrapText = true;
                alignments[i] = alignment;
                //interiors[i].Pattern = Pattern.Soid;
            }
                alignments[0].Horizontal = Horizontal.Center;
                alignments[1].Horizontal = Horizontal.Left;
                alignments[2].Horizontal = Horizontal.Right;

            Interior[] interiors = new Interior[3];
            for (int i = 0; i < interiors.Count(); i++)
            {
                Interior interior = new Interior();
                interior.Pattern = Pattern.Solid;
                interiors[i] = interior;
                //interiors[i].Pattern = Pattern.Soid;
            }
                interiors[0].Color = "#39B3D7";
                interiors[1].Color = "#EDFDCE";
                interiors[2].Color = "navy";

            // Border[] allBorders = new Border[4];


            Collection<Border>[] borders = new Collection<Border>[3];
            for (int i = 0; i < borders.Count(); i++)
            {
                borders[i] = new Collection<Border>()
                    {
                        new Border
                            {
                                Weight = Weight.Thin,
                                Position = Position.Top,
                                LineStyle = LineStyle.Continuous
                            },
                        new Border
                            {
                                Weight = Weight.Thin,
                                Position = Position.Bottom,
                                LineStyle = LineStyle.Continuous
                               
                            },
                        new Border
                            {
                                Weight = Weight.Thin,
                                Position = Position.Right,
                                LineStyle = LineStyle.Continuous
                            },
                        new Border
                            {
                                Weight = Weight.Thin,
                                Position = Position.Left,
                                LineStyle = LineStyle.Continuous
                            }

                    };
            }

            foreach (var item in borders[0])
            {
                item.Color = "gray";
            }

            foreach (var item in borders[1])
            {
                item.Color = "gray";
            }

            Style headerCenterAlign = new Style("s1")
            {
                Font = fonts[0],
                Borders = borders[0],
                Interior = interiors[0],
                Alignment = alignments[0],
            };

            Style bodyLeftAlign = new Style("s2")
            {
                Font = fonts[1],
                Borders = borders[1],
                Interior = interiors[1],
                Alignment = alignments[1]
                
            };

            Style bodyCentralAlign = new Style("s2Central")
            {
                Font = fonts[1],
                Borders = borders[1],
                Interior = interiors[1],
                Alignment = alignments[0]
            };


            Style footer = new Style("s3")
            {
                Font = fonts[0],
                Borders = borders[1],
                Interior = interiors[2]
            };
            //s1.Borders.Add(border);
            wb.AddStyle(headerCenterAlign);
            wb.AddStyle(bodyLeftAlign);
            wb.AddStyle(footer);
            wb.AddStyle(bodyCentralAlign);

            

            if (query == "products")
            {
                IEnumerable<ProductEditViewModel> viewModel = (IEnumerable<ProductEditViewModel>)TempData["Query"];

                Worksheet ws1 = new Worksheet("Продажи товаров");

                // Adding Headers
                //По товарам
                ws1.AddCell(0, 0, "Артикул", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 1, "Товар", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 2, "Категория", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 3, "Цена", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 4, "Количество", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 5, "Дата появления", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 6, "Дата изменения", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 7, "Активный", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 8, "Числится удаленным", 0, headerCenterAlign.ID);

                var products = viewModel.OrderBy(x => x.CategoryName).ThenBy(x => x.Name).ToList();
                int totalRows = 0;
                
                // appending rows with data
                for (int i = 0; i < products.Count; i++)
                {
                    var isActive = (products[i].IsActive == true) ? "Да" : "Нет";
                    var isDeleted = (products[i].IsDeleted == true) ? "Да" : "Нет";
                    if (products[i].ArticleNumber==null)
                    {
                        products[i].ArticleNumber = "";
                    }
                    ws1.AddCell(i + 1, 0, products[i].ArticleNumber, 0, bodyLeftAlign.ID);
                    ws1.AddCell(i + 1, 1, products[i].Name, 0, bodyLeftAlign.ID);
                    ws1.AddCell(i + 1, 2, products[i].CategoryName, 0, bodyLeftAlign.ID);
                    ws1.AddCell(i + 1, 3, products[i].Price, 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 4, products[i].Quantity, 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 5, products[i].StartDate.ToShortDateString(), 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 6, products[i].UpdateDate.ToShortDateString(), 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 7, isActive, 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 8, isDeleted, 0, bodyCentralAlign.ID);
                    totalRows++;
                }

               
                wb.AddWorksheet(ws1);

                // generate xml 
                string workstring = wb.ExportToXML();
                workstring =
                    workstring.Replace(
                        "<Table ss:ExpandedColumnCount=\"9\" ss:ExpandedRowCount=\"" +
                         (viewModel.Count() + 1).ToString() +
                        "\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultRowHeight=\"15\">",
                        "<Table ss:ExpandedColumnCount=\"9\" ss:ExpandedRowCount=\"" +
                         (viewModel.Count() + 1).ToString() +
                        "\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultRowHeight=\"15\">" +
                        "<Column  ss:Index=\"2\" ss:AutoFitWidth=\"0\" ss:Width=\"150\"/>" +
                        "<Column  ss:Index=\"3\" ss:AutoFitWidth=\"0\" ss:Width=\"130\"/>" +
                        "<Column  ss:Index=\"6\" ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>" +
                        "<Column  ss:Index=\"7\" ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>");

                workstring =
                    workstring.Replace("</WorksheetOptions>",
                                       "</WorksheetOptions><AutoFilter x:Range=\"R1C1:R1C9\" xmlns=\"urn:schemas-microsoft-com:office:excel\"></AutoFilter>");
                return new ExcelResult("Product.xls", workstring);
            }
            else if (query == "users")
            {
                IEnumerable<User> viewModel = (IEnumerable<User>)TempData["Query"];
                Worksheet ws1 = new Worksheet("База клиентов");

                // Adding Headers
                //По товарам
                ws1.AddCell(0, 0, "Логин", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 1, "Email", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 2, "Имя", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 3, "Телефон", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 4, "Активен", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 5, "Рассылка", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 6, "Активирован", 0, headerCenterAlign.ID);


                var users = viewModel.OrderBy(x => x.Login).ToList();
                int totalRows = 0;

                // appending rows with data
                for (int i = 0; i < users.Count; i++)
                {
                    var isActive = (users[i].IsActive == true) ? "Да" : "Нет";
                    var mailing = (users[i].Mailing == true) ? "Да" : "Нет";
                    var isActivated = (users[i].IsActivated == true) ? "Да" : "Нет";

                    ws1.AddCell(i + 1, 0, users[i].Login, 0, bodyLeftAlign.ID);
                    ws1.AddCell(i + 1, 1, users[i].Email, 0, bodyLeftAlign.ID);
                    ws1.AddCell(i + 1, 2, users[i].UserName, 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 3, users[i].Phone, 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 4, isActive, 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 5, mailing, 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 6, isActivated, 0, bodyCentralAlign.ID);
                    totalRows++;
                }


                wb.AddWorksheet(ws1);

                // generate xml 
                string workstring = wb.ExportToXML();
                workstring =
                    workstring.Replace(
                        "<Table ss:ExpandedColumnCount=\"7\" ss:ExpandedRowCount=\"" +
                        (viewModel.Count() + 1).ToString() +
                        "\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultRowHeight=\"15\">",
                        "<Table ss:ExpandedColumnCount=\"7\" ss:ExpandedRowCount=\"" +
                        (viewModel.Count() + 1).ToString() +
                        "\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultRowHeight=\"15\">" +
                        "<Column  ss:AutoFitWidth=\"0\" ss:Width=\"150\"/><Column  ss:AutoFitWidth=\"0\" ss:Width=\"130\"/><Column  ss:AutoFitWidth=\"0\" ss:Width=\"130\"/>" +
                        "<Column  ss:Index=\"5\" ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>" +
                        "<Column  ss:Index=\"6\" ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>");

                /*
                <Table ss:ExpandedColumnCount="7" ss:ExpandedRowCount="3" x:FullColumns="1"
   x:FullRows="1" ss:DefaultRowHeight="15">*/

                workstring =
                    workstring.Replace("</WorksheetOptions>",
                                       "</WorksheetOptions><AutoFilter x:Range=\"R1C1:R1C7\" xmlns=\"urn:schemas-microsoft-com:office:excel\"></AutoFilter>");
                return new ExcelResult("Users.xls", workstring);
            }
            else if (query == "orders")
            {
                IEnumerable<OrdersSummary> viewModel = (IEnumerable<OrdersSummary>)TempData["Query"];
                Worksheet ws1 = new Worksheet("Заказы");

                // Adding Headers
                //По товарам
                ws1.AddCell(0, 0, "Имя", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 1, "Email", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 2, "Адрес", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 3, "Телефон", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 4, "Сумма", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 5, "Доставка", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 6, "Итого", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 7, "Дата заказа", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 8, "Активен", 0, headerCenterAlign.ID);


                var orders = viewModel.OrderBy(x => x.TransactionDate).ToList();
                int totalRows = 0;

                // appending rows with data
                for (int i = 0; i < orders.Count; i++)
                {
                    var isActive = (orders[i].IsActive == true) ? "Да" : "Нет";


                    ws1.AddCell(i + 1, 0, orders[i].UserName, 0, bodyLeftAlign.ID);
                    ws1.AddCell(i + 1, 1, orders[i].Email, 0, bodyLeftAlign.ID);
                    ws1.AddCell(i + 1, 2, orders[i].UserAddress, 0, bodyLeftAlign.ID);
                    ws1.AddCell(i + 1, 3, orders[i].Phone, 0, bodyLeftAlign.ID);
                    ws1.AddCell(i + 1, 4, orders[i].TotalValue, 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 5, orders[i].ShippingPrice, 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 6, orders[i].TotalValue + orders[i].ShippingPrice, 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 7, orders[i].TransactionDate.ToShortDateString(), 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 8, isActive, 0, bodyCentralAlign.ID);
                    totalRows++;
                }


                wb.AddWorksheet(ws1);

                // generate xml 
                string workstring = wb.ExportToXML();
                workstring =
                    workstring.Replace(
                        "<Table ss:ExpandedColumnCount=\"9\" ss:ExpandedRowCount=\"" +
                        (viewModel.Count() + 1).ToString() +
                        "\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultRowHeight=\"15\">",
                        "<Table ss:ExpandedColumnCount=\"9\" ss:ExpandedRowCount=\"" +
                        (viewModel.Count() + 1).ToString() +
                        "\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultRowHeight=\"15\">" +
                        "<Column  ss:AutoFitWidth=\"0\" ss:Width=\"150\"/><Column  ss:AutoFitWidth=\"0\" ss:Width=\"130\"/><Column  ss:AutoFitWidth=\"0\" ss:Width=\"200\"/>" +
                        "<Column  ss:AutoFitWidth=\"0\" ss:Width=\"120\"/><Column  ss:AutoFitWidth=\"0\" ss:Width=\"100\"/><Column  ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>" +
                        "<Column  ss:AutoFitWidth=\"0\" ss:Width=\"100\"/><Column  ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>");


                workstring =
                    workstring.Replace("</WorksheetOptions>",
                                       "</WorksheetOptions><AutoFilter x:Range=\"R1C1:R1C9\" xmlns=\"urn:schemas-microsoft-com:office:excel\"></AutoFilter>");
                return new ExcelResult("Orders.xls", workstring);
            }
            else if (query == "orderDetails")
            {
                IEnumerable<OrderDetailsViewModel> viewModel = (IEnumerable<OrderDetailsViewModel>)TempData["Query"];
                Worksheet ws1 = new Worksheet("Детали заказа");

                // Adding Headers
                //По товарам
                ws1.AddCell(0, 0, "Категория", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 1, "Товар", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 2, "Цена за ед.", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 3, "Количество", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 4, "Сумма", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 5, "На складе, ед.", 0, headerCenterAlign.ID);
                ws1.AddCell(0, 6, "Цена на товар сейчас", 0, headerCenterAlign.ID);
                //ws1.AddCell(0, 7, "Дата заказа", 0, headerCenterAlign.ID);
                //ws1.AddCell(0, 8, "Активен", 0, headerCenterAlign.ID);


                var orderDetail = viewModel.OrderBy(x => x.ProductName).ToList();
                int totalRows = 0;

                // appending rows with data
                for (int i = 0; i < orderDetail.Count; i++)
                {
                    //var isActive = (orderDetail[i].IsActive == true) ? "Да" : "Нет";


                    ws1.AddCell(i + 1, 0, orderDetail[i].CategoryName, 0, bodyLeftAlign.ID);
                    ws1.AddCell(i + 1, 1, orderDetail[i].ProductName, 0, bodyLeftAlign.ID);
                    ws1.AddCell(i + 1, 2, orderDetail[i].PriceInOrder, 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 3, orderDetail[i].QuantityInOrder, 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 4, orderDetail[i].PriceInOrder * orderDetail[i].QuantityInOrder, 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 5, orderDetail[i].QuantityInStore, 0, bodyCentralAlign.ID);
                    ws1.AddCell(i + 1, 6, orderDetail[i].PriceNow, 0, bodyCentralAlign.ID);
                    //ws1.AddCell(i + 1, 7, orderDetail[i].TransactionDate.ToShortDateString(), 0, bodyCentralAlign.ID);
                    //ws1.AddCell(i + 1, 8, isActive, 0, bodyCentralAlign.ID);
                    totalRows++;
                }


                wb.AddWorksheet(ws1);

                // generate xml 
                string workstring = wb.ExportToXML();
                workstring =
                    workstring.Replace(
                        "<Table ss:ExpandedColumnCount=\"7\" ss:ExpandedRowCount=\"" +
                        (viewModel.Count() + 1).ToString() +
                        "\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultRowHeight=\"15\">",
                        "<Table ss:ExpandedColumnCount=\"7\" ss:ExpandedRowCount=\"" +
                        (viewModel.Count() + 1).ToString() +
                        "\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultRowHeight=\"15\">" +
                        "<Column  ss:AutoFitWidth=\"0\" ss:Width=\"150\"/><Column  ss:AutoFitWidth=\"0\" ss:Width=\"150\"/><Column  ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>" +
                        "<Column  ss:AutoFitWidth=\"0\" ss:Width=\"120\"/><Column  ss:AutoFitWidth=\"0\" ss:Width=\"100\"/><Column  ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>" +
                        "<Column  ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>");


                workstring =
                    workstring.Replace("</WorksheetOptions>",
                                       "</WorksheetOptions><AutoFilter x:Range=\"R1C1:R1C7\" xmlns=\"urn:schemas-microsoft-com:office:excel\"></AutoFilter>");
                return new ExcelResult("OrderDetails.xls", workstring);
            }
            return null;
        }


          public ActionResult Export()
          {
              var productListAsync = repositoryProduct.Products.ToList();//GetProductListAsync();
              var ordersListAsync = repositoryOrderSummary.OrdersSummaryInfo;// GetOrderSummaryListAsync();

            var userStatTmp = from od in repositoryOrderDetails.OrdersDetails.ToList()
                              join u in repositoryUser.UsersInfo.ToList() on od.UserID equals u.UserID
                              where u.IsActive = true
                              group u by new { u.Login, od.Price, od.OrderSummaryID };

            var userStat = from uod in userStatTmp
                           group uod by new { uod.Key.Login }
                               into js
                               select new UserSellReportView
                               {
                                   Login = js.Key.Login,
                                   PriceSummary = js.Sum(x => x.Key.Price),
                                   SellCount = js.Count(),
                                   OrderPriceAVG = Convert.ToInt32(js.Sum(x => x.Key.Price) / js.Count())
                               };


              //productListAsync.Wait();
              IEnumerable<Product> productsList = productListAsync; //.Result.ToList();

              //ordersListAsync.Wait();
              IEnumerable<OrdersSummary> osSummaries = ordersListAsync;//Result.ToList();

            var productStat = from od in repositoryOrderDetails.OrdersDetails.ToList()
                      join p in productsList on od.ProductID equals p.ProductID
                      join os in osSummaries on od.OrderSummaryID equals
                          os.OrderSummaryID
                      join osStat in repositoryOrderStatus.OrderStatuses.ToList() on os.OrderSummaryID equals
                          osStat.OrderSummaryID
                      join dos in repositoryDimOrderStatus.DimOrderStatuses.ToList() on osStat.DimOrderStatusID equals
                          dos.DimOrderStatusID
                      where dos.OrderStatusDesc.TrimEnd() == "Заказ выполнен"
                      group p by new { p.Name, p.Price}
                      into js
                      
                      select new ProductSellReportView
                          {
                              Name = js.Key.Name,
                              Price = js.Key.Price,
                              AllPrice = js.Sum(x => x.Price),
                              CellQuantity = js.Count()
                          };
            
            
                      
                      //select dos.OrderStatusDesc;
            
            
                      //select p.Name, ;
                     // where dos.OrderStatusDesc="Заказ выполнен"
            //запрос
                      /*
       /*    var viewModel = from order in dataManager.OrdersProcessor.Orders.ToList()
                          join user in dataManager.UsersRepository.UsersInfo.ToList() on  order.UserID equals
                              user.UserID
                          join product in dataManager.Products.Products.ToList() on order.ProductID equals
                              product.ProductID
                          group order by new { order.UserName, order.Phone, order.OrderNumber, order.Email, order.UserAddress, order.TransactionDate } into js// order.OrderNumber into js
                     
                          select new OrderViewModel 
                      {
                          UserName = js.Key.UserName,
                          Phone = js.Key.Phone,
                          Email = js.Key.Email,
                          OrderNumber = js.Key.OrderNumber,
                          UserAddress = js.Key.UserAddress,
                          TransactionDate = js.Key.TransactionDate, 
                          TotalValue = js.Sum(p=>p.Quantity*p.Price)
                      };        */                
/*var f = from c in repositoryCategory.Categories.ToList()
        join p in repositoryProduct.Products.ToList() on c.CategoryID equals p.CategoryID
        select c;
                       */
                      /*
                       
	Select p.Name, p. Price, sum(od.Price) as summ, count(od.Price)  as counted  -- as Price
							   from OrdersDetails od inner join Products p on od.ProductID=p.ProductID
								inner join OrdersSummaries os on od.OrderSummaryID=os.OrderSummaryID
								inner join OrderStatus ostatus on os.OrderSummaryID=ostatus.OrderSummaryID
								inner join DimOrderStatus dos on dos.DimOrderStatusID=ostatus.DimOrderStatusID
								where dos.OrderStatusDesc='Заказ выполнен'
								group by p.Name, p.Price
								order by  summ desc
                       */

            string result = string.Empty;
            Workbook wb = new Workbook();
            // properties
            wb.Properties.Author = "Calabonga";
            wb.Properties.Created = DateTime.Today;
            wb.Properties.LastAutor = "Calabonga";
            wb.Properties.Version = "14";
            // options sheets
            wb.ExcelWorkbook.ActiveSheet = 1;
            wb.ExcelWorkbook.DisplayInkNotes = false;
            wb.ExcelWorkbook.FirstVisibleSheet = 1;
            wb.ExcelWorkbook.ProtectStructure = false;
            wb.ExcelWorkbook.WindowHeight = 800;
            wb.ExcelWorkbook.WindowTopX = 0;
            wb.ExcelWorkbook.WindowTopY = 0;
            wb.ExcelWorkbook.WindowWidth = 600;
            // create style s1 for header

            Column column = new Column()
                {
                    AutoFitWidth = true,
                    Hidden = false
                };
            
            Border border = new Border()
                {
                    LineStyle = LineStyle.Continuous,
                    Color = "red",
                    Weight = Weight.Medium,
                    Position = Position.Top
                    
                };

            Font font = new Font
                {
                    Bold = true,
                    Color = "black",
                    Size = 12
                };

            Interior[] interiors = new Interior[3];
            for (int i = 0; i < interiors.Count(); i++)
            {
                Interior interior = new Interior();
                interior.Pattern = Pattern.Solid;
                interiors[i] = interior;
                //interiors[i].Pattern = Pattern.Soid;
            }
            interiors[0].Color = "yellow";
            interiors[1].Color = "green";
            interiors[2].Color = "navy";

           // Border[] allBorders = new Border[4];


            Collection<Border>[] borders = new Collection<Border>[3];
            for (int i = 0; i < borders.Count(); i++)
            {
                borders[i] = new Collection<Border>()
                    {
                        new Border
                            {
                                Weight = Weight.Thick,
                                Position = Position.Top,
                               LineStyle = LineStyle.Continuous
                            },
                        new Border
                            {
                                Weight = Weight.Thick,
                                Position = Position.Bottom,
                               LineStyle = LineStyle.Continuous
                            },
                        new Border
                            {
                                Weight = Weight.Thick,
                                Position = Position.Right,
                                LineStyle = LineStyle.Continuous
                            },
                        new Border
                            {
                                Weight = Weight.Thick,
                                Position = Position.Left,
                                LineStyle = LineStyle.Continuous
                            }

                    };
            }

            foreach (var item in borders[0])
            {
                item.Color = "red";
            }

            foreach (var item in borders[1])
            {
                item.Color = "black";
            }

           Style s1 = new Style("s1")
                {
                    Font = font,
                    Borders = borders[0],
                    Interior = interiors[0]
                };

            Style s2 = new Style("s2")
                {
                    Font = font,
                    Borders = borders[1],
                    Interior = interiors[1]
                };

            Style s3 = new Style("s3")
            {
                Font = font,
                Borders = borders[1],
                Interior = interiors[2]
            };
          //s1.Borders.Add(border);
            wb.AddStyle(s1);
            wb.AddStyle(s2);
            wb.AddStyle(s3);
             // create style s2 for header
          /*  Style s2 = new Style("s2");
           
         //   s2.Borders.Add(new Border());
           
            wb.AddStyle(s2);
            // First sheet
            Worksheet ws = new Worksheet("Лист 1");
             // adding headers
            ws.AddCell(0, 0, "qwerty1", 0, s1.ID);
            ws.AddCell(0, 1, "qwerty2", 0, s1.ID);
            ws.AddCell(0, 2, "qwerty3", 0, s1.ID);
            // adding row1
            ws.AddCell(1, 0, 1, 0);
            ws.AddCell(1, 1, 2, 0);
            ws.AddCell(1, 2, 3, 0);
            // adding row2
            ws.AddCell(2, 0, 4, 0);
            ws.AddCell(2, 1, 5, 0);
            ws.AddCell(2, 2, 6, 0);
            wb.AddWorksheet(ws);
            // Second sheet
            Worksheet ws2 = new Worksheet("Лист 2");
            ws2.AddCell(0, 0, 1, 0);
            ws2.AddCell(1, 0, 2, 0);
            ws2.AddCell(2, 0, 3, 0);
            ws2.AddCell(3, 0, 4, 0);
            ws2.AddCell(4, 0, 5, 0);
            ws2.AddCell(5, 0, 6, 0);
            ws2.AddCell(6, 0, 7, 0);
            ws2.AddCell(7, 0, 8, 0);
            ws2.AddCell(8, 0, 9, 0);
            ws2.AddCell(9, 0, 10, 0);
            ws2.AddCell(10, 0, 11, 0);
            ws2.AddCell(11, 0, 12, 0);
            ws2.AddCell(0, 1, 13, 0);
            ws2.AddCell(1, 1, 14, 0);
            ws2.AddCell(2, 1, 15, 0);
            ws2.AddCell(3, 1, 16, 0);
            ws2.AddCell(4, 1, 17, 0);
            ws2.AddCell(5, 1, 18, 0);
            ws2.AddCell(6, 1, 19, 0);
            ws2.AddCell(7, 1, 20, 0);
            ws2.AddCell(8, 1, 21, 0);
            ws2.AddCell(9, 1, 22, 0);
            ws2.AddCell(10, 1, 23, 0);
            ws2.AddCell(11, 1, 24, 0);
            wb.AddWorksheet(ws2); */
            // Third sheet 
                Worksheet ws1 = new Worksheet("Продажи товаров");
                Worksheet ws2 = new Worksheet("Активность клиентов");
                // Adding Headers
            //По продажам товара
                ws1.AddCell(0, 0, "Товар", 0, s1.ID);
                ws1.AddCell(0, 1, "Цена", 0, s1.ID);
                ws1.AddCell(0, 2, "Сумма", 0, s1.ID);
                ws1.AddCell(0, 3, "Количество", 0, s1.ID);
            //По клиентам
                ws2.AddCell(0, 0, "Логин", 0, s1.ID);
                ws2.AddCell(0, 1, "Сумма", 0, s1.ID);
                ws2.AddCell(0, 2, "Количество покупок", 0, s1.ID);
                ws2.AddCell(0, 3, "Средний чек", 0, s1.ID);
                // get data
                //List<Product> products = repositoryProduct.Products.ToList();// People.GetPeople();
                var products = productStat.ToList();
            var users = userStat.ToList();
            int totalRows = 0;
                
            // appending rows with data
            //по продажам
                for (int i = 0; i < products.Count; i++)
                  {
                      ws1.AddCell(i + 1, 0, products[i].Name, 0, s2.ID);
                      ws1.AddCell(i + 1, 1, products[i].Price, 0, s2.ID);
                      ws1.AddCell(i + 1, 2, products[i].AllPrice, 0, s2.ID);
                      ws1.AddCell(i + 1, 3, products[i].CellQuantity, 0, s2.ID);
                totalRows++;
                }
                totalRows++;
                // appending footer with formulas
                ws1.AddCell(totalRows, 0, string.Empty, 0, s3.ID);
                ws1.AddCell(totalRows, 1, 0, "=AVERAGE(R[-" + (totalRows - 1) + "]C:R[-1]C)", 0, s3.ID);
                ws1.AddCell(totalRows, 2, 0, "=SUM(R[-" + (totalRows - 1) + "]C:R[-1]C)", 0, s3.ID);
                ws1.AddCell(totalRows, 3, 0, "=SUM(R[-" + (totalRows - 1) + "]C:R[-1]C)", 0, s3.ID);


                wb.AddWorksheet(ws1);
                totalRows = 0;

                for (int i = 0; i < users.Count(); i++)
                {
                    ws2.AddCell(i + 1, 0, users[i].Login, 0, s2.ID);
                    ws2.AddCell(i + 1, 1, users[i].PriceSummary, 0, s2.ID);
                    ws2.AddCell(i + 1, 2, users[i].SellCount, 0, s2.ID);
                    ws2.AddCell(i + 1, 3, users[i].OrderPriceAVG, 0, s2.ID);
                    totalRows++;
                }
                totalRows++;
                // appending footer with formulas
                ws2.AddCell(totalRows, 0, string.Empty, 0, s3.ID);
                //ws2.AddCell(totalRows, 1, 0, "=AVERAGE(R[-" + (totalRows - 1) + "]C:R[-1]C)", 0, s3.ID);
                ws2.AddCell(totalRows, 1, 0, "=SUM(R[-" + (totalRows - 1) + "]C:R[-1]C)", 0, s3.ID);
                ws2.AddCell(totalRows, 2, 0, "=SUM(R[-" + (totalRows - 1) + "]C:R[-1]C)", 0, s3.ID);
                ws2.AddCell(totalRows, 3, string.Empty, 0, s3.ID);

            
                wb.AddWorksheet(ws2);
            //int sd = ws3.ColumnsCount;
            /*
            foreach (var item in ws3.Table.Columns)
            {
                item.AutoFitWidth = true;
            }*/
                
            // generate xml 
            string workstring = wb.ExportToXML();
            
            /*
              // Устанавливаем ширину колонок
            workstring = workstring.Replace("<Column ss:AutoFitWidth=\"0\" ss:Width=\"75\" />", "<Column ss:AutoFitWidth=\"0\" ss:Width=\"\" />");
            // Колонка1
           int col1Width = 10;
            workstring = workstring.Insert(workstring.IndexOf("<Column ss:AutoFitWidth=\"0\" ss:Width=\"\" />") + 38, (col1Width * 6.25).ToString());
           // Колонка2
           int col2Width = 11;
            workstring = workstring.Insert(workstring.IndexOf("<Column ss:AutoFitWidth=\"0\" ss:Width=\"\" />") + 38, (col2Width * 6.25).ToString());
           // Колонка3
           int col3Width = 12;
            workstring = workstring.Insert(workstring.IndexOf("<Column ss:AutoFitWidth=\"0\" ss:Width=\"\" />") + 38, (col3Width * 6.25).ToString());
             */

            // Send to user file
            return new ExcelResult("Product.xls", workstring);
        }




        //export into word-file
        [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult Print(int id=1)
        {
            /*Check.Argument.IsNotNegativeOrZero(id, "id");
            Contract contract = contractRepository.Find(id);
            Check.Argument.IsNotNull(contract, "contract");
            string contractText = DataReplacer.Replace(contract.TypeOfDeal.ContractText, contract);
            return new WordResult(String.Format("{0}_{1}.doc", contract.Number, contract.TypeOfDeal.Name), "Дововор услуги", contractText);
        */
            GridSortOptions gridSortOptions = new GridSortOptions();
            gridSortOptions.Column = "OrderSummaryID";

            var query = from o in repositoryOrderDetails.OrdersDetails.Where(x => x.OrderSummaryID == id)
                        select new OrderDetailsViewModel
                        {
                            OrderDetailsID = o.OrderDetailsID,
                            OrderSummaryID = o.OrderSummaryID,
                            ProductID = o.ProductID,
                            UserID = o.UserID,
                            CategoryName = o.Product.Category.Name,
                            ProductName = o.Product.Name,
                            PriceInOrder = o.Price,
                            QuantityInOrder = o.Quantity,
                            TotalPrice = o.Price * o.Quantity,
                            QuantityInStore = o.Product.Quantity,
                            PriceNow = o.Product.Price
                        };
            var pagedViewModel = new PagedViewModel<OrderDetailsViewModel>
            {
                ViewData = ViewData,
                Query = query,
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "OrderSummaryID",
                Page = 1,
                PageSize = Domain.Constants.ADMIN_PAGE_SIZE
            };
            pagedViewModel
                .Setup();

            OrdersSummary orderSummaryInfo = repositoryOrderSummary.OrdersSummaryInfo.Where(x => x.OrderSummaryID == id).Single();

            string html =
                "<p align='center'>Магазин тайской косметики tropic-store.ru</p><p align='center'>п. Томилино, ул. Есенина, д. 3, кв. 9, ИП Воронцов О.В.</p>" +
                "<h2 align = 'center'>НАКЛАДНАЯ</h2>"+
                "<table width='100%' border='1'><tr>" +
                    "<td align='center'><strong>Заказ №</strong>" + orderSummaryInfo.OrderNumber + "</td>" +
                    "<td align='center'><strong>Дата заказа:</strong> " + orderSummaryInfo.TransactionDate.ToShortDateString() + "</td>" +
                    "<td align='center'><strong>Время заказа:</strong> " + orderSummaryInfo.TransactionDate.ToShortTimeString() + "</td>" +
                "</tr></table><br/><br/>";
 
                html = html+"<table width='100%' border='1'><thead><tr color='red'>" +
                          "<th>Категория</th>" +
                          "<th>Товар</th>" +
                          "<th>Цена/ед, руб.</th>" +
                          "<th>Количество</th>" +
                          "<th>Сумма, руб.</th>" +
                          "<th>В том числе НДС, руб.</th>" +
                          "</tr></thead>";
            string tbody="";
            foreach (var p in pagedViewModel.Query)
            {
                tbody = tbody + "<tr><td>" + p.CategoryName.TrimEnd() + "</td>" +
                    "<td>" + p.ProductName.TrimEnd() + "</td>" +
                    "<td align = 'center'>" + p.PriceInOrder.ToString() + "</td>" +
                    "<td align = 'center'>" + p.QuantityInOrder.ToString() + "</td>" +
                    "<td align = 'center'>" + p.TotalPrice.ToString() + "</td>" +
                    "<td align = 'center'>" + (18 * p.TotalPrice / 100).ToString() + "</td>" +
                    "</tr>";
            }
            tbody = tbody + "<tr><td>Доставка</td>" +
                    "<td colspan='3' align = 'center'>" + orderSummaryInfo.ShippingType + "</td>" +
                    "<td align = 'center'>" + orderSummaryInfo.ShippingPrice + ",00</td>" +
                    "<td></td>" +
                    "</tr>";
            decimal totalPrice = orderSummaryInfo.TotalValue + ((orderSummaryInfo.ShippingPrice == null) ? 0 : Decimal.Parse(orderSummaryInfo.ShippingPrice.ToString()));
            html = html +"<tbody>"+tbody+"</tbody>"+
                "<tfoot><tr>" +
                /*   "<th></th>" +
                   "<th></th>" +
                   "<th></th>" +*/
                   "<th colspan='4'>Итого, руб.</th>" +
                   "<th>" + totalPrice + "</th>" +
                   "<th>" + (18*orderSummaryInfo.TotalValue/100).ToString() + "</th>" +
                   "</tr></tfoot>" + "</table>";

            html = html + "<br/>" + "<table width='100%'>" +
                   "<tr>" +
                       "<td align='center'>Продавец</td>" +
                       
                       "<td width='25%'></td>" +
                       "<td align='center'>Покупатель</td>" +
                   "</tr>" +

                   "<tr>" +
                       "<td font-size='2'></td>" +
                       "<td></td>" +
                       "<td>(ФИО)_________________</td>" +
                   "</tr>" +

                   "<tr>" +
                       "<td font-size='2'>Место для печати</td>" +
                       "<td></td>" +
                       "<td>(подпись)_________________</td>" +
                   "</tr>" +

                   "</table>";
            
            
            string experiment = Actions().ToString();
            string vs = "<!DOCTYPE html> " +
                        "<html><head>" +
                        "<meta http-equiv='content-type' " +
                        "content = 'text/html;charset=utf-8'/>" +
                        "</head>" +
                        "<body>" +
                        html +
                        "</body>" +
                        "</html>";
            string filename = string.Format(
                CultureInfo.InvariantCulture,
                "vedomost_{0}.doc",
                DateTime.Now.Year.ToString());
            
            return new WordResult(filename, "Бланк заказа", vs);
        }




        public void TryToUpdateDimSettingsNow(string settingTypeID, string settingTypeDesc, string settingsID, string settingsDesc, string settingValue)
        {
            //TryToUpdateDimSettings("ADMIN_EMAIL_SETTINGS", "Настройки рассылки", "MAIL_SERVER_USER_NAME", "Логин почты", Constants.USERNAME);

            var dimsSettingsListAsync = repositoryDimSetting.DimSettings.ToList();// GetDimSettingListAsync();
            if (String.IsNullOrEmpty(repositoryDimSettingType.DimSettingTypes.Where(x => x.SettingTypeID == settingTypeID).Select(x => x.SettingTypeID).SingleOrDefault()))
            {
                DimSettingType dt = new DimSettingType()
                {
                    SettingTypeID = settingTypeID,
                    SettingTypeDesc = settingTypeDesc
                };
                //    ViewBag.dt = dt;
                repositoryDimSettingType.SaveDimSettingType(dt, true);
            }
            
            //try
            //{
          //  dimsSettingsListAsync.Wait();
            //DimSetting ds0 = dimsSettingsListAsync.Result.FirstOrDefault(x => x.SettingsID == settingsID);
            DimSetting ds0 = dimsSettingsListAsync.FirstOrDefault(x => x.SettingsID == settingsID);
                if (ds0!=null)
                {
                    DimSettingType dt = new DimSettingType()
                        {
                            SettingTypeID = settingTypeID,
                            SettingTypeDesc = settingTypeDesc
                        };
                    DimSetting ds = new DimSetting
                        {
                            SettingsID = settingsID,
                            SettingTypeID = settingTypeID,
                            SettingsDesc = settingsDesc,
                            SettingsValue = settingValue
                        };
                    repositoryDimSetting.SaveDimSetting(ds, false);
                }
                else
                {
                        DimSetting ds = new DimSetting
                        {
                            SettingsID = settingsID,
                            SettingTypeID = settingTypeID,
                            SettingsDesc = settingsDesc,
                            SettingsValue = settingValue
                        };
                    repositoryDimSetting.SaveDimSetting(ds, true);

                }
            //}
            
         //  catch (Exception)
           // {
                
            //}
            
            
        }



#region Logger
        public ActionResult Nlog(string searchWord, string startDate, string endDate, string level, GridSortOptions gridSortOptions, int? page)
        {
           // var tmp = repositoryNLog.NLogErrors;

            if (startDate == null)
            {
                startDate = String.Format("{0:dd.mm.yyyy}", (DateTime.Now.Date.AddYears(-5)).ToShortDateString());
            }
            else
            {
                startDate = String.Format("{0:dd.mm.yyyy}", startDate);
            }

            if (endDate == null)
            {
                endDate = (DateTime.Now.Date.ToShortDateString());
            }
            else
            {
                endDate = String.Format("{0:dd.mm.yyyy}", endDate);
            }

            DateTime dStart = Convert.ToDateTime(startDate);
            DateTime dEnd = Convert.ToDateTime(endDate).AddDays(1);
            
             Session["StartDate"] = dStart.ToShortDateString();
            Session["EndDate"] = dEnd.ToShortDateString();

            var pagedViewModel = new PagedViewModel<NLog_Error>
            {
                ViewData = ViewData,
                Query = repositoryNLog.NLogErrors, //_service.GetAlbumsView(),
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "Time_stamp",
                Page = page,
                PageSize = 300 //(pageItemsCount == 0) ? Domain.Constants.ADMIN_PAGE_SIZE : pageItemsCount
                // Domain.Constants.ADMIN_PAGE_SIZE,
            }
            .AddFilter("searchWord", searchWord,
                           a => a.Level.Contains(searchWord) || a.Logger.Contains(searchWord) || a.Message.Contains(searchWord) || a.Type.Contains(searchWord))
            .AddFilter("level", level, a => ((level == "Все") ? true : a.Level==level))
            .AddFilter("startDate", startDate, a => a.Time_stamp >= dStart)
            .AddFilter("endDate", endDate, a => a.Time_stamp <= dEnd)
              .Setup();
            if (Request.IsAjaxRequest())
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                return PartialView("LogGridPartial", pagedViewModel);
            }
            return View(pagedViewModel);
        }
          
          [HttpPost]
          public ActionResult Nlog()
          {
              if (Session["LogDeleter"]!=null)
              {
                  PagedViewModel<NLog_Error> viewModel = (PagedViewModel<NLog_Error>)Session["LogDeleter"];
              
              repositoryNLog.DeleteLogData(viewModel.Query);
                    TempData["message"] = string.Format("Данные из лога были удалены!");
                    TempData["messageType"] = "warning-msg";
                  return RedirectToAction("Nlog");   
              }
              TempData["message"] = string.Format("Внимание! Данные из лога не были удалены!");
              TempData["messageType"] = "warning-msg";
              return RedirectToAction("Nlog");   
          }
          

          public ActionResult NlogClear()
          {
              repositoryNLog.DeleteLogData(repositoryNLog.NLogErrors);
              return RedirectToAction("Nlog");
          }

          /*
        public ActionResult DeleteRole(int roleId)
        {
            Role r = repositoryUser.Roles.FirstOrDefault(x => x.RoleID == roleId);

            if (r.RoleName == "User" || r.RoleName == "Admin")
            {
                TempData["message"] = string.Format("Данную роль нельзя удалять");
                return RedirectToAction("Roles");
            }
            else
            {
                string[] ur = repositoryUser.UserRoles.Where(x => x.RoleID == roleId).Select(x => x.User.Login).ToArray();
                foreach (var userRole in ur)
                {
                    repositoryUser.AddUserToRole(userRole, "User");
                }
                repositoryUser.DeleteRole(r);
            }
            return RedirectToAction("Roles");
        }*/

#endregion Logger



#region DimShipping

          public PartialViewResult DimShipping()
          {
              IEnumerable<DimShipping> df = repositoryDimShipping.DimShipping.OrderBy(x => x.ShippingPrice);
       
              return PartialView(df);
          }

          public ActionResult CreateDimShipping()
          {
              return View("EditDimShipping", new DimShipping());
          }

          public ActionResult EditDimShipping(int dimShippingId)
          {
              DimShipping dimShipping = repositoryDimShipping.DimShipping
                                                                      .FirstOrDefault(
                                                                          p => p.ShippingID == dimShippingId);
              return View(dimShipping);

          }

          [HttpPost]
          public ActionResult EditDimShipping(DimShipping dimShipping)
          {
              if (ModelState.IsValid)
              {
                  repositoryDimShipping.SaveDimShipping(dimShipping);
                  TempData["message"] = string.Format("Стоимость доставки  '{0}' изменена", dimShipping.ShippingType);
                  TempData["messageType"] = "information-msg";
                  logger.Warn(string.Format("{0}. Стоимость доставки {1} изменена", User.Identity.Name, dimShipping.ShippingType));
                  // return the user to the list
                  return RedirectToAction("DimsParameters");
                  //return RedirectToAction("EditDimOrderStatus", new { dimOrderStatusId = dimOrderStatus.DimOrderStatusID });
                  //return RedirectToAction("DimOrderStatus");
                  //return JavaScript("window.location.replace('http://localhost:57600/Admin/DimOrderStatus');");
                  // return Json(JsonStandardResponse.SuccessResponse(true));
              }
              else
              {
                  // there is something wrong with the data values
                  TempData["message"] = string.Format("{0} ! Изменения не внесены",
                                                      dimShipping.ShippingType);
                  TempData["messageType"] = "warning-msg";
                  //return View(dimOrderStatus);
                  /*return Json(JsonStandardResponse.ErrorResponse(string.Format("Статус {0}  - уже существует в базе! Изменения не внесены",
                                                      dimOrderStatus.OrderStatusDesc)), JsonRequestBehavior.DenyGet);*/
                  return RedirectToAction("EditDimShipping", new { dimShippingId = dimShipping.ShippingID }); //EditDimShipping(dimShipping.ShippingID)
              }
          }

          [HttpPost]
          public ActionResult DeleteDimShipping(int dimShippingId)
          {
              DimShipping dimShipping = repositoryDimShipping.DimShipping
                                                                      .FirstOrDefault(
                                                                          p => p.ShippingID == dimShippingId);
              if (dimShipping != null)
              {
                  repositoryDimShipping.DeleteDimShipping(dimShipping);
                  TempData["message"] = string.Format("Информация о доставке '{0}' была удалена", dimShipping.ShippingType);
                  TempData["messageType"] = "warning-msg";
                  logger.Warn(string.Format("{0}. Информация о доставке {1} удалена", User.Identity.Name, dimShipping.ShippingType));
              }
              return RedirectToAction("DimsParameters");
          }

          #endregion


#region Article

 [Authorize(Roles = "Admin, ContentManager")]
        public ActionResult ArticleList(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            TryToUpdateDimSettings("ADMIN_PAGE_SIZE",
                       "Количество элементов на странице в админке",
                       "ADMIN_PAGE_SIZE_Article",
                       "Количество элементов на странице в административном разделе Статьи");

            var pagedViewModel = new PagedViewModel<Article>
            {
                ViewData = ViewData,
                Query = repositoryArticle.Articles,
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "ArticleDate",
                Page = page,
                PageSize = Domain.Constants.ADMIN_PAGE_SIZE,
            }
    /*.AddFilter("searchWord", searchWord,
               a =>
               a.OrderNumber == sw
                   ||
               a.Phone.Contains(searchWord) || a.UserName.Contains(searchWord) ||
               a.UserAddress.Contains(searchWord))
    .AddFilter("startDate", Convert.ToDateTime(startDate), a => a.TransactionDate >= dStart)
    .AddFilter("endDate", Convert.ToDateTime(endDate), a => a.TransactionDate <= dEnd)
    .AddFilter("isActive", isActive, a => a.IsActive == isActive)*/
    .Setup();
            
            if (Request.IsAjaxRequest())
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                return PartialView("ArticleListGridPartial",pagedViewModel); 
            }

            return View(pagedViewModel);

            /*IEnumerable<NewsTape> allNews = repositoryNewsTape.NewsTapes.OrderByDescending(x => x.NewsDate);
            return PartialView(allNews);*/
        }

 [Authorize(Roles = "Admin, ContentManager")]
 public ActionResult CreateArticle(int articleId = 0)
 {
     if (articleId == 0)
     {
         Article article = new Article();
         article.ArticleDate = DateTime.Now;
         return PartialView(article);
     }
     else
     {
         Article article = repositoryArticle.Articles.FirstOrDefault(x => x.ArticleID == articleId);
         return PartialView(article);
     }

 }


 [HttpPost]
 [ValidateInput(false)]
 [Authorize(Roles = "Admin, ContentManager")]
 public ActionResult CreateArticle(Article article)
 {
     if (ModelState.IsValid)
     {
         repositoryArticle.SaveArticle(article);
         logger.Warn(string.Format("{0}. Статья {1} сохранена", User.Identity.Name, article.ArticleID));
         return View("ArticleList");
     }
     else
     {
         if (Request.IsAjaxRequest())
         {
             return PartialView("CreateArticle", article);
         }
         return RedirectToAction("ArticleList");
     }
 }

 [HttpPost]
 [ValidateInput(false)]
 [Authorize(Roles = "Admin, ContentManager")]
 public ActionResult DeleteArticle(int? ArticleID)
 {
     if (ArticleID == 0 || ArticleID == null)
     {
         RedirectToAction("ArticleList");
     }
     Article article = repositoryArticle.Articles.FirstOrDefault(x => x.ArticleID == ArticleID);
     if (article == null)
     {
         return RedirectToAction("ArticleList");
     }
     if (article.ArticleID == 0)
     {
         return RedirectToAction("ArticleList");
     }
     repositoryArticle.DeleteArticle(article);
     logger.Warn(string.Format("{0}. Статья {1} удалена", User.Identity.Name, article.ArticleID));
     string strExtension = System.IO.Path.GetExtension(article.ImgPath);
     string strSaveFileName = article.ArticleID + strExtension;
     string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                            Constants.ARTICLE_MINI_IMAGES_FOLDER,
                                                            strSaveFileName);
     /*
      * string strExtension = System.IO.Path.GetExtension(imagefile.FileName);
             string strSaveFileName = newsId + strExtension;
             string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                             Constants.NEWS_MINI_IMAGES_FOLDER,
                                                             strSaveFileName);
      */

     if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);

     return RedirectToAction("ArticleList");
 }


 [Authorize(Roles = "Admin, ContentManager")]
 public ActionResult UploadArticleImage(HttpPostedFileBase imagefile, int articleId)
 {
     if (imagefile == null)
     {
         return RedirectToAction("CreateArticle", new { articleId });
     }

     Article obj = repositoryArticle.Articles.FirstOrDefault(x => x.ArticleID == articleId);
     if (obj == null) return Content("NotFound"); //View("NotFound");
     try
     {
         if (imagefile != null)
         {
             // Определяем название конечного графического файла вместе с полным путём.
             // Название файла должно быть такое же, как ID объекта. Это гарантирует уникальность названия.
             // Расширение должно быть такое же, как расширение у исходного графического файла.
             string strExtension = System.IO.Path.GetExtension(imagefile.FileName);
             string strSaveFileName = articleId + strExtension;
             string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                             Constants.ARTICLE_MINI_IMAGES_FOLDER,
                                                             strSaveFileName);

             // Если файл с таким названием имеется, удаляем его.
             if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);

             imagefile.ResizeAndSave(Constants.NEWS_MINI_IMAGE_HEIGHT, Constants.ARTICLE_MINI_IMAGE_WIDTH,
                                     strSaveFullPath);
             obj.ImgPath = strExtension;

             repositoryArticle.SaveArticle(obj);

         }
     }
     catch (Exception ex)
     {
         string strErrorMessage = ex.Message;
         if (ex.InnerException != null)
             strErrorMessage = string.Format("{0} --- {1}", strErrorMessage, ex.InnerException.Message);
         logger.Error(string.Format("{0}. Ошибка при загрузке изображения. {1} ", User.Identity.Name, ex.Message));
         ViewBag.ErrorMessage = strErrorMessage;
         return View("Error");
     }

     //return View("", ""); //ReturnToObject(obj);
     //return RedirectToAction("Categories", "Admin");
     return RedirectToAction("ArticleList", "Admin", new { articleId });
 }




          
#endregion


 #region Mailing

 [Authorize(Roles = "Admin, ContentManager")]
 public ActionResult MailingList(string searchWord, GridSortOptions gridSortOptions, int? page)
 {
     TryToUpdateDimSettings("ADMIN_PAGE_SIZE",
                "Количество элементов на странице в админке",
                "ADMIN_PAGE_SIZE_Mailing",
                "Количество элементов на странице в административном разделе Рассылка");

     var pagedViewModel = new PagedViewModel<Mailing>
     {
         ViewData = ViewData,
         Query = repositoryMailing.Mailings,
         GridSortOptions = gridSortOptions,
         DefaultSortColumn = "UpdateDate",
         Page = page,
         PageSize = Domain.Constants.ADMIN_PAGE_SIZE,
     }
         /*.AddFilter("searchWord", searchWord,
                    a =>
                    a.OrderNumber == sw
                        ||
                    a.Phone.Contains(searchWord) || a.UserName.Contains(searchWord) ||
                    a.UserAddress.Contains(searchWord))
         .AddFilter("startDate", Convert.ToDateTime(startDate), a => a.TransactionDate >= dStart)
         .AddFilter("endDate", Convert.ToDateTime(endDate), a => a.TransactionDate <= dEnd)
         .AddFilter("isActive", isActive, a => a.IsActive == isActive)*/
.Setup();

     if (Request.IsAjaxRequest())
     {
         Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
         return PartialView("MailingListGridPartial", pagedViewModel);
     }

     return View(pagedViewModel);
 }

          [Authorize(Roles = "Admin, ContentManager")]
         public PartialViewResult Mailing(int mailingId=0)
          {
              if (mailingId == 0)
              {
                  Mailing mailing = new Mailing();
                  mailing.UpdateDate = DateTime.Now;
                  return PartialView(mailing);
              }
              else
              {
                  Mailing mailing = repositoryMailing.Mailings.FirstOrDefault(x => x.MailingID == mailingId);
                  return PartialView(mailing);
              }
              
          }

          [HttpPost]
          [ValidateInput(false)]
          public ActionResult Mailing(Mailing mailing, string action)
          {
              if (ModelState.IsValid)
              {
                  if (action=="Сохранить")
                  {
                      repositoryMailing.SaveMailing(mailing);
                  }
                  else if (action=="Отправить")
                  {
                      IEnumerable<string> emails = repositoryUser.UsersInfo.Where(x => x.Mailing == true && x.IsActive == true && x.Email == "gr*****@yandex.ru").Select(x => x.Email);
                  
                      //              IEnumerable<EmailOrderProcessor.EmailSettings> maillist;
                      repositoryOrder.MassMailingDelivery(mailing.Subject, mailing.Body, emails);
                      mailing.IsSent = true;
                      repositoryMailing.SaveMailing(mailing);
                  }
                  return View("MailingList");  //View();
              }
              else
              {
                  if (Request.IsAjaxRequest())
                  {
                      return PartialView("Mailing",mailing);    
                  }
                  TempData["message"] = string.Format("Некорректное содержание рассылки");
                  TempData["messageType"] = "warning-msg";
                  return RedirectToAction("MailingList");
              }
              
          }



          [HttpPost]
          [ValidateInput(false)]
          [Authorize(Roles = "Admin, ContentManager")]
          public ActionResult DeleteMailing(int? MailingID)
          {
              if (MailingID == 0 || MailingID == null)
              {
                  return RedirectToAction("MailingList");
              }
              Mailing mailing = repositoryMailing.Mailings.FirstOrDefault(x => x.MailingID == MailingID);
              if (mailing == null)
              {
                  return RedirectToAction("MailingList");
              }
              if (mailing.MailingID == 0)
              {
                  return RedirectToAction("MailingList");
              }
              repositoryMailing.DeleteMailing(mailing);
              logger.Warn(string.Format("{0}. Рассылка {1} удалена", User.Identity.Name, mailing.MailingID));
              
              return RedirectToAction("MailingList");
              
          }
 
 
 #endregion


          #region Galery

          public ActionResult Galery()
          {

             /* var files = from f in System.IO.Directory.GetFiles(
                                     Server.MapPath("~/Content/Galery/"),
                                     "*.*",
                                     SearchOption.TopDirectoryOnly)
                          select System.IO.Path.GetFileName(f);*/

              return View(GaleryImageList());
 


              /*string strSaveFileName = productId.ToString() + "_" + productImage.ProductImageID.ToString() + productImage.ImageExt;
              
              string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                              Constants.GALERY_IMAGES_FOLDER, strSaveFileName);
                              string strSavePreviewFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                                     Constants.PRODUCT_IMAGE_FOLDER,
                                                                                     Constants.PRODUCT_IMAGE_PREVIEW_FOLDER,
                                                                                     strSaveFileName);
                              if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);
                              if (System.IO.File.Exists(strSavePreviewFullPath)) System.IO.File.Delete(strSavePreviewFullPath);
              */
        //      files3 = files3;
          //    return View(files);

          }


          [HttpPost]
          public ActionResult UploadInGalery(HttpPostedFileBase imagefile)
          {
               // Получаем объект, для которого загружаем картинку
              if (imagefile == null)
              {
                  TempData["message"] = string.Format("Изображение не было загружено");
                  TempData["messageType"] = "error-msg";
                  return RedirectToAction("Galery");
              }
              
              try
              {
                      // Определяем название конечного графического файла вместе с полным путём.
                      // Название файла должно быть такое же, как ID объекта. Это гарантирует уникальность названия.
                      // Расширение должно быть такое же, как расширение у исходного графического файла.
                      string strExtension = System.IO.Path.GetExtension(imagefile.FileName);
                  string strSaveFileName = imagefile.FileName; //+ strExtension;
                      string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                      Constants.GALERY_IMAGES_FOLDER,
                                                                      strSaveFileName);

                      // Если файл с таким названием имеется, удаляем его.
                      if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);

                      // Сохраняем картинку, изменив её размеры.
                      imagefile.ResizeAndSave(Constants.GALERY_IMAGES_HEIGHT, Constants.GALERY_IMAGES_WIDTH,
                                              strSaveFullPath);
              }
              catch (Exception ex)
              {
                  string strErrorMessage = ex.Message;
                  if (ex.InnerException != null)
                      strErrorMessage = string.Format("{0} --- {1}", strErrorMessage, ex.InnerException.Message);
                  logger.Error(User.Identity.Name + ". Ошибка при сохранении изображения " + ex.Message);
                  ViewBag.ErrorMessage = strErrorMessage;
                  TempData["message"] = string.Format("Изображение не было загружено");
                  TempData["messageType"] = "information-msg";
              }
               

              return RedirectToAction("Galery");
              
          }



          [HttpPost]
          public ActionResult UploadManyInGalery(IEnumerable<HttpPostedFileBase> imagefiles)
          {
              IEnumerable<HttpPostedFileBase> imagefiles2 = imagefiles.Where(x => x != null);
              if (imagefiles.Count() == 0)
              {
                  TempData["message"] = string.Format("Изображение не было загружено");
                  TempData["messageType"] = "information-msg";
                  return RedirectToAction("Galery");
              }

              // Получаем объект, для которого загружаем картинку
              foreach (var imagefile in imagefiles2)
              {
                  try
                  {
                      // Определяем название конечного графического файла вместе с полным путём.
                      // Название файла должно быть такое же, как ID объекта. Это гарантирует уникальность названия.
                      // Расширение должно быть такое же, как расширение у исходного графического файла.
                      string strExtension = System.IO.Path.GetExtension(imagefile.FileName);
                      string strSaveFileName = imagefile.FileName; //+ strExtension;
                      string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                      Constants.GALERY_IMAGES_FOLDER,
                                                                      strSaveFileName);

                      // Если файл с таким названием имеется, удаляем его.
                      if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);

                      // Сохраняем картинку, изменив её размеры.
                      imagefile.ResizeAndSave(Constants.GALERY_IMAGES_HEIGHT, Constants.GALERY_IMAGES_WIDTH,
                                              strSaveFullPath);

                      if (Request.IsAjaxRequest())
                      {
                          return PartialView("GaleryPartial", GaleryImageList());
                      }
                  }
                  catch (Exception ex)
                  {
                      string strErrorMessage = ex.Message;
                      if (ex.InnerException != null)
                          strErrorMessage = string.Format("{0} --- {1}", strErrorMessage, ex.InnerException.Message);
                      logger.Error(User.Identity.Name + ". Ошибка при сохранении изображения " + ex.Message);
                      ViewBag.ErrorMessage = strErrorMessage;
                      TempData["message"] = string.Format("Изображение не было загружено");
                      TempData["messageType"] = "information-msg";
                  }
              }

              return RedirectToAction("Galery");

          }



          public ActionResult DeleteGaleryImage(string filename)
          {
              try
              {
                  string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                     Constants.GALERY_IMAGES_FOLDER,
                                                                     filename);

                  // Если файл с таким названием имеется, удаляем его.
                  if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Delete(strSaveFullPath);

                  if (Request.IsAjaxRequest())
                  {
                      return PartialView("GaleryPartial", GaleryImageList());
                  }
              }
              catch (Exception)
              {
                  
                  throw;
              }
              return RedirectToAction("Galery");
          }
          
          [HttpPost]
          public ActionResult GaleryRenameImage(string oldfilename, string newfilename)
          {
              try
              {
                  string strFolderPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                     Constants.GALERY_IMAGES_FOLDER);

                  string strSaveFullPath = System.IO.Path.Combine(Server.MapPath(Url.Content("~/Content")),
                                                                     Constants.GALERY_IMAGES_FOLDER,
                                                                     oldfilename);

                  string extension = oldfilename.Substring(oldfilename.LastIndexOf('.'));

                  // Если файл с таким названием имеется, удаляем его.
                  if (System.IO.File.Exists(strSaveFullPath)) System.IO.File.Move(strFolderPath + '/' + oldfilename, strFolderPath + '/' + newfilename+extension);

                  if (Request.IsAjaxRequest())
                  {
                      return PartialView("GaleryPartial", GaleryImageList());
                  }
              }
              catch (Exception)
              {
                  TempData["message"] = string.Format("Вероятно, что файл с таким именем уже существует");
                  TempData["messageType"] = "error-msg";
                  return RedirectToAction("Galery");
              }
              return RedirectToAction("Galery");
          }

          public IEnumerable<string> GaleryImageList()
          {
              var files = from f in System.IO.Directory.GetFiles(
                                     Server.MapPath("~/Content/galery/"),
                                     "*.*",
                                     SearchOption.TopDirectoryOnly)
                          select System.IO.Path.GetFileName(f);
              return files.OrderBy(x => x.ToString()).ToList();
          }

          #endregion


          #region Dasboard
       
              public IEnumerable<OrdersSummary> GetOrders()
              {
                  //Thread.Sleep(500);
                  IEnumerable<OrdersSummary> os = repositoryOrderSummary.OrdersSummaryInfo.ToList();
                  return os;
              }


              public async Task<IEnumerable<OrdersSummary>> SlowOperationAsync()
              {

                  IEnumerable<OrdersSummary> result = await repositoryOrderSummary.OrdersSummaryInfo.ToListAsync(); //GetAllAsync();
                  return result;
              }
              public IEnumerable<OrdersSummary> Get()
             
              {
                  var task = SlowOperationAsync(); //Task.Factory.StartNew<IEnumerable<OrdersSummary>>(SlowOperation);
                  var data = task.GetAwaiter().GetResult();

                  return data;
              }


          private async Task<IEnumerable<Product>> GetProductAsync()
          {
              Debug.WriteLine("Поток №{0} Product", Thread.CurrentThread.ManagedThreadId);
              Task<List<Product>> getProductTask = repositoryProduct.Products.ToListAsync();
              return await getProductTask;
          }

          private async Task<IEnumerable<OrderDetails>> GetOrderDetailsAsync()
          {
              Debug.WriteLine("Поток №{0} Product", Thread.CurrentThread.ManagedThreadId);
              Task<List<OrderDetails>> getOrderDetails = repositoryOrderDetails.OrdersDetails.ToListAsync();
              return await getOrderDetails;
          }


          //  [AsyncTimeout(8000)]
         // [HandleError(ExceptionType = typeof(TimeoutException), View = "TimedOut")]
          public async Task<ActionResult> Dasboard(string dashboardAction="")
          {
              
          StopWatchSimpleModel model = new StopWatchSimpleModel();
              
              model.AddMessage("Запущено");
              //ViewBag.Time1 = model.ElapsedTime;
              //var p111 = unchecked((long)model.ElapsedTime);
              Debug.WriteLine("Процденное время {0}", model.ElapsedTime);
              //TempData["GetTim1"] = p111; 


              
              
              //  var task = SlowOperationAsync();
              //IEnumerable<OrdersSummary> os1 = repositoryOrderSummary.OrdersSummaryInfo.ToList();

              //Task t = repositoryOrderSummary.GetAllAsync();
              //t.Start();
              //t.Wait();

              
              //var orderSummaryAsync = await Task.Run(() => repositoryOrderSummary.OrdersSummaryInfo.ToListAsync()); // GetOrderSummaryListAsync();
              var orderSummaryAsync = repositoryOrderSummary.OrdersSummaryInfo.ToList(); // GetOrderSummaryListAsync();
                
              //var productsAsync = await Task.Run(() => repositoryProduct.Products.ToListAsync());//repositoryProduct.Products.ToList();// GetProductListAsync();


              //Task<IEnumerable<Product>> productList = GetProductAsync();
              //var productsAsync = await Task.Run(() => GetProductAsync());
              var productsAsync = repositoryProduct.Products.ToList();
              //var productsAsync = await productList;

              //var categoryAsync = await Task.Run(() => repositoryCategory.Categories.ToListAsync());//repositoryCategory.Categories.ToList();// GetCategoryListAsync();
              var categoryAsync = repositoryCategory.Categories;//repositoryCategory.Categories.ToList();// GetCategoryListAsync();
              

              model.AddMessage("Окончание");
              Debug.WriteLine("Пройденное время {0}", model.ElapsedTime);


              //var userAsync = await Task.Run(() => repositoryUser.UsersInfo.ToListAsync());  //repositoryUser.UsersInfo.ToList();// GetUserListAsync();
              var userAsync = repositoryUser.UsersInfo.ToList();// GetUserListAsync();
              
              Debug.WriteLine("Поток №{0}", Thread.CurrentThread.ManagedThreadId);
              //IEnumerable<OrderStatus> ost = await Task.Run(() => repositoryOrderStatus.OrderStatuses.ToListAsync()); //repositoryOrderStatus.OrderStatuses.ToList();
              IEnumerable<OrderStatus> ost = repositoryOrderStatus.OrderStatuses.ToList();

              Debug.WriteLine("Поток №{0}", Thread.CurrentThread.ManagedThreadId);
              //IEnumerable<DimOrderStatus> dos1 = await Task.Run(() => repositoryDimOrderStatus.DimOrderStatuses.ToListAsync()); //repositoryDimOrderStatus.DimOrderStatuses.ToList();
              IEnumerable<DimOrderStatus> dos1 = repositoryDimOrderStatus.DimOrderStatuses.ToList(); //repositoryDimOrderStatus.DimOrderStatuses.ToList();

              Debug.WriteLine("Поток №{0}", Thread.CurrentThread.ManagedThreadId);
              IEnumerable<OrderDetails> orderDetails = await Task.Run(() => repositoryOrderDetails.OrdersDetails.ToListAsync());  //repositoryOrderDetails.OrdersDetails.ToList();
              //IEnumerable<OrderDetails> orderDetails = repositoryOrderDetails.OrdersDetails.ToList();

              Debug.WriteLine("Поток №{0}", Thread.CurrentThread.ManagedThreadId);
              //IEnumerable<SuperCategory> superCategories = await Task.Run(() => repositorySuperCategory.SuperCategories.ToListAsync());  // repositorySuperCategory.SuperCategories.ToList();
              IEnumerable<SuperCategory> superCategories = repositorySuperCategory.SuperCategories.ToList();  // repositorySuperCategory.SuperCategories.ToList();

              Debug.WriteLine("Поток №{0}", Thread.CurrentThread.ManagedThreadId);
              //IEnumerable<Role> roles = await Task.Run(() => repositoryUser.Roles.ToListAsync()); //repositoryUser.Roles.ToList();
              IEnumerable<Role> roles = repositoryUser.Roles.ToList();

              Debug.WriteLine("Поток №{0}", Thread.CurrentThread.ManagedThreadId);
        
           //   orderSummaryAsync.Wait();
              //IEnumerable<OrdersSummary> os1 = orderSummaryAsync.Result.ToList(); //repositoryOrderSummary.OrdersSummaryInfo.ToList();
              IEnumerable<OrdersSummary> os1 = orderSummaryAsync; //repositoryOrderSummary.OrdersSummaryInfo.ToList();

              var p1111 = model.ElapsedTime;
              TempData["GetTim2"] = p1111;
              Debug.WriteLine("Пройденное время {0}", model.ElapsedTime);
              //DasboardOrdersStatusCount========================================================================================================
              var z0 = from dos in dos1
                           join os in ost on dos.DimOrderStatusID equals
                               os.DimOrderStatusID
                           join osum in os1 on os.OrderSummaryID equals
                               osum.OrderSummaryID
                           select
                               new
                                   {
                                       dos.DimOrderStatusID,
                                       dos.OrderStatusDesc,
                                       os.OrderStatusID,
                                       osum.OrderSummaryID,
                                       osum.TransactionDate,
                                       osum.UserID
                                   };

                  var p0 = (from p1 in z0
                            group p1.OrderStatusDesc by p1.OrderStatusDesc
                            into g
                            select
                                new {Desc = g.Key, AllTime = g.Count(), Yearly = 0, Monthly = 0, Weekly = 0, Daily = 0})
                      .Union(
                          from p1 in z0
                          where
                              p1.TransactionDate <= DateTime.Now && p1.TransactionDate > DateTime.Now.AddYears(-1)
                          group p1.OrderStatusDesc by p1.OrderStatusDesc
                          into g
                          select new {Desc = g.Key, AllTime = 0, Yearly = g.Count(), Monthly = 0, Weekly = 0, Daily = 0})
                      .Union(
                          from p1 in z0
                          where
                              p1.TransactionDate <= DateTime.Now && p1.TransactionDate > DateTime.Now.AddMonths(-1)
                          group p1.OrderStatusDesc by p1.OrderStatusDesc
                          into g
                          select new {Desc = g.Key, AllTime = 0, Yearly = 0, Monthly = g.Count(), Weekly = 0, Daily = 0})
                      .Union(
                          from p1 in z0
                          where
                              p1.TransactionDate <= DateTime.Now && p1.TransactionDate > DateTime.Now.AddDays(-7)
                          group p1.OrderStatusDesc by p1.OrderStatusDesc
                          into g
                          select new {Desc = g.Key, AllTime = 0, Yearly = 0, Monthly = 0, Weekly = g.Count(), Daily = 0})
                      .Union(
                          from p1 in z0
                          where
                              p1.TransactionDate <= DateTime.Now && p1.TransactionDate > DateTime.Now.AddDays(-1)
                          group p1.OrderStatusDesc by p1.OrderStatusDesc
                          into g
                          select new {Desc = g.Key, AllTime = 0, Yearly = 0, Monthly = 0, Weekly = 0, Daily = g.Count()});

                  var z00 = from zed in p0
                          group zed by new {zed.Desc}
                          into g
                          select new DasboardOrderStatusCountModel
                              {
                                  DimOrderStatusDesc = g.Key.Desc,
                                  AllTimeCount = g.Sum(x => x.AllTime),
                                  YaerlyCount = g.Sum(x => x.Yearly),
                                  MonthlyCount = g.Sum(x => x.Monthly),
                                  WeeklyCount = g.Sum(x => x.Weekly),
                                  DailyCount = g.Sum(x => x.Daily)
                              };




                  var allTime0 = (from osum in os1
                                 select osum.OrderSummaryID).Except(from os in ost
                                                                    join osum in
                                                                        os1 on
                                                                        os.OrderSummaryID equals
                                                                        osum.OrderSummaryID
                                                                    select os.OrderSummaryID);

                  var yearly0 = (from osum in os1
                                where
                                    osum.TransactionDate <= DateTime.Now &&
                                    osum.TransactionDate > DateTime.Now.AddYears(-1)
                                select osum.OrderSummaryID).Except(from os in ost
                                                                   join osum in
                                                                       os1 on
                                                                       os.OrderSummaryID equals
                                                                       osum.OrderSummaryID
                                                                   where
                                                                       osum.TransactionDate <= DateTime.Now &&
                                                                       osum.TransactionDate > DateTime.Now.AddYears(-1)
                                                                   select os.OrderSummaryID);


                  var monthly0 = (from osum in os1
                                 where
                                     osum.TransactionDate <= DateTime.Now &&
                                     osum.TransactionDate > DateTime.Now.AddMonths(-1)
                                 select osum.OrderSummaryID).Except(from os in ost
                                                                    join osum in
                                                                        os1 on
                                                                        os.OrderSummaryID equals
                                                                        osum.OrderSummaryID
                                                                    where
                                                                        osum.TransactionDate <= DateTime.Now &&
                                                                        osum.TransactionDate >
                                                                        DateTime.Now.AddMonths(-1)
                                                                    select os.OrderSummaryID);

                  var weekly0 = (from osum in os1
                                where
                                    osum.TransactionDate <= DateTime.Now &&
                                    osum.TransactionDate > DateTime.Now.AddDays(-7)
                                select osum.OrderSummaryID).Except(from os in ost
                                                                   join osum in
                                                                       os1 on
                                                                       os.OrderSummaryID equals
                                                                       osum.OrderSummaryID
                                                                   where
                                                                       osum.TransactionDate <= DateTime.Now &&
                                                                       osum.TransactionDate > DateTime.Now.AddDays(-7)
                                                                   select os.OrderSummaryID);


                  var daily0 = (from osum in os1
                               where
                                   osum.TransactionDate <= DateTime.Now &&
                                   osum.TransactionDate > DateTime.Now.AddDays(-1)
                               select osum.OrderSummaryID).Except(from os in ost
                                                                  join osum in
                                                                      os1 on
                                                                      os.OrderSummaryID equals
                                                                      osum.OrderSummaryID
                                                                  where
                                                                      osum.TransactionDate <= DateTime.Now &&
                                                                      osum.TransactionDate > DateTime.Now.AddDays(-1)
                                                                  select os.OrderSummaryID);

                  var fullCount0 = (from fg in allTime0
                                   group fg by "Заказ не оформлен"
                                   into g
                                   select
                                       new
                                           {
                                               Desc = g.Key,
                                               AllTime = g.Count(),
                                               Yearly = 0,
                                               Monthly = 0,
                                               Weekly = 0,
                                               Daily = 0
                                           })
                      .Union(

                          from fg in yearly0
                          group fg by "Заказ не оформлен"
                          into g
                          select new {Desc = g.Key, AllTime = 0, Yearly = g.Count(), Monthly = 0, Weekly = 0, Daily = 0})
                      .Union(

                          from fg in monthly0
                          group fg by "Заказ не оформлен"
                          into g
                          select new {Desc = g.Key, AllTime = 0, Yearly = 0, Monthly = g.Count(), Weekly = 0, Daily = 0})
                      .Union(

                          from fg in weekly0
                          group fg by "Заказ не оформлен"
                          into g
                          select new {Desc = g.Key, AllTime = 0, Yearly = 0, Monthly = 0, Weekly = g.Count(), Daily = 0})
                      .Union(

                          from fg in daily0
                          group fg by "Заказ не оформлен"
                          into g
                          select new {Desc = g.Key, AllTime = 0, Yearly = 0, Monthly = 0, Weekly = 0, Daily = g.Count()});

                  var viewModel0 = from sd in fullCount0
                                  group sd by new {sd.Desc}
                                  into g
                                  select new DasboardOrderStatusCountModel()
                                      {
                                          DimOrderStatusDesc = g.Key.Desc,
                                          AllTimeCount = g.Sum(x => x.AllTime),
                                          YaerlyCount = g.Sum(x => x.Yearly),
                                          MonthlyCount = g.Sum(x => x.Monthly),
                                          WeeklyCount = g.Sum(x => x.Weekly),
                                          DailyCount = g.Sum(x => x.Daily)
                                      };

                  var ff = z00.Union(viewModel0);
              if (Request.IsAjaxRequest() && dashboardAction=="DasboardOrderStatusCount")
              {
                  return PartialView(ff);
              }

                  

              //End DasboardStatusCount===========================================================================================================




              //DasboardOrderStatusCharges  ===========================================================================================================
              

                  var tmp1 = (from dos in dos1
                             join os in ost on dos.DimOrderStatusID equals
                                 os.DimOrderStatusID
                             join osum in os1 on os.OrderSummaryID equals
                                 osum.OrderSummaryID
                             select new {dos.OrderStatusDesc, osum.TransactionDate, osum.TotalValue});


                  var allTime1 = (from a1 in tmp1
                                 group a1 by a1.OrderStatusDesc
                                 into g
                                 select
                                     new
                                         {
                                             Desc = g.Key,
                                             AllTime = g.Sum(x => x.TotalValue),
                                             Yearly = 0,
                                             Monthly = 0,
                                             Weekly = 0,
                                             Daily = 0
                                         });

                  var yearly1 = (from a1 in tmp1
                                where
                                    a1.TransactionDate <= DateTime.Now && a1.TransactionDate > DateTime.Now.AddYears(-1)
                                group a1 by a1.OrderStatusDesc
                                into g
                                select
                                    new
                                        {
                                            Desc = g.Key,
                                            AllTime = 0,
                                            Yearly = g.Sum(x => x.TotalValue),
                                            Monthly = 0,
                                            Weekly = 0,
                                            Daily = 0
                                        });


                  var monthly1 = (from a1 in tmp1
                                 where
                                     a1.TransactionDate <= DateTime.Now &&
                                     a1.TransactionDate > DateTime.Now.AddMonths(-1)
                                 group a1 by a1.OrderStatusDesc
                                 into g
                                 select
                                     new
                                         {
                                             Desc = g.Key,
                                             AllTime = 0,
                                             Yearly = 0,
                                             Monthly = g.Sum(x => x.TotalValue),
                                             Weekly = 0,
                                             Daily = 0
                                         });

                  var weekly1 = (from a1 in tmp1
                                where
                                    a1.TransactionDate <= DateTime.Now && a1.TransactionDate > DateTime.Now.AddDays(-7)
                                group a1 by a1.OrderStatusDesc
                                into g
                                select
                                    new
                                        {
                                            Desc = g.Key,
                                            AllTime = 0,
                                            Yearly = 0,
                                            Monthly = 0,
                                            Weekly = g.Sum(x => x.TotalValue),
                                            Daily = 0
                                        });


                  var daily1 = (from a1 in tmp1
                               where
                                   a1.TransactionDate <= DateTime.Now && a1.TransactionDate > DateTime.Now.AddDays(-1)
                               group a1 by a1.OrderStatusDesc
                               into g
                               select
                                   new
                                       {
                                           Desc = g.Key,
                                           AllTime = 0,
                                           Yearly = 0,
                                           Monthly = 0,
                                           Weekly = 0,
                                           Daily = g.Sum(x => x.TotalValue)
                                       });

                  var fullCount1 = (from fg in allTime1
                                   select
                                       new
                                           {
                                               Desc = fg.Desc,
                                               AllTime = (decimal) fg.AllTime,
                                               Yearly = (decimal) fg.Yearly,
                                               Monthly = (decimal) fg.Monthly,
                                               Weekly = (decimal) fg.Weekly,
                                               Daily = (decimal) fg.Daily
                                           }).Union(from fg in yearly1
                                                    select
                                                        new
                                                            {
                                                                Desc = fg.Desc,
                                                                AllTime = (decimal) fg.AllTime,
                                                                Yearly = (decimal) fg.Yearly,
                                                                Monthly = (decimal) fg.Monthly,
                                                                Weekly = (decimal) fg.Weekly,
                                                                Daily = (decimal) fg.Daily
                                                            }).Union(from fg in monthly1
                                                                     select
                                                                         new
                                                                             {
                                                                                 Desc = fg.Desc,
                                                                                 AllTime = (decimal) fg.AllTime,
                                                                                 Yearly = (decimal) fg.Yearly,
                                                                                 Monthly = (decimal) fg.Monthly,
                                                                                 Weekly = (decimal) fg.Weekly,
                                                                                 Daily = (decimal) fg.Daily
                                                                             }).Union(from fg in weekly1
                                                                                      select
                                                                                          new
                                                                                              {
                                                                                                  Desc = fg.Desc,
                                                                                                  AllTime =
                                                                                          (decimal) fg.AllTime,
                                                                                                  Yearly =
                                                                                          (decimal) fg.Yearly,
                                                                                                  Monthly =
                                                                                          (decimal) fg.Monthly,
                                                                                                  Weekly =
                                                                                          (decimal) fg.Weekly,
                                                                                                  Daily =
                                                                                          (decimal) fg.Daily
                                                                                              }).Union(from fg in daily1
                                                                                                       select
                                                                                                           new
                                                                                                               {
                                                                                                                   Desc
                                                                                                           =
                                                                                                           fg.Desc,
                                                                                                                   AllTime
                                                                                                           =
                                                                                                           (decimal)
                                                                                                           fg.AllTime,
                                                                                                                   Yearly
                                                                                                           =
                                                                                                           (decimal)
                                                                                                           fg.Yearly,
                                                                                                                   Monthly
                                                                                                           =
                                                                                                           (decimal)
                                                                                                           fg.Monthly,
                                                                                                                   Weekly
                                                                                                           =
                                                                                                           (decimal)
                                                                                                           fg.Weekly,
                                                                                                                   Daily
                                                                                                           =
                                                                                                           (decimal)
                                                                                                           fg.Daily
                                                                                                               });

                  // var tmp = from sd in fullCount
                  var viewModel1 = from sd in fullCount1
                                  group sd by new {sd.Desc}
                                  into g
                                  select new DasboardOrderStatusChargeModel()
                                      {
                                          DimOrderStatusDesc = g.Key.Desc,
                                          AllTimeCharge = g.Sum(x => x.AllTime),
                                          YaerlyCharge = g.Sum(x => x.Yearly),
                                          MonthlyCharge = g.Sum(x => x.Monthly),
                                          WeeklyCharge = g.Sum(x => x.Weekly),
                                          DailyCharge = g.Sum(x => x.Daily)
                                      };

                  //---расчет неучтенных заказов
                  var allTimeUnfixed1 = (from osum in os1
                                        select osum.OrderSummaryID).Except(from os in ost
                                                                           join osum in
                                                                               os1 on
                                                                               os.OrderSummaryID equals
                                                                               osum.OrderSummaryID
                                                                           select os.OrderSummaryID);

                  var yearlyUnfixed1 = (from osum in os1
                                       where
                                           osum.TransactionDate <= DateTime.Now &&
                                           osum.TransactionDate > DateTime.Now.AddYears(-1)
                                       select osum.OrderSummaryID).Except(from os in ost
                                                                          join osum in
                                                                              os1 on
                                                                              os.OrderSummaryID equals
                                                                              osum.OrderSummaryID
                                                                          where
                                                                              osum.TransactionDate <= DateTime.Now &&
                                                                              osum.TransactionDate >
                                                                              DateTime.Now.AddYears(-1)
                                                                          select os.OrderSummaryID);


                  var monthlyUnfixed1 = (from osum in os1
                                        where
                                            osum.TransactionDate <= DateTime.Now &&
                                            osum.TransactionDate > DateTime.Now.AddMonths(-1)
                                        select osum.OrderSummaryID).Except(from os in ost
                                                                           join osum in
                                                                               os1 on
                                                                               os.OrderSummaryID equals
                                                                               osum.OrderSummaryID
                                                                           where
                                                                               osum.TransactionDate <= DateTime.Now &&
                                                                               osum.TransactionDate >
                                                                               DateTime.Now.AddMonths(-1)
                                                                           select os.OrderSummaryID);

                  var weeklyUnfixed1 = (from osum in os1
                                       where
                                           osum.TransactionDate <= DateTime.Now &&
                                           osum.TransactionDate > DateTime.Now.AddDays(-7)
                                       select osum.OrderSummaryID).Except(from os in ost
                                                                          join osum in
                                                                              os1 on
                                                                              os.OrderSummaryID equals
                                                                              osum.OrderSummaryID
                                                                          where
                                                                              osum.TransactionDate <= DateTime.Now &&
                                                                              osum.TransactionDate >
                                                                              DateTime.Now.AddDays(-7)
                                                                          select os.OrderSummaryID);


                  var dailyUnfixed1 = (from osum in os1
                                      where
                                          osum.TransactionDate <= DateTime.Now &&
                                          osum.TransactionDate > DateTime.Now.AddDays(-1)
                                      select osum.OrderSummaryID).Except(from os in ost
                                                                         join osum in
                                                                             os1 on
                                                                             os.OrderSummaryID equals
                                                                             osum.OrderSummaryID
                                                                         where
                                                                             osum.TransactionDate <= DateTime.Now &&
                                                                             osum.TransactionDate >
                                                                             DateTime.Now.AddDays(-1)
                                                                         select os.OrderSummaryID);

                  var fullCountUnfixed1 = (from fg in allTimeUnfixed1
                                          join osum in os1 on fg equals osum.OrderSummaryID
                                          group osum by "Заказ не оформлен"
                                          into g
                                          select
                                              new
                                                  {
                                                      Desc = g.Key,
                                                      AllTime = (decimal) g.Sum(x => x.TotalValue),
                                                      Yearly = (decimal) 0,
                                                      Monthly = (decimal) 0,
                                                      Weekly = (decimal) 0,
                                                      Daily = (decimal) 0
                                                  })
                      .Union(

                          from fg in yearlyUnfixed1
                          join osum in os1 on fg equals osum.OrderSummaryID
                          group osum by "Заказ не оформлен"
                          into g
                          select
                              new
                                  {
                                      Desc = g.Key,
                                      AllTime = (decimal) 0,
                                      Yearly = (decimal) g.Sum(x => x.TotalValue),
                                      Monthly = (decimal) 0,
                                      Weekly = (decimal) 0,
                                      Daily = (decimal) 0
                                  })
                      .Union(

                          from fg in monthlyUnfixed1
                          join osum in os1 on fg equals osum.OrderSummaryID
                          group osum by "Заказ не оформлен"
                          into g
                          select
                              new
                                  {
                                      Desc = g.Key,
                                      AllTime = (decimal) 0,
                                      Yearly = (decimal) 0,
                                      Monthly = (decimal) g.Sum(x => x.TotalValue),
                                      Weekly = (decimal) 0,
                                      Daily = (decimal) 0
                                  })
                      .Union(

                          from fg in weeklyUnfixed1
                          join osum in os1 on fg equals osum.OrderSummaryID
                          group osum by "Заказ не оформлен"
                          into g
                          select
                              new
                                  {
                                      Desc = g.Key,
                                      AllTime = (decimal) 0,
                                      Yearly = (decimal) 0,
                                      Monthly = (decimal) 0,
                                      Weekly = (decimal) g.Sum(x => x.TotalValue),
                                      Daily = (decimal) 0
                                  })
                      .Union(

                          from fg in dailyUnfixed1
                          join osum in os1 on fg equals osum.OrderSummaryID
                          group osum by "Заказ не оформлен"
                          into g
                          select
                              new
                                  {
                                      Desc = g.Key,
                                      AllTime = (decimal) 0,
                                      Yearly = (decimal) 0,
                                      Monthly = (decimal) 0,
                                      Weekly = (decimal) 0,
                                      Daily = (decimal) g.Sum(x => x.TotalValue)
                                  });

                  var finalScope = from sd in fullCountUnfixed1
                                   group sd by new {sd.Desc}
                                   into g
                                   select new DasboardOrderStatusChargeModel()
                                       {
                                           DimOrderStatusDesc = g.Key.Desc,
                                           AllTimeCharge = g.Sum(x => x.AllTime),
                                           YaerlyCharge = g.Sum(x => x.Yearly),
                                           MonthlyCharge = g.Sum(x => x.Monthly),
                                           WeeklyCharge = g.Sum(x => x.Weekly),
                                           DailyCharge = g.Sum(x => x.Daily)
                                       };

                  //var ff = z.Union(viewModel);
                  viewModel1 = viewModel1.Union(finalScope);

              if (Request.IsAjaxRequest() && dashboardAction=="DasboardOrderStatusCharges")
              {
                  return PartialView(viewModel1);    
              }
              

              //End DasboardOrderStatusCharges  ===========================================================================================================

              //DasboardUsersRegistered  ===========================================================================================================

             // userAsync.Wait();
              //IEnumerable<User> users = userAsync.Result.ToList();
              IEnumerable<User> users = userAsync;
              

              var p2 = (from ui in users
                           group ui by "Всего"
                           into g
                           select new {Desc = g.Key, Count = g.Count()}).Union(
                               from ui in users
                               where ui.Created <= DateTime.Now && ui.Created > DateTime.Now.AddYears(-1)
                                     && ui.IsActivated == true && ui.IsActive == true
                               group ui by "За год"
                               into g
                               select new {Desc = g.Key, Count = g.Count()}).Union(
                                   from ui in users
                                   where ui.Created <= DateTime.Now && ui.Created > DateTime.Now.AddMonths(-1)
                                         && ui.IsActivated == true && ui.IsActive == true
                                   group ui by "За месяц"
                                   into g
                                   select new {Desc = g.Key, Count = g.Count()}).Union(
                                       from ui in users
                                       where ui.Created <= DateTime.Now && ui.Created > DateTime.Now.AddDays(-7)
                                             && ui.IsActivated == true && ui.IsActive == true
                                       group ui by "За неделю"
                                       into g
                                       select new {Desc = g.Key, Count = g.Count()}).Union(
                                           from ui in users
                                           where ui.Created <= DateTime.Now && ui.Created > DateTime.Now.AddDays(-1)
                                                 && ui.IsActivated == true && ui.IsActive == true
                                           group ui by "За день"
                                           into g
                                           select new {Desc = g.Key, Count = g.Count()});



                  var z2 = (from values in p2
                           select new DasboardUsersCount
                               {
                                   Desc = values.Desc,
                                   Count = values.Count
                               });

                  if (z2.Where(x => x.Desc == "За день").Count() == 0)
                  {
                      DasboardUsersCount userCount = new DasboardUsersCount() {Desc = "За день", Count = 0};

                  }

              if (Request.IsAjaxRequest() && dashboardAction=="DasboardUsersRegistered")
              {
                  return PartialView(z2);    
              }
              //End DasboardUsersRegistered  ===========================================================================================================


              // DasboardBestsellerByRevenue  ===========================================================================================================

             // productsAsync.Wait();
                //IEnumerable<Product> products = productsAsync.Result.ToList();
              IEnumerable<Product> products = productsAsync;

              var m3 = from od in orderDetails
                          join os in os1 on od.OrderSummaryID equals os.OrderSummaryID
                          join p in products on od.ProductID equals p.ProductID

                          group od by p.Name
                          into g
                          select new DasboardBestsellersViewModel
                              {Desc = g.Key, Charge = g.Sum(x => x.Price*x.Quantity), Count = g.Sum(x => x.Quantity)};
                  m3 = m3.OrderByDescending(x => x.Charge).Take(10);
              if (Request.IsAjaxRequest() && dashboardAction=="DasboardBestsellerByRevenue")
              {
                  return PartialView(m3);    
              }
              //End DasboardBestsellerByRevenue   ===========================================================================================================


              //DasboardBestsellerByQuantity   ===========================================================================================================
                  
                  var m4 = from od in orderDetails
                          join os in os1 on od.OrderSummaryID equals os.OrderSummaryID
                          join p in products on od.ProductID equals p.ProductID

                          group od by p.Name
                          into g
                          select
                              new DasboardBestsellersViewModel
                                  {
                                      Desc = g.Key,
                                      Charge = g.Average(x => x.Price),
                                      Count = g.Sum(x => x.Quantity)
                                  };
                  m4 = m4.OrderByDescending(x => x.Count).Take(10);   
              if (Request.IsAjaxRequest() && dashboardAction=="DasboardBestsellerByQuantity")
              {
                  return PartialView(m4);    
              }
              //End DasboardBestsellerByQuantity  ===========================================================================================================

              //DasboardGoodsByExpensive  ===========================================================================================================
              var m5 = from p in products
                          where p.IsDeleted != true
                          select new DasboardBestsellersViewModel {Desc = p.Name, Charge = p.Price};
                  m5 = m5.OrderByDescending(x => x.Charge).Take(10);
              if (Request.IsAjaxRequest() && dashboardAction=="DasboardGoodsByExpensive")
              {
                  return PartialView(m5);    
              }
              //End DasboardGoodsByExpensive  ===========================================================================================================

              //DasboardGoodsByCheapness  ===========================================================================================================
                var m6 = from p in products
                          where p.IsDeleted != true
                          select new DasboardBestsellersViewModel {Desc = p.Name, Charge = p.Price};
                  m6 = m6.OrderBy(x => x.Charge).Take(10);

              if (Request.IsAjaxRequest() && dashboardAction=="DasboardGoodsByCheapness")
              {
                  return PartialView("DasboardGoodsByExpensive", m6);
              }
              //End DasboardGoodsByCheapness  ===========================================================================================================


              //DasboardProductExistence  ===========================================================================================================
            //  categoryAsync.Wait();
              //IEnumerable<Category> categories = categoryAsync.Result.ToList();
              IEnumerable<Category> categories = categoryAsync;
              

                  var superCategoriesIds = (from superCat in
                                                superCategories
                                            select superCat.SuperCategoryID).Except(
                                                (from c in categories
                                                 select c.CategoryID).Distinct());


                  var productsIds7 = (from prod in
                                         products
                                     select prod.ProductID).Except(
                                         (from od in orderDetails
                                          select od.ProductID).Distinct());

                  var categoriesIds = (from c in
                                           categories
                                       select c.CategoryID).Except(
                                           (from pc in products
                                            select pc.CategoryID).Distinct());

                  var p7 = (from mn in superCategoriesIds.ToList()
                           group mn by "Пустые суперкатегории"
                           into g
                           select new {Desc = g.Key, Count = g.Count()}).Union(
                               from c in superCategories
                               group c by "Всего суперкатегорий"
                               into g
                               select new {Desc = g.Key, Count = g.Count()}).Union


                      (from mn in categoriesIds.ToList()
                       group mn by "Пустые категории"
                       into g
                       select new {Desc = g.Key, Count = g.Count()}).Union(
                           from c in categories
                           where c.IsDeleted != true
                           group c by "Всего активных категорий"
                           into g
                           select new {Desc = g.Key, Count = g.Count()}).Union(
                               from c in categories
                               where c.IsDeleted
                               group c by "Удаленных категорий"
                               into g
                               select new {Desc = g.Key, Count = g.Count()}).Union(
                                   from c in products
                                   //where c.IsDeleted
                                   group c by "Товаров всего"
                                   into g
                                   select new {Desc = g.Key, Count = g.Count()}
                      ).Union(
                          from c in products
                          where c.IsDeleted == true
                          group c by "Товаров удаленных"
                          into g
                          select new {Desc = g.Key, Count = g.Count()}
                      ).Union(
                          from c in products
                          where c.IsActive == false
                          group c by "Товаров неактивных"
                          into g
                          select new {Desc = g.Key, Count = g.Count()}
                      ).Union(
                          from c in products
                          where c.Quantity <= 0
                          group c by "Товаров c нулевым количеством"
                          into g
                          select new {Desc = g.Key, Count = g.Count()}
                      ).Union(
                          from pl in productsIds7.ToList()
                          join pr in products on pl equals pr.ProductID
                          where
                              pr.IsActive == true && pr.IsDeleted == false &&
                              pr.StartDate < DateTime.Now.AddMonths(-1)
                          group pr by "Товаров непроданных ни разу"
                          into g
                          select new {Desc = g.Key, Count = g.Count()}

                      );

                  var finalViewModel = from tmp in p7
                                       select new DasboardBestsellersViewModel()
                                           {
                                               Desc = tmp.Desc,
                                               Count = tmp.Count
                                           };
              if (Request.IsAjaxRequest() && dashboardAction=="DasboardProductExistence")
              {
                  return PartialView(finalViewModel);
              }
               
              //End DasboardProductExistence  ===========================================================================================================


              //DasboardProductsUnsale  ===========================================================================================================
              var productsIds = (from p in
                                         products
                                     select p.ProductID).Except(
                                         (from od in orderDetails
                                          select od.ProductID).Distinct());

                  var productsList = from pl in productsIds.ToList()
                                     join p in products on pl equals p.ProductID
                                     where
                                         p.IsActive == true && p.IsDeleted == false &&
                                         p.StartDate < DateTime.Now.AddMonths(-1)

                                     select new DasboardBestsellersViewModel()
                                         {
                                             Desc = p.Name,
                                             Charge = p.Price,
                                             Count = p.Quantity
                                         };
              productsList=productsList.Take(15).OrderByDescending(x => x.Charge);
              if (Request.IsAjaxRequest() && dashboardAction=="DasboardProductsUnsale")
              {
                  return PartialView(productsList);
              }
              //End DasboardProductsUnsale  ===========================================================================================================


              //DasboardInRoleCount  ===========================================================================================================
              
              //userRolesAsync.Wait();
                  IEnumerable<UserRole> userRoles = repositoryUser.UserRoles.ToList();


                  var userInRoleExceptionList = (from r in roles select r.RoleID).Except((from r in roles
                                                                                          join ur in userRoles on
                                                                                              r.RoleID equals ur.RoleID
                                                                                          select ur.RoleID).Distinct());

                  var userInRoleList = from r in roles
                                       join ur in userRoles on r.RoleID equals ur.RoleID
                                       select new {r, ur};

                  var userInRoleDistinctList = (from tmp in userInRoleList
                                                group tmp by tmp.r.RoleName
                                                into g
                                                select
                                                    new DasboardBestsellersViewModel {Desc = g.Key, Count = g.Count()})
                      .Union(
                          from ur in userInRoleExceptionList
                          join r in roles on ur equals r.RoleID
                          select new DasboardBestsellersViewModel() {Desc = r.RoleName, Count = 0});
              if (Request.IsAjaxRequest() && dashboardAction=="DasboardInRoleCount")
              {
                  return PartialView(userInRoleDistinctList);
              }
              //End DasboardInRoleCount  ===========================================================================================================

              DashboardViewModel viewModel = new DashboardViewModel()
                  {
                      DasboardOrderStatusCount = ff,
                      DasboardOrderStatusCharges = viewModel1,
                      DasboardUsersRegistered = z2,
                      DasboardBestsellerByRevenue = m3,
                      DasboardBestsellerByQuantity = m4,
                      DasboardGoodsByExpensive = m5,
                      DasboardGoodsByCheapness = m6,
                      DasboardProductExistence = finalViewModel,
                      DasboardProductsUnsale = productsList,
                      DasboardInRoleCount = userInRoleDistinctList
                  };


              return PartialView(viewModel);


              //End DasboardInRoleCount  ===========================================================================================================
              /*public
              PartialViewResult DasboardOrderStatusCount (){}
          public
              PartialViewResult DasboardOrderStatusCharges(){}
          public
              PartialViewResult DasboardUsersRegistered(){}
          public
              PartialViewResult DasboardBestsellerByRevenue(){}
          public
              PartialViewResult DasboardBestsellerByQuantity(){}
          public
              PartialViewResult DasboardGoodsByExpensive(){}
          public
              PartialViewResult DasboardGoodsByCheapness(){}
          public
              PartialViewResult DasboardProductExistence(){}
          public
              PartialViewResult DasboardProductsUnsale(){}
          public
              PartialViewResult DasboardInRoleCount(){}*/
          }

          #endregion Dasboard

          
    }

    //string searchWord, GridSortOptions gridSortOptions, int? categoryId, int? page, 

}

/*    var viewModel = from order in dataManager.OrdersProcessor.Orders.ToList()
                          join user in dataManager.UsersRepository.UsersInfo.ToList() on  order.UserID equals
                              user.UserID
                          join product in dataManager.Products.Products.ToList() on order.ProductID equals
                              product.ProductID
                          group order by new { order.UserName, order.Phone, order.OrderNumber, order.Email, order.UserAddress, order.TransactionDate } into js// order.OrderNumber into js
                     
                          select new OrderViewModel 
                      {
                          UserName = js.Key.UserName,
                          Phone = js.Key.Phone,
                          Email = js.Key.Email,
                          OrderNumber = js.Key.OrderNumber,
                          UserAddress = js.Key.UserAddress,
                          TransactionDate = js.Key.TransactionDate, 
                          TotalValue = js.Sum(p=>p.Quantity*p.Price)
                      };        */
/*
IEnumerable<string> s1 = repositoryProduct.Products.Select(x => x.Category.ShortName).Distinct().ToList();

var f = from c in repositoryCategory.Categories.ToList()
        join p in repositoryProduct.Products.ToList() on c.CategoryID equals p.CategoryID
        select c;

*/





/*

            IEnumerable<Category> s = from c in repositoryCategory.Categories.ToList()
                                      join p in repositoryProduct.Products.ToList() on c.CategoryID equals
                                          p.CategoryID
                                      group c by new {c.CategoryID, c.ShortName}
                                      into tmp
                    
                                          
                                     from j in repositoryCategory.Categories.ToList()
                                      select j.ShortName.Except(tmp.Key.ShortName) into tmp2
                    
                                      from a in repositoryCategory.Categories.ToList()
                                          where a.ShortName == "basketball" 
                                          
                                          
                                      select new Category() 
                                          {
                                              CategoryID = a.CategoryID,
                                              //Products = j.Products,
                                              Name = a.Name,
                                              ShortName = a.ShortName
                                          };

           */


/*
         
        public int PageSize = 4;
       private IProductRepository repository;

       public ProductController(IProductRepository productRepository)
       {
           repository = productRepository;
       }

       public ViewResult List(string category, int page = 1)
       {
           ProductsListViewModel viewModel = new ProductsListViewModel
               {
                   Products = repository.Products
                   .Where(p=>category==null || p.Category.Name==category & p.Quantity!=0)
                                        .OrderBy(p => p.ProductID)
                                        .Skip((page - 1)*PageSize)
                                        .Take(PageSize),
                   PagingInfo = new PagingInfo
                       {
                           CurrentPage = page,
                           ItemsPerPage = PageSize,
                           TotalItems = category ==null ? repository.Products.Count() : repository.Products.Where(e=>e.Category.Name==category).Count()
                       },
                       CurrentCategory = category
               };
           return View(viewModel);
         
        */





