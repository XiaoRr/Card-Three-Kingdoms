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
    public enum Timing { 抽卡前, 战斗后, 战斗中, 被攻击, 被法术锁定, 回合结束}

    //两个比较关键的类的引用，他们都是包含这个类的类
    public GameManager gm;  //游戏管理器
    public RealCard rc; //对应卡牌
    private static Dictionary<MetaSkill, List<Timing>> dic = new Dictionary<MetaSkill, List<Timing>>()
        {
            {MetaSkill.普通攻击,new List<Timing>{Timing.战斗中 } },
            {MetaSkill.火焰,new List<Timing>{Timing.战斗中,Timing.被攻击} },
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
        // 判断敌对group是谁
        CardGroup enemy = (cg == gm.ourField) ? gm.enemyField : gm.ourField;
        
        switch (skill.skill)
        {
            case MetaSkill.普通攻击:
                if (enemy.owner.childCount > pos)
                {
                    RealCard enemyCard = enemy.owner.GetChild(pos).GetComponent<RealCard>();    // 获取对面的卡牌

                    enemyCard.hp -= rc.atk; // 扣血
                    gm.logger.Log($"{rc.info.name}攻击了{enemyCard.info.name}");
                }
                else
                {
                    // 直接攻击
                    if (enemy == gm.enemyField) gm.enemyHealthBar.Hp -= rc.info.atk;
                    else gm.ourHealthBar.Hp -= rc.info.atk;
                    gm.logger.Log($"{rc.info.name}直接攻击了{((cg == gm.ourField) ? "敌":"我")}方玩家");
                }

                break;
        }
        yield return new WaitForSeconds(GameManager.gameSpeed);
    }
      
}