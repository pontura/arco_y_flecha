using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    ScoreUI scoreUI;

    public void Init()
    {
        scoreUI = GetComponent<ScoreUI>();
        scoreUI.Init();
    }
}
