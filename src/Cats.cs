using System;
using SDL2;

namespace cats {
	public class Cats {
		private IntPtr window = IntPtr.Zero;
		private IntPtr renderer = IntPtr.Zero;

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
			SDL.SDL_SetRenderDrawColor (renderer, 0x00, 0x00, 0x00, 0xFF);
		}
	}
}