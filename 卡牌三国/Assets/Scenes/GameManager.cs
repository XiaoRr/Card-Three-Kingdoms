using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Deck enemyDeck, ourDeck;   //敌我卡组

    public Hand enemyHand, ourHand;   //敌我手牌

    public BattleField enemyField, ourField;     //战场（水平布局）
    public Image largeImg; //大图浏览页面
    [HideInInspector]
    public List<RealCard> enemyGrave, ourGrave;   //敌我墓地
    [HideInInspector]
    public int turn = 1;   //当前回合数
    [HideInInspector]
    public int enemyHp, ourHp;  //敌我血量
    [HideInInspector]
    public Dictionary<string, Card> cards;



    //一些运行控制变量
    //public 
    //public Transform TS_enemyDeck, TS_ourDeck;  //牌库
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
        //测试每个区域的功能是否正常
        for (int i = 0; i < 4; i++)
        {
            GameObject card = Instantiate(Resources.Load("Prefabs/战场卡牌") as GameObject);
            card.GetComponent<RealCard>().initRealCard(this, cards["火炎兽"]);
            ourDeck.Add(card);
        }

        for (int i = 0; i < 4; i++)
        {
            GameObject card = Instantiate(Resources.Load("Prefabs/战场卡牌") as GameObject);
            card.GetComponent<RealCard>().initRealCard(this, cards["天雷卫"]);
            enemyDeck.Add(card);
        }

        for (int i = 0; i < 4; i++)
        {
            GameObject card = Instantiate(Resources.Load("Prefabs/战场卡牌") as GameObject);
            card.GetComponent<RealCard>().initRealCard(this, cards["诸葛亮"]);
            ourHand.Add(card);
        }

        for (int i = 0; i < 4; i++)
        {
            GameObject card = Instantiate(Resources.Load("Prefabs/战场卡牌") as GameObject);
            card.GetComponent<RealCard>().initRealCard(this, cards["曹操"]);
            enemyHand.Add(card);
        }

        for (int i = 0; i < 4; i++)
        {
            GameObject card = Instantiate(Resources.Load("Prefabs/战场卡牌") as GameObject);
            card.GetComponent<RealCard>().initRealCard(this, cards["藏獒"]);
            ourField.Add(card);
        }

        for (int i = 0; i < 4; i++)
        {
            GameObject card = Instantiate(Resources.Load("Prefabs/战场卡牌") as GameObject);
            card.GetComponent<RealCard>().initRealCard(this, cards["雪翼虎"]);
            enemyField.Add(card);
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
            //if (ourDeck.Count == 0) return;
            //ourHand.Add(ourDeck[ourDeck.Count-1]);
            //ourDeck.RemoveAt(ourDeck.Count - 1);
        }
        //敌方回合
        else
        {
           // if (enemyDeck.Count == 0) return;
            //enemyHand.Add(enemyDeck[enemyDeck.Count - 1]);
            //enemyDeck.RemoveAt(enemyDeck.Count - 1);
        }
    }

    //战斗阶段
    void Battle()
    {

    }

    void AddCardTo(int i,RealCard rc)
    {
        //GameObject go = new GameObject()
    }
}


//动画完成后执行事件 可能有用
//https://blog.csdn.net/qq_34244317/article/details/78756320