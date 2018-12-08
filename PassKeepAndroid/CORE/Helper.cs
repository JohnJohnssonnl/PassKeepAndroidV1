using Android.Support.V7.App;
using System;
using System.Threading.Tasks;

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