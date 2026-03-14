using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSlot : MonoBehaviour
{
    public string requiredID;
    public bool isOccupied = false;
    public Transform snapPoint;


    void Awake()
    {
        snapPoint = transform;
    }
}
