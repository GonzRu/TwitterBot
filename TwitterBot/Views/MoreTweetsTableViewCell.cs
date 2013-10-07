using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace TwitterBot
{
	public class MoreTweetsTableViewCell : UITableViewCell
	{
		UILabel u = new UILabel ();

		float _leftPadding = 20f;
		float _topPadding = 12f;
		private bool _selected = false;

		public MoreTweetsTableViewCell (string id) : base (UITableViewCellStyle.Default, id)
		{
			SelectionStyle = UITableViewCellSelectionStyle.None;

			u.Text = "Показать ещё";
			u.TextColor = UIColor.Black;
			u.TextAlignment = UITextAlignment.Center;
			u.BackgroundColor = UIColor.Clear;
		}

		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);

			using (var context = UIGraphics.GetCurrentContext ())
			{
				var r = new RectangleF (_leftPadding,
				                        _topPadding,
				                        rect.Width - 2 * _leftPadding,
				                        rect.Height - 2 * _topPadding);
				UIBezierPath b = UIBezierPath.FromRoundedRect (r, 10);

				UIColor.Black.SetStroke ();
				context.SetLineWidth (3);
				context.AddPath (b.CGPath);
				context.DrawPath (CGPathDrawingMode.Stroke);

				if (_selected) {
					DrawLinearGradient (context, r, UIColor.Gray.CGColor, UIColor.Black.CGColor);
					u.TextColor = UIColor.White;
				} else {
					DrawLinearGradient (context, r, UIColor.White.CGColor, UIColor.Gray.CGColor);
					u.TextColor = UIColor.Black;
				}

				AddSubview (u);
			}
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			float textWidth = 200f;
			float textHeight = 30f;
			u.Frame = new System.Drawing.RectangleF (Bounds.Width / 2 - textWidth / 2,
			                                         Bounds.Height / 2 - textHeight / 2,
			                                         textWidth,
			                                         textHeight);
		}

		public override void SetSelected (bool selected, bool animated)
		{
			base.SetSelected (selected, animated);

			_selected = selected;
			SetNeedsDisplay ();
		}

		private void DrawLinearGradient(CGContext context, RectangleF rect, CGColor startColor, CGColor  endColor)
		{
			CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB ();
			float [] locations = { 0.0f, 1.0f };

			CGColor [] colors = new CGColor[] { startColor, endColor };

			CGGradient gradient = new CGGradient (colorSpace, colors, locations);

			PointF startPoint = new PointF (rect.GetMidX (), rect.GetMinY ());
			PointF endPoint = new PointF (rect.GetMidX (), rect.GetMaxY ());

			context.SaveState ();
			context.AddPath (UIBezierPath.FromRoundedRect (rect, 10).CGPath);
			context.Clip ();
			context.DrawLinearGradient (gradient, startPoint, endPoint, 0);
			context.RestoreState ();
		}
	}
}

