using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartTutorial : UIDialogBox
{
    private int currentDialog = 0;
    private List<string> messages = new List<string>();
    //public bool isGoNext = true;
    void Awake()
    {
        //GetComponent<Game>().GoNext += GoNextDialog;
        messages.Add("新手教程1(点击鼠标左键跳过当前提示框)");
        messages.Add("新手教程2");
        messages.Add("点击右下角的古籍打开建造菜单，即可从中拖出构件修复建筑");
        messages.Add("");
    }

    public void GoNextDialog()
    {
        //CheckCondition();
        // if (!isGoNext)
        //     return;
        /*UIManager.Instance.Show<UIStartTutorial>().*/SetMessage(messages[currentDialog]);
        ++currentDialog;
        CurrentStepOperation();
        if(currentDialog >= messages.Count)
        {
            //GetComponent<Game>().GoNext -= GoNextDialog;
            GameStateManager.Instance.SwitchState(GameState.Phase1_Structure);
            Close();
        }
    }

    /// <summary>
    /// 检查是否完成当前步骤所要求的条件
    /// </summary>
    // void CheckCondition()
    // {
    //     switch (currentDialog)
    //     {
    //         case 3:
    //             if(GameObject.Find("BookBackground") != null)
    //             {
    //                 isGoNext = true;
    //             }
    //             break;
    //     }
    // }

    /// <summary>
    /// 检查当前步骤需要展示的特殊操作，比如说让你打开古籍
    /// </summary>
    void CurrentStepOperation()
    {
        switch (currentDialog)
        {
            case 3:
                GameObject.Find("Book(Clone)").transform.Find("BookSwitch").gameObject.SetActive(true);
                //UIManager.Instance.Show<UIBook>();
                break;
        }
    }
}
