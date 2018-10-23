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
    private static Dictionary<MetaSkill, Timing> dic = new Dictionary<MetaSkill, Timing>()
        {
            {MetaSkill.普通攻击,Timing.战斗中}
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

    public IEnumerator FindAndCastSkill(Timing timing)
    {
        foreach(var skill in skills)
        {
            if (!dic.ContainsKey(skill.skill)) continue;    //debug用，排除尚未加入的技能
            if (dic[skill.skill] != timing) continue;
            yield return Cast(skill);
        }
    }

    public IEnumerator Cast(Skill skill)
    {
        switch (skill.skill)
        {
            case MetaSkill.普通攻击:
                gm.logger.Log($"{rc.info.name}攻击了");
                break;
        }
        yield return new WaitForSeconds(GameManager.gameSpeed);
    }
      
}