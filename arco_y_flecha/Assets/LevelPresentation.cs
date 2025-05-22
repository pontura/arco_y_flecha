using System.Collections;
using UnityEngine;

public class LevelPresentation : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text field;
    System.Action OnNext;

    public void Init(System.Action OnNext)
    {
        this.OnNext = OnNext;
        gameObject.SetActive(true);
        StartCoroutine(SetOff());
    }
    IEnumerator SetOff()
    {
        int level = GameManager.Instance.levelsManager.levelID+1;
        field.text = "NIVEL " + level;
        yield return new WaitForSeconds(1);
        OnNext();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
