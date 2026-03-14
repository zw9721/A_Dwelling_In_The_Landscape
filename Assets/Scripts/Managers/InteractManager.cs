using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : Singleton<InteractManager>
{
    private GameObject currentGhost;
    private BuildingComponentData currentData;

    private Renderer targetRenderer; // 缓存命中物体的渲染器
    private Color originalColor;     // 缓存原始颜色

    public void GetData(BuildingComponentData currentData)
    {
        this.currentData = currentData;

        if (currentData.ghostPrefab != null)
        {
            // 实例化到世界原点，无旋转
            currentGhost = Object.Instantiate(currentData.ghostPrefab, Vector3.zero, Quaternion.identity);
            Debug.Log("Ghost预制体已实例化：" + currentGhost.name);
        }
    }

    public void OnDrag()
    {
        if(currentGhost == null)
        return;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100f,LayerMask.GetMask("Floor")))
        {
            currentGhost.transform.position = hit.point;
        }

        // 射线检测周围是否有 BuildSlot，做高亮提示
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, LayerMask.GetMask("Slot")))
        {
            SetHighlight(hit);
        }
        else
        {
            CancelHighlight();
        }
    }

    public void OnRelease()
    {
        if(currentGhost == null)
        return;
        BuildSlot closestSlot = FindClosestSlot(/*currentGhost.transform.position, 2.0f*/);
        if(closestSlot != null)
        {
            bool success = BuildManager.Instance.ValidatePlacement(currentData, closestSlot);
        }
        Object.Destroy(currentGhost);
        CancelHighlight();
    }

    BuildSlot FindClosestSlot(/*Vector3 position, float r*/)
    {
        // 现采用射线检测来检测槽位
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit info, 1000f, 1 << LayerMask.NameToLayer("Slot")))
        {
            Debug.Log("检测到槽位：" + info.collider.name);
            return info.transform.GetComponent<BuildSlot>();
        }
        #region 范围检测
        // Collider[] colliders = new Collider[1];
        // //Vector3 distance;
        // if(Physics.OverlapSphereNonAlloc(position, r, colliders,1 << LayerMask.NameToLayer("Slot")) != 0)
        // {
        //     // 距离排序逻辑
        //     // 计算两个点之间的距离 Vector3.Distance();
        //     return colliders[0].gameObject.GetComponent<BuildSlot>();
        // }
        #endregion
        return null;
    }

    void SetHighlight(RaycastHit hit)
    {
        if(targetRenderer == null)
        {
            if(hit.transform.GetComponent<BuildSlot>().isOccupied)
            return;
            targetRenderer = hit.transform.GetComponent<Renderer>();
            originalColor = targetRenderer.material.color;
            targetRenderer.material.color = Color.yellow;
        }
    }

    public void CancelHighlight()
    {
        if(targetRenderer != null)
        {
            targetRenderer.material.color = originalColor;
            targetRenderer = null;
        }
    }
}
