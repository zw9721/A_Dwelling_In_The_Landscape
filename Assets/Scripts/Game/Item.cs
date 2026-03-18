using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    //这个Item最终要拖拽到的地方
    public GameObject targetPosition;
    /// <summary>
    /// Item是否在拖拽状态中，当Item不处于拖拽状态时，Item才能吸附到目标位置
    /// </summary>
    public bool isDrag = false;
    /// <summary>
    /// Item是否到达目标位置，当Item碰到目标位置的触发器，即认为Item满足吸附的条件，设置为true
    /// </summary>
    public bool isCollide = false;
    /// <summary>
    /// 与目标位置Z轴上的偏移
    /// </summary>
    public float offsetZ;

    void Awake()
    {
        offsetZ = this.transform.position.z - targetPosition.transform.position.z;
    }
    /// <summary>
    /// 检测Item吸附的方法
    /// </summary>
    public void Adsorb()
    {
        if (isDrag && isCollide)
        {
            transform.position = targetPosition.transform.position + Vector3.forward * offsetZ;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == targetPosition)
        {
            isCollide = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == targetPosition)
        {
            isCollide = false;
        }
    }
}
