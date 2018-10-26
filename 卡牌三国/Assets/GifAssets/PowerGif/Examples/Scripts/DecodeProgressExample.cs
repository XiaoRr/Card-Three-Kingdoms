using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

namespace Assets.GifAssets.PowerGif.Examples.Scripts
{
	/// <summary>
	/// Decoding large GIF-files can take time. In this case, best practice is to display progress bar. This example shows how to use decode iterator.
	/// </summary>
	public class DecodeProgressExample : ExampleBase
	{
		public AnimatedImage AnimatedImage;
		public Image ProgressFill;

		public IEnumerator Start()
		{
			var bytes = File.ReadAllBytes(LargeSample);
			var iterator = Gif.DecodeIterator(bytes);
			var iteratorSize = Gif.GetDecodeIteratorSize(bytes);
			var frames = new List<GifFrame>();
			var index = 0f;

			foreach (var frame in iterator)
			{
				frames.Add(frame);
				ProgressFill.fillAmount = ++index / iteratorSize;
				yield return null;
			}

			var gif = new Gif(frames);

			AnimatedImage.Play(gif);
		}
	}
}