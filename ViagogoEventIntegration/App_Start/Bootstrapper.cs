//using System;
//using System.Web.Mvc;
//using Microsoft.Practices.Unity;
//using Microsoft.Practices.Unity.Mvc;
//using ViagogoEventIntegration.Abstractions;
//using ViagogoEventIntegration.Controllers;
//using ViagogoEventIntegration.Infrastruture;
//using ViagogoEventIntegration.Repositories;

//namespace ViagogoEventIntegration
//{
//    public class Bootstrapper
//    {
//        public static IUnityContainer Initialise()
//        {
//            var container = new UnityContainer();
//            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
//            RegisterTypes(container);
//            return container;
//        }

//        private static void RegisterTypes(UnityContainer container)
//        {
//            container.RegisterType<ICredentialProvider, CredentialProvider>(new ContainerControlledLifetimeManager());
//            container.RegisterType<IViagogoApiProvider, ViagogoApiProvider>(new ContainerControlledLifetimeManager(), new InjectionConstructor(container.Resolve<ICredentialProvider>(), "MyApplication"));
//            container.RegisterType<IEventRepo, EventRepo>(new InjectionConstructor(container.Resolve<IViagogoApiProvider>()));
//            container.RegisterType<HomeController>(new InjectionConstructor(container.Resolve<IEventRepo>()));
//            container.RegisterType<EventDetailsController>(new InjectionConstructor(container.Resolve<IEventRepo>()));
//        }
//    }
//}