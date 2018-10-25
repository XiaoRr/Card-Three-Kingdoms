using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingToken : Token {


    public Text text;
    //public 
    public override void setNum(int num)
    {
        text.text = num.ToString();
        this.num = num;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 新的回合 等待数-var 如果等待数小于等于0 返回true
    /// </summary>
    /// <param name="var">减少的等待回合数</param>
    /// <returns></returns>
    public bool CheckTurn(int var = 1)
    {
        num -= var;
        setNum(num);
        return num <= 0;
    }
}
