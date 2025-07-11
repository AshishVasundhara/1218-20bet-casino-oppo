using UnityEngine;
using System.Collections;
using System;

public static class ChipManager {
    internal static Chip selected = GameObject.Find("bluechip").GetComponent<Chip>();

    internal static int GetSelectedValue()
    {
        return selected.value;
    }
}