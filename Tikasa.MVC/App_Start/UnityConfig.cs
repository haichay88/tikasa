﻿using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using MyFinance.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tikasa.Business;
using Tikasa.Data;
using Tikasa.MVC.Ioc;
using Tikasa.Service;

namespace Tikasa.MVC.App_Start
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IDatabaseFactory, DatabaseFactory>(new HttpContextLifetimeManager<IDatabaseFactory>());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HttpContextLifetimeManager<IUnitOfWork>());
            container.RegisterType<BusinessProcess>(new HttpContextLifetimeManager<BusinessProcess>());

            #region service
            container.RegisterType<ITikasaService, TikasaService>(new HttpContextLifetimeManager<ITikasaService>());
            #endregion
            #region Business
            container.RegisterType<IUserBusiness, UserBusiness>();
            container.RegisterType<IWebsiteBusiness, WebsiteBusiness>();

            #endregion



        }
    }
}