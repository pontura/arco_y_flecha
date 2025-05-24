using System.Collections.Generic;
using UnityEngine;
using YaguarLib.UI;

public class EnemiesBars : MonoBehaviour
{
    [SerializeField] EnemyBar bar;
    [SerializeField] List<EnemyBar> bars;
    [SerializeField] Transform container;

    public void Reset()
    {
        YaguarLib.Xtras.Utils.RemoveAllChildsIn(container);
        bars.Clear();
    }
    public void Add(Enemy e)
    {
        print("ADD");
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
