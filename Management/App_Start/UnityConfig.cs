using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using DuongTrang.Core.IServices;
using DuongTrang.Core.DAL;
using System.Web.Http;
using Management.Controllers;
using Unity.Injection;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;
using Unity.Lifetime;

namespace Management
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IBookRepository, BookRepository>();
            container.RegisterType<IMenuRepository, MenuRepository>();
            container.RegisterType<IDataFirstRepository, DataFirstRepository>();
            container.RegisterType<IGetIdByName, GetIdByName>();
            container.RegisterType<IBorrowRepository, BorrowRepository>();
            container.RegisterType<ICardReaderRepository, CardReaderRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType(typeof(ISecureDataFormat<>), typeof(SecureDataFormat<>));
            container.RegisterType<ITextEncoder, Base64UrlTextEncoder>();
            container.RegisterType<IDataSerializer<AuthenticationTicket>, TicketSerializer>();
            container.RegisterType<IDataProtector>(new ContainerControlledLifetimeManager(),
            new InjectionFactory(c => new DpapiDataProtectionProvider().Create("App Name")));
            container.RegisterType<ApplicationUserManager>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}