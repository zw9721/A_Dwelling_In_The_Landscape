using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AnimationManager : SingletonMono<AnimationManager>
{
    private PlayableDirector playableDirector;
    private Dictionary<GameState,string> AnimResource = new Dictionary<GameState,string>();
    // 存储「Timeline轨道名称」与「目标Animator对象」的映射（可根据需求配置）
    private Dictionary<string, Animator> animatorBindings = new Dictionary<string, Animator>();
    void Awake()
    {
        playableDirector = gameObject.AddComponent<PlayableDirector>();
        AnimResource.Add(GameState.BeginingCinematic,"PlayableAsset/BeginingCinematic");
        AnimResource.Add(GameState.Phase1_Structure,"PlayableAsset/Phase1");
        AnimResource.Add(GameState.Phase2_Decoration,"PlayableAsset/Phase2");
        AnimResource.Add(GameState.EndingCinematic,"PlayableAsset/EndingCinematic");
    }

    private void RefreshBindings()
    {
        animatorBindings.Clear(); // 清除旧的、可能已经丢失的引用

        GameObject mainMenu = GameObject.Find("MainMenu");
        if (mainMenu != null)
        {
            animatorBindings.Add("UIMoveTrack", mainMenu.GetComponent<Animator>());
        }

        GameObject mainCamera = GameObject.Find("Main Camera");
        if (mainCamera != null)
        {
            animatorBindings.Add("CameraMoveTrack", mainCamera.GetComponent<Animator>());
        }
    }

    public void Play()
    {
        GameState currentState = GameStateManager.Instance.CurrentState;
        if (AnimResource.ContainsKey(currentState))
        {
            // 每次播放前重新获取当前场景最新的对象引用
            RefreshBindings();
            PlayableAsset timeLine = Resources.Load<PlayableAsset>(AnimResource[GameStateManager.Instance.CurrentState]);
            // 绑定轨道到目标Animator
            BindAnimatorToTimelineTracks(timeLine);
            playableDirector.Play(timeLine);
        }
    }

    void BindAnimatorToTimelineTracks(PlayableAsset timelineAsset)
    {
        // 获取Timeline的所有轨道
        foreach (var track in timelineAsset.outputs)
        {
            // 筛选出Animator轨道（可根据需要调整轨道类型）
            if (track.outputTargetType == typeof(Animator))
            {
                string trackName = track.streamName; // 轨道名称（编辑器内设置的名称）
                
                // 如果该轨道已注册绑定关系，则执行绑定
                if (animatorBindings.TryGetValue(trackName, out Animator targetAnimator))
                {
                    // 核心API：绑定轨道到目标对象的Animator
                    playableDirector.SetGenericBinding(track.sourceObject, targetAnimator.gameObject);
                    Debug.Log($"成功绑定轨道[{trackName}]到对象：{targetAnimator.gameObject.name}");
                }
                else
                {
                    Debug.LogWarning($"轨道[{trackName}]未找到对应的Animator绑定，请先注册！");
                }
            }
        }
    }
}
