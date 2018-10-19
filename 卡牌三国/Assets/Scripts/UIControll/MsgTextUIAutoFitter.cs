using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgTextUIAutoFitter : MonoBehaviour {
    public RectTransform card;
    public RectTransform father;

    private float lastwidth = 0f;
    private float lastheight = 0f;
    private float newY = -1;
    //功能：对unity的UI进行适配
    //该文本框上方有一张卡牌(card)，该文本框需要填满剩下的区域，经过4小时的尝试，unity无法处理两个元素间的相对布局(gtndunity)


    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        //检测分辨率变动，或许可以做成事件形式
        if (lastwidth != Screen.width || lastheight != Screen.height || newY<0) {   //其他两个元素可能尚未初始化
            lastwidth = Screen.width; lastheight = Screen.height;
            print("Resolution :" + Screen.width + " X" + Screen.height);
            newY = father.rect.height - card.rect.height - 10;
            GetComponent<RectTransform>().sizeDelta = new Vector2(
                GetComponent<RectTransform>().sizeDelta.x, newY);
            //Debug.Log(newY);
        }

    }
}
