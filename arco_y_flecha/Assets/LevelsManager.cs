using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] EnemiesManager enemiesManager;
    [SerializeField] Level level;
    [SerializeField] Level[] levels;
    public int levelID;

    public void Reset()
    {
        levelID = 0;
    }

    public void Init(int levelID)
    {
        this.levelID = levelID;
        foreach (Level l in levels)
            l.Hide();

        level = levels[levelID];
        level.Init();
        enemiesManager.Init(level.container);
    }
    public void Next()
    {
        levelID++;
        if (levelID >= levels.Length)
            levelID = 0;
        Init(levelID);
    }
}
