using UnityEngine;
using System.Collections;

public class DebugManager : MonoBehaviour {

    private static DebugManager instance;

    public bool forceMobile = false;
    
    void Start () {
        instance = this;
	}
	
	public static DebugManager getInstance()
    {
        return instance;
    }
}
