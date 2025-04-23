using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameManager gameManager;
    public Vector2 pos1; 

    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnHit(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Escape))
            gameManager.Esc();
        if (Input.GetKeyDown(KeyCode.F1))
            gameManager.Calibrate();
        if (Input.GetKeyDown(KeyCode.Space))
            gameManager.Space();
    }
    public void OnHit(Vector2 pos)
    {
        this.pos1 = pos;
        gameManager.OnHit(pos);
    }
}
