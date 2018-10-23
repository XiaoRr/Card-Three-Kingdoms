using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using static TokenManager;

public class GameManager : MonoBehaviour {

    public Deck enemyDeck, ourDeck;   //敌我卡组

    public Hand enemyHand, ourHand;   //敌我手牌

    public BattleField enemyField, ourField;     //战场（水平布局）

    public TurnCounter counter; //回合计数器

    public Logger logger;   //日志记录器
    public Image largeImg; //大图浏览页面
    [HideInInspector]
    public List<RealCard> enemyGrave, ourGrave;   //敌我墓地
    [HideInInspector]
    public int enemyHp, ourHp;  //敌我血量
    [HideInInspector]
    public Dictionary<string, Card> cards;

    public static float gameSpeed = 0.5f;

    //一些运行控制变量
    private bool flag = true;  //逻辑停止信号
    // Use this for initialization
    void Start () {
        cards = Global.Load();

        tmpInit();
        //largeImg.gameObject.SetActive(false);
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
            yield return new WaitForSeconds(gameSpeed);
            //更新等待回合数，这个过程发生在抽卡之前
            yield return UpdateWaitingField();
            //
            //抽卡
            yield return DrawCard();
            //yield return CastBeforeDraw();

            //logger.addLog("测试种");
            //CastBeforeBattle();

            //结算战斗
            yield return Battle();
            yield return new WaitForSeconds(gameSpeed);
            //回合计数器
            yield return counter.NextTurn();
            

        }

    }
    //测试用初始化函数
    void tmpInit()
    {
        string[] testCardsNames = { "火炎兽", "捕猎者", "枪骑兵", "侦察卫", "藏獒", "蒙冲船" };
        foreach(string testCardsName in testCardsNames)
        {
            GameObject card = Instantiate(Resources.Load("Prefabs/Card") as GameObject);
            card.GetComponent<RealCard>().initRealCard(this, cards[testCardsName]);
            ourDeck.Add(card);
        }
        foreach (string testCardsName in testCardsNames)
        {
            GameObject card = Instantiate(Resources.Load("Prefabs/Card") as GameObject);
            card.GetComponent<RealCard>().initRealCard(this, cards[testCardsName]);
            enemyDeck.Add(card);
        }
        ourDeck.shuffle();
        enemyDeck.shuffle();


    }

    //洗牌
    void Shuffle()
    {

    }

    //抽卡 将卡组的卡移到手牌
    private IEnumerator DrawCard()
    {
        //yield return new WaitForSeconds(gameSpeed);
        //我方回合
        if (counter.turn % 2 == 1 && !ourDeck.empty())
        {
            Transform tar = ourDeck.owner.GetChild(0);
            RealCard tarRC = tar.gameObject.GetComponent<RealCard>();
            tarRC.tm.SetToken(TokenType.等待, tarRC.info.turn);
            ourDeck.sendTo(tar, ourHand);
            logger.Log($"我方抽卡 {tarRC.info.name}");
        }
        //敌方回合
        if (counter.turn % 2 == 0 && !enemyDeck.empty())
        {
            Transform tar = enemyDeck.owner.GetChild(0);
            RealCard tarRC = tar.gameObject.GetComponent<RealCard>();
            tarRC.tm.SetToken(TokenType.等待, tarRC.info.turn);
            enemyDeck.sendTo(tar, enemyHand);
            logger.Log($"敌方抽卡 {tarRC.info.name}");
        }
        yield return new WaitForSeconds(gameSpeed);
    }

    //减少一次等待时间 如果有完成等待的卡 放入战场
    private IEnumerator UpdateWaitingField()
    {
        foreach(Transform obj in enemyHand.owner)
        {
            RealCard rc = obj.gameObject.GetComponent<RealCard>();
            if (rc.tm.waiting.CheckTurn())  //这张卡已经完成了冷却
            {
                rc.HandToField();
                logger.Log($"{rc.info.name}进场了");
                enemyHand.sendTo(obj.transform, enemyField);
                yield return new WaitForSeconds(gameSpeed);
            }
        }
        foreach (Transform obj in ourHand.owner)
        {
            RealCard rc = obj.gameObject.GetComponent<RealCard>();
            if (rc.tm.waiting.CheckTurn())  //这张卡已经完成了冷却
            {
                rc.HandToField();
                logger.Log($"{rc.info.name}进场了");
                ourHand.sendTo(obj.transform, ourField);
                yield return new WaitForSeconds(gameSpeed);
            }
        }
        yield return new WaitForSeconds(gameSpeed);
    }


    //战斗阶段
    private IEnumerator Battle()
    {
        if (counter.turn % 2 == 0)
        {
            foreach (Transform obj in enemyField.owner)
            {
                RealCard rc = obj.gameObject.GetComponent<RealCard>();
                logger.Log($"{rc.info.name}check");
                yield return rc.skill.FindAndCastSkill(SkillManager.Timing.战斗中);
            }
        }
        if (counter.turn % 2 == 1)
        {
            foreach (Transform obj in ourField.owner)
            {
                RealCard rc = obj.gameObject.GetComponent<RealCard>();
                yield return rc.skill.FindAndCastSkill(SkillManager.Timing.战斗中);
            }
        }

    }

    void AddCardTo(int i,RealCard rc)
    {
        //GameObject go = new GameObject()
    }
}


//动画完成后执行事件 可能有用
//https://blog.csdn.net/qq_34244317/article/details/78756320