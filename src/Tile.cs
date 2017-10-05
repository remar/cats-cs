using System;
using SDL2;

namespace cats {
	public class Tile {
		IntPtr image = IntPtr.Zero;
		SDL.SDL_Rect src = new SDL.SDL_Rect();
		SDL.SDL_Rect dest = new SDL.SDL_Rect();

		public Tile (int x, int y, int tileWidth, int tileHeight) {
			dest.x = x * tileWidth;
			dest.y = y * tileHeight;
			dest.w = tileWidth;
			dest.h = tileHeight;
		}

		public void Draw(IntPtr renderer) {
			SDL.SDL_RenderCopy (renderer, image, ref src, ref dest);
		}

		public void Set(TileSource source) {
			image = source.Image;
			src = source.Src;
		}
	}
}
