﻿using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace BootstrapSupport
{
    public class BootstrapBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-migrate-{version}.js",
                "~/Scripts/jquery.keypad.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/typeahead.js",
                "~/Scripts/jquery.validate.js",
                "~/scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.validate.unobtrusive-custom-for-bootstrap.js"
                ));


            bundles.Add(new ScriptBundle("~/neededjs").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-migrate-{version}.js",
                "~/Scripts/jquery.keypad.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/typeahead.js"
                ));


            bundles.Add(new ScriptBundle("~/registerBundle").Include(
                "~/scripts/hogan-2.0.0.js",
                "~/Scripts/knockout*",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/typeahead.js",
                "~/Scripts/Registerdog.js"
                ));


            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/typeahead.js-bootstrap.css",
                "~/Content/bootstrap-datepicker.css",
                "~/Content/body.css",
                "~/Content/bootstrap-responsive.css",
                "~/Content/validation.css"
                ));
        }
    }
}