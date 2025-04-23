using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameUI : MonoBehaviour
{

    ScoreUI scoreUI;
    TimerUI timerUI;

    public void Init()
    {
        scoreUI = GetComponent<ScoreUI>();
        timerUI = GetComponent<TimerUI>();
    }
    public void Restart()
    {
        scoreUI.Restart();
        timerUI.Restart();
    }
    public int GetScore()
    {
        return scoreUI.score;
    }
    public void OnUpdate()
    {
        timerUI.OnUpdate();
    }
}
