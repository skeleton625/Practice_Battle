using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnitManager unitManager;

    private void Awake()
    {
        unitManager.Initialize();
    }
}
