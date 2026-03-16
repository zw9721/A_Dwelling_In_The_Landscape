using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI的信息
/// </summary>
public class UIInfo
{
    public string resourcePath;
    public bool isCache;
    public GameObject uiInstance;
}

public class UIManager : Singleton<UIManager>
{
    private Dictionary<Type,UIInfo> UIResources = new Dictionary<Type, UIInfo>();

    /// <summary>
    /// 通过UIManager的构造函数来添加UI到字典里
    /// </summary>
    public UIManager()
    {
        UIResources.Add(typeof(UIDialogBox), new UIInfo(){resourcePath = "UI/DialogBox", isCache = true});
        UIResources.Add(typeof(UISettlement), new UIInfo(){resourcePath = "UI/Settlement", isCache = false});
        UIResources.Add(typeof(UIMainMenu), new UIInfo(){resourcePath = "UI/MainMenu", isCache = false});
    }

    public T Show<T>()
    {
        Type type = typeof(T);
        if (UIResources.ContainsKey(type))
        {
            UIInfo info = UIResources[type];
            if(info.uiInstance != null)
            {
                info.uiInstance.SetActive(true);
                return info.uiInstance.GetComponent<T>();
            }
            else
            {
                UnityEngine.Object prefab = Resources.Load(info.resourcePath);
                if(prefab != null)
                {
                    info.uiInstance = UnityEngine.Object.Instantiate(prefab) as GameObject;
                    return info.uiInstance.GetComponent<T>();
                }
                return default(T);
            }
        }
        return default(T);
    }

    public void Close(Type type)
    {
        if (UIResources.ContainsKey(type))
        {
            UIInfo info = UIResources[type];
            if (info.isCache)
            {
                info.uiInstance.SetActive(false);
            }
            else
            {
                UnityEngine.Object.Destroy(info.uiInstance);
                info.uiInstance = null;
            }
        }
    }
}
