    using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
    using System.Web.Security;
    using Domain .Abstract;
using Domain.Concrete;
    using NLog;
    using Ninject;
    using Ninject.Activation;
  using WebUI.Infrastructure.Abstract;
using WebUI.Infrastructure.Concrete;

namespace WebUI.Infrastructure
{
    public class NinjectControllerFactory: DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel=new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
            }
            catch (Exception exception)
            {
                
                throw;
            }
            
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IUserRepository>().To<EFUserRepository>();
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            ninjectKernel.Bind<ISuperCategoryRepository>().To<EFSuperCategoryRepository>();
            ninjectKernel.Bind<ICategoryRepository>().To<EFCategoryRepository>();
          //  ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>();
            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            ninjectKernel.Bind<IOrderSummaryRepository>().To<EFOrderSummaryRepository>();
            ninjectKernel.Bind<IOrderDetailsRepository>().To<EFOrderDetailsRepository>();
            ninjectKernel.Bind<IProductImageRepository>().To<EFProductImageRepository>();
            ninjectKernel.Bind<IOrderStatusRepository>().To<EFOrderStatusRepository>();
            ninjectKernel.Bind<IDimOrderStatusRepository>().To<EFDimOrderStatusRepository>();
            ninjectKernel.Bind<IDimSettingTypeRepository>().To<EFDimSettingTypeRepository>();
            ninjectKernel.Bind<IDimSettingRepository>().To<EFDimSettingRepository>();
            ninjectKernel.Bind<INewsTapeRepository>().To<EFNewsTapeRepository>();
            ninjectKernel.Bind<IArticleRepository>().To<EFArticleRepository>();
            ninjectKernel.Bind<INLogRepository>().To<EFNLogRepository>();
            ninjectKernel.Bind<IDimShippingRepository>().To<EFDimShippingRepository>();
            ninjectKernel.Bind<IMailingRepository>().To<EFMailingRepository>();


            ninjectKernel.Bind<IOrderProcessor>()
                         .To<EmailOrderProcessor>();
                          //.WithConstructorArgument("settings", emailSettings);
          /*  EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            */

            //ninjectKernel.Bind<ILogger>().To<NLogLogger>().InSingletonScope();
            ninjectKernel.Bind<ILogger>().ToMethod(CreateLog);

            ninjectKernel.Bind<EFDbContext>()
                  .ToSelf()
                  .WithConstructorArgument("connectionString",
                                           ConfigurationManager.ConnectionStrings[0].ConnectionString);
            ninjectKernel.Inject(Membership.Provider);
            ninjectKernel.Inject(Roles.Provider);


        }

        private static ILogger CreateLog(IContext ctx)
        {
            var p = ctx.Parameters.FirstOrDefault(x =>
                x.Name == LogConstants.LoggerNameParam);
            var loggerName = (null != p) ? p.GetValue(ctx, null).ToString() : null;

            if (string.IsNullOrWhiteSpace(loggerName))
            {
                if (null == ctx.Request.ParentRequest)
                {
                    throw new NullReferenceException(
                        "ParentRequest is null; unable to determine logger name; "
                        + "if not injecting into a ctor a parameter for the "
                        + "logger name must be provided");
                }

                var service = ctx.Request.ParentRequest.Service;
                loggerName = service.FullName;
            }

            var log = (ILogger)LogManager.GetLogger(loggerName, typeof(NLogLogger));
            return log;
        }

        public class LogConstants
        {
            public const string LoggerNameParam = "loggerName";
        }

        /*
        public void InjectMembership(MembershipProvider provider)
        {
            ninjectKernel.Inject(provider);
        }

        public void InjectRoleProvider(RoleProvider provider)
        {
            ninjectKernel.Inject(provider);
        }*/
    } 
}