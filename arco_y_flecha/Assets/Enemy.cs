using UnityEngine;

public class Enemy : MonoBehaviour
{
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

    public void Init()
    {
        anim = GetComponent<Animator>();
        anim.Play("off");
        Invoke("Hide", Random.Range(0.01f, 3.1f));
        //Hide();
    }
    public bool IsVulnerable()
    {
        return vulnerable;
    }
    public void Show(float duration)
    {
        CancelInvoke();
        this.duration = duration;
        state = states.vulnerable;
        SetVulnerable(true);
        anim.Play("on");
        Invoke("Shot", duration);
    }
    void Hide()
    {
        CancelInvoke();
        state = states.hidden;
        SetVulnerable(false);
        anim.Play("invulnerable");
    }
    public void Shot()
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Events.AddScore(-100, screenPoint);

        anim.Play("off");
        state = states.hidden;
        SetVulnerable(false);
        Invoke("Hide", 0.5f);
    }
    public void Kill()
    {
        print("KILL");
        CancelInvoke();
        anim.Play("killed");
        state = states.killed;
        SetVulnerable(false);
        Invoke("Respawn", 3);
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
