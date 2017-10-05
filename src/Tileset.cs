using System;
using System.IO;
using SDL2;
using Newtonsoft.Json.Linq;

namespace cats {
	public class Tileset {
		private IntPtr image = IntPtr.Zero;
		private int width;
		private int height;
		private TileSource[,] sources;

		public Tileset(string file) {
			JObject obj = JObject.Parse (File.ReadAllText (file));
			JObject imageDef = (JObject)obj ["image"];
			width = (int)imageDef ["width"];
			height = (int)imageDef ["height"];
			string basePath = Util.GetBasePath (file);
			image = ImageCache.Instance.GetImage (basePath + (string)imageDef ["path"]);
			SetupSources ();
		}

		public TileSource GetTileSource (int tileX, int tileY) {
			if (sources [tileX, tileY] == null) {
				sources [tileX, tileY] = new TileSource (image, tileX * width, tileY * height, width, height);
			}
			return sources[tileX, tileY];
		}

		private void SetupSources() {
			uint format;
			int access;
			int textureWidth, textureHeight;
			SDL.SDL_QueryTexture (image, out format, out access, out textureWidth, out textureHeight);
			sources = new TileSource[(int)Math.Ceiling(((double)textureWidth) / width),
			                         (int)Math.Ceiling(((double)textureHeight) / height)];
		}
	}
}
