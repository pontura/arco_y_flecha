using System;
using System.Collections;
using System.IO;
using UnityEngine;
using YaguarLib.Audio;

public class GameManager : MonoBehaviour
{
    InputManager inputManager;
    public states state;

    public enum states
    {
        intro,
        game,
        game_paused,
        calibrate,
        summary
    }
    [Serializable]
    public class SettingsData
    {
        public int level_1_totalKills;
        public int level_2_totalKills;
        public int level_3_totalKills = 1000;

        public int scoreResta;
        public int scoreRunner;
        public int scoreDefault;
        public int totalTime;

        public int level_1_enemy_duration_from;
        public int level_1_enemy_duration_to;
        public int level_1_enemy_runner_speed;

        public int level_2_enemy_duration_from;
        public int level_2_enemy_duration_to;
        public int level_2_enemy_runner_speed;

        public int level_3_enemy_duration_from;
        public int level_3_enemy_duration_to;
        public int level_3_enemy_runner_speed;
    }

    static GameManager mInstance = null;
    EnemiesManager enemiesManager;
    public LevelsManager levelsManager;
    [SerializeField] UIManager uiManager;
    public QuadUtils quadUtils;
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
        levelsManager = GetComponent<LevelsManager>();
        inputManager = GetComponent<InputManager>();
        if (!mInstance)
            mInstance = this;
        Events.CalibrationDone += CalibrationDone;
        Events.TimeOver += TimeOver;
        Events.LevelComplete += LevelComplete;
    }

    private void LevelComplete()
    {
        StopAllCoroutines();
        state = states.game_paused;
        uiManager.levelPresentation.Init(OnInitLevel);
    }
    void OnInitLevel()
    {
        enemiesManager.Reset();
        levelsManager.Next();
        state = states.game;
    }
    private void OnDestroy()
    {
        Events.CalibrationDone -= CalibrationDone;
        Events.TimeOver -= TimeOver;
        Events.LevelComplete -= LevelComplete;
    }
    void Start()
    {
        LoadSettings();
        enemiesManager = GetComponent<EnemiesManager>();
        Init();
    }
    void Init()
    {
        uiManager.Init();
        Intro();
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
    private void Update()
    {
        if (state == states.game)
            enemiesManager.OnUpdate();
        uiManager.OnUpdate();
    }
    Vector2 NormalizedToScreenPos(Vector2 pos)
    {
        Vector2 posNormalized = GameManager.Instance.quadUtils.FindUVInQuad(pos);

        posNormalized.x += 1;
        posNormalized.y += 1;

        posNormalized.x /= 2;
        posNormalized.y /= 2;

        posNormalized.x *= Screen.width;
        posNormalized.y *= Screen.height;
        return posNormalized;

    }
    public void OnHit(Vector2 _pos)
    {
        //-1 to 1:
        Vector2 pos = NormalizedToScreenPos(_pos);

        if (state == states.game)
            enemiesManager.CheckHit(pos);
        else if(state == states.calibrate)
            uiManager.DebugPoint(pos);
    }
    public void Space()
    {
        if (state == states.intro)
            InitGame();
        else if (state == states.calibrate)
            uiManager.CalibrateClicked(inputManager.pos1);
        else if (state == states.summary)
            Intro();
    }
    public void Intro()
    {
        uiManager.levelPresentation.Reset();
        state = states.intro;
        uiManager.SetScreen(state);
    }
    public void InitGame()
    {
        levelsManager.Reset();
        levelsManager.Init(0);
        state = states.game_paused;
        uiManager.levelPresentation.Init(OnNext);
    }
    void OnNext()
    {
        state = states.game;
        enemiesManager.Restart();
        uiManager.SetScreen(states.game);
    }
    public void Calibrate()
    {
        state = states.calibrate;
        uiManager.SetScreen(state);
    }
    public void Summary()
    {
        state = states.summary;
        uiManager.SetScreen(state);
    }
    void CalibrationDone()
    {
        print("CalibrationDone");
        Intro();
    }
    public void Esc()
    {
        if (state == states.calibrate)
            CalibrationDone();
        else if (state == states.game)
            EndGame();
        else if (state == states.summary)
            Intro();
    }
    private void TimeOver()
    {
        enemiesManager.Reset();
        Summary();
    }
    void EndGame()
    {
        Intro();
    }

}
