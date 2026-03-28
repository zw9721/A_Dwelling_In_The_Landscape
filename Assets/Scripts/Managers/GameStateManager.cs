using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum GameState { MainMenu = 1, BeginingCinematic, StartTutorial, Phase1_Structure,Phase2_Decoration, EndingCinematic, Settlement }

public class GameStateManager : SingletonMono<GameStateManager>
{
    public GameState CurrentState { get; private set; }
    public event Action switchState;

    public void SwitchState(GameState newState)
    {
        if(CurrentState == newState)
        return;
        CurrentState = newState;
        switchState?.Invoke();
        
        switch (newState)
        {
            case GameState.MainMenu:
                StartCoroutine(MainMenu());
                break;
            case GameState.BeginingCinematic:
                UIManager.Instance.Show<UIBook>();
                AnimationManager.Instance.Play();
                break;
            case GameState.StartTutorial:
                UIManager.Instance.Show<UIStartTutorial>().GoNextDialog();
                break;
            case GameState.Phase1_Structure:
                StartCoroutine(Phase1_Structure());
                break;
            case GameState.Phase2_Decoration:
                StartCoroutine(Phase2_Decoration());
                break;
            case GameState.EndingCinematic:
                StartCoroutine(EndingCinematic());
                break;
            case GameState.Settlement:
                UIManager.Instance.Show<UISettlement>().GetSettlementInfo(SettlementManager.Instance.CalculateFinalGrade());
                CurrentState = 0;
                BuildManager.Instance.ClearSlotsArray();
                BuildManager.Instance.currentStructureStep = 0;
                break;
        }
        Debug.Log("现在的阶段是" + CurrentState);
    }

    IEnumerator MainMenu()
    {
        BuildManager.Instance.ResetProgress();
        BuildManager.Instance.ActivateSlotsByType(ComponentType.Structure);// 仅激活结构件的吸附槽
        BuildManager.Instance.currentStructureType = ComponentSmallType.Pillar;
        BuildManager.Instance.ActivateSlotsByType(ComponentType.Structure, ComponentSmallType.Tile, false);
        yield return null;
    }

    IEnumerator Phase1_Structure()
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.Show<UIDialogBox>().SetMessage("第一阶段：修其骨（建筑结构修复）");
        AnimationManager.Instance.Play();
        yield return new WaitForSeconds(3f);
        SettlementManager.Instance.StartTimer();
        yield return null;
    }

    IEnumerator Phase2_Decoration()
    {
        yield return new WaitForSeconds(3f);
        BuildManager.Instance.ActivateSlotsByType(ComponentType.Decoration);
        UIManager.Instance.Show<UIDialogBox>().SetMessage("第二阶段：铸其魂（文化装饰修复）");
        AnimationManager.Instance.Play();
    }
    
    IEnumerator EndingCinematic()
    {
        SettlementManager.Instance.StopTimer();
        UIManager.Instance.Close(typeof(UIBook));
        yield return new WaitForSeconds(3f);
        UIManager.Instance.Show<UIDialogBox>().SetMessage("青瓦复位，灯火重明。\n一座老宅在你手中再度有了人间气。");
        AnimationManager.Instance.Play();
        yield return new WaitForSeconds(5f);
        UIManager.Instance.Close(typeof(UIDialogBox));
        yield return new WaitForSeconds(1f);
        BuildManager.Instance.ResetAllBuildingComponentDescription();
        SwitchState(GameState.Settlement);
        yield return null;
    }
}
