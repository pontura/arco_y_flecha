using System;
using UnityEngine;

public class PArticlesManager : MonoBehaviour
{
    [SerializeField] GameObject explotion;
    [SerializeField] GameObject shoot;
    [SerializeField] Transform container;

    void Start()
    {
        Events.AddParticle += AddParticle;
    }
    void OnDestroy()
    {
        Events.AddParticle -= AddParticle;
    }
    private void AddParticle(string type, Vector2 pos)
    {
        GameObject go = null;
        switch(type)
        {
            case "explotion": go = Instantiate(explotion, container); break;
            default: go = Instantiate(shoot, container); break;
        }
        go.transform.localPosition = pos;    
    }

}
