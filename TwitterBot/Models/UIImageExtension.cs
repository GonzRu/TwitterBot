using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace TwitterBot
{
	static public class UIImageExtension
	{
		public static UIImage SetMask (this UIImage image, string pathToMask)
		{
			if (image == null)
				return null;

			var i = UIImage.FromBundle (pathToMask);

			if (i == null)
				return null;

			CGImage maskImage = i.CGImage;

			CGImage mask = CGImage.CreateMask (maskImage.Width,
			                                   maskImage.Height,
			                                   maskImage.BitsPerComponent,
			                                   maskImage.BitsPerPixel,
			                                   maskImage.BytesPerRow,
			                                   maskImage.DataProvider, null, false);

			var res = image.CGImage.WithMask (mask);

			return new UIImage (res);
		}

		public static UIImage Scale (this UIImage source, SizeF newSize)
		{
			UIGraphics.BeginImageContext (newSize);
			var context = UIGraphics.GetCurrentContext ();
			context.TranslateCTM (0, newSize.Height);
			context.ScaleCTM (1f, -1f);

			context.DrawImage (new RectangleF (0, 0, newSize.Width, newSize.Height), source.CGImage);

			var scaledImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();

			return scaledImage;         
		}
	}
}

