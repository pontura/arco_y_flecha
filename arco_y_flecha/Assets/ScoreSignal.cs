using UnityEngine;

public class ScoreSignal : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text field;
    public void Init(int score, Vector2 pos)
    {
        transform.position = pos;
        field.text = score.ToString();
        Invoke("Reset", 2);
    }
    void Reset()
    {
        Destroy(gameObject);
    }
}
