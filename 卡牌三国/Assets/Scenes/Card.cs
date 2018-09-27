using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card  {

    public enum Type{错误=0,武将,步兵,亡灵,异兽,术,军备,骑兵, }  //卡牌种类
    public enum Skill {错误=0,长枪击, 蛊惑 }    //技能，数量繁多
    public enum Rare { Ashy, White,Green,Blue,Orange,Red}    //稀有度依次上升
    public enum Camp { Wei,Shu,Wu,Qun}  //魏蜀吴群

    public string name;
    public Type type;   //卡牌种类
    public Camp camp;   //阵营
    public Rare rare;   //稀有度
    public int turn, atk, hp;    //血 攻 防
    public int soldierNum, treasureNum, magicNum;   //3种卡牌的携带数量（武将限定）
    public List<Skill> skills;  //技能

    //构造函数，info为卡牌对应的图片名称，记录了卡牌的所有信息
    public Card(string info)
    {
        //以下为解析info并赋值的过程
        string[] tmp = info.Split(new char[] { '_' });

        //解析阵营
        switch (tmp[0])
        {
            case "魏": camp = Camp.Wei; break;
            case "蜀": camp = Camp.Shu; break;
            case "吴": camp = Camp.Wu; break;
            case "群": camp = Camp.Qun; break;
            default:
                Debug.Log("无法解析阵营" + tmp[0]);
                break;
        }
        //解析种类 数量太多 不使用swtich
        string[] types = Enum.GetNames(typeof(Type));
        foreach (var type in types)
        {
            if (tmp[1] == type)
            {
                this.type = (Type)Enum.Parse(typeof(Type), type);
                break;
            }
            Debug.Log("错误的卡片类型" + tmp[1]);
            this.type = (Type)0;    //未找到，返回错误值
        }

        name = tmp[2];  //姓名
        //解析稀有度
        switch (tmp[3])
        {
            case "A": rare = Rare.Ashy;break;
            case "W": rare = Rare.White;break;
            case "G": rare = Rare.Green;break;
            case "B": rare = Rare.Blue;break;
            case "O": rare = Rare.Orange;break;
            case "R": rare = Rare.Red;break;
            default:
                Debug.Log("无法解析稀有度"+tmp[2]);
                break;
        }
        


        if(type == Type.武将)
        {
            ParseCardNum(tmp[4]);
            ParseTAH(tmp[5],tmp[6],tmp[7]);
            ParseSkill(tmp, 8);
        }
        else if(type == Type.术)
        {
            ParseTAH(tmp[5], "-1", "-1");   //法术没有hp和攻击力，缺省
            ParseSkill(tmp, 6);
        }
        else
        {
            ParseTAH(tmp[4], tmp[5], tmp[6]);
            ParseSkill(tmp, 7);
        }



    }
    //解析携带的牌数（武将限定）
    void ParseCardNum(string tmp)
    {
        this.soldierNum = tmp[0] - '0';
        this.treasureNum = tmp[1] - '0';
        this.magicNum = tmp[2] - '0';
    }

    //解析三围
    void ParseTAH(string turn,string atk,string hp)
    {
        this.turn = int.Parse(turn);
        this.atk = int.Parse(atk);
        this.hp = int.Parse(hp);
    }
    
    //解析技能
    void ParseSkill(string[] tmp,int startNum)
    {
        skills = new List<Skill>();
        for (int i = startNum; i < tmp.Length; i++) {
            skills.Add(MatchSkill(tmp[i]));
        }

    }

    //根据字符串匹配枚举值
    Skill MatchSkill(string _skill)
    {
        string[] skills = Enum.GetNames(typeof(Skill));
        foreach (var skill in skills)
        {
            if (_skill == skill)
            {
                return (Skill)Enum.Parse(typeof(Skill), skill);
            }
        }
        Debug.Log("错误的技能类型" + _skill);
        return (Skill)0;
    }
}
