using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using static Card;
using static TokenManager;

public class RealCard:MonoBehaviour
{
    //Card类似于数据库，而RealCard则是实际的卡牌
    //场上可能出现任意张同种类的卡
    //这里存放所有技能的结算,以及debuff,以及卡牌所在的实体Object
    [HideInInspector]
    public Card info;
    [HideInInspector]
    public int hp {
        get
        {
            return tm.health.hp;
        }
        set
        {
            tm.health.setNum(value);
        }
    }  //实际血量
    [HideInInspector]
    public int atk; //实际攻击力
    [HideInInspector]
    public int turn;    //实际回合数
    [HideInInspector]
    public SkillManager skill;
    private GameManager gm; //管理类的引用

    public TokenManager tm; //状态管理类

    /// <summary>
    /// monobeha类无法使用new，但是可以使用这种方式初始化
    /// </summary>
    /// <param name="gm">gameManager的引用</param>
    /// <param name="info">卡牌信息</param>
    public void initRealCard(GameManager gm, Card info)
    {
        this.info = info;
        this.hp = info.hp;
        this.atk = info.atk;
        this.turn = info.turn;
        this.GetComponent<Image>().overrideSprite = info.image;
        skill = new SkillManager(gm, this, info.skills);
        //this.buffs = new List<Buff>();
        //this.buffs.Add(new Buff(_Buff.在牌堆));
        this.gm = gm;
    }
    /// <summary>
    /// 被点击时调用，该函数注册于游戏中卡牌prefab的点击事件上
    /// </summary>
    public void beClicked()
    {
        Debug.Log("Click" + info.name);
        gm.largeImg.overrideSprite = info.image;
    }

    /// <summary>
    /// 从手牌置入战场时调用，主要是token的重设
    /// </summary>
    public void HandToField()
    {
        tm.RemoveToken(TokenType.等待);
        tm.SetToken(TokenType.攻击力, info.atk);
        tm.SetToken(TokenType.生命, info.hp);
    }
}
