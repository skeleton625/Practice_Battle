using System.Collections.Generic;
using UnityEngine;

public class Tower : Unit
{
    [Space(20), Header("Combat Setting")]
    [SerializeField] private int AttackUnitCount = 0;
    [SerializeField] private float AttackRadius = 0f;
    [SerializeField] private float AttackTimer = 0f;
    [SerializeField] private float AttackValue = 0f;

    private HashSet<int> targetTransformHash = null;
    private List<float> attackTimerList = null;
    private List<Unit> targetUnitList = null;

    public override void Initialize(EnemyData enemyData)
    {
        base.Initialize(enemyData);

        targetTransformHash = new HashSet<int>();
        attackTimerList = new List<float>();
        targetUnitList = new List<Unit>();
    }

    private void Update()
    {
        for (int i = 0; i < enemyData.EnemyList.Count; ++i)
        {
            Unit unit = enemyData.EnemyList[i];
            int unitHash = unit.transform.GetInstanceID();
            if (targetUnitList.Count >= AttackUnitCount ||
                    (transform.position - unit.transform.position).sqrMagnitude >= AttackRadius ||
                        targetTransformHash.Contains(unitHash))
                continue;

            attackTimerList.Add(AttackTimer);
            targetUnitList.Add(unit);
            targetTransformHash.Add(unitHash);
        }

        Debug.Log(targetUnitList.Count);
        for (int i = 0; i < targetUnitList.Count; ++i)
        {
            Unit unit = targetUnitList[i];
            if ((transform.position - unit.transform.position).sqrMagnitude < AttackRadius)
            {
                if (!unit.IsAlived)
                {
                    attackTimerList.RemoveAt(i);
                    targetUnitList.RemoveAt(i--);
                    targetTransformHash.Remove(unit.transform.GetInstanceID());
                }
                else if (attackTimerList[i] < AttackTimer)
                    attackTimerList[i] += Time.deltaTime;
                else
                {
                    attackTimerList[i] = 0;
                    unit.Health -= AttackValue;
                }
            }
            else
            {
                attackTimerList.RemoveAt(i);
                targetUnitList.RemoveAt(i--);
                targetTransformHash.Remove(unit.transform.GetInstanceID());
            }
        }
    }
}
