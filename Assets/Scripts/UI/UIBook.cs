using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBook : UIBase
{
    public GameObject book;

    void Awake()
    {
        //book = transform.Find("BookBackground").gameObject;
        GameStateManager.Instance.switchState += ReplaceBook;
    }
    public void SwitchBookActive()
    {
        book.SetActive(book.activeSelf? false: true);
    }

    public void ReplaceBook()
    {
        if(GameStateManager.Instance.CurrentState == GameState.Phase2_Decoration)
        {
            book.SetActive(false);
            book = transform.Find("BookBackground 2").gameObject;
            GameStateManager.Instance.switchState -= ReplaceBook;
        }
    }
}
