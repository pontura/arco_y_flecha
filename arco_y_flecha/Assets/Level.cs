using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform container;

    public void Init()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
