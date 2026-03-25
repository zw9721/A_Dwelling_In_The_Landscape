using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DragSystem : MonoBehaviour
{
    public GameObject endingCinematic;
    private PlayableDirector timeLine;
    //public event Action GoNext;

    void Start()
    {
        //测试GameStateManager里的SwitchState方法
        GameStateManager.Instance.SwitchState(GameState.MainMenu);
        //timeLine = GetComponent<PlayableDirector>();
    }
    // Update is called once per frame
    void Update()
    {
        Drag();

        if (Input.GetMouseButtonDown(0))
        {
            //CloseUI();
        }
    }

    void Drag()
    {
        if (Input.GetMouseButton(0))
        {
            InteractManager.Instance.OnDrag();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            InteractManager.Instance.OnRelease();
        }
    }

    #region 点击屏幕关闭提示UI
    // void CloseUI()
    // {
    //     GameObject ui = GameObject.Find("DialogBox(Clone)");
    //     if(ui != null && ui.activeSelf)
    //     {
    //         if(GameStateManager.Instance.CurrentState == GameState.BeginingCinematic)
    //         {
    //             GoNext?.Invoke();
    //             return;
    //         }
    //         ui.GetComponent<UIDialogBox>().Close();
    //     }
    // }
    #endregion
    
    #region 3D物体拖拽
    // RaycastHit hitInfo;
    // [SerializeField]
    // [Tooltip("正在拖拽的Item")]
    // private GameObject dragItem;
    // /// <summary>
    // /// 正在拖拽的Item的item脚本
    // /// </summary>
    // private Item item;
    // [SerializeField]
    // [Tooltip("拖拽所处的平面")]
    // private GameObject floor;
    // public const string itemLayer = "Item";//给需要拖拽的物体添加到Item的Layer层级
    // public const string floorLayer = "Floor";//给拖拽物体所移动的平面添加到Floor的Layer层级
    // private float offsetZ;
    
    // void Drag()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000, 1 << LayerMask.NameToLayer(itemLayer)))
    //         {
    //             dragItem = hitInfo.collider.gameObject;
    //             item = dragItem.GetComponent<Item>();
    //             item.isDrag = true;
    //             //print(dragItem.name);
    //         }
    //         if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000, 1 << LayerMask.NameToLayer(floorLayer)))
    //         {
    //             floor = hitInfo.collider.gameObject;
    //             offsetZ = dragItem.transform.position.z - floor.transform.position.z;
    //         }
    //     }

    //     if (Input.GetMouseButton(0))
    //     {
    //         if (dragItem == null)
    //         return;
    //         if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000, 1 << LayerMask.NameToLayer(floorLayer)))
    //         {
    //             //offsetZ = dragItem.transform.position.z - hitInfo.point.z;在这里计算z轴偏移量会导致Item闪烁,所以在Inspector面板手动添加偏移量
    //             dragItem.transform.position = hitInfo.point + Vector3.forward * offsetZ;
    //         }
    //     }

    //     if (Input.GetMouseButtonUp(0))
    //     {
    //         if(dragItem != null && item != null)
    //         {
    //             item.Adsorb();
    //             item.isDrag = false;
    //             dragItem = null;
    //         }

    //         if(floor != null)
    //         {
    //             floor = null;
    //         }
    //     }
    
        // 废案
        // r = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Debug.DrawLine(r.origin, r.direction);
        // if (Physics.Raycast(r, out hitInfo, 1000, 1<<LayerMask.NameToLayer(itemTag)))
        // {
        //     if (hitInfo.transform.CompareTag(itemTag))
        //     {
        //         print("命中");
        //         hitInfo.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, hitInfo.transform.position.z);
        //     }
        // }
    //}
    #endregion
}
