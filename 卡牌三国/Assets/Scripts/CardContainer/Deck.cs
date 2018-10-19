using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Experimental.UIElements;

public class Deck:CardGroup
{

    public new void Add(GameObject card)
    {
        base.Add(card);
        card.transform.position = new Vector3(10000, 10000, 0);     //移动到视野外，因为牌组内的牌不可见
        //Debug.Log(card.transform.position);
        //card.transform.parent = this.owner.transform;

    }
    public override void refresh()
    {
        //if()
        //throw new NotImplementedException();
    }

    //卡组不需要显示卡片 只需要显示卡组里有么有卡
    void Update()
    {
        if (owner.childCount == 0)
        {
            //this.GetComponent<CanvasGroup>().alpha = 1;
            this.GetComponent<Image>().material = Resources.Load("Shader/Default Gray") as Material;
        }
        else
        {
            this.GetComponent<Image>().material = null;
        }
    }
}

