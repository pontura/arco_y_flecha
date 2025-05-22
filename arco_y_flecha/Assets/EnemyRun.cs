using UnityEngine;

public class EnemyRun : Enemy
{
    [SerializeField] float initial_x = 13;
    [SerializeField] float speed = 6;
    bool isOn;
    float _x;
    bool movingLeft;

    private void Update()
    {
        if (!isOn) return;
        if(movingLeft)
            _x -= Time.deltaTime * speed;
        else
            _x += Time.deltaTime * speed;
        if ((_x > initial_x && !movingLeft) || (_x < -initial_x && movingLeft))
            Hide();
        transform.localPosition = new Vector3(_x, transform.localPosition.y, transform.localPosition.z);
    }
    void StartRun()
    {
        int levelID = GameManager.Instance.levelsManager.levelID;
        switch (levelID)
        {
            case 0:
                speed = GameManager.Instance.settings.level_1_enemy_runner_speed;
                break;
            case 1:
                speed = GameManager.Instance.settings.level_2_enemy_runner_speed;
                break;
            default:
                speed = GameManager.Instance.settings.level_3_enemy_runner_speed;
                break;
        }
        if (Random.Range(0,10)<5)
        {
            movingLeft = true;
            _x = initial_x;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            movingLeft = false;
            _x = -initial_x;
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.localPosition = new Vector3(_x, transform.localPosition.y, transform.localPosition.z);
    }
    public override void Kill()
    {
        isOn = false;
        base.Kill();
    }
    public override void Show(float duration)
    {
        base.Show(0);
        StartRun();
        isOn = true;
    }
    public override void Hide()
    {
        base.Hide();
        isOn = false;
    }

}
