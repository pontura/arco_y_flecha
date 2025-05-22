using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] Enemy[] enemies;
    [SerializeField] EnemiesBars enemiesBars;

    [SerializeField] Camera mainCamera;   // Asigna la cámara principal (o usa Camera.main)
    public float rayDistance = 100f;
    float timer;
    float delay;
    bool isOn;
    Transform container;
    [SerializeField] int totalKills;
    [SerializeField] int kills;
    [SerializeField] int levelID;

    public void Reset()
    {
        enemiesBars.Reset(); 
        foreach (Enemy enemy in enemies)
        {
            enemy.Reset();
        }
        enemies = new Enemy[0];
    }
    public void Init(Transform container)
    {
        levelID = GameManager.Instance.levelsManager.levelID;
        kills = 0; 
        switch (levelID)
        {
            case 0:
                totalKills = GameManager.Instance.settings.level_1_totalKills;
                break;
            case 1:
                totalKills = GameManager.Instance.settings.level_2_totalKills;
                break;
            default:
                totalKills = GameManager.Instance.settings.level_3_totalKills;
                break;
        }
        this.container = container;
        enemies = container.GetComponentsInChildren<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.Init();
            enemiesBars.Add(enemy);
        }
    }
    public void Restart()
    {
        delay = 2;
    }
    public void OnUpdate()
    {
        if(enemies.Length == 0) return;
        timer += Time.deltaTime;
        if(timer> delay)
        {
            delay = Random.Range(0.5f, 3f);
            SetEnemyOn();
            timer = 0;
        }
        enemiesBars.OnUpdate();
    }
    void SetEnemyOn()
    {
        Enemy e = GetHidden();
        if (e == null) return;
       
        int from, to;
        switch (levelID)
        {
            case 0:
                from = GameManager.Instance.settings.level_1_enemy_duration_from;
                to = GameManager.Instance.settings.level_1_enemy_duration_to;
                break;
            case 1:
                from = GameManager.Instance.settings.level_2_enemy_duration_from;
                to = GameManager.Instance.settings.level_2_enemy_duration_to;
                break;
            default:
                from = GameManager.Instance.settings.level_3_enemy_duration_from;
                to = GameManager.Instance.settings.level_3_enemy_duration_to;
                break;
        }
        float duration = Random.Range(from, to);
        e.Show(duration);
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

        Events.AddParticle("shoot", mouseWorldPos);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorldPos, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.transform.IsChildOf(container))
            {
                Enemy e = hit.transform.GetComponent<Enemy>();
                if (e != null)
                {
                    kills++;
                    if (kills >= totalKills)
                    {
                        Events.LevelComplete();
                    }
                    e.Kill();
                    return;
                }
            }
        }
    }
}
