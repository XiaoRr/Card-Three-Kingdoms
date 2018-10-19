using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TokenManager;

public class Token : MonoBehaviour {
    [HideInInspector]
    public int info;     //token数据 只有部分token（生命 冰冻 攻击力 等待）有这个数据 剩下的不会显示数据
    [HideInInspector]
    public TokenKind tk;    //token种类 见enum TokenKind
    [HideInInspector]
    public RealCard caster; //施加这个状态的对象，可能为空 这个沙雕变量只为瘟疫而存在 因为瘟疫不能叠加

    public Text text;   //token上的文字
    //8种图标
    public Sprite waiting;
    public Sprite health;
    public Sprite attack;
    public Sprite frozen;
    public Sprite burning;
    public Sprite poison;
    public Sprite plague;
    public Sprite trap;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetInfo(int info)
    {
        this.info = info;
        text.text = info.ToString();
    }

    //设定token种类 使用switch而不是dictionary是因为开销比较小
    public void SetKind(TokenKind tk)
    {
        Image img = GetComponent<Image>();
        switch (tk)
        {
            case TokenKind.中毒:
                img.overrideSprite = poison;
                break;
            case TokenKind.生命:
                img.overrideSprite = health;
                break;
            case TokenKind.攻击力:
                img.overrideSprite = attack;
                break;
            case TokenKind.瘟疫:
                img.overrideSprite = plague;
                break;
            case TokenKind.燃烧:
                img.overrideSprite = burning;
                break;
            case TokenKind.冰冻:
                img.overrideSprite = frozen;
                break;
            case TokenKind.陷阱:
                img.overrideSprite = trap;
                break;
            case TokenKind.等待:
                img.overrideSprite = waiting;
                break;
        }
    }


    public void Set(TokenKind kd,int info,RealCard caster = null)
    {
        SetKind(kd);
        SetInfo(info);
        this.caster = caster;
    }
}
