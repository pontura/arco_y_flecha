using UnityEngine;

public class Enemy : MonoBehaviour
{
    public states state;
    public enum states
    {
        hidden,
        vulnerable        
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
        this.duration = duration;
        state = states.vulnerable;
        SetVulnerable(true);
        anim.SetBool("show", true);
        Invoke("Hide", duration);
    }
    void Hide()
    {
        state = states.hidden;
        SetVulnerable(false);
        anim.SetBool("show", false);
    }
    public void Die()
    {
        SetVulnerable(false);
    }
    public void SetVulnerable(bool vulnerable)
    {
        this.vulnerable = vulnerable;
        if (vulnerable)
            mr.material = available_mat;
        else
            mr.material = notAvailable_mat;
    }
}
