using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISettlement : UIBase
{
    public Text gradeText;
    public Text gameTimeText;
    public Text errorCountText;
    private int min;
    private int sec;

    private void Awake()
    {
        gameObject.transform.SetParent(GameObject.Find("Canvas").transform);
        gameObject.transform.SetAsFirstSibling();
    }

    public void GetSettlementInfo(Grade grade)
    {
        min = (int)(SettlementManager.Instance.TimeElapsed / 60f);
        sec = (int)(SettlementManager.Instance.TimeElapsed % 60f);
        gameTimeText.text =string.Format("{0}分{1}秒", min, sec);
        errorCountText.text = SettlementManager.Instance.ErrorCount.ToString();
        switch (grade)
        {
            case Grade.Grandmaster:
            gradeText.text = "宗师级";
            break;
            case Grade.Artisan:
            gradeText.text = "匠人级";
            break;
            case Grade.Apprentice:
            gradeText.text = "学徒级";
            break;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
