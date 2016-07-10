using Skia.Forms.Demo;
using SkiaSharp;
using Xamarin.Forms;

namespace SkiaSharpDemo.Views
{
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();
      viewDemo();
    }
    public void viewDemo()
    {
      Title = "TEST";
      Content = new SkiaView(Demos.GetSample("DrawTateSample"));
    }
  }
}
