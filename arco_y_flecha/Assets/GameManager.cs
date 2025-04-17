using UnityEngine;

public class GameManager : MonoBehaviour
{
    EnemiesManager enemiesManager;

    void Start()
    {
        enemiesManager = GetComponent<EnemiesManager>();
        Init();
    }
    void Init()
    {
        enemiesManager.Init();
    }
    private void Update()
    {
        enemiesManager.OnUpdate();
    }
    public void OnHit(Vector2 pos)
    {
        enemiesManager.CheckHit(pos);
    }
}
