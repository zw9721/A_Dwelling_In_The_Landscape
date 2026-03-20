using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogBox : UIBase
{
    private Text text;
    //public bool isClose = false;
    //public float delayTime = 0.5f;
    public void SetMessage(string message)
    {
        if(text == null)
        {
            text = GetComponentInChildren<Text>();
        }
        text.text = message;
        //StartCoroutine(DelayClose());
    }

    public void Close()
    {
        // if (!isClose)
        // {
        //     return;
        // }
        UIManager.Instance.Close(GetType());
        //isClose = false;
    }

    // IEnumerator DelayClose()
    // {
    //     yield return new WaitForSeconds(delayTime);
    //     isClose = true;
    // }
}
