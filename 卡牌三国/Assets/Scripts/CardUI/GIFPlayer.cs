using Assets.GifAssets.PowerGif;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GIFPlayer : MonoBehaviour {

    public AnimatedImage animatedImage;

    public IEnumerator playGIFAnimation()
    {
        animatedImage.gameObject.SetActive(true);
        //var bytes = File.ReadAllBytes("Assets/Resources/Skills/抓.gif");
        var bytes = File.ReadAllBytes("Assets/GifAssets/PowerGif/Examples/Samples/Large.gif");
        var gif = Gif.Decode(bytes);

        yield return animatedImage.ExPlay(gif);
        //gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update () {
		
	}


}
