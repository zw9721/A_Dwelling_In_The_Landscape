using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum GameState { MainMenu, Phase1_Structure,Phase2_Decoration, EndingCinematic, Settlement }

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
                break;
            case GameState.Phase1_Structure:
                BuildManager.Instance.ResetProgress();
                BuildManager.Instance.ActivateSlotsByType(ComponentType.Structure);// 仅激活结构件的吸附槽
                break;
            case GameState.Phase2_Decoration:
                BuildManager.Instance.ActivateSlotsByType(ComponentType.Decoration);
                break;
            case GameState.EndingCinematic:
                GameObject.Find("DragSystem").GetComponent<DragSystem>().endingCinematic.SetActive(true);
                break;
            case GameState.Settlement:
                GameObject.Find("DragSystem").GetComponent<DragSystem>().settlement.SetActive(true);
                break;
        }
    }
}
