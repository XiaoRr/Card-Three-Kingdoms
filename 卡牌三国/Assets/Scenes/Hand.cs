using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Hand : CardGroup
{
    public override void refresh()
    {
        //throw new NotImplementedException();
    }

    public new void Add(RealCard card)
    {
        base.Add(card);
        card.transform.parent = this.owner.transform;   //设置卡牌父节点为手牌
    }
}

