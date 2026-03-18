using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIBase
{
    public void StartGame()
    {
        GameStateManager.Instance.SwitchState(GameState.Phase1_Structure);
        Destroy(gameObject);
    }
}
