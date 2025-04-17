using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnHit(Input.mousePosition);
    }
    void OnHit(Vector2 pos)
    {
        gameManager.OnHit(pos);
    }
}
