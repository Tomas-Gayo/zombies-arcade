using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public AtackState atackState;
    [HideInInspector] public IEnemyState currentState;

    [Tooltip("How far the enemy can attack.")]
    public float attackDistance;

    // References
    [HideInInspector] public Animator anim;  
    [HideInInspector] public EnemyHealth health;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        // States creation
        idleState = new IdleState(this);
        chaseState = new ChaseState(this);
        atackState = new AtackState(this);

        // NavMesh Reference
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Patrol is the default state
        currentState = idleState;
    }

    private void Update()
    {
        // Calling update function from the state
        currentState.UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(other);
    }
    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }

}
