using System;
using System.Collections.Generic;
using SDL2;

namespace cats {
	public class TileLayer {
		private Tile[,] tiles;
		private int width, height;
		private int tileWidth, tileHeight;
		private int xOffset, yOffset;

		public TileLayer (int width, int height, int tileWidth, int tileHeight)
		{
			tiles = new Tile[width, height];
			this.width = width;
			this.height = height;
			this.tileWidth = tileWidth;
			this.tileHeight = tileHeight;
			InitTiles ();
		}

		public void Draw(IntPtr renderer) {
			foreach (Tile t in tiles) {
				t.Draw (renderer, xOffset, yOffset);
			}
		}

		public void SetTile (int x, int y, TileSource source) {
			tiles [x, y].Set (source);
		}

		public void SetScroll (int x, int y) {
			xOffset = x;
			yOffset = y;
		}

		private void InitTiles () {
			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					tiles [x, y] = new Tile (x, y, tileWidth, tileHeight);
				}
			}
		}
	}
}
