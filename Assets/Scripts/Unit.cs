using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Unit Setting")]
    [SerializeField] protected bool isAlly = false;
    [SerializeField] protected float UnitRadius = 0f;
    [SerializeField] protected float DefaultHealth = 100;

    private bool isAlived = false;
    private float health = 0f;

    protected EnemyData enemyData = null;

    public bool IsAlived
    {
        get => isAlived;
        private set => isAlived = value;
    }
    public bool IsAlly
    {
        get => isAlly;
    }
    public float Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
            {
                IsAlived = false;
                gameObject.SetActive(false);
                if (IsAlly) UnitManager.Instance.RemoveAllyUnit(this);
                else UnitManager.Instance.RemoveEnemyUnit(this);
            }
        }
    }

    public virtual void Initialize(EnemyData enemyData)
    {
        IsAlived = true;
        Health = DefaultHealth;
        this.enemyData = enemyData;
    }

    public float GetAttackDistance(Vector3 position)
    {
        return (transform.position - position).sqrMagnitude - UnitRadius;
    }
}
