using System;
using Xamarin.Forms;
using SkiaSharp;
using System.Diagnostics;

namespace Skia.Forms.Demo
{
	public class SkiaView : View, ISkiaViewController
	{
		Demos.Sample sample;

		public SkiaView (Demos.Sample sample)
		{
			this.sample = sample;
		}

		void ISkiaViewController.SendDraw (SKCanvas canvas)
		{
      Debug.WriteLine("SkiaView SendDraw");
			Draw (canvas);
		}

		void ISkiaViewController.SendTap ()
		{
			sample?.TapMethod?.Invoke ();
		}

		protected virtual void Draw (SKCanvas canvas)
		{
      Debug.WriteLine("SkiaView Draw");
      sample?.Method?.Invoke (canvas, (int)Width, (int)Height);
		}
	}
}

