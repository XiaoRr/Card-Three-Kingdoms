using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GifAssets.PowerGif
{
	/// <summary>
	/// This script simply switches GIF-frames (textures) to get "animation" effect.
	/// </summary>
	[RequireComponent(typeof(Image))]
	public class AnimatedImage : MonoBehaviour
	{
		public Gif Gif;

		/// <summary>
		/// Will play GIF (if it's assigned) on app start if script is enabled.
		/// </summary>
		public void Start ()
		{
			if (Gif != null)
			{
				StartCoroutine(Animate(Gif, 0));
			}
		}

		/// <summary>
		/// Play GIF.
		/// </summary>
		public void Play(Gif gif)
		{
			Gif = gif;
			StopAllCoroutines();
			StartCoroutine(Animate(Gif, 0));
		}

		private IEnumerator Animate(Gif gif, int index)
		{
			var texture = gif.Frames[index].Texture;

			GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
			GetComponent<Image>().preserveAspect = true;

			if (gif.Frames.Count == 1) yield break;

			var delay = gif.Frames[index].Delay;

			if (delay < 0.02f) // Chrome browser behaviour
			{
				delay = 0.1f;
			}

			yield return new WaitForSeconds(delay);

			if (++index == gif.Frames.Count)
			{
				index = 0;
			}

			StartCoroutine(Animate(gif, index));
		}

        public IEnumerator ExPlay(Gif gif)
        {
            Gif = gif;
            StopAllCoroutines();
            yield return StartCoroutine(Animate(Gif, 0));
        }
    }
}