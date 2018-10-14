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
    public void sendTo(GameObject card,Transform otherGroup)
    {
        card.transform.SetParent(otherGroup);
    }

    void Start()
    {
    }
    //添加一张卡片
    public void Add(GameObject card)
    {
        card.transform.SetParent(owner);
    }

    //刷新事件，容器内容变化时 需要重新显示容器 例如手牌由2张变为3张时的重绘
    public abstract void refresh();
}
