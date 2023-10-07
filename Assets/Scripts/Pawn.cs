using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Pawn : Unit
{
    [Space(20), Header("Combat Setting")]
    [SerializeField] private float AttackRadius = 0f;
    [SerializeField] private float AttackTimer = 0f;
    [SerializeField] private float AttackValue = 0f;

    private float preAttackTime = 0f;
    private Unit targetUnit = null;
    private NavMeshPath path = null;
    private NavMeshAgent agent = null;

    public override void Initialize(EnemyData enemyData)
    {
        base.Initialize(enemyData);

        path = new NavMeshPath();
        agent = GetComponent<NavMeshAgent>();

        preAttackTime = AttackTimer;
    }

    private void Update()
    {
        if (targetUnit == null)
        {
            if (enemyData.EnemyList.Count.Equals(0)) return;

            float minDist = int.MaxValue;
            Unit minUnit = null;
            for (int i = 0; i < enemyData.EnemyList.Count; ++i)
            {
                float preDist = (transform.position - enemyData.EnemyList[i].transform.position).sqrMagnitude;
                if (minDist > preDist)
                {
                    minDist = preDist;
                    minUnit = enemyData.EnemyList[i];
                }
            }

            targetUnit = minUnit;
        }
        else
        {
            if (targetUnit.GetAttackDistance(transform.position) < AttackRadius)
            {
                agent.isStopped = true;
                if (!targetUnit.IsAlived)
                    targetUnit = null;
                else if (preAttackTime < AttackTimer)
                    preAttackTime += Time.deltaTime;
                else
                {
                    preAttackTime = 0;
                    targetUnit.Health -= AttackValue;
                }
            }
            else
            {
                preAttackTime = AttackTimer;
                MovePosition(targetUnit.transform.position);
            }
        }
    }

    public void MovePosition(Vector3 position)
    {
        position.y = 100;
        if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, 1000, -1))
        {
            agent.isStopped = false;
            agent.CalculatePath(hit.point, path);
            switch (path.status)
            {
                case NavMeshPathStatus.PathComplete:
                    agent.SetPath(path);
                    break;
            }
        }
    }
}
