using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class UIMainMenu : UIBase
{
    public void StartGame()
    {
        GameStateManager.Instance.SwitchState(GameState.BeginingCinematic);
        //GameStateManager.Instance.SwitchState(GameState.Phase1_Structure);
        //GameObject.Find("DragSystem").GetComponent<PlayableDirector>().Play();
        StartCoroutine(ToStartTutorial(3.5f));
        //Destroy(gameObject,3.6f);
    }

    IEnumerator ToStartTutorial(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameStateManager.Instance.SwitchState(GameState.StartTutorial);
        gameObject.SetActive(false);
    }
}
