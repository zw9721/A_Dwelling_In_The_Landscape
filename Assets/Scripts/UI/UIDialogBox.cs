using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogBox : UIBase
{
    private Text text;
    public GameObject happyImage;
    public GameObject sadImage;
    public GameObject midImage;
    //public bool isClose = false;
    //public float delayTime = 0.5f;

    void Awake()
    {
        if(happyImage.activeSelf)
            happyImage.SetActive(false);
        if(sadImage.activeSelf)
            sadImage.SetActive(false);
        if(midImage.activeSelf)
            midImage.SetActive(false);
    }
    public void SetMessage(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            Close();
            return;
        }
        if (text == null)
        {
            text = GetComponentInChildren<Text>();
        }
        text.text = message;
        midImage.SetActive(true);
        //StartCoroutine(DelayClose());
    }

    public void SetMessage(string message, bool isHappy)
    {
        SetMessage(message);
        midImage.SetActive(false);
        happyImage.SetActive(isHappy);
        sadImage.SetActive(!isHappy);
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
