using System;
using System.Collections.Generic;
using SDL2;

namespace cats {
	public class Cats {
		private IntPtr window = IntPtr.Zero;
		private IntPtr renderer = IntPtr.Zero;
		private Dictionary<string,SpriteDefinition> spriteDefinitions = new Dictionary<string,SpriteDefinition>();
		private Dictionary<int,SpriteInstance> spriteInstances = new Dictionary<int,SpriteInstance>();
		private int nextSpriteId = 0;

		public Cats () {
		}

		public void Init(int width, int height) {
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

		public void Redraw(float delta) {
			int deltaMillis = (int)(delta * 1000);
			SDL.SDL_RenderClear (renderer);
			foreach (var spriteInstance in spriteInstances) {
				spriteInstance.Value.Draw (renderer, deltaMillis);
			}
			SDL.SDL_RenderPresent (renderer);
		}

		public void SetBackgroundColor(byte red, byte green, byte blue) {
			SDL.SDL_SetRenderDrawColor (renderer, red, green, blue, 0xFF);
		}

		public void LoadSprite(string file) {
			string name = Util.FilenameToName (file);
			spriteDefinitions.Add (name, new SpriteDefinition (file));
		}

		public int CreateSpriteInstance(string spritename) {
			int spriteId = nextSpriteId;
			nextSpriteId++;
			spriteInstances.Add (spriteId, new SpriteInstance (spriteDefinitions [spritename]));
			return spriteId;
		}

		public void ShowSprite(int spriteId, bool visible) {
			spriteInstances [spriteId].Visible = visible;
		}

		public void SetSpritePosition(int spriteId, int x, int y) {
			spriteInstances [spriteId].SetPosition (x, y);
		}

		public void SetAnimation(int spriteId, string animation) {
			spriteInstances [spriteId].SetAnimation (animation);
		}
	}
}
