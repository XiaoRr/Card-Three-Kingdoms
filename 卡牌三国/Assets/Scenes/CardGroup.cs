using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//卡片容器类的父类，抽象
public abstract class CardGroup:MonoBehaviour
{
    //protected List<RealCard> cards;    //一个有序的抽象数组

    public Transform owner;    //游戏中对应的载体
    //把该类中的指定对象送向另一个CardGroup
    public void sendTo(RealCard tar,CardGroup otherGroup)
    {
        if (!cards.Contains(tar))
        {
            Debug.Log("未找到对应卡片" + tar.name);
            return;
        }
        otherGroup.Add(tar);
        cards.Remove(tar);
    }

    //另一个可能的重载
    public void sendTo(int tarId,CardGroup otherGroup)
    {
        if (cards.Count <= tarId)
        {
            Debug.Log("未找到对应卡片" + cards[tarId].name);
            return;
        }
        otherGroup.Add(cards[tarId]);
        cards.RemoveAt(tarId);
        //对两个容器进行刷新
        this.refresh();
        otherGroup.refresh();
    }

    void Start()
    {
        cards = new List<RealCard>();
    }
    public void Add(RealCard card)
    {
        cards.Add(card);
    }

    //刷新事件，容器内容变化时 需要重新显示容器 例如手牌由2张变为3张时的重绘
    public abstract void refresh();
}
