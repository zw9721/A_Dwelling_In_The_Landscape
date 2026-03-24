using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSlot : MonoBehaviour
{
    public string requiredID;
    public int orderIndex; // 建造顺序要求
    public bool isOccupied = false;
    public Transform snapPoint;
    //private Color originalColor;     // 缓存原始颜色
    private Outline outline;

    void Awake()
    {
        //snapPoint = transform;
        //originalColor = GetComponent<Renderer>().material.color;
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void SetHighlight(bool active)
    {
        if (isOccupied)
        {
            outline.enabled = false;
            return;
        }
        if (active)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }
    // public void SetHighlight(bool active)
    // {
    //     if (isOccupied)
    //     {
    //         GetComponent<Renderer>().material.color = originalColor;
    //         return;
    //     }
    //     if (active)
    //     {
    //         if(originalColor == Color.yellow)
    //         return;
    //         GetComponent<Renderer>().material.color = Color.yellow;
    //     }
    //     else
    //     {
    //         GetComponent<Renderer>().material.color = originalColor;
    //     }
    // }
}
