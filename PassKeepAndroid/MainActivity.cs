﻿using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using PassKeepAndroid.CORE;
using Android.Content;

namespace PassKeepAndroid
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);
            
            //Look, I'm finding buttons from the Layout, party mode!
            Button CreatePassStorageButton      = FindViewById<Button>(Resource.Id.CreatePassStorageButton);
            Button LookupPassStorageButton      = FindViewById<Button>(Resource.Id.LookupPassStorageButton);
            Button RemovePassStorageButton      = FindViewById<Button>(Resource.Id.RemovePassStorageButton);
            Button SwipePassStorageButton       = FindViewById<Button>(Resource.Id.SwipePassStorageButton);
            ImageView iv                        = FindViewById<ImageView>(Resource.Id.imageView2);
            iv.SetImageResource(Resource.Drawable.passkeeperV2);
            TextView VersionSet                 = FindViewById<TextView>(Resource.Id.VersionSet);
            VersionSet.Text                     = Helper.GetApplicationVersion();  //Sets application version
            

            //Add clicked events, need to do something right?
            CreatePassStorageButton.Click       += CreatePassStorageButtonClicked;
            LookupPassStorageButton.Click       += LookupPassStorageButtonClicked;
            RemovePassStorageButton.Click       += RemovePassStorageButtonClicked;
            SwipePassStorageButton.Click        += SwipePassStorageButtonClicked;
            iv.Click                            += GitHubButtonClickedEH;
        }

		public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void CreatePassStorageButtonClicked(object sender, EventArgs eventArgs)
        {
            String      FileDir             = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).Path;
            EditText    EncryptDecryptCode  = FindViewById<EditText>(Resource.Id.EncryptDecryptCode);
            EditText    WebsiteURL          = FindViewById<EditText>(Resource.Id.WebsiteURL);
            EditText    WebsitePass         = FindViewById<EditText>(Resource.Id.WebsitePass);
            TextView    SYSOutput           = FindViewById<TextView>(Resource.Id.SystemOutput);
            TextView    NumOfIterations     = FindViewById<TextView>(Resource.Id.NumOfIterations);
            String Ret = UIHandlingService.CreatePass(EncryptDecryptCode.Text, FileDir, WebsiteURL.Text, WebsitePass.Text, NumOfIterations.Text);
            WebsitePass.Text                = "";
            WebsiteURL.Text                 = "";
            SYSOutput.Text                  = Ret;
        }
        private void LookupPassStorageButtonClicked(object sender, EventArgs eventArgs)
        {
            String      FileDir             = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).Path;
            EditText    EncryptDecryptCode  = FindViewById<EditText>(Resource.Id.EncryptDecryptCode);
            EditText    WebsiteURL          = FindViewById<EditText>(Resource.Id.WebsiteURL);
            TextView    SYSOutput           = FindViewById<TextView>(Resource.Id.SystemOutput);
            TextView    NumOfIterations     = FindViewById<TextView>(Resource.Id.NumOfIterations);
            String      Ret                 = UIHandlingService.ReadPass(EncryptDecryptCode.Text, FileDir, WebsiteURL.Text, NumOfIterations.Text);
 
            WebsiteURL.Text = "";

            SYSOutput.Text = Ret;
        }
        private void RemovePassStorageButtonClicked(object sender, EventArgs eventArgs)
        {
            TextView    SYSOutput           = FindViewById<TextView>(Resource.Id.SystemOutput);
            String      FileDir             = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).Path;
            EditText    WebsiteURL          = FindViewById<EditText>(Resource.Id.WebsiteURL);
            String      Ret                 = UIHandlingService.RemoveStorageSpecific(FileDir, WebsiteURL.Text);

            SYSOutput.Text = Ret;
        }
        private void SwipePassStorageButtonClicked(object sender, EventArgs eventArgs)
        {
            String FileDir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).Path;

            String      Ret                 = UIHandlingService.WipeAllPasses(FileDir);
            TextView    SYSOutput           = FindViewById<TextView>(Resource.Id.SystemOutput);

            SYSOutput.Text                  = Ret;
        }
        private void GitHubButtonClickedEH(object sender, EventArgs eventArgs)
        {
            var uri = Android.Net.Uri.Parse("https://github.com/JohnJohnssonnl/PassKeepAndroidV1");
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }
    }
}

