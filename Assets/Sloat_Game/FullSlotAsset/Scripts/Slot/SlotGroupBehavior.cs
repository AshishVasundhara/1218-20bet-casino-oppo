using UnityEngine;
using System;

public class SlotGroupBehavior : MonoBehaviour {

    [Space(10, order = 0)]
    [Header("Transforms", order = 1)]
    public Transform TilesGroup;

    private int tileCount;
    GameObject[] slotSymbols;

    /// <summary>
    /// Instantiate slot tiles 
    /// </summary>
    internal void CreateSlotCylinder(Sprite [] sprites, int tileCount, GameObject tilePrefab )
    {
        this.tileCount = tileCount;
        slotSymbols = new GameObject[tileCount];
        float distTileY = 3.48f;

        float radius = ((tileCount + 1) * distTileY) / (2 * Mathf.PI);
        float deltaAngleDeg = 360.0f /(float)tileCount;
        float deltaAngleRad = (2.0f*Mathf.PI) / (float)tileCount;
       
        TilesGroup.localPosition = new Vector3(TilesGroup.localPosition.x, TilesGroup.localPosition.y, radius);
        for (int i = 0; i < tileCount; i++)
        {
                float tileAngleRad = i * deltaAngleRad;
                float tileAngleDeg = i * deltaAngleDeg;

            slotSymbols[i] = (GameObject)Instantiate(tilePrefab, transform.position, Quaternion.identity);
            slotSymbols[i].transform.parent = TilesGroup;
            slotSymbols[i].transform.localPosition = new Vector3(0,radius*Mathf.Sin(tileAngleRad) , -radius * Mathf.Cos(tileAngleRad));
            slotSymbols[i].transform.localScale = new Vector3(1f,1f,1f);
            slotSymbols[i].transform.localEulerAngles = new Vector3(tileAngleDeg, 0,0);

        }
        int length = sprites.Length;
        for (int i = 0; i < slotSymbols.Length; i++)
        {
            int symNumber = i % length;
            slotSymbols[i].GetComponent<SlotSymbol>().SetIcon(SlotController.Instance.iconSprites[symNumber], symNumber);
        }
    }

    /// <summary>
    /// Async rotate cylinder
    /// </summary>
    internal void RotateCylinder(LeanTweenType rotType, float rotateTime, int number, Action rotCallBack)
    {
        // calc random angle
        float angleDivider = 360f / tileCount;
        float angleX = (tileCount * 2) + UnityEngine.Random.Range((number + 1) * 1000, (number + 2) * 1000) % (tileCount / 2);
        angleX *= angleDivider;

        // set start rotation 
        float currAngleX = TilesGroup.localRotation.eulerAngles.x;
        currAngleX = Mathf.RoundToInt(currAngleX / angleDivider) * angleDivider;
        TilesGroup.localRotation = Quaternion.Euler(currAngleX, TilesGroup.localRotation.eulerAngles.y, TilesGroup.localRotation.eulerAngles.z);
        Vector3 startAngles = TilesGroup.localRotation.eulerAngles;
        float oldVal = 0f;
        LeanTween.value(TilesGroup.gameObject, 0f, angleX, rotateTime)
              .setOnUpdate((float val) =>
              {
                  TilesGroup.Rotate(val - oldVal, 0, 0);
                  oldVal = val;
              })
              .setOnComplete(() =>
              {
                  TilesGroup.localRotation = Quaternion.Euler(startAngles.x + angleX, startAngles.y, startAngles.z);
                  if (rotCallBack != null) rotCallBack();
              }).setEase(rotType).setDelay(number * 0.1f);
    }
}
