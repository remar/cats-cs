using System;
using SDL2;

namespace cats {
	public class TileSource {
		private IntPtr image;
		private SDL.SDL_Rect src;

		public TileSource (IntPtr image, int x, int y, int w, int h) {
			this.image = image;
			src = new SDL.SDL_Rect ();
			src.x = x;
			src.y = y;
			src.w = w;
			src.h = h;
		}

		public IntPtr Image {
			get {
				return image;
			}
		}

		public SDL.SDL_Rect Src {
			get {
				return src;
			}
		}
	}
}
