using UnityEngine;

public class GameManager : MonoBehaviour
{
    EnemiesManager enemiesManager;
    [SerializeField] UIManager UIManager;

    void Start()
    {
        enemiesManager = GetComponent<EnemiesManager>();
        Init();
    }
    void Init()
    {
        UIManager.Init();
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
