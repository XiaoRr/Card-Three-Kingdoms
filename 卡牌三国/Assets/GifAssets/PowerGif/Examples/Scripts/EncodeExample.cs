using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.GifAssets.PowerGif.Examples.Scripts
{
	/// <summary>
	/// Encoding GIF example.
	/// </summary>
	public class EncodeExample : ExampleBase
	{
		public List<Texture2D> Frames;
		public AnimatedImage AnimatedImage;

		#if UNITY_EDITOR

		public void Start()
		{
			var frames = Frames.Select(f => new GifFrame(f, 0.1f)).ToList();
			var gif = new Gif(frames);
			var path = UnityEditor.EditorUtility.SaveFilePanel("Save", SampleFolder, "Encoded", "gif");

			if (path == "") return;

			var bytes = gif.Encode();

			File.WriteAllBytes(path, bytes);
			Debug.LogFormat("Saved to: {0}", path);
			AnimatedImage.Play(gif);
		}

		#endif
	}
}