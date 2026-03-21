using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum GameState { MainMenu, BeginingCinematic, StartTutorial, Phase1_Structure,Phase2_Decoration, EndingCinematic, Settlement }

public class GameStateManager : Singleton<GameStateManager>
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
                //UIManager.Instance.Show<UIMainMenu>();
                break;
            case GameState.BeginingCinematic:
                UIManager.Instance.Show<UIBook>();
                AnimationManager.Instance.Play();
                break;
            case GameState.StartTutorial:
                UIManager.Instance.Show<UIStartTutorial>().GoNextDialog();
                break;
            case GameState.Phase1_Structure:
                AnimationManager.Instance.Play();
                BuildManager.Instance.ResetProgress();
                BuildManager.Instance.ActivateSlotsByType(ComponentType.Structure);// 仅激活结构件的吸附槽
                //UIManager.Instance.Show<UIBook>();
                SettlementManager.Instance.StartTimer();
                break;
            case GameState.Phase2_Decoration:
                BuildManager.Instance.ActivateSlotsByType(ComponentType.Decoration);
                break;
            case GameState.EndingCinematic:
                SettlementManager.Instance.StopTimer();
                UIManager.Instance.Close(typeof(UIBook));
                GameObject.Find("DragSystem").GetComponent<DragSystem>().endingCinematic.SetActive(true);
                break;
            case GameState.Settlement:
                UIManager.Instance.Show<UISettlement>().GetSettlementInfo(SettlementManager.Instance.CalculateFinalGrade());
                break;
        }
        Debug.Log("现在的阶段是" + CurrentState);
    }
}
