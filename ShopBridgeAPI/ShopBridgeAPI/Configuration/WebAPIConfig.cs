using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using ShopBridgeAPI.DataAccess;
using System.Reflection;
using System.Web.Http;

namespace ShopBridgeAPI.Configuration
{
    public static class WebAPIConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableCors();


            //var builder = new ContainerBuilder();
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly(), typeof(Controller.ItemController).Assembly);
            //builder.RegisterType<Model1Container>().As<IModel1Container>().InstancePerRequest();

            //var container = builder.Build();
            ////app.UseAutofacMiddleware(container);
            //app.UseAutofacMiddleware(container);

            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container); //Set the WebApi DependencyResolver

        }
    }
}