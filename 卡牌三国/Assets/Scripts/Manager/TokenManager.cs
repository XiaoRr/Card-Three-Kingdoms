using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenManager : MonoBehaviour {

    public AttackToken attack;
    public HealthToken health;
    public WaitingToken waiting;

    /**
 *     token种类
 *     攻击力，生命：基本数据
 *     燃烧：回合结束时Hp-n
 *     冰冻：n回合无法行动
 *     瘟疫：失去1攻击和1hp 同来源不叠加
 *     中毒: 下一回合结束时损失nhp
 *     陷阱：下回合无法行动
 *     等待：在手牌区特有的属性，每回合-1 为0时上场
 *     毒发：本回合结束时失去n hp  该状态无法获得，由【中毒】经过一回合后转移而来
 */
    public enum TokenType { 攻击力, 燃烧, 冰冻, 生命, 瘟疫, 中毒, 陷阱, 等待, 毒发 };
    // Use this for initialization
    void Start () {
        Reset();
    }
	
	// Update is called once per frame
	void Update () {

	}

    //添加一个状态
    public void SetToken(TokenType ty, RealCard real,int num = 1)
    {
        switch (ty)
        {
            case TokenType.攻击力:
                attack.show();
                attack.setNum(num);
                
                break;
            case TokenType.生命:
                health.show();
                health.setNum(num);
                health.maxHp = real.info.hp;    //需要额外存储一个hp，这里的real并不是敌人而是本体
                break;
            case TokenType.等待:
                waiting.show();
                waiting.setNum(num);
                break;
        }
    }

    //移除一个状态
    public void RemoveToken(TokenType ty,int num = -1)
    {
        switch (ty)
        {
            case TokenType.攻击力:
                attack.hide();
                attack.setNum(num);
                break;
            case TokenType.生命:
                health.hide();
                health.setNum(num);
                break;
            case TokenType.等待:
                waiting.hide();
                waiting.setNum(num);
                break;
        }
    }

    //重置所有状态
    public void Reset()
    {
        attack.hide();
        health.hide();
        waiting.hide();
    }
}
