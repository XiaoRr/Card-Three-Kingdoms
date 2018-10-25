using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card;

/// <summary>
/// 所有技能的管理类，计划中，也管理技能动画等
/// </summary>
public class SkillManager{
    public List<Skill> skills;
    public enum Timing { 抽卡前,抽卡后,战斗后, 战斗中, 被攻击, 被法术锁定, 回合结束}

    //两个比较关键的类的引用，他们都是包含这个类的类
    public GameManager gm;  //游戏管理器
    public RealCard rc; //对应卡牌
    private static Dictionary<MetaSkill, List<Timing>> dic = new Dictionary<MetaSkill, List<Timing>>()
        {
            {MetaSkill.普通攻击,new List<Timing>{Timing.战斗中 } },
            {MetaSkill.火焰,new List<Timing>{Timing.战斗中,Timing.被攻击} },
            {MetaSkill.冲阵,new List<Timing>{Timing.抽卡后} }
        };
    public SkillManager(GameManager _gm,RealCard _rc,List<Skill> _skills)
    {
        gm = _gm;
        rc = _rc;
        skills = new List<Skill>();
        foreach (var _skill in _skills) //deepCopy
        {  
            skills.Add(_skill);
        }
        skills.Add(new Skill { skill = MetaSkill.普通攻击, var = 1 });  //每张卡都有普通攻击技能，如果后续有特殊攻击技能，普通攻击会被移除

    }

    /// <summary>
    /// 给定时机，让卡牌自己决定是否要发动技能
    /// </summary>
    /// <param name="timing">发动时机</param>
    /// <param name="position">卡牌位置</param>
    /// <returns></returns>
    public IEnumerator FindAndCastSkill(Timing timing,CardGroup cg,int position = -1)
    {
        /*
        if(position == -1)  // 无法判断卡牌位置
        {
            for(int i=0)
        }
        */

        foreach(var skill in skills)
        {
            if (!dic.ContainsKey(skill.skill)) continue;    // debug用，排除尚未加入的技能
            bool flg = false;
            foreach(Timing tim in dic[skill.skill]) //就算是同个技能，也可能有多个timing 
            {
                if(tim == timing)
                {
                    flg = true;
                    break;
                }
            }
            if (!flg) continue;
            yield return Cast(skill,cg,position);
        }
    }

    public IEnumerator Cast(Skill skill,CardGroup cg,int pos)
    {
        switch (skill.skill)
        {
            case MetaSkill.普通攻击:
                Debug.Assert(cg == gm.our.field || cg == gm.enemy.field);
                if (cg.side.enemy.field.owner.childCount > pos)
                {
                    RealCard enemyCard = cg.side.enemy.field.owner.GetChild(pos).GetComponent<RealCard>();    // 获取对面的卡牌

                    enemyCard.hp -= rc.atk; // 扣血
                    gm.logger.Log($"{rc.info.name}攻击了{enemyCard.info.name}");
                }
                else
                {
                    // 直接攻击
                    cg.side.enemy.healthBar.Hp -= rc.info.atk;
                    gm.logger.Log($"{rc.info.name}直接攻击了{((cg == gm.our.field) ? "敌":"我")}方玩家");
                }

                break;
            case MetaSkill.冲阵:

                if (cg.side.hand.owner.childCount == 0) {
                    gm.logger.Log($"{rc.info.name}试图发动冲阵{skill.var}，但是手牌是空的");
                }
                else
                {
                    //随机选择一个目标
                    RealCard tar = cg.side.hand.owner.GetChild(UnityEngine.Random.Range(0, cg.side.hand.owner.childCount)).GetComponent<RealCard>();
                    gm.logger.Log($"{rc.info.name}为{tar.info.name}发动了冲阵{skill.var}");
                    if (tar.tm.waiting.CheckTurn(skill.var))    //如果因此进场的话
                    {
                        yield return new WaitForSeconds(GameManager.gameSpeed);
                        tar.HandToField();
                        gm.logger.Log($"{tar.info.name}进场了");
                        cg.sendTo(tar.transform, cg.side.field);
                    }
                }
                
                break;
        }
        yield return new WaitForSeconds(GameManager.gameSpeed);
    }
      
}