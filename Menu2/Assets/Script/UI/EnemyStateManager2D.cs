using UnityEngine;
using TMPro;

public class EnemyStateManager_2D : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Chase,
        Attack,
    }

    [Header("Referencias")]
    [SerializeField] private Transform Player;
    [SerializeField] private Transform[] waypoint;
    [SerializeField] private TextMeshProUGUI stateText; // Opcional
    private Animator anim;

    [Header("Range")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float attackRange = 1f;

    [Header("Movement")]
    [SerializeField] private float patrolspeed = 2f;
    [SerializeField] private float chasespeed = 4f;

    [Header("Attack")]
    [SerializeField] private float coolDownAttacks = 1.2f;

    private EnemyState currentState;
    private int currentWaypointIndex = 0;
    private float attackTimer;

    [Header("Debug")]
    public TextMeshProUGUI txtStateDebug;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        ChangeState(EnemyState.Patrol);
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                Chase();
                break;

            case EnemyState.Attack:
                Attack();
                break;
        }

        // DEBUG EN PANTALLA
        if (txtStateDebug != null)
        {
            txtStateDebug.text = "Enemy State: " + currentState.ToString();
        }

        UpdateSpriteDirection();
    }

    private void Patrol()
    {
        MoveTo(waypoint[currentWaypointIndex], patrolspeed);

        anim.SetBool("isChasing", false);
        anim.SetBool("isAttacking", false);

        if (Vector2.Distance(transform.position, Player.position) <= detectionRange)
        {
            ChangeState(EnemyState.Chase);
            return;
        }

        if (Vector2.Distance(transform.position, waypoint[currentWaypointIndex].position) < 0.2f)
        {
            int nextIdex = currentWaypointIndex;
            while (nextIdex == currentWaypointIndex && waypoint.Length > 1)
            {
                nextIdex = Random.Range(0, waypoint.Length);
            }
            currentWaypointIndex = nextIdex;
        }
    }

    private void Chase()
    {
        MoveTo(Player, chasespeed);

        anim.SetBool("isChasing", true);
        anim.SetBool("isAttacking", false);

        float distance = Vector2.Distance(transform.position, Player.position);

        if (distance > detectionRange)
        {
            ChangeState(EnemyState.Patrol);
            return;
        }

        if (distance <= attackRange)
        {
            ChangeState(EnemyState.Attack);
            return;
        }
    }

    private void Attack()
    {
        float distance = Vector2.Distance(transform.position, Player.position);

        if (distance > attackRange)
        {
            anim.SetBool("isAttacking", false);
            ChangeState(EnemyState.Chase);
            return;
        }

        anim.SetBool("isAttacking", true);
        anim.SetBool("isChasing", false);

        attackTimer += Time.deltaTime;
        if (attackTimer >= coolDownAttacks)
        {
            Debug.Log("El enemigo ataca");

            // CAMBIO AQUÍ: Ahora buscamos PlayerMovement que es donde quedó la vida
            PlayerMovement playerScript = Player.GetComponent<PlayerMovement>();

            if (playerScript != null)
            {
                playerScript.TakeDamage(1f); // Esto activará el color rojo y restará vida
            }

            attackTimer = 0;
        }
    }

    private void MoveTo(Transform target, float speed)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void UpdateSpriteDirection()
    {
        Vector3 targetPos = (currentState == EnemyState.Patrol)
            ? waypoint[currentWaypointIndex].position
            : Player.position;

        if (targetPos.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void ChangeState(EnemyState newState)
    {
        currentState = newState;

        // DEBUG EN CONSOLA
        Debug.Log("Enemy changed to state: " + currentState.ToString());
    }
}
