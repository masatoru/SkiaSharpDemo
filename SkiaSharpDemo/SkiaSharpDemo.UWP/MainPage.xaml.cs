using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SkiaSharpDemo.UWP
{
  public sealed partial class MainPage
  {
    public MainPage()
    {
      // set up resource paths
      //string fontName = "content-font.ttf";
      string fontName = "ipaexm.ttf";
      SkiaSharp.Demos.CustomFontPath = Path.Combine(Package.Current.InstalledLocation.Path, "Assets", fontName);
      Debug.WriteLine($"MainPage CustomFontPath={SkiaSharp.Demos.CustomFontPath}");

      SkiaSharp.Demos.WorkingDirectory = ApplicationData.Current.LocalFolder.Path;
      SkiaSharp.Demos.OpenFileDelegate =
        async name =>
        {
          var file = await ApplicationData.Current.LocalFolder.GetFileAsync(name);
          await Windows.System.Launcher.LaunchFileAsync(file);
        };

      this.InitializeComponent();

      LoadApplication(new SkiaSharpDemo.App());
    }
  }
}
