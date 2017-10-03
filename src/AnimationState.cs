using System;

namespace cats
{
	public class AnimationState {
		private Animation animation;
		private int currentFrame;
		private int currentFrameTime;
		private int currentFrameDuration;
		private int animationLength;
		private bool animationDone = false;

		public AnimationState(Animation animation) {
			SetAnimation(animation);
		}

		public void SetAnimation(Animation animation) {
			this.animation = animation;
			currentFrame = 0;
			currentFrameTime = 0;
			currentFrameDuration = animation.GetFrameDuration (currentFrame);
			animationLength = animation.Length;
		}

		public void Animate (int deltaMillis) {
			if(animationDone) {
				return;
			}

			currentFrameTime += deltaMillis;

			if (currentFrameTime >= currentFrameDuration) {
				NextFrame ();
			}
		}

		public void Draw (IntPtr renderer, int x, int y)
		{
			animation.Draw (renderer, x, y, currentFrame);
		}

		private void NextFrame ()
		{
			if (currentFrame + 1 == animationLength) {
				if (animation.Looping) {
					currentFrame = 0;
				} else {
					animationDone = true;
				}
			} else {
				currentFrame++;
			}
			currentFrameTime -= currentFrameDuration;
			currentFrameDuration = animation.GetFrameDuration (currentFrame);
		}
	}
}
