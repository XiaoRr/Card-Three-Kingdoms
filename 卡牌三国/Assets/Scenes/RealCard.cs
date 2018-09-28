using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RealCard:MonoBehaviour
{
    //Card类似于数据库，而RealCard则是实际的卡牌
    //场上可能出现任意张同种类的卡
    //这里存放所有技能的结算,以及debuff,以及卡牌所在的实体Object
    Card info;
    
    public int hp;  //实际血量
    public int atk; //实际攻击力
    public int turn;    //实际回合数
    public enum Buff { 冰冻};
    public Buff buff;   //一些状态效果
    public RealCard(Card info)
    {
        this.info = info;
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


}
