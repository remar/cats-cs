using System;

namespace cats {
	public class SpriteInstance {
		private SpriteDefinition spriteDefinition;
		private AnimationState animationState;
		private int x = 0;
		private int y = 0;

		public SpriteInstance (SpriteDefinition spriteDefinition) {
			this.spriteDefinition = spriteDefinition;
			animationState = new AnimationState (spriteDefinition.GetDefaultAnimation ());
		}

		public void Draw(IntPtr renderer, int deltaMillis) {
			animationState.Animate (deltaMillis);
			animationState.Draw (renderer, x, y);
		}

		public void SetPosition (int x, int y) {
			this.x = x;
			this.y = y;
		}
	}
}
