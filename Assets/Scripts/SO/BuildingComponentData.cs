using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewComponent", menuName =
"Building/ComponentData")]
public class BuildingComponentData : ScriptableObject
{
 [Header("基础信息")]
 public string componentID; // 构件唯一标识 (如 "Column_01")
 public ComponentType type; // 构件类型 (Structure-结构,Decoration-装饰)
 public int orderIndex; // 拼装顺序 (仅 Phase 1 有效，0:石础, 1:柱...)[Header("视觉与资产")]
 public GameObject prefab; // 正式模型预制体
 public GameObject ghostPrefab; // 拖拽时的半透明/发光预览模型
 public Sprite uiIcon; // 书页UI插图

 [Header("听觉与文案")]
 public AudioClip snapSFX; // 吸附音效（木/石/瓷）
 [TextArea]
 public string loreDescription; // 古籍科普文字（如“柱——承重之本”）
}
public enum ComponentType { Structure, Decoration }
public enum ComponentSmallType{Pillar, Bridge, Triangle, Tile, Ridge, Decoration}
