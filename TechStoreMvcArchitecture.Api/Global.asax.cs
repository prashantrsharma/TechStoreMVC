using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Newtonsoft.Json;
using TechStoreMvcArchitecture.Api.App_Start;
using TechStoreMvcArchitecture.Api.Formatters;

namespace TechStoreMvcArchitecture.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Formatters.Add(new CsvMediaTypeFormatter());
            GlobalConfiguration.Configuration.Formatters.Add(new SyndicationMediaTypeFormatter());
            GlobalConfiguration.Configuration.Formatters.Add(new XmlMediaTypeFormatter() { UseXmlSerializer = true});
            GlobalConfiguration.Configuration.Formatters.Add(new JsonDotNetFormatter());

            Bootstrapper.Run();
        }
    }
}