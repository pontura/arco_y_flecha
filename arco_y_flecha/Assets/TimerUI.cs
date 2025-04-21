using Spine;
using UnityEngine;
using YaguarLib.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text field;
    [SerializeField] ProgressBar progressBar;
    float totalTime;
    float timer = 0;
    bool isOn;

    public void Init()
    {
        isOn = true;
        this.totalTime = GameManager.Instance.settings.totalTime;
        timer = totalTime;
    }
    public void Update()
    {
        if (!isOn) return;
        timer -= Time.deltaTime;
        if (timer<=0)
        {
            isOn = false;
            Events.TimeOver();
            timer = 0;
        }
        SetField();
    }
    void SetField()
    {
        field.text = YaguarLib.Xtras.Utils.FormatTime(timer);
        progressBar.SetValue(timer/ totalTime);
    }
}
