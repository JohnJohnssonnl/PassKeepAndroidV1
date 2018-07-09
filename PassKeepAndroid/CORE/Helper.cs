using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PassKeepAndroid.CORE
{
    public class Helper
    {
        //Helper methods
        public static string GetApplicationVersion()
        {
            String VersionName = global::Android.App.Application.Context.PackageManager.GetPackageInfo(global::Android.App.Application.Context.PackageName, 0).VersionName.ToString();
            String VersionId = global::Android.App.Application.Context.PackageManager.GetPackageInfo(global::Android.App.Application.Context.PackageName, 0).VersionCode.ToString();
            return "Build: " + VersionId + " (" + VersionName + ")";
        }
    }
}