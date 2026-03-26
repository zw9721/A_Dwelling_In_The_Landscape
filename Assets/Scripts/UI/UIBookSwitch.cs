using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBookSwitch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject star;
    public GameObject bookLight;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!star.activeSelf || !bookLight.activeSelf)
        {
            star.SetActive(true);
            bookLight.SetActive(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(star.activeSelf || bookLight.activeSelf)
        {
            star.SetActive(false);
            bookLight.SetActive(false);
        }
    }
}
