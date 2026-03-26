using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BuildManager : Singleton<BuildManager>
{
    public int currentStructureStep = 0;
    public ComponentSmallType currentStructureType = ComponentSmallType.Pillar;
    public event Action<BuildingComponentData> OnBuildSuccess;// 成功事件
    private GameObject[] structureSlots;//用来存储场景中的Structure槽位
    private GameObject[] decorationSlots;//用来存储场景中的Decoration槽位
    private GameState state;

    /// <summary>
    /// 检测构件是否放置正确
    /// </summary>
    /// <param name="data"></param>
    /// <param name="slot"></param>
    /// <returns></returns>
    public bool ValidatePlacement(BuildingComponentData data,BuildSlot slot)
    {
        if(slot.isOccupied)
        return false;
        if(data.componentID != slot.requiredID)
        {
            SettlementManager.Instance.AddError();
            UIManager.Instance.Show<UIDialogBox>().SetMessage("构件与位点不匹配");
            return false;
        }
        
        state = GameStateManager.Instance.CurrentState;
        //游戏阶段1
        if (state == GameState.Phase1_Structure)
        {
            if (slot.orderType == currentStructureType)
            {
                ExecuteSuccess(data, slot);
                currentStructureStep++;
                CheckSwitchType();
                Debug.LogFormat("当前游戏阶段：{0}，当前完成的步骤：{1}", state, currentStructureStep);
                CheckPhaseProgress();
                OnBuildSuccess?.Invoke(data);
                slot.gameObject.SetActive(false);
                return true;
            }
            else
            {
                SettlementManager.Instance.AddError();
                UIManager.Instance.Show<UIDialogBox>().SetMessage("请依照工序，先完成承重构件");
            }
        }
        //游戏阶段2
        else if (state == GameState.Phase2_Decoration)
        {
            ExecuteSuccess(data, slot);
            currentStructureStep++;
            Debug.LogFormat("当前游戏阶段：{0}，当前完成的步骤：{1}", state, currentStructureStep);
            CheckPhaseProgress(); // 检查是否达到8件触发结局
            OnBuildSuccess?.Invoke(data);
            slot.gameObject.SetActive(false);
            return true;
        }
        
        return false;
    }

    #region 吸附成功的校验逻辑
    void ExecuteSuccess(BuildingComponentData data,BuildSlot slot)
    {
        UnityEngine.Object.Instantiate(data.prefab,slot.snapPoint.position,slot.snapPoint.rotation);
        slot.isOccupied = true;
    }

    void CheckPhaseProgress()// 检查是否达到10件进入下一阶段
    {
        switch (state)
        {
            case GameState.Phase1_Structure:
                if(currentStructureStep >= 16)
                {
                    GameStateManager.Instance.SwitchState(GameState.Phase2_Decoration);
                    Debug.Log("切换游戏阶段至：" + GameStateManager.Instance.CurrentState);
                    currentStructureStep = 0;
                }
            break;
            case GameState.Phase2_Decoration:
            if(currentStructureStep >= 10)
                {
                    GameStateManager.Instance.SwitchState(GameState.EndingCinematic);
                    Debug.Log("切换游戏阶段至：" + GameStateManager.Instance.CurrentState);
                    currentStructureStep = 0;
                }
            break;
        }
        
    }
    #endregion

    #region 游戏阶段切换的初始化逻辑
    /// <summary>
    /// 仅激活构件的吸附槽
    /// </summary>
    public void ResetProgress()
    {
        if(structureSlots == null)
        structureSlots = GameObject.FindGameObjectsWithTag("Structure");
        if(decorationSlots == null)
        decorationSlots = GameObject.FindGameObjectsWithTag("Decoration");
    }

    public void ClearSlotsArray()
    {
        structureSlots = null;
        decorationSlots = null;
    }

    public void ActivateSlotsByType(ComponentType type)
    {
        Collider tempCollider;
        Renderer tempRenderer;
        Thread.Sleep(100);
        for(int i = 0; i < structureSlots.Length; i++)
        {
            tempCollider = structureSlots[i].GetComponent<Collider>();
            if(tempCollider.enabled == (type == ComponentType.Structure ? false : true))
            tempCollider.enabled = type == ComponentType.Structure ? true : false;
            tempRenderer = structureSlots[i].GetComponent<Renderer>();
            if(tempRenderer.enabled == (type == ComponentType.Structure ? false : true))
            tempRenderer.enabled = type == ComponentType.Structure ? true : false;
        }
        for(int i = 0; i < decorationSlots.Length; i++)
        {
            tempCollider = decorationSlots[i].GetComponent<Collider>();
            if(tempCollider.enabled == (type == ComponentType.Decoration ? false : true))
            tempCollider.enabled = type == ComponentType.Decoration ? true : false;
            tempRenderer = decorationSlots[i].GetComponent<Renderer>();
            if(tempRenderer.enabled == (type == ComponentType.Decoration ? false : true))
            tempRenderer.enabled = type == ComponentType.Decoration ? true : false;
        }
    }
    #endregion

    public void ActivateSlotsByType(ComponentType type, ComponentSmallType smallType, bool isActive)
    {
        if(type == ComponentType.Structure)
        {
            for(int i = 0; i < structureSlots.Length; i++)
            {
                if(structureSlots[i].GetComponent<BuildSlot>().orderType == smallType)
                {
                    structureSlots[i].GetComponent<Renderer>().enabled = isActive;
                    structureSlots[i].GetComponent<Collider>().enabled = isActive;
                }
            }
        }

        else if(type == ComponentType.Decoration)
        {
            for(int i = 0; i < decorationSlots.Length; i++)
            {
                if(decorationSlots[i].GetComponent<BuildSlot>().orderType == smallType)
                {
                    decorationSlots[i].GetComponent<Renderer>().enabled = isActive;
                    decorationSlots[i].GetComponent<Collider>().enabled = isActive;
                }
            }
        }
    }

    public void CheckSwitchType()
    {
        if(state == GameState.Phase1_Structure)
        {
            switch (currentStructureType)
            {
                case ComponentSmallType.Pillar:
                    if(currentStructureStep >= 2)
                    {
                        currentStructureType = ComponentSmallType.Bridge;
                    }
                    break;
                case ComponentSmallType.Bridge:
                    if(currentStructureStep >= 4)
                    {
                        currentStructureType = ComponentSmallType.Triangle;
                    }
                    break;
                case ComponentSmallType.Triangle:
                    if(currentStructureStep >= 7)
                    {
                        currentStructureType = ComponentSmallType.Tile;
                        ActivateSlotsByType(ComponentType.Structure, ComponentSmallType.Tile, true);
                    }
                    break;
                case ComponentSmallType.Tile:
                    if(currentStructureStep >= 15)
                    {
                        currentStructureType = ComponentSmallType.Ridge;
                    }
                    break;
            }
        }
    }

    // 旧的校验逻辑
    // public bool ValidatePlacement(BuildingComponentData data,BuildSlot slot)
    // {
    //     if(slot.isOccupied)
    //     return false;
    //     if(data.componentID != slot.requiredID)
    //     {
    //         SettlementManager.Instance.AddError();
    //         UIManager.Instance.Show<UIDialogBox>().SetMessage("构件与位点不匹配");
    //         return false;
    //     }
        
    //     state = GameStateManager.Instance.CurrentState;
    //     //游戏阶段1
    //     if (state == GameState.Phase1_Structure)
    //     {
    //         if (slot.orderIndex == currentStructureStep)
    //         {
    //             ExecuteSuccess(data, slot);
    //             currentStructureStep++;
    //             Debug.LogFormat("当前游戏阶段：{0}，当前完成的步骤：{1}", state, currentStructureStep);
    //             CheckPhaseProgress();
    //             OnBuildSuccess?.Invoke(data);
    //             return true;
    //         }
    //         else
    //         {
    //             SettlementManager.Instance.AddError();
    //             UIManager.Instance.Show<UIDialogBox>().SetMessage("请依照工序，先完成承重构件");
    //         }
    //     }
    //     //游戏阶段2
    //     else if (state == GameState.Phase2_Decoration)
    //     {
    //         ExecuteSuccess(data, slot);
    //         currentStructureStep++;
    //         Debug.LogFormat("当前游戏阶段：{0}，当前完成的步骤：{1}", state, currentStructureStep);
    //         CheckPhaseProgress(); // 检查是否达到8件触发结局
    //         OnBuildSuccess?.Invoke(data);
    //         return true;
    //     }
        
    //     return false;
    // }
}
