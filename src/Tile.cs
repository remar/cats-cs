using System;
using SDL2;

namespace cats {
	public class Tile {
		IntPtr image = IntPtr.Zero;
		SDL.SDL_Rect src = new SDL.SDL_Rect();
		SDL.SDL_Rect dest = new SDL.SDL_Rect();
		int xPosition, yPosition;

		public Tile (int x, int y, int tileWidth, int tileHeight) {
			xPosition = x * tileWidth;
			yPosition = y * tileHeight;
			dest.w = tileWidth;
			dest.h = tileHeight;
		}

		public void Draw(IntPtr renderer, int xOffset, int yOffset) {
			dest.x = xPosition + xOffset;
			dest.y = yPosition + yOffset;
			SDL.SDL_RenderCopy (renderer, image, ref src, ref dest);
		}

		public void Set(TileSource source) {
			image = source.Image;
			src = source.Src;
		}
	}
}
