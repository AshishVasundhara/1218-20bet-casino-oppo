using System;
using UnityEngine;

public class LineButtonBehavior : MonoBehaviour, ICustomMessageTarget
{
    public Sprite normalSprite;
    public Sprite pressedSprite;
    public int lineNumber;

    private bool pressed = false;
    private LineButtonBehavior myClone; // clone button on the other side

    public bool Pressed
    {
        get { return pressed; }
    }
    public Action<int> PressButtonDelegate;
    public Action<int> UnPressButtonDelegate;

    void Start()
    {
        //1) find clone button 
        LineButtonBehavior[] lbbs = FindObjectsOfType<LineButtonBehavior>();
        for (int i = 0; i < lbbs.Length; i++)
        {
            if (lbbs[i].lineNumber == lineNumber && lbbs[i]!=this)
            {
                myClone = lbbs[i];
                break;
            }
        }
        //2) handle clone button
        PressButtonDelegate += (num) => { myClone.pressed = pressed; myClone.GetComponent<SpriteRenderer>().sprite = (pressed) ? pressedSprite : normalSprite; };
        UnPressButtonDelegate += (num) => { myClone.pressed = pressed; myClone.GetComponent<SpriteRenderer>().sprite = (pressed) ? pressedSprite : normalSprite; };

    }

    #region touch callbacks
    public void PointerUp(TouchPadEventArgs tpea)
    {

    }
    public void PointerDown(TouchPadEventArgs tpea)
    {
        pressed = !pressed;
        GetComponent<SpriteRenderer>().sprite = (pressed) ? pressedSprite : normalSprite;
        SoundMasterController.Instance.SoundPlayCheck(0, null);
        if (pressed)
        {
            if (PressButtonDelegate != null) PressButtonDelegate(lineNumber);
        }
        else
        {
            if (UnPressButtonDelegate != null) UnPressButtonDelegate(lineNumber);
        }
        
        // handle other lines
        if (tpea != null)
        {
            foreach (LineButtonBehavior lbb in FindObjectsOfType<LineButtonBehavior>())
            {
                if ( Pressed && lbb.lineNumber < lineNumber && lbb.Pressed == false )
                {
                    lbb.PointerDown(null);
                }
                if (Pressed && lbb.lineNumber > lineNumber && lbb.Pressed == true )
                {
                    lbb.PointerDown(null);
                }
                if (!Pressed && lbb.lineNumber > lineNumber && lbb.Pressed == true )
                {
                    lbb.PointerDown(null);
                }
  
            }
        }
    }

    public void DragBegin(TouchPadEventArgs tpea) { }
    public void DragEnter(TouchPadEventArgs tpea) { }
    public void DragExit(TouchPadEventArgs tpea) { }
    public void DragDrop(TouchPadEventArgs tpea) { }
    public void Drag(TouchPadEventArgs tpea) { }
    public GameObject GetDataIcon()
    {
        return null;
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    #endregion touch callbacks
}
