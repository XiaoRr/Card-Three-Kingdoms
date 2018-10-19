using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Deck enemyDeck, ourDeck;   //敌我卡组

    public Hand enemyHand, ourHand;   //敌我手牌

    public BattleField enemyField, ourField;     //战场（水平布局）

    public Logger logger;   //日志记录器
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
    private bool flag = true;  //逻辑停止信号
    // Use this for initialization
    void Start () {
        cards = Global.Load();

        tmpInit();

        StartCoroutine(GameLogic());
    }

    // Update is called once per frame
    void Update () {

        //结算死亡

            //结算胜利
            //结算buff

	}

    IEnumerator GameLogic()
    {
        while (flag)
        {
            //抽卡
            //DrawCard();
            //logger.addLog("测试种");
            //CastBeforeBattle();

            //结算战斗
            //Battle();
            yield return new WaitForSeconds(0.5f);
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
        //yield return new WaitForSeconds(0.5f);
        //我方回合
        if (turn % 2 == 1)
        {

        }
        //敌方回合
        else
        {

        }
        //yield return new WaitForSeconds(0.5f);
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