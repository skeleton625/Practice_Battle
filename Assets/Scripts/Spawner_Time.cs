using UnityEngine;

public class Spawner_Time : MonoBehaviour
{
    [Header("Spawner Setting")]
    [SerializeField] private float SpawnRadius = 0f;
    [SerializeField] private float SpawnTime = 0f;
    [SerializeField] private int SpawnCount = 0;
    [SerializeField] private bool isStop = false;

    [Space(20), Header("Spawn Object Setting")]
    [SerializeField] private Unit SpawnObject = null;
    [SerializeField] private bool isAlly = false;

    private float preTimer = 0f;

    private void Start()
    {
        preTimer = SpawnTime;
        if (SpawnObject == null)
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
            for (int i = 0; i < SpawnCount; i++)
            {
                Vector3 position = transform.position + Random.insideUnitSphere * SpawnRadius;
                position.y = 100;
                if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, 1000, -1))
                {
                    Unit unit = Instantiate(SpawnObject, hit.point, Quaternion.identity);
                    if (isAlly) UnitManager.Instance.InsertAllyUnit(unit);
                    else UnitManager.Instance.InsertEnemyUnit(unit);
                }
            }
            preTimer = 0;
        }
    }
}
