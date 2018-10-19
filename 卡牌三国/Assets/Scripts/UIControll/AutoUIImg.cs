using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制图标的大小 图标大小应该为卡牌宽度的三分之一
//缩放而不是直接改变大小，是因为该图标内还有子物体
public class AutoUIImg : MonoBehaviour {

    public RectTransform Card;  //寻找一个卡牌作为参照物
	// Use this for initialization
	void Start () {
        var size = Card.sizeDelta.x / 3;
        this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(size,size);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
