using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System;
using ShopBridgeAPI.DataAccess;

using Owin;
using System;
using System.Data.Entity.Infrastructure.Interception;
using System.Reflection;
using System.Web.Http;
using Microsoft.Owin;


namespace ShopBridgeAPI
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {

            //var builder = new ContainerBuilder();
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly(), typeof(Controller.ItemController).Assembly);
            //builder.RegisterType<Model1Container>().As<IModel1Container>().InstancePerRequest();

            //var container = builder.Build();
            ////app.UseAutofacMiddleware(container);
            //app.UseAutofacMiddleware(container);

            ////var httpConfiguration = new HttpConfiguration();
            ////httpConfiguration.MapHttpAttributeRoutes();

            ////WebApiConfig.Register(httpConfiguration);

            ////httpConfiguration.EnableDependencyInjection();
            ////httpConfiguration.EnsureInitialized();

            ////var httpServer = new HttpServer(httpConfiguration);
            ////app.UseWebApi(httpServer);
            ////app.UseAutofacWebApi(httpConfiguration);
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container); //Set the WebApi DependencyResolver

        }
    }
}