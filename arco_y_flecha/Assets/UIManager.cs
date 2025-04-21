using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    ScoreUI scoreUI;
    TimerUI timerUI;

    public void Init()
    {
        scoreUI = GetComponent<ScoreUI>();
        timerUI = GetComponent<TimerUI>();  

        scoreUI.Init();
        timerUI.Init();
    }
}
