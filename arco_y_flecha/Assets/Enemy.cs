using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int score;
    public states state;
    public enum states
    {
        hidden,
        vulnerable,
        killed
    }
    [SerializeField] Animator anim;
    bool vulnerable;
    public float duration;
    public TYPES type;
    public enum TYPES
    {
        HIDDEN,
        RUNNER

    }
    public void Reset()
    {
        if (state == states.vulnerable) 
            Hide();
    }
    public void Init()
    {
        CancelInvoke();
        switch (type)
        {
            case TYPES.HIDDEN:
                score = GameManager.Instance.settings.scoreDefault;
                break;
            case TYPES.RUNNER:
                score = GameManager.Instance.settings.scoreRunner;
                break;
            default:
                break;
        }
        anim = GetComponent<Animator>();
        anim.Play("off");
        Invoke("Hide", Random.Range(0.01f, 3.1f));
        //Hide();
    }
    public bool IsVulnerable()
    {
        return vulnerable;
    }
    public virtual void Show(float duration)
    {
        CancelInvoke();
        this.duration = duration;
        state = states.vulnerable;
        SetVulnerable(true);
        anim.Play("on");
        if(duration >0)
        Invoke("Shot", duration);
    }
    public virtual void Hide()
    {
        CancelInvoke();
        state = states.hidden;
        SetVulnerable(false);
        anim.Play("invulnerable");
    }
    public void Shot()
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Events.AddScore(-15, screenPoint);

        anim.Play("off");
        state = states.hidden;
        SetVulnerable(false);
        Invoke("Hide", 0.5f);
    }
    public virtual void Kill()
    {
        print("KILL");
        CancelInvoke();
        anim.Play("killed");
        state = states.killed;
        SetVulnerable(false);
        Invoke("Respawn", 1);
    }
    public void SetVulnerable(bool vulnerable)
    {
        this.vulnerable = vulnerable;
    }
    void Respawn()
    {
        Hide();
    }
}
