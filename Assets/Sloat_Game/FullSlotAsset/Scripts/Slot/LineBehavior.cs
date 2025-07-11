using UnityEngine;
using System.Collections.Generic;
using System;

public enum LineWinType
{
    None, ThreeSymbol, FourSymbol, FiveSymbol,
    ThreeSpins, FourSpins, FiveSpins
}

public class LineBehavior : MonoBehaviour {

    public GameObject[] lineTiles;
    public RayCaster[] rayCasters;
    public int myNumber; // 1-9

    private MaterialPropertyBlock[] mpB;
    private Renderer[] rend;
    private LineWinType lineWin;

    public List<SlotSymbol> WinnSymbols;
    private bool winTweenComplete = true;
    public LineButtonBehavior lButton;

   

    public bool IsSelected
    {
        get { return lButton.Pressed; }
    }

    public bool IsWinningLine
    {
        get { return lineWin != LineWinType.None; }
    }
    /// <summary>
    /// Get spins won
    /// </summary>
    public int WonSpins
    {
        get
        {
            switch (lineWin)
            {
                case LineWinType.ThreeSpins:
                    return 1;
                case LineWinType.FourSpins:
                    return 2;
                case LineWinType.FiveSpins:
                    return 3;
                default: return 0;
            }
        }
    }
    /// <summary>
    /// Get coins won
    /// </summary>
    internal int WonCoins
    {
      get
        {
            switch (lineWin)
            {
                case LineWinType.ThreeSymbol:
                    return GetWinnings();

                case LineWinType.FourSymbol:
                    return GetWinnings();

                case LineWinType.FiveSymbol:
                    return GetWinnings();
                default: return 0;
            }
        }
    }
    /// <summary>
    /// Return true if is won tween complete
    /// </summary>
    internal bool IsWonTweenComplete
    {
        get { return winTweenComplete; }
    }

    void Start()
    {
        //1) set handlers for line buttons
        LineButtonBehavior[] lbbs = FindObjectsOfType<LineButtonBehavior>();
        for (int i = 0; i < lbbs.Length; i++)
        {
            if (lbbs[i].lineNumber == myNumber)
            {
                if (lButton==null) lButton = lbbs[i];
                lbbs[i].PressButtonDelegate += (num) => { SetLineVisible(true); };
                lbbs[i].UnPressButtonDelegate += (num) => { SetLineVisible(false); };
            }
        }

        //2 cashe data 
        rend = new Renderer[lineTiles.Length];
        mpB = new MaterialPropertyBlock[lineTiles.Length];
        for (int i = 0; i < lineTiles.Length; i++)
        {
            rend[i] = lineTiles[i].GetComponent<Renderer>();
            MaterialPropertyBlock mP = new MaterialPropertyBlock();
            mP.Clear();
            rend[i].GetPropertyBlock(mP);
            mpB[i] = mP;
        }
        lineWin = LineWinType.None;
    }

    /// <summary>
    /// Enable or disable the flashing material
    /// </summary>
    internal void LineFlashing(bool flashing)
    {
        if (flashing)
        {
            for (int i = 0; i < lineTiles.Length; i++)
            {
                mpB[i].SetFloat("_FadeEnable", 1);
            }

        }
        else
        {
            for (int i = 0; i < lineTiles.Length; i++)
            {
                mpB[i].SetFloat("_FadeEnable", 0);
            }
        }

        for (int i = 0; i < lineTiles.Length; i++)
        {
            rend[i].SetPropertyBlock(mpB[i]);
        }

    }

    /// <summary>
    /// Enable or disable line elemnts.
    /// </summary>
    internal void SetLineVisible(bool visible)
    {
        foreach (GameObject gO in lineTiles)
        {
            gO.SetActive(visible);
        }
    }

    /// <summary>
    /// Set Order for line spite rendrer.
    /// </summary>
    private void SetLineRenderOrder(int order)
    {
        foreach (GameObject gO in lineTiles)
        {
            SpriteRenderer sR = gO.GetComponent<SpriteRenderer>();
            sR.sortingOrder = order;
        }
    }

    /// <summary>
    /// Find  and fill winning symbols list 
    /// </summary>
    internal void FindWinnSymbols()
    {
        WinnSymbols = new List<SlotSymbol>();
        Dictionary<int, int> winnDict = new Dictionary<int, int>();
        for (int i = 0; i < rayCasters.Length; i++)
        {
            SlotSymbol s = rayCasters[i].GetSymbol();
            if (winnDict.ContainsKey(s.iconID)) winnDict[s.iconID] += 1;
            else { winnDict.Add((s.iconID), 1); }
        }

        int winnSymbID = -1; int winnSymbCount = 0;
        foreach (KeyValuePair<int, int> kp in winnDict)
        {
            if (kp.Value >= 3) { winnSymbID = kp.Key; winnSymbCount = kp.Value; } // get winning symbol id and count
        }

        lineWin = LineWinType.None;
        switch (winnSymbCount)
        {
            case 3:
                lineWin = (winnSymbID == 3) ? LineWinType.ThreeSpins : LineWinType.ThreeSymbol;
                break;
            case 4:
                lineWin = (winnSymbID == 3) ? LineWinType.FourSpins : LineWinType.FourSymbol;
                break;
            case 5:
                lineWin = (winnSymbID == 3) ? LineWinType.FiveSpins : LineWinType.FiveSymbol;
                break;
        }

        if (lineWin != LineWinType.None)
        {
            for (int i = 0; i < rayCasters.Length; i++)
            {
                SlotSymbol s = rayCasters[i].GetSymbol();
                if (s.iconID == winnSymbID) { WinnSymbols.Add(s); }
            }
        }
    }

    /// <summary>
    /// Reset old winnig data 
    /// </summary>
    internal void ResetLineWinning()
    {
        lineWin = LineWinType.None;
        WinnSymbols = null;
    }

    /// <summary>
    /// Instantiate particles for each winning symbol
    /// </summary>
    internal void ShowWinSymbolsParticles(bool activate)
    {
        if (IsWinningLine)
        {
            //WinnSymbols.ForEach((wS) => { wS.ShowParticles(activate, SlotController.Instance.particlesStars); });
            WinnSymbols.ForEach((wS) => { wS.ShowParticles(activate, SlotController.Instance.particlesCoin); });
            //Debug.Log("win");
        }
    }

    /// <summary>
    /// Instantiate jump clone for each symbol
    /// </summary>
    internal void LineWonJumps(float delay, Action<int, int> callBack)
    {
        winTweenComplete = false;
        TweenSeq ts = new TweenSeq();
        int counter = 0;
            ts.Add((completeCallBack) =>
            {
                foreach (SlotSymbol s in WinnSymbols)
                {
                    if(counter < WinnSymbols.Count-1)
                    s.WonJump(() =>
                    {  }, SlotController.Instance.topTweenTarget, SlotController.Instance.letterTarget, 0.0f);
                    else {
                        s.WonJump(() =>
                        { completeCallBack(); }, SlotController.Instance.topTweenTarget, SlotController.Instance.letterTarget, 0.0f);
                    }
                    counter++;
                }
            });
        
        ts.Add((completeCallBack) => {
            winTweenComplete = true;
            if (callBack != null) callBack(WonSpins, WonCoins);
        });
        ts.Start();
    }

    private int GetWinnings()
    {
        int w = 0;
        foreach (SlotSymbol s in WinnSymbols)
        {
            w += SlotController.Instance.iconCost[s.iconID];
        }
        return w;
    }

}
