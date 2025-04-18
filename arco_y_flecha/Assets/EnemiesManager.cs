using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] Enemy[] enemies;
    [SerializeField] Transform container;
    [SerializeField] EnemiesBars enemiesBars;

    [SerializeField] Camera mainCamera;   // Asigna la cámara principal (o usa Camera.main)
    public float rayDistance = 100f;
    float timer;

    public void Init()
    {
        enemies = container.GetComponentsInChildren<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.Init();
            enemiesBars.Add(enemy);
        }
    }
    public void OnUpdate()
    {
        timer += Time.deltaTime;
        if(timer>2)
        {
            SetEnemyOn();
            timer = 0;
        }
        enemiesBars.OnUpdate();
    }
    void SetEnemyOn()
    {
        Enemy e = GetHidden();
        if (e == null) return;
        e.Show(5);
    }
    int vLoopNum = 0;
    Enemy GetHidden()
    {
        vLoopNum = 0;
        return GetHiddenLoop();
    }
    Enemy GetHiddenLoop()
    {
        Enemy e = GetRandom();
        if (e.state == Enemy.states.hidden)
            return e;
        else
        {
            vLoopNum++;
            if (vLoopNum > 10) return null;
            else return GetHiddenLoop();
        }
    }
    Enemy GetRandom()
    {
        return enemies[Random.Range(0, enemies.Length)];
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
