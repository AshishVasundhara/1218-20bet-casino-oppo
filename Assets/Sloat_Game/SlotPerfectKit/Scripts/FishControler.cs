using System.Collections;
using System.Collections.Generic;
using BE;
using UnityEngine;
using UnityEngine.UI;

public class FishControler : MonoBehaviour {

    float time__;
    public Sprite[] images;
	// Use this for initialization
	void Start () {
        time__ = Time.time + 1;
	}
	
	// Update is called once per frame
	void Update () {

       // print("Time - -- - > " + Time.time);
        if(Time.time<time__){
            return;
        }
        else{
            time__ = Time.time + 1;
            //if (SceneSlotGame.instance.Bonusgamepanel.activeSelf && gameObject.GetComponent<Animator>().enabled == false)
            //{
            //    int a = Random.Range(0, 3);
            //    print("a----->" + a);
            //    if(a<2)
            //    {
            //        gameObject.GetComponent<Image>().sprite = images[0];
            //    }
            //    else{
            //        gameObject.GetComponent<Image>().sprite = images[1];
            //    }
            //}
        }

	}



}
