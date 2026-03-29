using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualAudioModule : SingletonMono<VisualAudioModule>
{
    private float currentSaturation = -100f;
    private int decorationCount = 0;
    public AudioSource audioSource;
    public float soundEffectVolum = 1;
    public AudioClip errorSound;

    void Start()
    {
        BuildManager.Instance.OnBuildSuccess += HandleBuildSuccess;
        InitBGM();
        errorSound = Resources.Load<AudioClip>("Sounds/error");
    }

    private void HandleBuildSuccess(BuildingComponentData data)
    {
        print("播放音效");
        //AudioSource.PlayClipAtPoint(data.snapSFX,Camera.main.transform.position);
        Play2DAudioClip(data.snapSFX);

        // if (GameStateManager.Instance.CurrentState == GameState.Phase2_Decoration)
        // {
        //     decorationCount++;
        //     currentSaturation += 12.5f;
        //     UpdatePostProcessing();
        //     if(decorationCount == 2)
        //         StartCoroutine(FadeAudioTrack("Volume_Zheng", 0f, 2f));

        // }
    }

    public void Play2DAudioClip(AudioClip clip)
    {
        if (clip == null) return; // 空检测，避免报错

        // 创建临时GameObject承载AudioSource
        GameObject tempAudioObj = new GameObject("Temp2DAudio");
        AudioSource audioSource = tempAudioObj.AddComponent<AudioSource>();

        // 设置为2D音效（空间混合设为0，完全2D）
        audioSource.spatialBlend = 0f;
        // 赋值音效片段
        audioSource.clip = clip;
        audioSource.volume = soundEffectVolum;
        // 播放音效
        audioSource.Play();

        // 播放完毕后自动销毁临时对象（避免内存泄漏）
        Destroy(tempAudioObj, clip.length);
    }

    public void PlayErrorAudioClip()
    {
        Play2DAudioClip(errorSound);
    }

    void InitBGM()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = Resources.Load<AudioClip>("Sounds/BGM");
        audioSource.Play();
    }

    // 视觉：插值提升饱和度
    void UpdatePostProcessing()
    {
        
    }

    IEnumerator FadeAudioTrack(string instrumentName, float enterVolume, float finalVolume)
    {
        yield return null;
    }

    public void WakeUp()
    {
        
    }
}
