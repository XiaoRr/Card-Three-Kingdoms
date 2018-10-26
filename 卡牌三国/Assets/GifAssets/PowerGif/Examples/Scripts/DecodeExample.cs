using System.IO;

namespace Assets.GifAssets.PowerGif.Examples.Scripts
{
	/// <summary>
	/// Decoding GIF example.
	/// </summary>
	public class DecodeExample : ExampleBase
	{
		public AnimatedImage AnimatedImage;

		public void Start()
		{
			var bytes = File.ReadAllBytes(LargeSample);
			var gif = Gif.Decode(bytes);

			AnimatedImage.Play(gif);
		}
	}
}