using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{

    /*
      private IAuthProvider authProvider;
          private DataManager dataManager;

          public AccountController(IAuthProvider auth, DataManager dataManager)
          {
              authProvider = auth;
              this.dataManager = dataManager;
     */

    public class EmailOrderProcessor: IOrderProcessor
    {
        private EmailSettings emailSettings;
        private EmailSettings operatorEmailSettings;
        private EFDbContext context;


        public EmailOrderProcessor(EFDbContext context, EmailSettings settings, EmailSettings operatorSettings)
        {
            this.context = context;
            emailSettings = settings;
            operatorEmailSettings = operatorSettings;
            
            
          
        }


        

        public void EmailTest()
        {
            var cont = context.DimSettings.Where(x => x.SettingTypeID == "ADMIN_EMAIL_SETTINGS" || x.SettingTypeID == "OPERATOR_EMAIL_SETTINGS").ToList();
            //OPERATOR_EMAIL_SETTINGS
            try
            {

                emailSettings = new EmailSettings()
                {
                    MailFromAddress = cont.FirstOrDefault(x => x.SettingsID == "MAIL_FROM_ADDRESS").SettingsValue,
                    UseSsl = Boolean.Parse(cont.FirstOrDefault(x => x.SettingsID == "MAIL_USE_SSL").SettingsValue),
                    UserName = cont.FirstOrDefault(x => x.SettingsID == "MAIL_SERVER_USER_NAME").SettingsValue,
                    Password = cont.FirstOrDefault(x => x.SettingsID == "MAIL_SERVER_PASSWORD").SettingsValue,
                    ServerName = cont.FirstOrDefault(x => x.SettingsID == "MAIL_SERVER_NAME").SettingsValue,
                    ServerPort = Int32.Parse(cont.FirstOrDefault(x => x.SettingsID == "MAIL_SERVER_PORT").SettingsValue),
                    WriteAsFile = Boolean.Parse(cont.FirstOrDefault(x => x.SettingsID == "MAIL_WRITE_AS_FILE").SettingsValue),
                    FileLocation = @"c:/sportstore"
                };


                // emailSettings.MailToAddress =
                //     context.DimSettings.FirstOrDefault(x => x.SettingsID == "MAIL_TO_ADDRESS").SettingsValue; // "***";
                /*          emailSettings.MailFromAddress = context.DimSettings.FirstOrDefault(x => x.SettingsID == "MAIL_FROM_ADDRESS").SettingsValue; //"***@ya.ru";
                          emailSettings.UseSsl = Boolean.Parse(context.DimSettings.FirstOrDefault(x => x.SettingsID == "MAIL_USE_SSL").SettingsValue);//true;
                          emailSettings.UserName = context.DimSettings.FirstOrDefault(x => x.SettingsID == "MAIL_SERVER_USER_NAME").SettingsValue;  //"gr*****";
                          emailSettings.Password = context.DimSettings.FirstOrDefault(x => x.SettingsID == "MAIL_SERVER_PASSWORD").SettingsValue;//"7777";
                          emailSettings.ServerName = context.DimSettings.FirstOrDefault(x => x.SettingsID == "MAIL_SERVER_NAME").SettingsValue; //"smtp.yandex.ru";
                          emailSettings.ServerPort = Int32.Parse(context.DimSettings.FirstOrDefault(x => x.SettingsID == "MAIL_SERVER_PORT").SettingsValue);//587;
                          emailSettings.WriteAsFile = Boolean.Parse(context.DimSettings.FirstOrDefault(x => x.SettingsID == "MAIL_WRITE_AS_FILE").SettingsValue);//false;
                          emailSettings.FileLocation = @"c:/sportstore";//context.DimSettings.FirstOrDefault(x => x.SettingsID == "MAIL_FILE_LOCATION").SettingsValue; //@"c:/sportstore/emails";*/
            }
            catch (Exception)
            {
                //       emailSettings.MailToAddress = Constants.MAIL_TO_ADDRESS;
                emailSettings.MailFromAddress = Constants.MAIL_FROM_ADDRESS;
                emailSettings.UseSsl = Constants.USE_SSL;
                emailSettings.UserName = Constants.USERNAME;
                emailSettings.Password = Constants.PASSWORD;
                emailSettings.ServerName = Constants.SERVERNAME;
                emailSettings.ServerPort = Constants.SERVER_PORT;
                emailSettings.WriteAsFile = Constants.WRITE_AS_FILE;
                emailSettings.FileLocation = @"c:/sportstore";//Constants.FILE_LOCATION;
            }

            try
            {
                operatorEmailSettings = new EmailSettings()
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

                /*   operatorEmailSettings.MailFromAddress = context.DimSettings.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_FROM_ADDRESS").SettingsValue; //"gr*****@ya.ru";
                   operatorEmailSettings.UseSsl = Boolean.Parse(context.DimSettings.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_USE_SSL").SettingsValue);//true;
                   operatorEmailSettings.UserName = context.DimSettings.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_SERVER_USER_NAME").SettingsValue;  //"gr*****";
                   operatorEmailSettings.Password = context.DimSettings.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_SERVER_PASSWORD").SettingsValue;//"7777";
                   operatorEmailSettings.ServerName = context.DimSettings.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_SERVER_NAME").SettingsValue; //"smtp.yandex.ru";
                   operatorEmailSettings.ServerPort = Int32.Parse(context.DimSettings.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_SERVER_PORT").SettingsValue);//587;
                   operatorEmailSettings.WriteAsFile = Boolean.Parse(context.DimSettings.FirstOrDefault(x => x.SettingsID == "OPERATOR_MAIL_WRITE_AS_FILE").SettingsValue);//false;
                   operatorEmailSettings.FileLocation = @"c:/sportstore";//context.DimSettings.FirstOrDefault(x => x.SettingsID == "MAIL_FILE_LOCATION").SettingsValue; //@"c:/sportstore/emails";*/
            }
            catch (Exception)
            {
                operatorEmailSettings.MailFromAddress = Constants.MAIL_FROM_ADDRESS;
                operatorEmailSettings.UseSsl = Constants.USE_SSL;
                operatorEmailSettings.UserName = Constants.USERNAME;
                operatorEmailSettings.Password = Constants.PASSWORD;
                operatorEmailSettings.ServerName = Constants.SERVERNAME;
                operatorEmailSettings.ServerPort = Constants.SERVER_PORT;
                operatorEmailSettings.WriteAsFile = Constants.WRITE_AS_FILE;
                operatorEmailSettings.FileLocation = @"c:/sportstore";//Constants.FILE_LOCATION;
            }
        }

        public void ProcessOrder(Cart cart, ShippingDatails shippingInfo, OrdersSummary os, string subject, string header, string[] contentManagersEmails)
        {
            EmailTest();
            using (var smtpClient = new SmtpClient())
            {
                {
           
                    smtpClient.EnableSsl = operatorEmailSettings.UseSsl;
                    smtpClient.Host = emailSettings.ServerName;
                    smtpClient.Port = emailSettings.ServerPort;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials=new NetworkCredential(emailSettings.UserName, emailSettings.Password);
                    
                    if (emailSettings.WriteAsFile)
                    {
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        smtpClient.PickupDirectoryLocation=emailSettings.FileLocation;
                        smtpClient.EnableSsl = false;
                    }

                    StringBuilder body = new StringBuilder();
                        /*          .AppendLine("<h3>Ваш заказ оформлен. Наши специалисты свяжутся с Вами в ближайшее время!</h3>")
                        .AppendLine("<p>------------------------------------</p>")
                        .AppendLine("<p>Товары:</p>");
              */

                    /*    .AppendLine("" + 
                                "<style>" +
                                 "    " + 
                                ".features-table " + "{width: 100%; margin: 0 auto; border-collapse: separate; border-spacing: 0; text-shadow: 0 1px 0 #fff; color: #2a2a2a; background: #fafafa;  background-image: -moz-linear-gradient(top, #fff, #eaeaea, #fff); background-image: -webkit-gradient(linear,center bottom,center top,from(#fff),color-stop(0.5, #eaeaea),to(#fff)); }" +
                                ".features-table td {height: 50px;line-height: 50px;padding: 0 20px;border-bottom: 1px solid #cdcdcd;box-shadow: 0 1px 0 white;-moz-box-shadow: 0 1px 0 white;-webkit-box-shadow: 0 1px 0 white;white-space: nowrap;text-align: center;}" +
                                ".features-table tbody td{text-align: center;font: normal 12px Verdana, Arial, Helvetica;width: 150px;}"+
                                ".features-table tbody td:first-child { width: auto; text-align: left; } " +
                                ".features-table td:nth-child(1), .features-table td:nth-child(2), .features-table td:nth-child(3),.features-table td:nth-child(4) { background: #efefef; background: rgba(144,144,144,0.15); border-right: 1px solid white; } " +
                                ".features-table td:nth-child(5) {background: #e7f3d4;  background: rgba(184,243,85,0.3);}"+
                                ".features-table thead td { font: bold 1.3em 'trebuchet MS', 'Lucida Sans', Arial;  -moz-border-radius-topright: 10px;-moz-border-radius-topleft: 10px; border-top-right-radius: 10px;border-top-left-radius: 10px;border-top: 1px solid #eaeaea; }"+
                                ".features-table thead td:first-child {border-top: none;}"+
                                ".features-table tfoot td { font: bold 1.4em Georgia;  -moz-border-radius-bottomright: 10px;-moz-border-radius-bottomleft: 10px; border-bottom-right-radius: 10px;border-bottom-left-radius: 10px;border-bottom: 1px solid #dadada;} " +
                                ".features-table tfoot td:first-child {border-bottom: none;}"+
                                "</style>");
                    */
                    if (header == "inserted")
                    {
                        body.AppendLine("<h3>Ваш заказ оформлен. Номер заказа: " + os.OrderNumber + ". Наши специалисты свяжутся с Вами в ближайшее время!</h3>");
                        if (cart.Lines.Count() == 1)
                        {
                            body.AppendLine("<p>Вами был сделан заказ на следующий товар:</p>");
                        }
                        else
                        {
                            body.AppendLine("<p>Вами был сделан закан на следующие товары:</p>");
                        }    
                    }
                    else if (header == "updated")
                    {
                        body.AppendLine("<p>Ваш заказ был скорректирован. Заказ №" + os.OrderNumber + " </p>");
                    }
                    else if (header == "succeeded")     
                        {
                            body.AppendLine("<p>Ваш заказ №" + os.OrderNumber + " выполнен. Наши специалисты свяжутся с Вами в ближайшее время, чтобы рассказать как можно получить товар!</p>");
                        }
                    
                    string tdHeader = "<td style='border: dotted 2px gray; text-align: center;'>";
                    string tdStyle = "<td style='border: dotted 2px gray;'>";
                    string tdFooter = "<td style='border: dotted 2px gray; text-align: center;'>";
                    body.AppendLine(" " +
                                    "<table style='" +
                                    "width: 100%; " +
                                    "margin: 0 auto; " +
                                    "border-collapse: separate; " +
                                    "border-spacing: 1px; " +
                                    "text-shadow: 0 1px 0 #fff; " +
                                    "color: #2a2a2a; " +
                                    "border: dotted 2px gray; " +
                                    "background: #fafafa;  " +
                                    "background-image: " +
                                    "-moz-linear-gradient(top, #fff, #eaeaea, #fff); " +
                                    "/* Firefox 3.6 */ " +
                                    "background-image: " +
                                    "-webkit-gradient(linear,center bottom,center top,from(#fff),color-stop(0.5, #eaeaea),to(#fff));'>");
                    body.AppendLine(" " +
                                    "<thead style='" +
                                    "font: bold 1.4em Georgia;  " +
                                    "text-align: center;"+
                                    "-moz-border-radius-bottomright: 10px;" +
                                        "-moz-border-radius-bottomleft: 10px; " +
                                        "border-bottom-right-radius: 10px;" +
                                        "border-bottom-left-radius: 10px;" +
                                        "border-bottom: 1px solid #dadada;'>");
                     body.AppendLine(" " +               
                                    "<tr>" +
                                        tdHeader + "Товар</td>" +
                                        tdHeader + "Категория</td>" +
                                        tdHeader + "Цена</td>" +
                                        tdHeader + "Количество</td>" +
                                        tdHeader + "Сумма</td>" +
                                    "</tr></thead>");

                    body.AppendLine("<tfoot style='" +
                                        "font: bold 1.4em Georgia;  " +
                                        "-moz-border-radius-bottomright: 10px;" +
                                        "-moz-border-radius-bottomleft: 10px; " +
                                        "border-bottom-right-radius: 10px;" +
                                        "border-bottom-left-radius: 10px;" +
                                        "border-bottom: 1px solid #dadada;'>");
                    decimal summary = cart.Lines.Sum(x => x.Product.Price*x.Quantity);
                    body.AppendLine("<tr>" + tdFooter + "</td>" + tdFooter + "</td>" + tdFooter + "</td>" + tdFooter + "Сумма:</td>" + tdFooter + summary + " руб.</td></tr>");
                    body.AppendLine("<tr>" + tdFooter + "</td>" + tdFooter + "</td>" + tdFooter + "</td>" + tdFooter + "Доставка:</td>" + tdFooter + os.ShippingPrice + " руб.</td></tr>");
                    summary += Convert.ToDecimal(os.ShippingPrice);    
                    body.AppendLine("<tr>" + tdFooter + "</td>" + tdFooter + "</td>" + tdFooter + "</td>" + tdFooter + "Итого:</td>" + tdFooter + summary + " руб.</td></tr>");
                    body.AppendLine("</tfoot>");

                    body.AppendLine("<tbody style='" +
                                    "text-align: center;" +
                                    "font: normal 1.1em Verdana, Arial, Helvetica;" +
                                    "width: 150px;'>");
                    foreach (var line in cart.Lines)
                    {
                        body.AppendLine("<tr>");
                        //ссылка работать не будет до полноценного хотсинга
                        body.AppendLine(tdStyle + line.Product.Name + "</td>" +
                                        tdStyle + line.Product.Category.Name.TrimEnd() + "</td>" +
                                        tdStyle +line.Product.Price + "</td>" +
                                        tdStyle + line.Quantity + "</td>" +
                                        tdStyle + line.Product.Price * line.Quantity + "</td>");
                        body.AppendLine("</tr>");
                    }

                    body.AppendLine("</tbody>");
                        
                        body.AppendLine("</table>");

                    //body.AppendFormat("Итого: {0:c}", cart.ComputeTotalValue())

                    body.AppendLine("<p>Детали доставки:</p>")
                        .AppendLine("<table style='font: normal 1.1em Verdana, Arial, Helvetica'>" +
                                    "<tr>" +
                                    "<td>Имя: </td>" +
                                    "<td>" + shippingInfo.ShippingName + "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                    "<td>Адрес доставки: </td>" +
                                    "<td>" + shippingInfo.ShippingAddress + "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                    "<td>Телефон: </td>" +
                                    "<td>" + shippingInfo.ShippingPhone + "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                    "<td>Email: </td>" +
                                    "<td>" + shippingInfo.ShippingEmail + "</td>" +
                                    "</tr>" +
                                    "</table>")
                        .AppendLine("<p>Контактный телефон <a href='" +
                                    Constants.SITE_URL +"'>" +
                        Constants.SITE_NAME + "</a> :  " +
                        Constants.SITE_NUMBER + "</p>");

                        
                        
                        //body.AppendFormat("Подарочная упаковка: {0}", shippingInfo.GiftWrap ? "Да" : "Нет");

                    
                    MailMessage mailMessage = new MailMessage(
                        emailSettings.MailFromAddress,
                        //emailSettings.MailToAddress,
                        shippingInfo.ShippingEmail,
                        subject,
                        body.ToString()
                        );
                    mailMessage.IsBodyHtml = true;


                    if (emailSettings.WriteAsFile)
                    {
                        mailMessage.BodyEncoding = Encoding.UTF8;
                    }
                    
                    
                   // smtpClient.Send(mailMessage);

                    try
                    {
                        smtpClient.Send(mailMessage);

                        
                    }
                    catch (Exception ex)
                    {
                        var p = ex.Message;
                      //  throw;
                    }

                    try
                    {
                 //       MailMessage mailMessageForContentManagers = new MailMessage(
                 //operatorEmailSettings.MailFromAddress,
                 //operatorEmailSettings.MailFromAddress,
                 //subject,
                 //body.ToString()
                 //);
                 //       mailMessageForContentManagers.IsBodyHtml = true;
                 //       mailMessageForContentManagers.To.Add(operatorEmailSettings.MailFromAddress);

                 //       foreach (var email in contentManagersEmails)
                 //       {
                 //           mailMessageForContentManagers.To.Add(email);
                 //       }


                 //       if (operatorEmailSettings.WriteAsFile)
                 //       {
                 //           mailMessageForContentManagers.BodyEncoding = Encoding.UTF8;
                 //       }

                        mailMessage.To.Clear();
                        mailMessage.To.Add(operatorEmailSettings.MailFromAddress);

                        try
                        {
                            smtpClient.Send(mailMessage);
                        }
                        catch (Exception exception)
                        {
                            var er_text = exception.Message;
                            
                        }
                    }
                    catch (Exception)
                    {
                     
                    }

                }
            }
        }

        public void EmailActivation(User user, string host)
        {
            EmailTest();
            string activationLink = "<p>Добрый день, " + user.UserName + "</p><p>Спасибо за интерес, проявленный к нашему сайту</p>" +
                "<p>Вы получили уведомление, потому что была произведена регистрация Вашего адреса</p>"+
                "<p>Для активизации Вашего аккаунта пройдите по ниже следующей ссылке</p>";
             //   activationLink= activationLink + "<p> <a href='http://localhost:57600/Account/Activate/" + user.Login + "/" + user.NewEmailKey + "'> http://localhost:57600/Account/Activate/" + user.Login + "/" + user.NewEmailKey + "</a></p>";
            activationLink = activationLink + "<p> <a href='http://"+host+"/Account/Activate/" + user.Login + "/" + user.NewEmailKey + "'> http://"+host+"/Account/Activate/" + user.Login + "/" + user.NewEmailKey + "</a></p>";
                activationLink = activationLink + "<p>Ваш логин: " + user.Login + "</p>" + "<p>Ваш пароль: " + user.Password + "</p>" +
                "<p>Если Вы не предпринимали попытку регистрации на сайте, то, пожалуйста, проигнорируйте данное письмо</p>";
            
            //string url = HttpWebRequest.
           /* MailMessage mailMessage = new MailMessage(
               emailSettings.MailFromAddress,
               emailSettings.MailToAddress,
               "Активация аккаунта",
               activationLink
               );
            
            mailMessage.IsBodyHtml = true;*/

                MailMessage mailMessage = new MailMessage(
                            emailSettings.MailFromAddress,
                    //emailSettings.MailToAddress,
                            user.Email,
                            "Активация аккаунта",
                            activationLink
                            );
                mailMessage.IsBodyHtml = true;


            if (emailSettings.WriteAsFile)
            {
                mailMessage.BodyEncoding = Encoding.UTF8;
            }

            using (var smtpClient = new SmtpClient())
            {
               // emailSettings.MailToAddress = user.Email;
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.UserName, emailSettings.Password);
                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception)
                {

                }
            }


        }

        /*
         smtpClient.EnableSsl = emailSettings.UseSsl;
                    smtpClient.Host = emailSettings.ServerName;
                    smtpClient.Port = emailSettings.ServerPort;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials=new NetworkCredential(emailSettings.UserName, emailSettings.Password);
                    
                    if (emailSettings.WriteAsFile)
                    {
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        smtpClient.PickupDirectoryLocation=emailSettings.FileLocation;
                        smtpClient.EnableSsl = false;
                    }
         * 
         * 
         *  
                    MailMessage mailMessage = new MailMessage(
                        emailSettings.MailFromAddress,
                        //emailSettings.MailToAddress,
                        shippingInfo.ShippingEmail,
                        "Заказ в магазине",
                        body.ToString()
                        );
                    mailMessage.IsBodyHtml = true;
         */


        public void EmailRecovery(User user)
        {
            EmailTest();
            using (var smtpClient = new SmtpClient())
            {
                emailSettings.MailToAddress = user.Email;
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.UserName, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("<p>Здравствуйте " + user.UserName + "! Ваш логин и пароль для авторизации на сайте: </p>")
                    .AppendLine("<p>------------------------------------</p>")
                    .AppendLine("<p>Логин: " + user.Login + "</p>")
                    .AppendLine("<p>Пароль: " + user.Password + "</p>")
                    .AppendLine("<p>------------------------------------</p>")
                    .AppendLine("<p>Для авторизации пройдите по ссылке: http://sssss.ru</p>");
                    
                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "Восстановление пароля",
                    body.ToString()
                    );
                mailMessage.IsBodyHtml = true;

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }


                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception)
                {

                }
            }
        }

        public void FeedBackRequest(FeedBack feedBack)
        {
            EmailTest();
            using (var smtpClient = new SmtpClient())
            {
                //operatorEmailSettings.MailToAddress = feedBack.FeedBackEmail;
                smtpClient.EnableSsl = operatorEmailSettings.UseSsl;
                smtpClient.Host = operatorEmailSettings.ServerName;
                smtpClient.Port = operatorEmailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(operatorEmailSettings.UserName, operatorEmailSettings.Password);

                if (operatorEmailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = operatorEmailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("<p>Здравствуйте " + feedBack.Name + "! Вы задали следующий вопрос администрации на нашем сайте: </p>")
                    .AppendLine("<p>------------------------------------</p>")
                    .AppendLine("<p>" + feedBack.Question + "</p>")
                    .AppendLine("<p>------------------------------------</p>")
                    .AppendLine("<p>Мы постараемся ответить вам в ближайшее время!</p>");

                MailMessage mailMessage = new MailMessage(
                    operatorEmailSettings.MailFromAddress,
                    feedBack.FeedBackEmail,
                    "Вопрос к администрации сайта",
                    body.ToString()
                    );
                mailMessage.IsBodyHtml = true;

                if (operatorEmailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }


                try
                {
                    smtpClient.Send(mailMessage);


                 /*   try
                    {
                        StringBuilder body2 = new StringBuilder()
                       .AppendLine("<p>Вопрос от " + feedBack.Name + "! </p>")
                       .AppendLine("<p>------------------------------------</p>")
                       .AppendLine("<p>" + feedBack.Question + "</p>")
                       .AppendLine("<p>------------------------------------</p>");


                        MailMessage mailMessage2 = new MailMessage(
                            operatorEmailSettings.MailFromAddress,
                            operatorEmailSettings.MailFromAddress,
                            "Вопрос к администрации сайта",
                            body2.ToString()
                            );
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Subject = "Клиент задал вопрос!";

                        if (operatorEmailSettings.WriteAsFile)
                        {
                            mailMessage.BodyEncoding = Encoding.UTF8;
                        }


                        smtpClient.Send(mailMessage2);
                    }
                    catch (Exception)
                    {

                    }*/

                    
                }
                catch (Exception)
                {
                    
                }





                

               
            }
        }


        public void FeedBackRequestForContentManagers(FeedBack feedBack, string[] emails)
        {
            EmailTest();
            using (var smtpClient = new SmtpClient())
            {
                //operatorEmailSettings.MailToAddress = feedBack.FeedBackEmail;
                smtpClient.EnableSsl = operatorEmailSettings.UseSsl;
                smtpClient.Host = operatorEmailSettings.ServerName;
                smtpClient.Port = operatorEmailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(operatorEmailSettings.UserName,
                                                               operatorEmailSettings.Password);

                if (operatorEmailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = operatorEmailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                        .AppendLine("<p>Вопрос от " + feedBack.Name + "! </p>")
                        .AppendLine("<p>------------------------------------</p>")
                        .AppendLine("<p>" + feedBack.Question + "</p>")
                        .AppendLine("<p>------------------------------------</p>")
                        .AppendLine("<p>Контактный email <a href='mailto:" +
                                    feedBack.FeedBackEmail +"'>" +
                        feedBack.FeedBackEmail + "</a> :  " +
                        "</p>");


                MailMessage mailMessage = new MailMessage(
                    operatorEmailSettings.MailFromAddress,
                    operatorEmailSettings.MailFromAddress,
                    "Клиент задал вопрос!",
                    body.ToString()
                    );
                mailMessage.IsBodyHtml = true;

                foreach (var email in emails)
                {
                    mailMessage.To.Add(email);
                }
                

                if (operatorEmailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }


                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception)
                {


                }
            }
        }

        public void MassMailingDelivery(string subject, string body, IEnumerable<string> emails)
        {
            EmailTest();
                    using (var smtpClient = new SmtpClient())
                    {
                        smtpClient.EnableSsl = operatorEmailSettings.UseSsl;
                        smtpClient.Host = operatorEmailSettings.ServerName;
                        smtpClient.Port = operatorEmailSettings.ServerPort;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(operatorEmailSettings.UserName,
                                                                       operatorEmailSettings.Password);

                        MailMessage mailMessage = new MailMessage(operatorEmailSettings.MailFromAddress, operatorEmailSettings.MailFromAddress,subject,
                            body);
                        mailMessage.IsBodyHtml = true;
                        try
                        {
                            foreach (var email in emails)
                            {
                                mailMessage.To.Clear();
                                mailMessage.To.Add(email);
                                
                                try
                                {
                                    smtpClient.Send(mailMessage);
                                    
                                }
                                catch (Exception)
                                {
                                    
                                    throw;
                                }
                            }
                            
                        }
                        catch (Exception)
                        {
                            
                         
                        }
                            
                         
                    }
            }
        

        /*
        public class EmailSettings
        {

          
            public string MailToAddress =  "gr*****@ya.ru";
            public string MailFromAddress = "gr*****@ya.ru";
            public bool UseSsl = true;
            public string UserName = "gr*****";
            public string Password = "7777";
            public string ServerName = "smtp.yandex.ru";
            public int ServerPort = 587;
            public bool WriteAsFile = false;
            public string FileLocation = @"c:/sportstore/emails";
        }

          */

        public class EmailSettings
        {
            public string MailToAddress;
            public string MailFromAddress;
            public bool UseSsl;
            public string UserName;
            public string Password;
            public string ServerName;
            public int ServerPort;
            public bool WriteAsFile;
            public string FileLocation;
        }


      
      
    }


}





