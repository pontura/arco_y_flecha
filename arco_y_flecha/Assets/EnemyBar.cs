using UnityEngine;
using YaguarLib.UI;

public class EnemyBar : ProgressBar
{
    [SerializeField] GameObject all;
    Enemy e;
    float duration;
    bool isOn;
    float timer;

    public void InitProgress()
    {
        all.SetActive(true);
        timer = 0;
        isOn = true;
        this.duration = e.duration;
    }
    public void SetOff()
    {
        isOn = false;
        all.SetActive(false);
    }
    public void OnUpdate()
    {
        if (e == null) return;

        if (e.state == Enemy.states.vulnerable)
        {
            if (!isOn)
                InitProgress();
        }
        else if (isOn)
            SetOff();

        if (isOn)
        {
            timer += Time.deltaTime;
            SetValue(timer/duration);
            if (duration < timer)
                Done();
        }
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(e.transform.position);
        transform.position = screenPoint;
    }
    void Done()
    {
        SetOff();
    }
    public void InitEnemy(Enemy e)
    {
        SetOff();
        this.e = e;
    }
}
