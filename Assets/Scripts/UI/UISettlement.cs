using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISettlement : MonoBehaviour
{
    public Text gradeText;
    public Text gameTimeText;
    public Text errorCountText;
    public void GetSettlementInfo(Grade grade)
    {
        gameTimeText.text = "游戏时间：" + (SettlementManager.Instance.TimeElapsed / 60f).ToString();
        errorCountText.text = "错误次数：" + SettlementManager.Instance.ErrorCount.ToString();
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

    public void EndGame()
    {
        Application.Quit();
    }
}
