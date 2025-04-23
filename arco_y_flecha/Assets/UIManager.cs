using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] IntroUI intro;
    [SerializeField] GameUI game;
    [SerializeField] CalibrateUI calibrate;
    [SerializeField] SummaryUI summary;

    public void Init()
    {
        game.Init();
        intro.Init();
        summary.Init();
        calibrate.Init();
    }
    public void OnUpdate()
    {
        switch (GameManager.Instance.state)
        {
            case GameManager.states.intro:
                break;
            case GameManager.states.game:
                game.OnUpdate();
                break;
            case GameManager.states.calibrate:
                break;
            case GameManager.states.summary:
                break;
            default:
                break;
        }
    }
    public void SetScreen(GameManager.states state)
    {
        switch (state)
        {
            case GameManager.states.intro:
                intro.gameObject.SetActive(true);
                game.gameObject.SetActive(false);
                calibrate.gameObject.SetActive(false);
                summary.gameObject.SetActive(false);
                break;
            case GameManager.states.game:
                intro.gameObject.SetActive(false);
                game.gameObject.SetActive(true);
                calibrate.gameObject.SetActive(false);
                summary.gameObject.SetActive(false);
                game.Restart();
                break;
            case GameManager.states.calibrate:
                intro.gameObject.SetActive(false);
                game.gameObject.SetActive(false);
                calibrate.gameObject.SetActive(true);
                summary.gameObject.SetActive(false);
                calibrate.InitCalibrate();
                break;
            case GameManager.states.summary:
                intro.gameObject.SetActive(false);
                game.gameObject.SetActive(false);
                calibrate.gameObject.SetActive(false);
                summary.gameObject.SetActive(true);
                summary.SetScore(game.GetScore());
                break;
            default:
                break;
        }
    }
    public void CalibrateClicked(Vector2 pos)
    {
        calibrate.Set(pos);
        calibrate.Next();
    }
    public void DebugPoint(Vector2 pos)
    {
        print("Pos_ " + pos);
    }
}
