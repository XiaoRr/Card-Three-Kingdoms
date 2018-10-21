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
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //新的回合 等待数-1 如果等待数<=0 返回true
    public bool CheckTurn()
    {
        setNum(--num);
        return num <= 0;
    }
}
