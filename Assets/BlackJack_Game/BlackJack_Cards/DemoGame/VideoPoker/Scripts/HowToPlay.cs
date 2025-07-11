using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour {

    public Sprite on, off;
    public Transform[] onoffPos;
    public Transform next, previous;
    public Transform[] groups;

    int countNext =0;

    public void Next() {
        if (countNext < groups.Length-1) {
            onoffPos[countNext].GetComponent<Image>().sprite = off;
            groups[countNext].gameObject.SetActive(false);
            countNext++;
            groups[countNext].gameObject.SetActive(true);
            onoffPos[countNext].GetComponent<Image>().sprite = on;
            if (countNext == groups.Length - 1) {
                next.gameObject.SetActive(false);
            }
            if (countNext > 0) {
                previous.gameObject.SetActive(true);
            }
        }
    }

    public void Previous() {
        print(countNext);
        if (countNext > 0) {
            groups[countNext].gameObject.SetActive(false);
            onoffPos[countNext].GetComponent<Image>().sprite = off;
            countNext--;
            groups[countNext].gameObject.SetActive(true);
            onoffPos[countNext].GetComponent<Image>().sprite = on;
            if (countNext == 0) {
                previous.gameObject.SetActive(false);
                next.gameObject.SetActive(true);
            }
         
        }
    }
}
