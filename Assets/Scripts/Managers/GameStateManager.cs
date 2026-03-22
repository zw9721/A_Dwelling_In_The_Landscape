using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum GameState { MainMenu, BeginingCinematic, StartTutorial, Phase1_Structure,Phase2_Decoration, EndingCinematic, Settlement }

public class GameStateManager : SingletonMono<GameStateManager>
{
    public GameState CurrentState { get; private set; }

    public void SwitchState(GameState newState)
    {
        if(CurrentState == newState)
        return;
        CurrentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
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
                BuildManager.Instance.ActivateSlotsByType(ComponentType.Decoration);
                UIManager.Instance.Show<UIDialogBox>().SetMessage("第二阶段：铸其魂（文化装饰修复）");
                AnimationManager.Instance.Play();
                break;
            case GameState.EndingCinematic:
                StartCoroutine(EndingCinematic());
                break;
            case GameState.Settlement:
                UIManager.Instance.Show<UISettlement>().GetSettlementInfo(SettlementManager.Instance.CalculateFinalGrade());
                break;
        }
        Debug.Log("现在的阶段是" + CurrentState);
    }

    IEnumerator Phase1_Structure()
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.Show<UIDialogBox>().SetMessage("第一阶段：修其骨（建筑结构修复）");
        AnimationManager.Instance.Play();
        BuildManager.Instance.ResetProgress();
        BuildManager.Instance.ActivateSlotsByType(ComponentType.Structure);// 仅激活结构件的吸附槽
        SettlementManager.Instance.StartTimer();
        yield return null;
    }
    IEnumerator EndingCinematic()
    {
        SettlementManager.Instance.StopTimer();
        UIManager.Instance.Close(typeof(UIBook));
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.Show<UIDialogBox>().SetMessage("山水之间，心安是家。");
        AnimationManager.Instance.Play();
        yield return new WaitForSeconds(5f);
        UIManager.Instance.Close(typeof(UIDialogBox));
        yield return new WaitForSeconds(1f);
        SwitchState(GameState.Settlement);
        yield return null;
    }
}
