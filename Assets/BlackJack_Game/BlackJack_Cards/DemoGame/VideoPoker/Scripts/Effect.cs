using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum kindEffect
{
    none,
    alpha,
    rotation,

}
public class Effect : MonoBehaviour {

    public float angle = 5;
    public kindEffect kindEffect = kindEffect.none;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(Vector3.forward, angle);
	}
}
