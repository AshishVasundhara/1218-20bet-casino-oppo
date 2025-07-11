using UnityEngine;

[ExecuteInEditMode]
public class BackgroundScaler : MonoBehaviour {
    [SerializeField]
    private float width;
    [SerializeField]
    private float height;
    [SerializeField]
    private float baseRatio = 0.75f;
    [SerializeField]
    private float currScrRatio;

    void Start()
    {
        ScaleBkg();
    }

    void Update()
    {
        if (width != Screen.width || height != Screen.height)
        {
            ScaleBkg();
        }
    }

    void ScaleBkg()
    {
        width = Screen.width; height = Screen.height;
        currScrRatio = height / width;
        float k = 1f;
        if (baseRatio > currScrRatio)
        {
            k = baseRatio / currScrRatio;
        }
        gameObject.transform.localScale = new Vector3(k, k, k);
    }

}
