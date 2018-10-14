using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

class Deck:CardGroup
{


    public new void Add(GameObject card)
    {
        base.Add(card);
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
        if (owner.childCount != 0)
        {
            this.GetComponent<CanvasGroup>().alpha = 1;
        
        }
        else
        {
            this.GetComponent<CanvasGroup>().alpha = 0;
        }
    }
}

