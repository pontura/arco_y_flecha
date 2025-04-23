using UnityEngine;

public class CalibrateUI : MonoBehaviour
{
    [SerializeField] CalibratePointUI[] points;
    int id;

    public void Init()
    {
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
