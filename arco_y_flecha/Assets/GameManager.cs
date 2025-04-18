using System;
using System.IO;
using UnityEngine;
using YaguarLib.Audio;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class SettingsData
    {
        public int scoreDefault;
    }

    static GameManager mInstance = null;
    EnemiesManager enemiesManager;
    [SerializeField] UIManager UIManager;
    public SettingsData settings;

    public static GameManager Instance
    {
        get
        {
            return mInstance;
        }
    }
    void Awake()
    {
        if (!mInstance)
            mInstance = this;
    }
    void Start()
    {
        LoadSettings();
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
    void LoadSettings()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "settings.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            settings = JsonUtility.FromJson<SettingsData>(json);

            Debug.Log("Settings loaded: " + json);
        }
        else
        {
            Debug.LogWarning("Settings file not found at " + path);
        }
    }
}
