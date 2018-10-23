using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthToken : Token {

    public Text text;
    [HideInInspector]
    public int hp;
    //public 
    public override void setNum(int num)
    {
        text.text = num.ToString();
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
