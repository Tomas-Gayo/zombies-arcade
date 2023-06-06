using UnityEngine;
using UnityEngine.AI;

public class EnemyDroneAI : MonoBehaviour
{
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public IEnemyDroneState currentState;

    [HideInInspector] public NavMeshAgent navMeshAgent;

    public Light myLight;
    public float speed = 5f;
    public float timeBetweenShoots = 1f;
    public float rotationTime = 3f;
    public float alertRange = 50f;
    public float shotRange = 25f;
    public bool isStatic = false;
    public Transform[] wayPoints;
    public Color color = Color.green;
    [HideInInspector] public Animator anim;

    [Tooltip("Damage Particles when the enemy is about to be destroyed by the player.")]
    public ParticleSystem damageParticles;
    [Tooltip("Explosion Particles when the enemy is destroyed by the player.")]
    public ParticleSystem explosionParticles;

    private bool isDead;            // So we know if the enemy is dead

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        // States creation
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        chaseState = new ChaseState(this);
        attackState = new AttackState(this);

        // Patrol is the default state
        currentState = patrolState;

        // NavMesh Reference
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isDead)
            return;

        // Calling update function from the state
        currentState.UpdateState();
    }

    public void Dead()
    {
        isDead = true;
        anim.SetTrigger("Die");
        GetComponentInChildren<BoxCollider>().enabled = false;
        GetComponentInChildren<Light>().enabled = false;
        var damage = Instantiate(damageParticles, transform.position, transform.rotation);
        var explosion = Instantiate(explosionParticles, transform.position, transform.rotation);
        Destroy(damage.gameObject, damageParticles.main.duration);
        Destroy(explosion.gameObject, explosion.main.duration);
        Destroy(gameObject, damageParticles.main.duration);
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
