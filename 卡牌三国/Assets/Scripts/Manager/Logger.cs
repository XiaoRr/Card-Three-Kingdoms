using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 游戏右下角的日志记录器
 * 目前功能：增加一行日志 清除当前日志
 * 待加入功能：拖动查看日志时 日志不再自动移动至最新一条 
 * 否则（或者日志在底部时） 日志自动更新
 * 
 * 
 */
public class Logger : MonoBehaviour {

    public Text text;
    private string tmpTxt;  //考虑到更新非常的不频繁 不适用stringbuilder 
	// Use this for initialization
	void Start () {
        text.text = "开始记录对局" + Environment.NewLine;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //添加日志
    public void Log(string newLog)
    {
        newLog += Environment.NewLine;
        text.text += newLog;
    }

    //清除日志
    public void clrLog()
    {
        text.text = "";
    }
}
