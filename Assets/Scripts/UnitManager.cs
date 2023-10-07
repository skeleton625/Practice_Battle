using UnityEngine;
using TMPro;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    [Header("Test Setting")]
    [SerializeField] private TextMeshProUGUI AllyCount = null;
    [SerializeField] private TextMeshProUGUI EnemyCount = null;

    private EnemyData allyUnitData = null;
    private EnemyData enemyUnitData = null;

    public void Initialize()
    {
        Instance = this;

        allyUnitData = Resources.Load<EnemyData>("Datas/UnitData_Ally");
        enemyUnitData = Resources.Load<EnemyData>("Datas/UnitData_Enemy");
        allyUnitData.Initialize();
        enemyUnitData.Initialize();

        Unit[] units = FindObjectsOfType<Unit>();
        for (int i = 0; i < units.Length; ++i)
        {
            if (units[i].IsAlly)
            {
                allyUnitData.Insert(units[i]);
                units[i].Initialize(enemyUnitData);
            }
            else
            {
                enemyUnitData.Insert(units[i]);
                units[i].Initialize(allyUnitData);
            }
        }
    }

    ~UnitManager()
    {
        Resources.UnloadAsset(allyUnitData);
        Resources.UnloadAsset(enemyUnitData);
        allyUnitData = null;
        enemyUnitData = null;
    }

    public void InsertAllyUnit(Unit unit)
    {
        allyUnitData.Insert(unit);
        unit.Initialize(enemyUnitData);
        AllyCount.text = allyUnitData.EnemyList.Count.ToString();
    }

    public void InsertEnemyUnit(Unit unit)
    {
        enemyUnitData.Insert(unit);
        unit.Initialize(allyUnitData);
        EnemyCount.text = enemyUnitData.EnemyList.Count.ToString();

    }

    public void RemoveAllyUnit(Unit unit)
    {
        allyUnitData.Remove(unit);
        AllyCount.text = allyUnitData.EnemyList.Count.ToString();
    }

    public void RemoveEnemyUnit(Unit unit)
    {
        enemyUnitData.Remove(unit);
        EnemyCount.text = enemyUnitData.EnemyList.Count.ToString();
    }
}
