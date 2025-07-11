using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class PayTableController: MonoBehaviour, IPopUp {

    public Button[] buttons;
    public Transform layuotGrid;
    public GameObject iconPayPrefab;

    bool showed;

    public void CloseButton_click()
    {
        SetControlActivity(false);
        SoundMasterController.Instance.SoundPlayPopUp(0.2f, null);
        GetComponent<GuiFader_v2>().FadeOut(0, ()=> { CloseHandler(); });
    }

    public void OkButton_click()
    {
        SetControlActivity(false);
        SoundMasterController.Instance.SoundPlayPopUp(0.2f, null);
        GetComponent<GuiFader_v2>().FadeOut(0, () => { CloseHandler(); });
    }

    public void CloseHandler()
    {
        showed = false;
        closeD(gameObject);
    }

    public void OpenHandler()
    {
        List<GameObject> gl = new List<GameObject>();
        GetChilds(layuotGrid.gameObject, ref gl);

        // destroy old paytable
        if (gl.Count > 0)
            foreach (GameObject g in gl)
            {
                Destroy(g);
            }

        Sprite[] icons = SlotController.Instance.iconSprites;
        int[] iconsCost = SlotController.Instance.iconCost;
        for (int ic = 0; ic < icons.Length; ic++)
        {
            GameObject pI = (GameObject)Instantiate(iconPayPrefab, layuotGrid.transform.position, Quaternion.identity);
            pI.transform.localScale = layuotGrid.lossyScale;
            pI.transform.parent = layuotGrid;

            pI.GetComponent<PayTableIcon>().Init(icons[ic], ic, iconsCost[ic], iconsCost[ic]*2, iconsCost[ic]*3);
        }

        SetControlActivity(true);
        showed = true;
        openD(gameObject);
    }

    public void SetControlActivity(bool activity)
    {
        foreach (Button b in buttons)
        {
            b.interactable = activity;
        }
    }

    public bool IsShowed()
    {
        return showed;
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    Action<GameObject> openD;
    Action<GameObject> closeD;
    public void PopUpInit(Action<GameObject> openDel, Action<GameObject> closeDel)
    {
        if (openDel != null) openD = openDel;
        else
        {
            openD = new Action<GameObject>((gameObject) => { });
        }

        if (closeDel != null) closeD = closeDel;
        else
        {
            closeD = new Action<GameObject>((gameObject) => { });
        }
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
