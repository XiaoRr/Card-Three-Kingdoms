using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Slider slider;
    public Text text;
    [HideInInspector]
    private int hp;

    public int Hp {
        set { hp = value; text.text = value.ToString(); }
        get { return hp; }
    }
    public void Init(int maxHp)
    {
        slider.maxValue = maxHp;
        hp = maxHp;
    }
	// Use this for initialization
	void Start () {
        text.text = hp.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        slider.value = hp;
        text.text = hp.ToString();
    }
}
