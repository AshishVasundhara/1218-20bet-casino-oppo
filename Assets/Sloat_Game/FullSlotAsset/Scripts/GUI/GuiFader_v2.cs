using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class GuiFader_v2 : MonoBehaviour {

    public float fadingTime = 1.0f;

    public GameObject [] gObjects;

    public UnityEvent completeCallback_in;
    public UnityEvent completeCallback_out;

    private bool initialised = false;
    private FaderObjectsRec fObjectR;

    public void FadeIn(float tweenDelay, Action completeCallBack)
    {
        if (!initialised)
        {
            Initialize();
        }

        fObjectR.SetActive(false);
        fObjectR.SetAlpha(0f);
        fObjectR.SetActive(true);
        LeanTween.value(gameObject, 0f, 1.0f, fadingTime).setEase(LeanTweenType.easeInCirc).setOnUpdate((float val) => { fObjectR.SetAlphaK(val); }).
            setDelay(tweenDelay).
            setOnComplete(()=> {if(completeCallBack!=null) completeCallBack(); if (completeCallback_in != null) completeCallback_in.Invoke(); });
    }

    public void FadeOut(float tweenDelay, Action completeCallBack)
    {
        if (!initialised)
        {
            Initialize();
        }

        fObjectR.SetActive(true);
        LeanTween.value(gameObject, 1.0f, 0.0f, fadingTime).setEase(LeanTweenType.easeInCirc).setOnUpdate((float val) => { fObjectR.SetAlphaK(val); }).
             setDelay(tweenDelay).
            setOnComplete(() => {
                fObjectR.SetActive(false);
                if (completeCallBack != null) completeCallBack();
                if (completeCallback_out != null) completeCallback_out.Invoke();
            });
    }

    public void SetInitState()
    {
        if (initialised)
        {
            fObjectR.SetInitState();
        }
    }

    private void Initialize()
    {
        fObjectR = new FaderObjectsRec(gObjects[0]);
        for (int i = 1; i < gObjects.Length; i++)
        {
            fObjectR.Add(new FaderObjectsRec(gObjects[i]));
        }

        initialised = true;
    }

}

    public class FaderObject
{
    public  Image image;
    public  Text  text;
    public GameObject gOb;

    private  float initAlpha;
    private bool isInitActiv;

    private float currAlpha;

    public FaderObject(GameObject gO)
    {
        Image imageIn = gO.GetComponent<Image>();
        Text textIn = gO.GetComponent<Text>();
        isInitActiv = gO.activeSelf;
        gOb = gO;

        if (imageIn != null) { image = imageIn; initAlpha = GetAlpha(image); }
        if (textIn != null) { text = textIn; initAlpha = GetAlpha(text); }
        currAlpha = initAlpha;
    }

    private float GetAlpha(Image im)
    {
        Color c = im.color;
        return c.a;
    }

    private float GetAlpha(Text tx)
    {
        Color c = tx.color;
        return c.a;
    }

    public void SetInitState()
    {
        if (text != null)
        {
            Color c = text.color;
            c.a = initAlpha;
            text.color = c;
            text.gameObject.SetActive(isInitActiv);
        }

        if (image != null)
        {
            Color c = image.color;
            c.a = initAlpha;
            image.color = c;
            image.gameObject.SetActive(isInitActiv);
        }
        currAlpha = initAlpha;
    }

    public void SetAlpha(float alpha)
    {
        currAlpha = alpha;
        if (text != null)
        {
            Color c = text.color;
            c.a = currAlpha;
            text.color = c;
        }

        if (image != null)
        {
            Color c = image.color;
            c.a = currAlpha;
            image.color = c;
        }
    }

    public void SetAlphaK(float multiplier)
    {
      currAlpha = initAlpha * multiplier;
        SetAlpha(currAlpha);
    }

    public void SetActive(bool activity)
    {
        gOb.SetActive(activity);
    }

    public float GetCurrAlpha()
    {
        return currAlpha;
    }

}

    public class FaderObjectsRec
    {
    List<FaderObject> fObjects;
    List<FaderObject> parents;

    public FaderObjectsRec(GameObject gObjectParent)
    {
        List<GameObject>  gObjects = new List<GameObject>();
        parents = new List<FaderObject>();
        gObjects.Add(gObjectParent);
        parents.Add(new FaderObject(gObjectParent));
        fObjects = new List<FaderObject>();
        GetChilds(gObjectParent, ref gObjects);
        gObjects.ForEach((gO) => { fObjects.Add(new FaderObject (gO)); });
    }

    public void SetInitState()
    {
        fObjects.ForEach((fO)=> { fO.SetInitState(); });
    }

    public void SetAlpha(float alpha)
    {
        fObjects.ForEach((fO) => { fO.SetAlpha(alpha); });
    }

    public void SetAlphaK(float multiplier)
    {
        fObjects.ForEach((fO) => { fO.SetAlphaK(multiplier); });
    }

    /// <summary>
    /// Set Active only parent objects
    /// </summary>
    public void SetActive(bool activity)
    {
        parents.ForEach((pFO) => { pFO.SetActive(activity); });
    }

    public void Add(FaderObjectsRec fOb)
    {
        fOb.fObjects.ForEach((ob)=> { fObjects.Add(ob); });
        fOb.parents.ForEach((pOb)=> { parents.Add(pOb); });
    }

    private void GetChilds(GameObject g, ref List<GameObject> gList)
    {
        int childs = g.transform.childCount;
        if (childs > 0)//The condition that limites the method for calling itself
            for (int i = 0; i < childs; i++)
            {
                Transform gT = g.transform.GetChild(i);
                GameObject gC = gT.gameObject;
                if (gC) gList.Add(gC);
                GetChilds(gT.gameObject, ref gList);
            }
    }
}