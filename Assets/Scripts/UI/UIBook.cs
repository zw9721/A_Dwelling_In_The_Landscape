using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBook : UIBase
{
    public GameObject book;

    public void SwitchBookActive()
    {
        book.SetActive(book.activeSelf? false: true);
    }
}
