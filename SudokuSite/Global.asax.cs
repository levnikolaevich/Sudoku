using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SudokuSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            if (Request.Url.ToString().ToLower().Contains("fincase.azurewebsites.net/home/index"))
            {
                UriBuilder builder = new UriBuilder("http://fincase.azurewebsites.net/");
                Response.StatusCode = 301;
                Response.RedirectLocation = builder.ToString();
                Response.AddHeader("Location", builder.ToString());
                Response.End();
            }

            if (Request.Url.ToString().ToLower().Contains("fincase.azurewebsites.net/fincase/index"))
            {
                UriBuilder builder = new UriBuilder("http://fincase.azurewebsites.net/");
                Response.StatusCode = 301;
                Response.RedirectLocation = builder.ToString();
                Response.AddHeader("Location", builder.ToString());
                Response.End();
            }

            if (Request.Url.ToString().ToLower().Contains("/home") && Request.HttpMethod.ToLower() == "get")
            {
                Response.StatusCode = 301;
                Response.RedirectLocation = Request.Url.ToString().ToLower().Replace("/home", "/fincase");
                Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("/home", "/fincase"));
                Response.End();
            }
           

        }



    }
}
