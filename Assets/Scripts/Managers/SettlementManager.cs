using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Grade { Grandmaster, Artisan, Apprentice }
public class SettlementManager : Singleton<SettlementManager>
{
    private float timeElapsed = 0f;
    public float TimeElapsed{get=>timeElapsed;}
    private int errorCount = 0;
    public int ErrorCount{get=>errorCount;}
    public void AddError() { errorCount++; }
    
    public Grade CalculateFinalGrade()
    {
        float minutes = timeElapsed / 60f;

        // 宗师级：用时 ≤ 12 分钟，错误 ≤ 2，提示 ≤ 1
        if (minutes <= 12f && errorCount <= 2)
        return Grade.Grandmaster;

        // 匠人级：用时 ≤ 15 分钟，错误 ≤ 5，提示 ≤ 3
        else if (minutes <= 15f && errorCount <= 5)
        return Grade.Artisan;

        // 学徒级：其余情况
        else
        return Grade.Apprentice;
    }

    public void StartTimer()
    {
        timeElapsed = Time.time;//记录进入游戏场景之前的时间
        UIManager.Instance.Show<UITimer>();
    }

    public void StopTimer()
    {
        timeElapsed = Time.time - timeElapsed;//这时用总时间减去“进入游戏场景之前的时间”得到进入游戏场景的时间
        UIManager.Instance.Close(typeof(UITimer));
    }
}