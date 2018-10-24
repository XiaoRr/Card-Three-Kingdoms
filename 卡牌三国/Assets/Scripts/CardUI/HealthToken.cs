using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthToken : Token {

    public Text text;
    [HideInInspector]
    public int hp;
    [HideInInspector]
    public int maxHp;
    /*
    public int Hp{
        get{return hp;}
        set{setNum(value);}
    }
    */
    public override void setNum(int num)
    {
        text.text = (num < maxHp) ? $"<color=#FFBBFF>{num}</color>" : num.ToString();   //残血变红
        this.hp = num;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
