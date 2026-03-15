using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BuildManager : Singleton<BuildManager>
{
    public int currentStructureStep = 0;
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
            if (slot.orderIndex == currentStructureStep)
            {
                ExecuteSuccess(data, slot);
                currentStructureStep++;
                Debug.LogFormat("当前游戏阶段：{0}，当前完成的步骤：{1}", state, currentStructureStep);
                CheckPhaseProgress();
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
            return true;
        }
        
        return false;
    }

    #region 吸附成功的校验逻辑
    void ExecuteSuccess(BuildingComponentData data,BuildSlot slot)
    {
        Object.Instantiate(data.prefab,slot.snapPoint.position,slot.snapPoint.rotation);
        slot.isOccupied = true;
    }

    void CheckPhaseProgress()// 检查是否达到10件进入下一阶段
    {
        switch (state)
        {
            case GameState.Phase1_Structure:
                if(currentStructureStep >= 10)
                {
                    GameStateManager.Instance.SwitchState(GameState.Phase2_Decoration);
                    Debug.Log("切换游戏阶段至：" + GameStateManager.Instance.CurrentState);
                    currentStructureStep = 0;
                }
            break;
            case GameState.Phase2_Decoration:
            if(currentStructureStep >= 8)
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
        if(structureSlots == null || structureSlots[0] == null)//不加后面这个条件，重新开始游戏会报错
        structureSlots = GameObject.FindGameObjectsWithTag("Structure");
        if(decorationSlots == null || decorationSlots[0] == null)
        decorationSlots = GameObject.FindGameObjectsWithTag("Decoration");
    }

    public void ActivateSlotsByType(ComponentType type)
    {
        Collider tempCollider;
        Thread.Sleep(100);
        for(int i = 0; i < structureSlots.Length; i++)
        {
            tempCollider = structureSlots[i].GetComponent<Collider>();
            if(tempCollider.enabled == (type == ComponentType.Structure ? false : true))
            tempCollider.enabled = type == ComponentType.Structure ? true : false;
        }
        for(int i = 0; i < decorationSlots.Length; i++)
        {
            tempCollider = decorationSlots[i].GetComponent<Collider>();
            if(tempCollider.enabled == (type == ComponentType.Decoration ? false : true))
            tempCollider.enabled = type == ComponentType.Decoration ? true : false;
        }
    }
    #endregion
}
