using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] ResourceControl[] allResouce;
    [SerializeField] float autoCollectMultipy = 0.1f;
    internal double GetAutoCollectGold()
    {
        double allValue = 0;
        for (int i = 0; i < allResouce.Length; i++)
        {
            if (allResouce[i].hasUnlock)
                allValue += allResouce[i].output;
        }
        return allValue * autoCollectMultipy;
    }

    internal double GetClickGold()
    {
        double allValue = 0;
        for (int i = 0; i < allResouce.Length; i++)
        {
            if (allResouce[i].hasUnlock)
                allValue += allResouce[i].output;
        }
        return allValue;
    }
}
