using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    public GameObject panel;
    public Slider bgm;
    public Slider soundEffect;
    public AudioClip sound;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(GameObject.Find("Canvas").transform);
        gameObject.transform.SetAsLastSibling();
        bgm.value = 1;
        soundEffect.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        VisualAudioModule.Instance.audioSource.volume = bgm.value;
        VisualAudioModule.Instance.soundEffectVolum = soundEffect.value;
    }

    public void OpenPanel()
    {
        VisualAudioModule.Instance.Play2DAudioClip(sound);
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        VisualAudioModule.Instance.Play2DAudioClip(sound);
        panel.SetActive(false);
    }

    public void QuitGame()
    {
        VisualAudioModule.Instance.Play2DAudioClip(sound);
        print("退出游戏");
        Application.Quit();
    }
}
