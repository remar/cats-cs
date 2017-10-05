using System;
using System.IO;
using SDL2;
using Newtonsoft.Json.Linq;

namespace cats {
	public class Tileset {
		private IntPtr image = IntPtr.Zero;
		private int width;
		private int height;

		public Tileset(string file) {
			JObject obj = JObject.Parse (File.ReadAllText (file));
			JObject imageDef = (JObject)obj ["image"];
			width = (int)imageDef ["width"];
			height = (int)imageDef ["height"];
			string basePath = Util.GetBasePath (file);
			image = ImageCache.Instance.GetImage (basePath + (string)imageDef ["path"]);
		}

		public TileSource GetTileSource (int tileX, int tileY) {
			return new TileSource (image, tileX * width, tileY * height, width, height);
		}
	}
}
