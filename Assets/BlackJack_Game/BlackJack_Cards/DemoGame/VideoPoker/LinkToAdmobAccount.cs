using UnityEngine;
using EasyButtons;
using System.Collections;

public class LinkToAdmobAccount : MonoBehaviour
{
    [Button]
    public void GoToAdmob()
    {
        Application.OpenURL("https://admob.google.com/home/");
    }

}
