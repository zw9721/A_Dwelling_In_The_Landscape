using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : Singleton<BuildManager>
{
    public bool ValidatePlacement(BuildingComponentData data,BuildSlot slot)
    {
        if(data.componentID == slot.requiredID)
        {
            ExecuteSuccess(data, slot);
        }
        return false;
    }

    void ExecuteSuccess(BuildingComponentData data,BuildSlot slot)
    {
        Object.Instantiate(data.prefab,slot.snapPoint.position,slot.snapPoint.rotation);
        slot.isOccupied = true;
    }
}
