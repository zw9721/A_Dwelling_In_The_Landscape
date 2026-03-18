using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIBase : MonoBehaviour
{
    private CanvasScaler rootCanvas;
    private CanvasScaler myCanvas;
    void Awake()
    {
        myCanvas = GetComponent<CanvasScaler>();
        rootCanvas = GameObject.Find("Canvas").GetComponent<CanvasScaler>();
        SetCanvasSize();//不这样设置会导致主菜单画面错位
    }

    void SetCanvasSize()
    {
        myCanvas.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        myCanvas.referenceResolution = rootCanvas.referenceResolution;
    }
}
