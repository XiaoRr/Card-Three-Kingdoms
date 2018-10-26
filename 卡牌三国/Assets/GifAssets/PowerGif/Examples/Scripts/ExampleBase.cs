using UnityEngine;

namespace Assets.GifAssets.PowerGif.Examples.Scripts
{
	public abstract class ExampleBase : MonoBehaviour
	{
		protected const string SmallSample = "Assets/GifAssets/PowerGif/Examples/Samples/Small.gif";
		protected const string LargeSample = "Assets/GifAssets/PowerGif/Examples/Samples/Large.gif";
		protected const string SampleFolder = "Assets/GifAssets/PowerGif/Examples/Samples";

		public void Review()
		{
			Application.OpenURL("https://www.assetstore.unity3d.com/#!/content/120039");
		}
	}
}