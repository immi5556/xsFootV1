using System;
using System.Web.Optimization;

namespace app.bsms
{
	public class BundleConfig
	{
		public BundleConfig()
		{
		}

		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add((new ScriptBundle("~/bundles/jquery")).Include(new string[] { "~/scripts/jQuery/jquery-{version}.js", "~/scripts/jQuery/jquery-1.12.1.min.js" }));
			bundles.Add((new ScriptBundle("~/bundles/jqueryval")).Include(new string[] { "~/scripts/jQuery/jquery.validate*", "~/Scripts/jQuery/jquery.unobtrusive*", "~/scripts/expressive.annotations.validate.min.js" }));
			bundles.Add((new ScriptBundle("~/bundles/modernizr")).Include("~/scripts/others/modernizr-*", new IItemTransform[0]));
            bundles.Add((new ScriptBundle("~/bundles/bootstrap")).Include(new string[] { "~/scripts/bootstrap/bootstrap.bundle.min.js", "~/scripts/bootstrap/bootstrap-datepicker.min.js", "~/scripts/jpeg_camera/jpeg_camera_with_dependencies.min.js", "~/scripts/camera/custom.js", "~/scripts/camera/layouts.js", "~/scripts/camera/app.js", "~/Scripts/others/cropper.min.js", "~/Scripts/others/masking-input.js", "~/scripts/portal.js", "~/Scripts/others/respond.js" }));
            bundles.Add((new StyleBundle("~/Content/css")).Include(new string[] { "~/css/bootstrap/bootstrap.css", "~/css/bootstrap/bootstrap-grid.css", "~/css/bootstrap/bootstrap-datepicker.css", "~/css/font-awesome/font-awesome.css", "~/css/site.css" }));
            //bundles.Add((new ScriptBundle("~/bundles/bootstrap")).Include(new string[] { "~/scripts/bootstrap/bootstrap.bundle.min.js", "~/scripts/bootstrap/bootstrap-datepicker.min.js", "~/scripts/jpeg_camera/jpeg_camera_with_dependencies.min.js", "~/scripts/camera/custom.js", "~/scripts/camera/layouts.js", "~/scripts/camera/app.js", "~/Scripts/others/cropper.min.js", "~/Scripts/others/masking-input.js", "~/scripts/portal.js", "~/Scripts/others/respond.js" }));
            //bundles.Add((new StyleBundle("~/Content/css")).Include(new string[] { "~/css/bootstrap/bootstrap.css", "~/css/bootstrap/bootstrap-grid.css", "~/css/bootstrap/bootstrap-datepicker.css", "~/css/font-awesome/font-awesome.css", "~/css/site.css" }));
        }
    }
}