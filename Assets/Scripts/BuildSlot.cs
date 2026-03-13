using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSlot : MonoBehaviour
{
    public string requiredID;
    public Transform snapPoint;

    void Awake()
    {
        snapPoint = transform;
    }
}
