using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GifAssets.PowerGif.Examples.Scripts
{
	/// <summary>
	/// Encoding large GIF-files can take much more time than decoding. In this case, best practice is to display progress bar. This example shows how to use decode iterator.
	/// </summary>
	public class EncodeProgressExample : ExampleBase
	{
		public AnimatedImage AnimatedImage;
		public Image ProgressFill;

		#if UNITY_EDITOR

		public IEnumerator Start()
		{
			var path = UnityEditor.EditorUtility.SaveFilePanel("Save", SampleFolder, "Encoded", "gif");

			if (path == "") yield break;

			var bytes = File.ReadAllBytes(LargeSample);
			var gif = Gif.Decode(bytes);
			var iterator = gif.EncodeIterator();
			var iteratorSize = gif.GetEncodeIteratorSize();
			var parts = new List<byte>();
			var index = 0; // 0 = first frame, 2 = second frame, penultimate index = GIF header with global color table, last index = GIF trailer

			foreach (var part in iterator)
			{
				if (index == iteratorSize - 1) // GIF header should be placed to sequence start!
				{
					parts.InsertRange(0, part);
				}
				else
				{
					parts.AddRange(part);
				}

				parts.AddRange(part);
				ProgressFill.fillAmount = ++index / (float) iteratorSize;
				yield return null;
			}

			bytes = parts.ToArray();
			File.WriteAllBytes(path, bytes);
			Debug.LogFormat("Saved to: {0}", path);
			AnimatedImage.Play(gif);
		}

		#endif
	}
}