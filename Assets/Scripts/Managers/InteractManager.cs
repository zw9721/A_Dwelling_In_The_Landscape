using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : Singleton<InteractManager>
{
    private GameObject currentGhost;
    private BuildingComponentData currentData;

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

    public void OnDrag(Vector2 mousePosition)
    {
        if(currentGhost == null)
        return;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f,
        LayerMask.GetMask("Floor")))
        {
            currentGhost.transform.position = hit.point;
            // 射线检测周围是否有 BuildSlot，做高亮提示
        }
    }

    public void OnRelease()
    {
        if(currentGhost == null)
        return;
        BuildSlot closestSlot = FindClosestSlot(currentGhost.transform.position, 2.0f);
        if(closestSlot != null)
        {
            bool success = BuildManager.Instance.ValidatePlacement(currentData, closestSlot);
        }
        Object.Destroy(currentGhost);
    }

    BuildSlot FindClosestSlot(Vector3 position, float r)
    {
        Collider[] colliders = new Collider[1];
        //Vector3 distance;
        if(Physics.OverlapSphereNonAlloc(position, r, colliders,1 << LayerMask.NameToLayer("Slot")) != 0)
        {
            // 距离排序逻辑
            // for(int i = 0; i < colliders.Length; i++)
            // {
            //     distance = position - colliders[i].gameObject.transform.position;
            // }
            return colliders[0].gameObject.GetComponent<BuildSlot>();
        }
        return null;
    }
}
