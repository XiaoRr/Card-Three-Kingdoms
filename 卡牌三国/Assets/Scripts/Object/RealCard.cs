using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RealCard:MonoBehaviour
{
    //Card类似于数据库，而RealCard则是实际的卡牌
    //场上可能出现任意张同种类的卡
    //这里存放所有技能的结算,以及debuff,以及卡牌所在的实体Object
    [HideInInspector]
    public Card info;
    [HideInInspector]
    public int hp;  //实际血量
    [HideInInspector]
    public int atk; //实际攻击力
    [HideInInspector]
    public int turn;    //实际回合数
    private GameManager gm; //管理类的引用
    public enum _Buff {在牌堆,在手牌,被冰冻,在墓地,被诅咒,被陷阱};
    public class Buff {
        _Buff buff;   //一些状态效果
        int var;    //状态可能具有参数
        RealCard caster;    //状态可能具有施加者

        public Buff(_Buff buff,int var = 1,RealCard caster = null)
        {
            this.buff = buff;
            this.var = var;
            this.caster = caster;
        }
    }

    public List<Buff> buffs;
    public void initRealCard(GameManager gm, Card info)
    {
        this.info = info;
        this.hp = info.hp;
        this.atk = info.atk;
        this.turn = info.turn;
        this.GetComponent<Image>().overrideSprite = info.image;
        this.buffs = new List<Buff>();
        this.buffs.Add(new Buff(_Buff.在牌堆));
        this.gm = gm;
    }

    //准备阶段调用
    public void onInit()
    {

    }
    //被指定时发动（包括被攻击）
    public void beCasted()
    {

    }
    //战斗发动
    public void onBattle()
    {
        //攻击力不为0的话 先攻击

    }
       
    //回合结束时发动
    public void onEndTurn()
    {

    }

    public void beClicked()
    {
        Debug.Log("Click" + info.name);
        gm.largeImg.overrideSprite = info.image;
    }
}
