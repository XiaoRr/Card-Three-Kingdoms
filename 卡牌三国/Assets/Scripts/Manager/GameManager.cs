using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using static TokenManager;

public class GameManager : MonoBehaviour {

    public class Side
    {
        public Deck deck;
        public Hand hand;
        public BattleField field;
        public HealthBar healthBar;
        public Grave grave;
        public Side enemy;
        public Side(Deck d,Hand h,BattleField f,HealthBar hb,Grave g)
        {
            deck = d;hand = h;field = f;healthBar = hb;grave = g;
            d.side = this;
            h.side = this;
            f.side = this;
            g.side = this;
        }
    }
    [SerializeField]
    private Deck _enemyDeck, _ourDeck;   //敌我卡组
    [SerializeField]
    private Hand _enemyHand, _ourHand;   //敌我手牌
    [SerializeField]
    private BattleField _enemyField, _ourField;     //战场（水平布局）
    [SerializeField]
    private HealthBar _ourHealthBar, _enemyHealthBar;  //血条
    [SerializeField]
    private Grave _enemyGrave, _ourGrave;   //敌我墓地
    public Side enemy, our; //敌我全部容器
    public TurnCounter counter; //回合计数器
    public Logger logger;   //日志记录器
    public Image largeImg; //大图浏览页面

    [HideInInspector]
    public Dictionary<string, Card> cards;

    public static float gameSpeed = 0.2f;

    //一些运行控制变量
    private bool flag = true;  //逻辑停止信号
    // Use this for initialization
    void Start () {
        //初始化工作 各种容器按功能归位
        enemy = new Side(_enemyDeck,_enemyHand,_enemyField,_enemyHealthBar,_enemyGrave);
        our = new Side(_ourDeck,_ourHand, _ourField, _ourHealthBar, _ourGrave);
        enemy.enemy = our;
        our.enemy = enemy;
        cards = Global.Load();

        tmpInit();
        //largeImg.gameObject.SetActive(false);
        StartCoroutine(GameLogic());

        enemy.healthBar.Init(20);
        our.healthBar.Init(20);

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

            //抽卡
            yield return DrawCard();

            //抽卡后，战斗前某些卡可能发动效果
            yield return CastBeforeDraw();

            //logger.addLog("测试种");
            //CastBeforeBattle();

            //结算战斗
            yield return Battle();

            //结算伤亡
            yield return CheckDeath(counter.turn % 2 == 1 ? enemy.field : our.field);

            //回合计数器
            yield return counter.NextTurn();

            yield return new WaitForSeconds(gameSpeed);
        }

    }

    private IEnumerator CastBeforeDraw()
    {
        if (counter.turn % 2 == 1)
        {
            yield return CastSkill(SkillManager.Timing.抽卡后, our.field);
        }
        else
        {
            yield return CastSkill(SkillManager.Timing.抽卡后, enemy.field);
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
            our.deck.Add(card);
        }
        foreach (string testCardsName in testCardsNames)
        {
            GameObject card = Instantiate(Resources.Load("Prefabs/Card") as GameObject);
            card.GetComponent<RealCard>().initRealCard(this, cards[testCardsName]);
            enemy.deck.Add(card);
        }
        our.deck.shuffle();
        enemy.deck.shuffle();


    }
    //抽卡 将卡组的卡移到手牌
    private IEnumerator DrawCard()
    {
        //yield return new WaitForSeconds(gameSpeed);
        //我方回合
        if (counter.turn % 2 == 1 && !our.deck.empty())
        {
            Transform tar = our.deck.owner.GetChild(0);
            RealCard tarRC = tar.gameObject.GetComponent<RealCard>();
            tarRC.tm.SetToken(TokenType.等待, tarRC,tarRC.info.turn);
            our.deck.sendTo(tar, our.hand);
            logger.Log($"我方抽卡 {tarRC.info.name}");
        }
        //敌方回合
        if (counter.turn % 2 == 0 && !enemy.deck.empty())
        {
            Transform tar = enemy.deck.owner.GetChild(0);
            RealCard tarRC = tar.gameObject.GetComponent<RealCard>();
            tarRC.tm.SetToken(TokenType.等待, tarRC,tarRC.info.turn);
            enemy.deck.sendTo(tar, enemy.hand);
            logger.Log($"敌方抽卡 {tarRC.info.name}");
        }
        yield return new WaitForSeconds(gameSpeed);
    }

    //减少一次等待时间 如果有完成等待的卡 放入战场
    private IEnumerator UpdateWaitingField()
    {
        foreach(Transform obj in enemy.hand.owner)
        {
            RealCard rc = obj.gameObject.GetComponent<RealCard>();
            if (rc.tm.waiting.CheckTurn())  //这张卡已经完成了冷却
            {
                rc.HandToField();
                logger.Log($"{rc.info.name}进场了");
                enemy.hand.sendTo(obj.transform, enemy.field);
                yield return new WaitForSeconds(gameSpeed);
            }
        }
        foreach (Transform obj in our.hand.owner)
        {
            RealCard rc = obj.gameObject.GetComponent<RealCard>();
            if (rc.tm.waiting.CheckTurn())  //这张卡已经完成了冷却
            {
                rc.HandToField();
                logger.Log($"{rc.info.name}进场了");
                our.hand.sendTo(obj.transform, our.field);
                yield return new WaitForSeconds(gameSpeed);
            }
        }
        yield return new WaitForSeconds(gameSpeed);
    }


    public IEnumerator CastSkill(SkillManager.Timing timing, CardGroup group)
    {
        for (int i = 0; i < group.owner.transform.childCount; i++)
        {
            yield return group.owner.transform.GetChild(i).GetComponent<RealCard>().skill.FindAndCastSkill(timing, group, i);
        }
    }
    //战斗阶段
    private IEnumerator Battle()
    {
        if (counter.turn % 2 == 0)
        {
            yield return CastSkill(SkillManager.Timing.战斗中, enemy.field);
        }
        if (counter.turn % 2 == 1)
        {
            yield return CastSkill(SkillManager.Timing.战斗中, our.field);
        }

    }

    void AddCardTo(int i,RealCard rc)
    {
        //GameObject go = new GameObject()
    }

    private IEnumerator CheckDeath(CardGroup field)
    {
        Debug.Assert(field == our.field || field == enemy.field);
        List<Transform> deadList = new List<Transform>();
        for (int i = 0; i < field.owner.transform.childCount; i++)
        {
            if (field.owner.transform.GetChild(i).GetComponent<RealCard>().hp <= 0)
                deadList.Add(field.owner.transform.GetChild(i));
        }
        foreach (Transform ts in deadList)
        {
            field.sendTo(ts, field.side.grave);
            ts.GetComponent<RealCard>().skill.FindAndCastSkill(SkillManager.Timing.送往墓地后, field.side.grave);
            yield return new WaitForSeconds(gameSpeed);
        }
    }
}


// 动画完成后执行事件 可能有用
// https://blog.csdn.net/qq_34244317/article/details/78756320