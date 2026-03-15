using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogBox : MonoBehaviour
{
    private Text text;

    public void SetMessage(string message)
    {
        if(text == null)
        {
            text = GetComponentInChildren<Text>();
        }
        text.text = message;
    }

    public void Close()
    {
        UIManager.Instance.Close(GetType());
    }
}
