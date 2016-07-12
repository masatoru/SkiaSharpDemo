using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.IO;
using System.Diagnostics;

namespace SkiaSharpDemo.iOS
{
  // The UIApplicationDelegate for the application. This class is responsible for launching the 
  // User Interface of the application, as well as listening (and optionally responding) to 
  // application events from iOS.
  [Register("AppDelegate")]
  public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
  {
    //
    // This method is invoked when the application has loaded and is ready to run. In this 
    // method you should instantiate the window, load the UI into it and then make the window
    // visible.
    //
    // You have 17 seconds to return from this method, or iOS will terminate your application.
    //
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
      // set up resource paths
      //string fontName = "content-font.ttf";
      string fontName = "ipaexm.ttf";
      var custonFontPath = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fontName), Path.GetExtension(fontName));
      Debug.WriteLine($"AppDelegate FinishedLaunching CustomFontPath={custonFontPath}");
      SkiaSharp.Demos.OpenFontStream = () => File.OpenRead(custonFontPath);

      var dir = Path.Combine(Path.GetTempPath(), "SkiaSharp.Demos", Path.GetRandomFileName());
      if (!Directory.Exists(dir))
      {
        Directory.CreateDirectory(dir);
      }
      SkiaSharp.Demos.WorkingDirectory = dir;
      SkiaSharp.Demos.OpenFileDelegate = path =>
      {
        var vc = Xamarin.Forms.Platform.iOS.Platform.GetRenderer(Xamarin.Forms.Application.Current.MainPage) as UIViewController;
        var resourceToOpen = NSUrl.FromFilename(Path.Combine(dir, path));
        var controller = UIDocumentInteractionController.FromUrl(resourceToOpen);
        if (!controller.PresentOpenInMenu(vc.View.Bounds, vc.View, true))
        {
          new UIAlertView("SkiaSharp", "Unable to open file.", null, "OK").Show();
        }
      };

      global::Xamarin.Forms.Forms.Init();
      LoadApplication(new App());

      return base.FinishedLaunching(app, options);
    }
  }
}
