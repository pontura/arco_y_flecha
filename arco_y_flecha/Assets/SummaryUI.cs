using UnityEngine;

public class SummaryUI : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text field;

    public void Init()
    {

    }
    public void SetScore(int score)
    {
        field.text = YaguarLib.Xtras.Utils.FormatNumbers(score);
    }
}
