using System;

using Android.App;
using Android.Content.PM;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;

namespace SkiaSharpDemo.Droid
{
  [Activity(Label = "SkiaSharpDemo.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
  {
    protected override void OnCreate(Bundle bundle)
    {
      //TabLayoutResource = Resource.Layout.Tabbar;
      //ToolbarResource = Resource.Layout.Toolbar;

      // set up resource paths
      //string fontName = "content-font.ttf";
      string fontName = "ipaexm.ttf";   //IPAフォント
      string fontPath = Path.Combine(CacheDir.AbsolutePath, fontName);
      using (var asset = Assets.Open(fontName))
      using (var dest = File.Open(fontPath, FileMode.Create))
      {
        asset.CopyTo(dest);
      }
      SkiaSharp.Demos.OpenFontStream = () => File.OpenRead(fontPath);

      var dir = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "SkiaSharp.Demos");
      if (!Directory.Exists(dir))
      {
        Directory.CreateDirectory(dir);
      }
      SkiaSharp.Demos.WorkingDirectory = dir;
      SkiaSharp.Demos.OpenFileDelegate = path =>
      {
        var file = new Java.IO.File(Path.Combine(dir, path));
        var uri = Android.Net.Uri.FromFile(file);
        StartActivity(new Intent(Intent.ActionView, uri));
      };


      base.OnCreate(bundle);

      global::Xamarin.Forms.Forms.Init(this, bundle);
      LoadApplication(new App());
    }
  }
}

