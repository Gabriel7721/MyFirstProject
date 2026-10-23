using UnityEngine;
public enum EnemyState { Idle, Chase, Attack }
[RequireComponent(typeof(EnemyCombat))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float chaseRange = 6f;
    [SerializeField] private float attackRange = 1.5f;
    private EnemyState currentState = EnemyState.Idle;
    public bool IsChasing => currentState == EnemyState.Chase;
    private EnemyCombat enemyCombat;
    public Transform Player => player;
    private void Awake()
    {
        enemyCombat = GetComponent<EnemyCombat>();
        if (player == null)
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }
    private void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        Debug.Log(gameObject.name + " State: " + currentState);
    }
    private void UpdateState()
    {
        float distance = Vector2
            .Distance(transform.position, player.position);

        if (distance > chaseRange)
        {
            ChangeState(EnemyState.Idle);
        }
        else if (distance <= attackRange)
        {
            ChangeState(EnemyState.Attack);
        }
        else
        {
            ChangeState(EnemyState.Chase);
        }
    }
    private void Update()
    {
        if (player == null)
        {
            ChangeState(EnemyState.Idle);
            return;
        }
        UpdateState();

        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdle();
                break;

            case EnemyState.Chase:
                HandleChase();
                break;

            case EnemyState.Attack:
                HandleAttack();
                break;
        }
    }
    private void OnValidate()
    {
        chaseRange = Mathf.Max(0f, chaseRange);
        attackRange = Mathf.Clamp(attackRange, 0f, chaseRange);
    }
    private void HandleIdle() { }
    private void HandleChase() { }
    private void HandleAttack()
    {
        if (enemyCombat != null)
        {
            enemyCombat.TryAttack(player);
        }
    }
    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(
    //         transform.position,
    //         chaseRange
    //     );

    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(
    //         transform.position,
    //         attackRange
    //     );
    // }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(
            transform.position,
            Vector3.forward,
            chaseRange
        );

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(
            transform.position,
            Vector3.forward,
            attackRange
        );
    }
#endif
}
