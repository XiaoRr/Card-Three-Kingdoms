using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenManager : MonoBehaviour {



    /**
 *     token种类
 *     攻击力，生命：基本数据
 *     燃烧：回合结束时Hp-n
 *     冰冻：n回合无法行动
 *     瘟疫：失去1攻击和1hp 同来源不叠加
 *     中毒: 下一回合结束时损失nhp
 *     陷阱：下回合无法行动
 *     等待：在手牌区特有的属性，每回合-1 为0时上场
 *     毒发：本回合结束时失去n hp  该状态无法获得，由【中毒】转移而来
 */
    public enum TokenType { 攻击力, 燃烧, 冰冻, 生命, 瘟疫, 中毒, 陷阱, 等待, 毒发 };
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
