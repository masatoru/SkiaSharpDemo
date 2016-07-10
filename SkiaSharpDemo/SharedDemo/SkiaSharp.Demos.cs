using System;
using SkiaSharp;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace SkiaSharp
{
	public class Demos
	{
    static void drawTate(SKCanvas canvas, string text, float pos_x, float pos_y,int width, int height, SKPaint paint)
    {
      float stt_y = pos_y + paint.TextSize;
      float cur_y = stt_y;
      float cur_x = width- paint.TextSize;    //右端
      foreach (char ch in text)
      {
        canvas.DrawText(ch.ToString(), cur_x, cur_y, paint);
        cur_y += paint.TextSize;
        if (cur_y >= height)  //折り返し
        {
          cur_y = stt_y;
          cur_x -= paint.TextSize;
        }
      }
    }
    public static void DrawTateSample(SKCanvas canvas, int width, int height)
    {
      Debug.WriteLine($"DrawTateSample width={width} height={height}");
      Debug.WriteLine($"DrawTateSample CustomFontPath={CustomFontPath}");

      string text = "　いづれの御時にか、女御、更衣あまたさぶらひたまひけるなかに、いとやむごとなききはにはあらぬが、すぐれて時めきたまふありけり。はじめよりわれはと思ひあがりたまへる御かたがた、めざましきものにナシおとしめそねみたまふ。同じほど、それより下臈の更衣たちは、ましてやすからず。";
      float FONT_SIZE = 35;

      using (var paint = new SKPaint())
      using (var tf = SKTypeface.FromFile(CustomFontPath/*,0*/))
      {
        canvas.DrawColor(SKColors.White);

        paint.IsAntialias = true;
        paint.TextSize = FONT_SIZE;
        paint.Typeface = tf;
        //一文字ずつ縦向きで書いていく
        drawTate(canvas,text,0,0,width,height,paint);
      }
    }

    [Flags]
		public enum Platform
		{
			iOS = 1,
			Android = 2,
			OSX = 4,
			WindowsDesktop = 8,
			UWP = 16,
			tvOS = 32,
			All = 0xFFFF,
		}

		public class Sample 
		{
			public string Title { get; internal set; }
			public Action <SKCanvas, int, int> Method { get; internal set; }
			public Platform Platform { get; internal set; }
			public Action TapMethod { get; internal set; }
		}

		public static string [] SamplesForPlatform (Platform platform)
		{
			return sampleList.Where (s => 0 != (s.Platform & platform)).Select (s => s.Title).ToArray ();
		}

		public static Sample GetSample (string title)
		{
			return sampleList.Where (s => s.Title == title).First ();
		}

		public static string CustomFontPath { get; set; }

		public static string WorkingDirectory { get; set; }

		public static Action<string> OpenFileDelegate { get; set; }

		static Sample [] sampleList = new Sample[] {
      new Sample {Title="DrawTateSample", Method = DrawTateSample, Platform = Platform.All},
    };
	}
}

