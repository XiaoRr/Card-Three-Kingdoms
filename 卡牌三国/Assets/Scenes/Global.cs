using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

public static class Global
{
    //读取所有卡片
    public static Dictionary<string, Card> Load()
    {

        List<Card> cards = new List<Card>();
        Dictionary<string,Card> dictionary = new Dictionary<string, Card>();
        //路径  
        string fullPath = "Assets/Resources/Cards/";  //路径

        //获取指定路径下面的所有资源文件  
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

            Debug.Log("卡牌张数："+files.Length);


            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                if ('0' <= files[i].Name[0] && files[i].Name[0] <= '9' || 'a' <= files[i].Name[0] && files[i].Name[0] <= 'z')      //未完成重命名
                {
                    continue;
                }
                Card tmp;
                cards.Add(tmp = new Card(files[i].Name));
                if (dictionary.ContainsKey(tmp.name))   //出现重复名字是因为大部分卡有两种品质
                {
                    if(dictionary[tmp.name].rare < tmp.rare)    //如果字典里的品质较低
                    {
                        //Debug.Log("出现了不同品质的卡" + tmp.name);
                        dictionary[tmp.name + 'L'] = dictionary[tmp.name];  //把品质低的key增加一个L字样
                        dictionary[tmp.name] = tmp;
                    }
                    else if(dictionary[tmp.name].rare > tmp.rare)
                    {
                        //Debug.Log("出现了不同品质的卡" + tmp.name);
                        dictionary[tmp.name + 'L'] = tmp;    //把品质低的key增加一个L字样
                    }
                    else
                    {
                        Debug.Log("出现了重复卡" + tmp.name);
                    }
                }
                dictionary[tmp.name] = tmp;    //添加到map索引
                //cards.Last<Card>();
            }
        }

        return dictionary;
    }

}