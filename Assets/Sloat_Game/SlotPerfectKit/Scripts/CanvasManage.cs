using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManage : MonoBehaviour
{
    private void Awake()
    {
        float ratio = (float)Screen.width / Screen.height;
        if(ratio<1.55)
        {
            gameObject.GetComponent<UnityEngine.UI.CanvasScaler>().matchWidthOrHeight = 0;
        }
        else{
            gameObject.GetComponent<UnityEngine.UI.CanvasScaler>().matchWidthOrHeight = 1;
            
        }
        //float diff = ratio - 1.3333f;
        //float diff1 = Mathf.Clamp(1 - diff, 0.5f, 1f);
      
    }

}
