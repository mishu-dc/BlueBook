﻿using System.Web;
using System.Web.Optimization;

namespace BlueBook.MvcUi
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Assets/Scripts/jQuery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Assets/Scripts/jQuery/jquery.validate*",
                        "~/Assets/Scripts/jQuery/additional-methods.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Assets/Scripts/bootstrap/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/gijgo").Include(
                      "~/Assets/Scripts/gijgo/combined/gijgo.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Assets/css/bootstrap/bootstrap-cerulean.css",
                      "~/Assets/css/site.css"));

            bundles.Add(new StyleBundle("~/Content/gijgo").Include(
                      "~/Assets/css/gijgo/combined/gijgo.css"));
        }
    }
}
