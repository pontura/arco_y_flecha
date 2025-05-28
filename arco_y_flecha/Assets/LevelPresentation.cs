using System.Collections;
using UnityEngine;

public class LevelPresentation : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text field;
    [SerializeField] TMPro.TMP_Text desc;

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
        field.text = "ACTO " + level;
        if(level == 1)
            desc.text = GameManager.Instance.settings.level_1;
        else if (level == 2)
            desc.text = GameManager.Instance.settings.level_2;
        else
            desc.text = GameManager.Instance.settings.level_3;
        level++;
        yield return new WaitForSeconds(4);
        GetComponent<Animation>().Play("off");
        OnNext();
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
