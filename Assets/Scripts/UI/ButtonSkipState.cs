using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSkipState : MonoBehaviour
{
    public void Skip()
    {
        GameStateManager.Instance.SwitchState(GameStateManager.Instance.CurrentState + 1);
    }
}
