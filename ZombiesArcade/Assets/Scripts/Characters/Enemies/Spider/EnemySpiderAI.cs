using UnityEngine;
using UnityEngine.AI;

public class EnemySpiderAI : MonoBehaviour
{
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public ChaseSpiderState chaseSpiderState;
    [HideInInspector] public ExplosionState explosionState;
    [HideInInspector] public IEnemySpiderState currentState;

    [HideInInspector] public NavMeshAgent navMeshAgent;

    public Light myLight;
    public float speed = 10f;
    public float explosionRange = 5f;
    public Color color = Color.green;
    [Tooltip("Explosion Particles when the enemy is destroyed by the player.")]
    public ParticleSystem explosionParticles;

    [HideInInspector] public Animator anim;
    [HideInInspector]  public bool isDead;

    private void Awake()
    {
        anim = GetComponent<Animator>();   
    }

    private void Start()
    {
        // States creation
        idleState = new IdleState(this);
        chaseSpiderState = new ChaseSpiderState(this);
        explosionState = new ExplosionState(this);

        // Patrol is the default state
        currentState = idleState;

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

    // Called once enemy is hit so we substract life
    public void Dead()
    {
        isDead = true;
        navMeshAgent.isStopped = true;
        anim.SetTrigger("Die");
        GetComponentInChildren<BoxCollider>().enabled = false;
        var explosion = Instantiate(explosionParticles, transform.position, transform.rotation);
        Destroy(explosion.gameObject, explosion.main.duration);
        Destroy(gameObject, explosionParticles.main.duration);
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
