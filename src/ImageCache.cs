using System;
using System.Collections.Generic;
using SDL2;

namespace cats {
	public class ImageCache	{
		private static readonly ImageCache instance = new ImageCache();
		private Dictionary<string,IntPtr> cache = new Dictionary<string,IntPtr>();
		private IntPtr renderer = IntPtr.Zero;

		private ImageCache () {}

		public static ImageCache Instance {
			get {
				return instance;
			}
		}

		public void ConnectRenderer(IntPtr renderer) {
			this.renderer = renderer;
		}

		public IntPtr GetImage(string path) {
			if (!cache.ContainsKey (path)) {
				var surface = SDL_image.IMG_Load (path);
				cache [path] = SDL.SDL_CreateTextureFromSurface (renderer, surface);
				SDL.SDL_FreeSurface (surface);
			}
			return cache [path];
		}
	}
}
