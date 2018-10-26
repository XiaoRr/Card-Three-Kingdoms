using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GifAssets.PowerGif.Examples.Scripts
{
	/// <summary>
	/// In some cases you may need to get GIF previews (first frame) instead of decoding all frames. For example, you are developing image gallery and it should work fast.
	/// Just use decode iterator and stop it on the first iteration. Then you'll get the first frame. You even don't need to use a coroutine in this case.
	/// </summary>
	public class PreviewExample : ExampleBase
	{
		public Image Image;

		public void Start()
		{
			var bytes = File.ReadAllBytes(LargeSample);
			var iterator = Gif.DecodeIterator(bytes);
			Texture2D texture = null;

			foreach (var frame in iterator)
			{
				texture = frame.Texture;
				break;
			}

			if (texture == null) return;

			Image.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
		}
	}
}