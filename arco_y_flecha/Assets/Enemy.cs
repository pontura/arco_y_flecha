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
    [SerializeField] MeshRenderer mr;
    [SerializeField] Material notAvailable_mat;
    [SerializeField] Material available_mat;
    bool vulnerable;
    public float duration;

    public void Init()
    {
        anim = GetComponent<Animator>();
        Hide();
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
        Events.AddScore(-100, transform.position); 
        Hide();
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
        if (vulnerable)
            mr.material = available_mat;
        else
            mr.material = notAvailable_mat;
    }
    void Respawn()
    {
        Hide();
    }
}
