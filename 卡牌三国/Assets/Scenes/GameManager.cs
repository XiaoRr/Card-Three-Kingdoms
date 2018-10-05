﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour {
    [HideInInspector]
    public List<RealCard> enemyDeck, ourDeck;   //敌我卡组
    [HideInInspector]
    public List<RealCard> enemyHand, ourHand;   //敌我手牌
    [HideInInspector]
    public List<RealCard> enemyGrave, ourGrave;   //敌我墓地
    [HideInInspector]
    public int turn = 1;   //当前回合数
    [HideInInspector]
    public int enemyHp, ourHp;  //敌我血量
    [HideInInspector]
    public Dictionary<string, Card> cards;

    public Transform enemyField, ourField;     //战场（水平布局）
    public Transform enemyWaiting, ourWaiting;     //手牌（水平布局）
    
    // Use this for initialization
    void Start () {
        cards = Global.Load();
        tmpInit();
    }

    // Update is called once per frame
    void Update () {
        //抽卡
        DrawCard();
        //结算战斗
        Battle();
        //结算死亡

            //结算胜利
            //结算buff

        if (turn > 80)
        {
            Debug.Log("draw");
        }
	}

    //测试用初始化函数
    void tmpInit()
    {
        //为双方卡组装填7张卡牌
        //A方 究极藏獒队

        RealCard rc = new RealCard(cards["藏獒"]);
        for (int i = 0; i < 7; i++)
        {
            GameObject card = (GameObject)Instantiate(Resources.Load("Prefabs/战场卡牌"));
            GameObject mUICanvas = GameObject.Find("我方手牌");

            //enemyDeck.Add(rc);

        }
        //B方 沙雕杂鱼队
        rc = new RealCard(cards["游侠"]);
        for (int i = 0; i < 7; i++)
        {
            ourDeck.Add(rc);

        }
    }

    //洗牌
    void Shuffle()
    {

    }
    //抽卡 将卡组的卡移到手牌
    void DrawCard()
    {
        //我方回合
        if (turn % 2 == 1)
        {
            if (ourDeck.Count == 0) return;
            ourHand.Add(ourDeck[ourDeck.Count-1]);
            ourDeck.RemoveAt(ourDeck.Count - 1);
        }
        //敌方回合
        else
        {
            if (enemyDeck.Count == 0) return;
            enemyHand.Add(enemyDeck[enemyDeck.Count - 1]);
            enemyDeck.RemoveAt(enemyDeck.Count - 1);
        }
    }

    //战斗阶段
    void Battle()
    {

    }

    void addCardTo(int i,RealCard rc)
    {
        //GameObject go = new GameObject()
    }
}
