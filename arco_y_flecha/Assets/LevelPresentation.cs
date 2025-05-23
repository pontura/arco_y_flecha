using System.Collections;
using UnityEngine;

public class LevelPresentation : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text field;
    System.Action OnNext;
    int level = 1;
    public void Reset()
    {
        level = 1;
    }
    public void Init(System.Action OnNext)
    {
        this.OnNext = OnNext;
        gameObject.SetActive(true);
        StartCoroutine(SetOff());
    }
    IEnumerator SetOff()
    {
        field.text = "NIVEL " + level;
        level++;
        yield return new WaitForSeconds(3);
        GetComponent<Animation>().Play("off");
        OnNext();
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
