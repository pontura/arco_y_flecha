using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] MeshRenderer mr;
    [SerializeField] Material notAvailable_mat;
    [SerializeField] Material available_mat;
    bool vulnerable;

    public void Init()
    {
    }
    public bool IsVulnerable()
    {
        return vulnerable;
    }
    public void Show()
    {
        SetVulnerable(true);
    }
    public void Hide()
    {
        SetVulnerable(false);
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
