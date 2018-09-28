using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

public class Global
{
    public List<Card> cards;    //所有卡片的数据库

    public void Load()
    {
        //读取所有卡片
        cards = new List<Card>();
        //路径  
        string fullPath = "Assets/Resources/Cards/";  //路径

        //获取指定路径下面的所有资源文件  
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

            Debug.Log(files.Length);

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                cards.Add(new Card(files[i].Name));
            }
        }
    }

}