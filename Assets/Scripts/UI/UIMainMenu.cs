using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class UIMainMenu : UIBase
{
    public void StartGame()
    {
        //GameStateManager.Instance.SwitchState(GameState.BeginingCinematic);
        GameStateManager.Instance.SwitchState(GameState.Phase1_Structure);
        GameObject.Find("Game").GetComponent<PlayableDirector>().Play();
        Destroy(gameObject,1.5f);
    }
}
