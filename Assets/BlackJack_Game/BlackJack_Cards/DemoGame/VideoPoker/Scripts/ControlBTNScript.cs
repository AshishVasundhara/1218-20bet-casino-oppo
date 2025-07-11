using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlBTNScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{


    public void OnPointerUp(PointerEventData eventData) {
        Application.OpenURL(transform.parent.GetChild(2).GetComponent<Text>().text);
    }

    public void OnPointerDown(PointerEventData eventData) {

    }
}
