using UnityEngine;
using System.Collections;

public class Chip : MonoBehaviour {
    
    Vector3 startScale;

    const float SELECTED_SCALE = 1.2f;

    public int value;

    // Use this for initialization
    void Start () {
        startScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
            gameObject.transform.localScale = this.startScale * ((ChipManager.selected == this) ? SELECTED_SCALE : 1);
	}

    void OnMouseDown()
    {
        ChipManager.selected = this;
    }
}
