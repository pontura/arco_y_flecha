using System.Collections.Generic;
using UnityEngine;
using YaguarLib.UI;

public class EnemiesBars : MonoBehaviour
{
    [SerializeField] EnemyBar bar;
    [SerializeField] List<EnemyBar> bars;
    [SerializeField] Transform container;

    public void Add(Enemy e)
    {
        EnemyBar pb = Instantiate(bar, container);
        bars.Add(pb);
        pb.InitEnemy(e);
    }
    public void OnUpdate()
    {
        foreach (EnemyBar pb in bars)
        {
            pb.OnUpdate();
        }
    }
}
