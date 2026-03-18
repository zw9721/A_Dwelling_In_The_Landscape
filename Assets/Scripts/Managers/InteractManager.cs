using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : Singleton<InteractManager>
{
    private GameObject currentGhost;
    private BuildingComponentData currentData;

    private BuildSlot currentSlot;
    // private Renderer targetRenderer; // 缓存命中物体的渲染器
    // private Color originalColor;     // 缓存原始颜色

    public void GetData(BuildingComponentData currentData)
    {
        this.currentData = currentData;

        if (currentData.ghostPrefab != null)
        {
            // 实例化到世界原点，无旋转
            currentGhost = Object.Instantiate(currentData.ghostPrefab, Vector3.up * 100f, Quaternion.identity);
            //Debug.Log("Ghost预制体已实例化：" + currentGhost.name);
        }
    }

    #region 拖拽和松开拖拽逻辑
    public void OnDrag()
    {
        if(currentGhost == null)
        return;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200f,LayerMask.GetMask("Floor")))
        {
            currentGhost.transform.position = hit.point;
        }

        // 射线检测周围是否有 BuildSlot，做高亮提示
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, LayerMask.GetMask("Slot")))
        {
            if(currentSlot == null)
            {
                currentSlot = hit.transform.GetComponent<BuildSlot>();
                currentSlot.SetHighlight(true);
            }
            else if(currentSlot != hit.transform.GetComponent<BuildSlot>())//防止两个黏在一起，鼠标移动到另一个上面了，currentSlot还是原来的Slot
            {
                currentSlot.SetHighlight(false);
                currentSlot = hit.transform.GetComponent<BuildSlot>();
                currentSlot.SetHighlight(true);
            }
            //SetHighlight(hit);
        }
        else
        {
            if(currentSlot != null)
            {
                currentSlot.SetHighlight(false);
                currentSlot = null;
            }
            //CancelHighlight();
        }
    }

    public void OnRelease()
    {
        if(currentGhost == null)
        return;
        if(currentSlot != null)
        {
            bool success = BuildManager.Instance.ValidatePlacement(currentData, currentSlot);
            currentSlot.SetHighlight(false);
            currentSlot = null;
        }
        Object.Destroy(currentGhost);

        // BuildSlot closestSlot = FindClosestSlot(/*currentGhost.transform.position, 2.0f*/);
        // if(closestSlot != null)
        // {
        //     bool success = BuildManager.Instance.ValidatePlacement(currentData, closestSlot);
        // }
        // Object.Destroy(currentGhost);
        // CancelHighlight();
    }
    #endregion


    // BuildSlot FindClosestSlot(/*Vector3 position, float r*/)
    // {
    //     // 现采用射线检测来检测槽位
    //     if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit info, 1000f, 1 << LayerMask.NameToLayer("Slot")))
    //     {
    //         //Debug.Log("检测到槽位：" + info.collider.name);
    //         return info.transform.GetComponent<BuildSlot>();
    //     }
    //     #region 范围检测
    //     // Collider[] colliders = new Collider[1];
    //     // //Vector3 distance;
    //     // if(Physics.OverlapSphereNonAlloc(position, r, colliders,1 << LayerMask.NameToLayer("Slot")) != 0)
    //     // {
    //     //     // 距离排序逻辑
    //     //     // 计算两个点之间的距离 Vector3.Distance();
    //     //     return colliders[0].gameObject.GetComponent<BuildSlot>();
    //     // }
    //     #endregion
    //     return null;
    // }

    #region 槽位高亮逻辑
    // void SetHighlight(RaycastHit hit)
    // {
    //     if(targetRenderer == null)
    //     {
    //         if(hit.transform.GetComponent<BuildSlot>().isOccupied)
    //         return;
    //         targetRenderer = hit.transform.GetComponent<Renderer>();
    //         originalColor = targetRenderer.material.color;
    //         targetRenderer.material.color = Color.yellow;
    //     }
    // }

    // public void CancelHighlight()
    // {
    //     if(targetRenderer != null)
    //     {
    //         targetRenderer.material.color = originalColor;
    //         targetRenderer = null;
    //     }
    // }
    #endregion
}
