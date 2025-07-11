using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Linq;

public class SlotController : MonoBehaviour {

    [Space(8, order = 0)]
    [Header("Icon Sprites", order = 1)]
    public Sprite []  iconSprites;

    [Space(8, order = 0)]

    [Header("Icon Cost", order = 1)]
    public int  [] iconCost;

    [Space(8, order = 0)]
    [Header("Free spin icon number", order = 1)]
    public int freeSpin_id; // if use free spin set its serial number from iconSprites array, if not "-1";

    [Space(10, order = 0)]
    [Header("Prefabs", order = 1)]
    public GameObject tilePrefab;
    public GameObject tileClonePrefab;
    public GameObject coinPrefab;
    //public GameObject particlesStars;
    public GameObject particlesCoin;

    [Space(8, order = 0)]
    [Header("Slot groups", order = 1)]
    public SlotGroupBehavior[] slotGroupsBeh;

    [Space(8, order = 0)]
    [Header("Tween Targets transform", order = 1)]
    public Transform letterTarget;
    public Transform topTweenTarget;
   
    [Space(8, order = 0)]
    [Header("Rotation tweening type", order = 1)]
    public LeanTweenType rotType = LeanTweenType.easeInCubic;
    public float rotateTime = 5f;

    private int slotTilesCount = 30;
    public static SlotController Instance;
    private LinesController linesController;

    WaitForSeconds wfs1_0;
    WaitForSeconds wfs0_2;

    public Text demo;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        wfs1_0 = new WaitForSeconds(1.0f);
        wfs0_2 = new WaitForSeconds(0.2f);
    }

    void Start()
    {
        // create slots
       int slotsGrCount = slotGroupsBeh.Length;

        foreach (SlotGroupBehavior sGB in slotGroupsBeh)
        {
            sGB.CreateSlotCylinder(iconSprites, slotTilesCount, tilePrefab);
        }
        linesController = new LinesController(FindObjectsOfType<LineBehavior>());
        SlotMenuController.Instance.Refresh();
        SetInputActivity(true);
    }

    internal void ReStartSettings()
    {
        linesController.WonEffectsShow(false, false);
        linesController.ResetLineWinning();
        LineButtonBehavior[] lbbs = FindObjectsOfType<LineButtonBehavior>();
        for (int i = 0; i < lbbs.Length; i++)
        {
            if (lbbs[i].Pressed)
            {
                lbbs[i].PointerDown(null);
            }
        }
        foreach (SlotGroupBehavior sGB in slotGroupsBeh)
        {
            sGB.TilesGroup.localEulerAngles = Vector3.zero;
        }
    }
    internal void RunSlots()
    {
        linesController.WonEffectsShow(false, false);
        linesController.ResetLineWinning();
        if (!SlotPlayer.Instance.AnyLineSelected)
        {
            GuiController.Instance.wMC.ShowMessage("Please select a any line.", () => { });
            return;
        }
        if (!SlotPlayer.Instance.ApllyFreeSpin() && !SlotPlayer.Instance.ApplyBet())
        {
            GuiController.Instance.wMC.ShowMessage("You have no money.", () => { });
            return;
        }

        StartCoroutine(RunSlotsAsync());
    }

    private IEnumerator RunSlotsAsync()
    {
                //1 ---------------start preparation------------------------------
                SetInputActivity(false);
                yield return wfs1_0;
                linesController.HideAllLines();
                SoundMasterController.Instance.SoundPlayRotation(2f, true, null);

                //3 --------start rotating -------------------------------------------
                bool fullRotated = false;
                RotateSlots(() => { SoundMasterController.Instance.StopLoopClip(); fullRotated = true; });
                while (!fullRotated)   yield return wfs0_2;  // wait 

                //4 --------check result-------------------------------------
                linesController.FindWinnSymbols();
                if (linesController.HasAnyWinn())
                {
                    bool fullJumps = false;
                    linesController.WonEffectsShow(true, true);
                    linesController.WonSymbolJumpsShow(
                           (spins, coins) =>
                           {
                               SlotPlayer.Instance.AddSpins(spins);
                               SlotPlayer.Instance.AddCoins(coins);
                               SlotMenuController.Instance.Refresh();
                           },
                           ()=>{ fullJumps = true; }
                           );
                    while (!fullJumps)  yield return wfs0_2;  // wait for jumps
                }
                else
                {
                    SoundMasterController.Instance.SoundPlaySlotLoose(0, false, null);
                }
        SetInputActivity(true);
    }

    private void RotateSlots(Action rotCallBack)
    {
        for (int i = 0; i < slotGroupsBeh.Length; i++)
        {
            if (i < slotGroupsBeh.Length - 1)
                slotGroupsBeh[i].RotateCylinder(rotType, rotateTime, i, null);
            else slotGroupsBeh[i].RotateCylinder(rotType, rotateTime, i, rotCallBack); // set callback for last cylinder
        }
    }

    /// <summary>
    /// Set touch activity for game and gui elements of slot scene
    /// </summary>
    private void SetInputActivity(bool activity)
    {
        TouchManager.SetTouchActivity(activity);
        SlotMenuController.Instance.SetControlActivity(activity);
    }
}

// helper for winning symbols check
public class LinesController
{
    List<LineBehavior> lineBehL;

    public LinesController( LineBehavior [] lineBehaviors)
    {
        lineBehL = new List<LineBehavior>(lineBehaviors);
    }

    /// <summary>
    /// Return true if slot has any winning
    /// </summary>
    internal bool HasAnyWinn()
    {
        bool res = false;
        foreach (LineBehavior lB in lineBehL)
        {
            if (lB.IsWinningLine) { res = true; break; }
        }
        return res;
    }

    /// <summary>
    /// Find winning symbols 
    /// </summary>
    internal void FindWinnSymbols()
    {
        foreach (LineBehavior lB in lineBehL)
        {
            if (lB.IsSelected)
            {
                lB.FindWinnSymbols();
            }
        }
    }

    /// <summary>
    /// Show symbols particles and lines glowing
    /// </summary>
    internal void WonEffectsShow(bool flashingLines, bool showSymbolParticles)
    {
        HideAllLines();
            lineBehL.ForEach((lB) =>
            {
                if (lB.IsWinningLine)
                {
                    lB.SetLineVisible(flashingLines);
                    lB.LineFlashing(flashingLines);
                }
            });

        foreach (LineBehavior lB in lineBehL)
        {
            lB.ShowWinSymbolsParticles(showSymbolParticles);
        }
    }

    /// <summary>
    /// Show won symbols jumps
    /// </summary>
    internal void WonSymbolJumpsShow(Action<int, int> lineJumpCallBack, Action lastCallBack)
    {
        TweenSeq ts = new TweenSeq();
        int counter = 0;
        foreach (LineBehavior lB in lineBehL)
        {
            if (lB.IsWinningLine)

                ts.Add((completeCallBack) => {
                    lB.LineWonJumps(0, (spins, coins) => {
                        if (lineJumpCallBack != null) lineJumpCallBack(spins, coins); completeCallBack(); });
                    });
            counter++;
        }
        ts.Add((completeCallBack) => { if (lastCallBack != null) lastCallBack(); completeCallBack();});
        ts.Start();
    }

    /// <summary>
    /// Show selected lines with flashing or without
    /// </summary>
    internal void ShowSelectedLines (bool flashing)
    {
        lineBehL.ForEach((lB) => {
            if (lB.IsSelected)
            {
                lB.SetLineVisible(true);
            }
                lB.LineFlashing(flashing);
        });
    }

    /// <summary>
    /// Hide selected lines
    /// </summary>
    internal void HideAllLines()
    {
        lineBehL.ForEach((lB) => {
            lB.LineFlashing(false);
            lB.SetLineVisible(false);
        });
    }

    /// <summary>
    /// Reset winning line data
    /// </summary>
    internal void ResetLineWinning()
    {
        foreach(LineBehavior lb in lineBehL)
        {
            lb.ResetLineWinning();
        }
    }
}

// GetInterface method for gameobject
public static class GameObjectExtensions
{
    /// <summary>
    /// Returns all monobehaviours (casted to T)
    /// </summary>
    /// <typeparam name="T">interface type</typeparam>
    /// <param name="gObj"></param>
    /// <returns></returns>
    public static T[] GetInterfaces<T>(this GameObject gObj)
    {
        if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
        //  var mObjs = gObj.GetComponents<MonoBehaviour>();
        var mObjs = MonoBehaviour.FindObjectsOfType<MonoBehaviour>();
        return (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(T)) select (T)(object)a).ToArray();
    }

    /// <summary>
    /// Returns the first monobehaviour that is of the interface type (casted to T)
    /// </summary>
    /// <typeparam name="T">Interface type</typeparam>
    /// <param name="gObj"></param>
    /// <returns></returns>
    public static T GetInterface<T>(this GameObject gObj)
    {
        if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
        return gObj.GetInterfaces<T>().FirstOrDefault();
    }

    /// <summary>
    /// Returns the first instance of the monobehaviour that is of the interface type T (casted to T)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gObj"></param>
    /// <returns></returns>
    public static T GetInterfaceInChildren<T>(this GameObject gObj)
    {
        if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
        return gObj.GetInterfacesInChildren<T>().FirstOrDefault();
    }

    /// <summary>
    /// Gets all monobehaviours in children that implement the interface of type T (casted to T)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gObj"></param>
    /// <returns></returns>
    public static T[] GetInterfacesInChildren<T>(this GameObject gObj)
    {
        if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");

        var mObjs = gObj.GetComponentsInChildren<MonoBehaviour>();

        return (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(T)) select (T)(object)a).ToArray();
    }
}

// tween helper
public class TweenSeq
{

    List<Action<Action>> seqL;
    List<int> seqControlL;
    List<Action> callBackL;
    List<TweenSeq> tweenGroupL;

    Action fullComplete;
    Action complCallBack;
    bool isComplete;
    bool breakSeq = false;

    public bool IsComplete
    {
        get { return isComplete; }
        private set { isComplete = value; }
    }

    /// <summary>
    /// Run actions sequence 
    /// </summary>
    public void Start()
    {
        breakSeq = false;
        IsComplete = false;
        fullComplete = new Action(() => { IsComplete = true; });
        CreateCB();
        callBackL[callBackL.Count - 1]();
    }

    public TweenSeq()
    {
        IsComplete = false;
        seqL = new List<Action<Action>>();
        seqControlL = new List<int>();
        callBackL = new List<Action>();
    }

    /// <summary>
    /// Add new action to sequence (action with callback) 
    /// </summary>
    public void Add(Action<Action> tweenAction)
    {
        seqL.Add(tweenAction);
        seqControlL.Add(0);
    }

    private void SetCompleted(int i)
    {
        seqControlL[i] = 2;
    }

    private void CreateCB()
    {
        callBackL.Add(() => {
            if (!breakSeq)
                seqL[seqL.Count - 1](() => {
                    SetCompleted(seqL.Count - 1);
                    if (fullComplete != null) fullComplete();
                    if (complCallBack != null) complCallBack();

                });
        });
        for (int i = 1; i < seqL.Count; i++)
        {
            Action cb = callBackL[i - 1];
            int counter = seqL.Count - 1 - i;

            callBackL.Add(() => {
                if (!breakSeq)
                    seqL[counter](() => {
                        cb();
                        SetCompleted(counter);
                    });
            });
        }

    }

    /// <summary>
    /// Set last callback in sequence
    /// </summary>
    public void OnComplete(Action complCallBack)
    {
        this.complCallBack = complCallBack;
    }

    /// <summary>
    /// Break sequence
    /// </summary>
    public void Break()
    {
        breakSeq = true;
    }
}