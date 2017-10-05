using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace cats {
	public class SpriteDefinition {
		private Dictionary<string,Animation> animations = new Dictionary<string,Animation>();
		private string defaultAnimation = null;

		public SpriteDefinition (string file) {
			JObject obj = JObject.Parse (File.ReadAllText (file));
			JObject animationsObject = obj["animations"] as JObject;
			foreach(var animation in animationsObject) {
				animations.Add(animation.Key, new Animation(animation.Value as JObject, file));
				if(defaultAnimation == null) {
					defaultAnimation = animation.Key;
				}
			}
		}

		public Animation GetDefaultAnimation() {
			return GetAnimation (defaultAnimation);
		}

		public Animation GetAnimation (string animation) {
			return animations [animation];
		}
	}
}
