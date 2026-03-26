using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualAudioModule : SingletonMono<VisualAudioModule>
{
    private float currentSaturation = -100f;
    private int decorationCount = 0;

    void Start()
    {
        BuildManager.Instance.OnBuildSuccess += HandleBuildSuccess;
    }

    private void HandleBuildSuccess(BuildingComponentData data)
    {
        print("播放音效");
        AudioSource.PlayClipAtPoint(data.snapSFX,Camera.main.transform.position);

        // if (GameStateManager.Instance.CurrentState == GameState.Phase2_Decoration)
        // {
        //     decorationCount++;
        //     currentSaturation += 12.5f;
        //     UpdatePostProcessing();
        //     if(decorationCount == 2)
        //         StartCoroutine(FadeAudioTrack("Volume_Zheng", 0f, 2f));

        // }
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
