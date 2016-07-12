using System;
using SkiaSharp;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SkiaSharp
{
  public class Demos
  {
    public class SKChar
    {
      public SKTextEncoding Encoding { get; set; }
      public char Char { get; set; }
      public int GlyphId { get; set; }
    }

    static void drawTate(SKCanvas canvas, List<SKChar> chlst, float pos_x, float pos_y, int width, int height, SKPaint paint)
    {
      float stt_y = pos_y + paint.TextSize;
      float cur_y = stt_y;
      float cur_x = width - paint.TextSize;    //右端
      foreach (SKChar ch in chlst)
      {
        //Encodingを切り替え
        paint.TextEncoding = ch.Encoding;
        //通常の文字列
        if (ch.Encoding == SKTextEncoding.Utf8)
        {
          canvas.DrawText(ch.Char.ToString(), cur_x, cur_y, paint);
        }
        //GlyphID
        if (ch.Encoding == SKTextEncoding.GlyphId)
        {
          int[] glyph = new int[] { ch.GlyphId };
          IntPtr parray = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(int)) * glyph.Length);
          Marshal.Copy(glyph, 0, parray, glyph.Length);
          canvas.DrawText(parray, glyph.Length, cur_x, cur_y, paint);
        }
        cur_y += paint.TextSize;
        if (cur_y >= height)  //折り返し
        {
          cur_y = stt_y;
          cur_x -= paint.TextSize;
        }
      }
    }
    static List<SKChar> createCharList(string text)
    {
      List<SKChar> lst = new List<SKChar>();

      //括弧などの縦向きのUTF
      //http://www.unicode.org/charts/PDF/UFE30.pdf
      //CID一覧
      //http://www.adobe.com/content/dam/Adobe/en/devnet/font/pdfs/5078.Adobe-Japan1-6.pdf
      string tbl1 = "、。";
      string tbl2 = "︑︒"; //縦組み用に置き換え
      string tbl3 = "っゃゅょぁぃぅぇぉ";
      //int[] tbl4 = { 7923, 7924, 7925, 7926, 7918, 7919, 7920, 7921, 7922 }; //GlyphIDで縦組み用に置き換え
      int[] tbl4 = { 7509, 7510, 7511, 7512, 7504, 7505, 7506, 7507, 7508}; //TTFはGID??? CIDと違う
      foreach (char ch in text)
      {
        //句読点 UTF置き換え
        if (tbl1.IndexOf(ch) >= 0)
        {
          lst.Add(new SKChar
          {
            Encoding = SKTextEncoding.Utf8,
            Char = tbl2[tbl1.IndexOf(ch)]
          });
          continue;
        }
        //拗促音 GlyphIdに置き換え
        if (tbl3.IndexOf(ch) >= 0)
        {
          lst.Add(new SKChar
          {
            Encoding = SKTextEncoding.GlyphId,
            GlyphId = tbl4[tbl3.IndexOf(ch)]
          });
          continue;
        }
        //通常の文字列
        lst.Add(new SKChar
        {
          Encoding = SKTextEncoding.Utf8,
          Char = ch
        });
      }
      return lst;
    }

    public static void DrawTateSample(SKCanvas canvas, int width, int height)
    {
      Debug.WriteLine($"DrawTateSample width={width} height={height}");
      Debug.WriteLine($"DrawTateSample CustomFontPath={CustomFontPath}");

      string text = "　いづれの御時にか、っゃゅょぁぃぅぇぉ女御、更衣あまたさぶらひたまひけるなかに、いとやむごとなききはにはあらぬが、すぐれて時めきたまふありけり。はじめよりわれはと思ひあがりたまへる御かたがた、めざましきものにナシおとしめそねみたまふ。同じほど、それより下臈の更衣たちは、ましてやすからず。";
      float FONT_SIZE = 35;

      //縦向き用の文字に置き換え
      List<SKChar> chlst = createCharList(text);

      using (var paint = new SKPaint())
      using (var tf = SKTypeface.FromFile(CustomFontPath))
      {
        canvas.DrawColor(SKColors.White);

        paint.IsAntialias = true;
        paint.TextSize = FONT_SIZE;
        paint.Typeface = tf;
        //paint.IsVerticalText = true;    //1.49.4-betaで追加されたけどイメージは横向きのまま
        //一文字ずつ縦向きで書いていく
        drawTate(canvas,chlst,0,0,width,height,paint);
        //canvas.DrawText(text, FONT_SIZE, 0, paint);
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
      public Action<SKCanvas, int, int> Method { get; internal set; }
      public Platform Platform { get; internal set; }
      public Action TapMethod { get; internal set; }
    }

    public static string[] SamplesForPlatform(Platform platform)
    {
      return sampleList.Where(s => 0 != (s.Platform & platform)).Select(s => s.Title).ToArray();
    }

    public static Sample GetSample(string title)
    {
      return sampleList.Where(s => s.Title == title).First();
    }

    public static string CustomFontPath { get; set; }

    public static string WorkingDirectory { get; set; }

    public static Action<string> OpenFileDelegate { get; set; }

    static Sample[] sampleList = new Sample[] {
      new Sample {Title="DrawTateSample", Method = DrawTateSample, Platform = Platform.All},
    };
  }
}

