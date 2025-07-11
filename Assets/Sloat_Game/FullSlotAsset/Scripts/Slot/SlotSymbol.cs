using UnityEngine;
using System;

public class SlotSymbol : MonoBehaviour {

    public int iconID;
    private GameObject particles;

    internal void SetIcon(Sprite iconSprite, int iconID)
    {
        this.iconID = iconID;
        GetComponent<SpriteRenderer>().sprite = iconSprite;
    }
    internal void ShowParticles(bool activity, GameObject particlesPrefab)
    {
        if (activity)
        {
            SetRendererOrder(4);
            if (particlesPrefab)
            {
                if (particles == null)
                {
                    particles = (GameObject)Instantiate(particlesPrefab, transform.position, transform.rotation);
                    particles.transform.parent = transform.parent;
                    particles.transform.localScale = transform.localScale;
                }
            }
        }
        else
        {
            SetRendererOrder(2);
            if (particles) { Destroy(particles);}
        }
    }

    internal void WonJump(Action callBack, Transform firstPos, Transform secPos, float delay)
    {
        TweenSeq tS = new TweenSeq();
        // 0 create clone
        GameObject tweenClone = CreateJumpClone(SlotController.Instance.tileClonePrefab);
        
        // 1 scale clone
        tS.Add((completeCallBack) => {
            LeanTween.value(tweenClone, transform.localScale.x, transform.localScale.x * 2f, 0.2f).setOnUpdate((float val) => {
                if (!tweenClone.activeSelf)
                {
                    tweenClone.SetActive(true);
                }
                tweenClone.transform.localScale = new Vector3(val, val, val);
            }).setOnComplete(() => { completeCallBack(); }).setDelay(delay);
         //   
        });

        // 2 jump to first position  
        tS.Add((completeCallBack) => {
            LeanTween.move(tweenClone, firstPos.position, 0.5f).setOnComplete(() => { completeCallBack(); }).setEase(LeanTweenType.easeOutBounce);
        });

        //3 jump to second position 
        tS.Add((completeCallBack) => {
            LeanTween.move(tweenClone, secPos.position, 0.5f).setEase(LeanTweenType.easeInCirc).setOnComplete(() => { completeCallBack(); });
            LeanTween.value(tweenClone, tweenClone.transform.localScale.x, 0, 0.25f).setOnUpdate((float val) => { tweenClone.transform.localScale = new Vector3(val, val, val); }).setDelay(0.26f).
            setOnComplete(() => {Destroy(tweenClone);  if (callBack != null) callBack(); });
        });

        tS.Start();
    }

    private GameObject CreateJumpClone(GameObject prefab)
    {
        GameObject twClone;
        twClone = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
        twClone.transform.parent = transform.parent;
        twClone.transform.localScale = transform.localScale;
        twClone.GetComponent<SlotSymbol>().SetIcon(GetComponent<SpriteRenderer>().sprite, iconID);
        twClone.GetComponent<SlotSymbol>().SetRendererOrder(7);
        return twClone;
    }
    /// <summary>
    /// Set Order for spite rendrer.
    /// </summary>
    private void SetRendererOrder(int order)
    {
        GetComponent<SpriteRenderer>().sortingOrder = order;
    }
}


