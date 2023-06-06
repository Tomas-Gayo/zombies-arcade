using UnityEngine;
using UnityEngine.AI;

public class EnemyGroundAI : MonoBehaviour
{ 
    [HideInInspector] public ChaseGroundState chaseGroundState;
    [HideInInspector] public AtackGroundState atackGroundState;
    [HideInInspector] public IEnemyGroundState currentState;

    [Tooltip("How far the enemy can attack.")]
    public float attackDistance;

    [Header("Other parameters.")]
    [Tooltip("The distance the enemy will stand up.")]
    public float standUpDistance = 10;

    // References
    [HideInInspector] public Animator anim;  
    [HideInInspector] public EnemyHealth health;
    [HideInInspector] public bool readyToAttack;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        // States creation
        chaseGroundState = new ChaseGroundState(this);
        atackGroundState = new AtackGroundState(this);

        // NavMesh Reference
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Chase is the default state
        currentState = chaseGroundState;
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
