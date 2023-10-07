using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_EnemyData", menuName = "Datas/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public List<Unit> EnemyList = null;

    public void Initialize()
    {
        EnemyList = new List<Unit>();        
    }

    public void Insert(Unit unit)
    {
        EnemyList.Add(unit);
    }

    public void Remove(Unit unit)
    {
        EnemyList.Remove(unit);
    }
}
