using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SDL2;

namespace cats {
	public class Animation {
		private List<Frame> frames = new List<Frame> ();
		private IntPtr image = IntPtr.Zero;
		private int tileWidth;
		private int tileHeight;
		private SDL.SDL_Rect src = new SDL.SDL_Rect ();
		private SDL.SDL_Rect dest = new SDL.SDL_Rect ();

		public Animation (JObject animation, string spriteDefinitionFile) {
			Looping = (bool)animation ["looping"];
			AddFrames ((JArray)animation ["frames"]);
			AddImageDef ((JObject)animation ["image"], Util.GetBasePath (spriteDefinitionFile));
			src.w = dest.w = tileWidth;
			src.h = dest.h = tileHeight;
		}

		public void Draw (IntPtr renderer, int x, int y, int frame)
		{
			int index = frames [frame].index;
			if (index == -1) {
				return; // Hide sprite instance during frame duration
			}
			src.x = tileWidth * frames [frame].index;
			dest.x = x;
			dest.y = y;
			SDL.SDL_RenderCopy (renderer, image, ref src, ref dest);
		}

		public int GetFrameDuration (int frame)
		{
			return frames [0].duration;
		}

		public int Length {
			get {
				return frames.Count;
			}
		}

		public bool Looping {
			get;
			set;
		}

		private void AddFrames (JArray framesArray) {
			foreach (var frame in framesArray) {
				JArray frameParts = (JArray)frame;
				Frame f;
				f.index = (int)frameParts [0];
				f.duration = (int)frameParts [1];
				frames.Add (f);
			}
		}
		
		private void AddImageDef(JObject imageDef, string basePath) {
			tileWidth = (int)imageDef ["width"];
			tileHeight = (int)imageDef ["height"];
			image = ImageCache.Instance.GetImage (basePath + (string)imageDef ["path"]);
		}
	}

	public struct Frame {
		public int index;
		public int duration;
	}
}
