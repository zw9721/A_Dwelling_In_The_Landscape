using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : UIBase
{
    public Text text;
    private float timeElapsed;
    private int min = 0;
    private int sec = 0;

    void Awake()
    {
        timeElapsed = SettlementManager.Instance.TimeElapsed;
    }

    void FixedUpdate()
    {
        min = (int)(Time.time - timeElapsed) / 60;
        sec = (int)(Time.time - timeElapsed) % 60;
        text.text = string.Format("用时：{0}分{1}秒", min, sec);
    }
}
