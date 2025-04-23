using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using YaguarLib.Events;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text field;
    [SerializeField] ScoreSignal scoreSignal;
    [SerializeField] Transform container;
    public int score = 0;

    private void Awake()
    {
        Events.AddScore += AddScore;
    }
    private void OnDestrot()
    {
        Events.AddScore -= AddScore;
    }

    private void AddScore(int v, Vector2 vector)
    {
        score += v;
        if(score<0) score = 0;
        SetScore();
        ScoreSignal s = Instantiate(scoreSignal, container);
        s.Init(v, vector);
    }

    public void Restart()
    {
        score = 0;
        SetScore();
    }
    void SetScore()
    {
        field.text = YaguarLib.Xtras.Utils.FormatNumbers(score);
    }
}
