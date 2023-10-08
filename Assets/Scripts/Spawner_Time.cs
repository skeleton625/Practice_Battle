using System.Linq;
using UnityEngine;

public class Spawner_Time : MonoBehaviour
{
    [Header("Spawn Object Setting")]
    [SerializeField] private Unit[] SpawnObjects = null;
    [SerializeField] private int[] SpawnCounts = null;
    [SerializeField] private float SpawnRadius = 0f;
    [SerializeField] private float SpawnTime = 0f;
    [SerializeField] private bool isStop = false;
    [SerializeField] private bool isAlly = false;

    private float preTimer = 0f;

    private void Start()
    {
        preTimer = SpawnTime;
        if (SpawnObjects.Count().Equals(0) || SpawnCounts.Equals(0))
        {
            Debug.Log("There is No Spawn Object");
            enabled = false;
        }
    }

    private void Update()
    {
        if (isStop) return;

        preTimer += Time.deltaTime;
        if (preTimer >= SpawnTime)
        {
            for (int objectIndex = 0; objectIndex < SpawnObjects.Length; ++objectIndex)
            {
                for (int i = 0; i < SpawnCounts[objectIndex]; i++)
                {
                    Vector3 position = transform.position + Random.insideUnitSphere * SpawnRadius;
                    position.y = 100;
                    if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, 1000, -1))
                    {
                        Unit unit = Instantiate(SpawnObjects[objectIndex], hit.point, Quaternion.identity);
                        if (isAlly) UnitManager.Instance.InsertAllyUnit(unit);
                        else UnitManager.Instance.InsertEnemyUnit(unit);
                    }
                }
            }
            preTimer = 0;
        }
    }
}
