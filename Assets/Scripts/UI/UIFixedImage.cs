using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFixedImage : MonoBehaviour
{
    RectTransform currentRectTransform;
    Vector2 originPosition;
    Image image;
    void Awake()
    {
        currentRectTransform = GetComponent<RectTransform>();
        originPosition = currentRectTransform.anchoredPosition;
        image = GetComponent<Image>();
    }
    
    public void SetFixedImage(RectTransform target, Sprite targerSprite)
    {
        transform.SetParent(target.parent);
        currentRectTransform.anchoredPosition = target.anchoredPosition;
        image.sprite = targerSprite;
        image.SetNativeSize();
        currentRectTransform.sizeDelta = target.sizeDelta;
        image.color = Color.gray;
    }

    public void ResetFixedImage()
    {
        transform.SetParent(null);
        currentRectTransform.anchoredPosition = originPosition;
    }
}
