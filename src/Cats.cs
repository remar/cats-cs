using System;
using System.Collections.Generic;
using SDL2;

namespace cats {
	public static class Cats {
        private static IntPtr window = IntPtr.Zero;
        private static IntPtr renderer = IntPtr.Zero;
        private static Dictionary<string,SpriteDefinition> spriteDefinitions = new Dictionary<string,SpriteDefinition>();
        private static Dictionary<int,SpriteInstance> spriteInstances = new Dictionary<int,SpriteInstance>();
        private static TileLayer tileLayer = null;
        private static Dictionary<string,Tileset> tilesets = new Dictionary<string,Tileset> ();
        private static int nextSpriteId = 0;

		public static void Init(int width, int height) {
			window = SDL.SDL_CreateWindow ("Cats#",
			                               SDL.SDL_WINDOWPOS_UNDEFINED,
			                               SDL.SDL_WINDOWPOS_UNDEFINED,
			                               width,
			                               height,
			                               SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
			renderer = SDL.SDL_CreateRenderer(window, -1,
			                                  SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
			ImageCache.Instance.ConnectRenderer (renderer);
			SDL.SDL_RenderSetLogicalSize (renderer, width, height);
			SDL.SDL_SetRenderDrawColor (renderer, 0x00, 0x00, 0x00, 0xFF);
		}

		public static void Redraw(float delta) {
			int deltaMillis = (int)(delta * 1000);
			SDL.SDL_RenderClear (renderer);
			if (tileLayer != null) {
				tileLayer.Draw (renderer);
			}
			foreach (var spriteInstance in spriteInstances) {
				spriteInstance.Value.Draw (renderer, deltaMillis);
			}
			SDL.SDL_RenderPresent (renderer);
		}

		public static void SetBackgroundColor(byte red, byte green, byte blue) {
			SDL.SDL_SetRenderDrawColor (renderer, red, green, blue, 0xFF);
		}

		public static void LoadSprite(string file) {
			string name = Util.FilenameToName (file);
			spriteDefinitions.Add (name, new SpriteDefinition (file));
		}

		public static int CreateSpriteInstance(string spritename) {
			int spriteId = nextSpriteId;
			nextSpriteId++;
			spriteInstances.Add (spriteId, new SpriteInstance (spriteDefinitions [spritename]));
			return spriteId;
		}

		public static void RemoveSpriteInstance(int spriteId) {
			spriteInstances.Remove (spriteId);
		}

		public static void ShowSprite(int spriteId, bool visible) {
			spriteInstances [spriteId].Visible = visible;
		}

        public static void SetSpritePosition(int spriteId, int x, int y) {
			spriteInstances [spriteId].SetPosition (x, y);
		}

        public static void SetAnimation(int spriteId, string animation) {
			spriteInstances [spriteId].SetAnimation (animation);
		}

        public static void SetupTileLayer(int width, int height, int tileWidth, int tileHeight) {
			tileLayer = new TileLayer (width, height, tileWidth, tileHeight);
		}

        public static void LoadTileset(string file) {
			string name = Util.FilenameToName (file);
			tilesets.Add (name, new Tileset (file));
		}

        public static void SetTile(int x, int y, string tileset, int tileX, int tileY) {
			tileLayer.SetTile (x, y, tilesets[tileset].GetTileSource(tileX, tileY));
		}

        public static void SetScroll(int x, int y) {
			tileLayer.SetScroll (x, y);
		}
	}
}
