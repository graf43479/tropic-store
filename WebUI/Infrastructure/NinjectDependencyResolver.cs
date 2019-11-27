using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Domain.Abstract;
using Domain.Concrete;
using Ninject;
using WebUI.Infrastructure.Abstract;

namespace WebUI.Infrastructure.Concrete
{
  /*  public class NinjectDependencyResolver: IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

       // protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
       // {
       //     return controllerType == null ? null : (IController)kernel.Get(controllerType);
       // }
       // 
        private void AddBindings()
        {
            kernel.Bind<IUserRepository>().To<EFUserRepository>(); 
            kernel.Bind<IProductRepository>().To<EFProductRepository>();
            kernel.Bind<ICategoryRepository>().To<EFCategoryRepository>();
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            kernel.Inject(Membership.Provider);
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>()
                         .To<EmailOrderProcessor>()
                         .WithConstructorArgument("settings", emailSettings);

            kernel.Bind<EFDbContext>()
                  .ToSelf()
                  .WithConstructorArgument("connectionString",
                                           ConfigurationManager.ConnectionStrings[0].ConnectionString);

            
        }

       

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    } */
}