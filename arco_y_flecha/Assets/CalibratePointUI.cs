using UnityEngine;

public class CalibratePointUI : MonoBehaviour
{
    [SerializeField] Animation anim;
    [SerializeField] TMPro.TMP_Text field;
    public Vector2 value;

    public void Init()
    {
        anim.Play("on");
    }
    public void Set(Vector2 v)
    {
        this.value = v;
       // this.value.y = Screen.height - v.y; // Reverse Y
        print("V " + v + " value = " + value);
        field.text = this.value.ToString();
    }
    public void Done()
    {
        anim.Play("off");
    }
}
