using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] Enemy[] enemies;
    [SerializeField] Transform container;

    [SerializeField] Camera mainCamera;   // Asigna la cámara principal (o usa Camera.main)
    public float rayDistance = 100f;

    public void Init()
    {
        enemies = container.GetComponentsInChildren<Enemy>();
    }
    public void CheckHit(Vector2 pos)
    {
        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(pos);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorldPos, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.transform.IsChildOf(container))
            {
                Debug.Log("Le diste a: " + hit.collider.name);
            }
        }
    }
}
