using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TokenManager;

/**
 * 游戏中各种效果作用于卡片上 形成各种UI效果
 * 例如攻击力和生命会额外标注在卡牌上
 * 而等待回合这个UI只会在手牌出现 而且覆盖整个卡面
 * 这个类是管理所有效果所产生的UI 以及存储效果自身数据的父类
 * 每个效果 作为Componment 而存在着
 */
public abstract class Token : MonoBehaviour {

    [HideInInspector]
    public int num; //部分效果会有数字（生命 冰冻 攻击力 等待）
    [HideInInspector]
    public TokenType tk;   //token种类 见enum TokenKind
    [HideInInspector]
    public RealCard caster; //施加这个状态的对象，可能为空 这个沙雕变量只为瘟疫而存在 因为瘟疫不能叠加


    public abstract void setNum(int num);
    public void show()
    {
        this.enabled = true;
    }
    public void hide()
    {
        this.enabled = false;
    }
}
