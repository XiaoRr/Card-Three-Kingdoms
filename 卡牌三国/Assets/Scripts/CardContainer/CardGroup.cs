using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 卡片容器类的父类，抽象类
/// </summary>
public abstract class CardGroup:MonoBehaviour
{
    //protected List<RealCard> cards;    //一个有序的抽象数组

    public Transform owner;    //游戏中对应的载体
    //把该类中的指定对象送向另一个CardGroup
    public void sendTo(Transform card,CardGroup otherGroup)
    {
        card.SetParent(otherGroup.owner,false);
    }

    void Start()
    {
    }
    //添加一张卡片
    public void Add(GameObject card)
    {
        card.transform.SetParent(owner, false);
    }

    /// <summary>
    /// 洗牌功能 待测试 洗牌均匀程度待论证
    /// </summary>
    public void shuffle()
    {
        int num = owner.childCount; //卡牌数量
        foreach(Transform ts in owner)
        {
            ts.SetSiblingIndex(Random.Range(0,num));
        }
    }
    //刷新事件，容器内容变化时 需要重新显示容器 例如手牌由2张变为3张时的重绘
    public abstract void refresh();

    public bool empty()
    {
        return owner.childCount == 0;
    }
}
