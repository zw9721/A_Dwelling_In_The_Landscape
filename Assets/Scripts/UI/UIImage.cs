using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIImage : MonoBehaviour, IPointerDownHandler//, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public BuildingComponentData currentData;

    public void OnPointerDown(PointerEventData eventData)
    {
        InteractManager.Instance.GetData(GetComponent<UIImage>().currentData);
    }
    #region UI拖拽
    // [SerializeField]
    // private Vector2 defaultAnchoredPosition; 
    // private RectTransform rectTransform;
    // //记录鼠标点击位置相对于UI锚点的偏移量
    // private Vector2 offset; 

    // void Awake()
    // {
    //     rectTransform = GetComponent<RectTransform>();
    //     defaultAnchoredPosition = rectTransform.anchoredPosition;
    // }

    // public void OnBeginDrag(PointerEventData eventData)
    // {
    //     // 计算鼠标点击位置与UI锚点的偏移
    //     if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
    //         rectTransform.parent as RectTransform,
    //         eventData.position,
    //         eventData.pressEventCamera,
    //         out Vector2 clickPos))
    //     {
    //         // 偏移量 = UI当前锚点位置 - 鼠标点击的本地坐标
    //         offset = rectTransform.anchoredPosition - clickPos;
    //     }
    // }

    // public void OnDrag(PointerEventData eventData)
    // {
    //     if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
    //         rectTransform.parent as RectTransform,
    //         eventData.position,
    //         eventData.pressEventCamera,
    //         out Vector2 localPos))
    //     {
    //         // 修正：用鼠标当前位置 + 偏移量，让UI跟随鼠标点击的位置
    //         rectTransform.anchoredPosition = localPos + offset;
    //     }
    // }

    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     rectTransform.anchoredPosition = defaultAnchoredPosition;
    // }
    #endregion
}