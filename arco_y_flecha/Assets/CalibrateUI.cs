using UnityEngine;

public class CalibrateUI : MonoBehaviour
{
    [SerializeField] CalibratePointUI[] points;
    int id;

    public void Init()
    {
        CheckCalibrationSaved();
    }
    void CheckCalibrationSaved()
    {
        print("Checking Calibrations...!");
        if (PlayerPrefs.HasKey("calibrate_1_x"))
        {
            print("CheckCalibrationSaved!");
            int x_1 = PlayerPrefs.GetInt("calibrate_1_x");
            int y_1 = PlayerPrefs.GetInt("calibrate_1_y");
            int x_2 = PlayerPrefs.GetInt("calibrate_2_x");
            int y_2 = PlayerPrefs.GetInt("calibrate_2_y");
            int x_3 = PlayerPrefs.GetInt("calibrate_3_x");
            int y_3 = PlayerPrefs.GetInt("calibrate_3_y");
            int x_4 = PlayerPrefs.GetInt("calibrate_4_x");
            int y_4 = PlayerPrefs.GetInt("calibrate_4_y");

            GameManager.Instance.quadUtils.Set(
                new Vector2(x_1, y_1),
                new Vector2(x_2, y_2),
                new Vector2(x_3, y_3),
                new Vector2(x_4, y_4)
                );

            Events.CalibrationDone();
        }
    }
    public void InitCalibrate()
    {
        id = 0;
        Next();
    }
    public void Next()
    {
        foreach ( CalibratePointUI point in points )
            point.Done();

        if (id >= points.Length)
        {
            PlayerPrefs.SetInt("calibrate_1_x", (int)points[0].value.x);
            PlayerPrefs.SetInt("calibrate_1_y", (int)points[0].value.y);

            PlayerPrefs.SetInt("calibrate_2_x", (int)points[1].value.x);
            PlayerPrefs.SetInt("calibrate_2_y", (int)points[1].value.y);

            PlayerPrefs.SetInt("calibrate_3_x", (int)points[2].value.x);
            PlayerPrefs.SetInt("calibrate_3_y", (int)points[2].value.y);

            PlayerPrefs.SetInt("calibrate_4_x", (int)points[3].value.x);
            PlayerPrefs.SetInt("calibrate_4_y", (int)points[3].value.y);

            GameManager.Instance.quadUtils.Set(
                points[0].value,
                points[1].value,
                points[2].value,
                points[3].value);

            Events.CalibrationDone();
        }
        else
            points[id].Init();
        id++;
    }
    public void Set(Vector2 v)
    {
        points[id-1].Set(v);

    }
}
