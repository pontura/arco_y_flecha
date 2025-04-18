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
        else if (e.state == Enemy.states.hidden)
        {
            if (isOn)
                SetOff();
        }
        else if (e.state == Enemy.states.killed)
        {
            if (isOn)
                Killed();
        }

        if (isOn)
        {
            timer += Time.deltaTime;
            SetValue(timer/duration);
            if (duration < timer)
                Done();
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(e.transform.position);
            transform.position = screenPoint;
        }
    }
    void Killed()
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(e.transform.position);
        Events.AddScore((int)((duration*10)- (timer*10)), screenPoint);
        Events.AddParticle("explotion", e.transform.position);
        SetOff();
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
