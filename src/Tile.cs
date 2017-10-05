using System;
using SDL2;

namespace cats {
	public class Tile {
		IntPtr image = IntPtr.Zero;
		SDL.SDL_Rect src = new SDL.SDL_Rect();
		SDL.SDL_Rect dest = new SDL.SDL_Rect();
		SDL.SDL_Rect offsetDest = new SDL.SDL_Rect();

		public Tile (int x, int y, int tileWidth, int tileHeight) {
			dest.x = x * tileWidth;
			dest.y = y * tileHeight;
			offsetDest.w = dest.w = tileWidth;
			offsetDest.h = dest.h = tileHeight;
		}

		public void Draw(IntPtr renderer, int xOffset, int yOffset) {
			offsetDest.x = dest.x + xOffset;
			offsetDest.y = dest.y + yOffset;
			SDL.SDL_RenderCopy (renderer, image, ref src, ref offsetDest);
		}

		public void Set(TileSource source) {
			image = source.Image;
			src = source.Src;
		}
	}
}
