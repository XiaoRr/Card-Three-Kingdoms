using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnCounter : MonoBehaviour {

    public Text text;
    public Animation animatePoint;
    //public AnimationClip pointUp, pointDown;

    [HideInInspector]
    public int turn = 1;
	// Use this for initialization
	void Start () {
        turn = 1;
        text.text = turn.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setNum(int num)
    {
        this.turn = num;
        text.text = turn.ToString();
    }

    public IEnumerator NextTurn()
    {

        if(turn%2 == 1)
        {
            animatePoint.Play("PointerMoveUp");
        }
        else
        {
            animatePoint.Play("PointerMoveDown");
        }
        yield return new WaitForSeconds(0.3f);
        setNum(++turn);
        yield return new WaitForSeconds(0.2f);

    }
}
