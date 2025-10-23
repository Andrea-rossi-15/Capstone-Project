using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyConeVision _enemyConeVision;
    public EnemyState _currentState;
    public SO_Enemie EnemyData;
    private EnemyBaseDamageController _enemyDamage;

    private Rigidbody2D _enemyRb;
    private int _direction = 1;

    public Transform[] PatrolPoints;
    private SpriteRenderer _enemySpriteRenderer;
    public Sprite[] _enemySpritesStates;
    private int _currentPatrolIndex = 0;

    private CircleCollider2D _enemyCollider;

    private EnemyShooting _enemyShooting;
    public bool _EnemyIsShooting;

    private Vector2 _prevPos;

    private PlayerLifeController _playerLifeController;

    // Start is called before the first frame update
    void Start()
    {
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
        _enemyDamage = GetComponent<EnemyBaseDamageController>();
        _enemyRb = GetComponent<Rigidbody2D>();
        EnemyData.EnemyTarget = GameObject.FindGameObjectWithTag("Player").transform;
        _enemyCollider = GetComponent<CircleCollider2D>();
        _enemyShooting = GetComponent<EnemyShooting>();
        _enemyConeVision = GetComponent<EnemyConeVision>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerLifeController = player.GetComponent<PlayerLifeController>();
        }

        _prevPos = transform.position;
    }

    void Update()
    {
        switch (_currentState)
        {
            case EnemyState.PATROLLING:
                _enemySpriteRenderer.sprite = _enemySpritesStates[0];
                EnemyPatrolling();
                break;

            case EnemyState.CHASE:
                _enemySpriteRenderer.sprite = _enemySpritesStates[1];
                EnemyChasing();
                break;

            case EnemyState.ATTACK:
                _enemySpriteRenderer.sprite = _enemySpritesStates[2];
                EnemyAttacking();
                break;

            case EnemyState.DIE:
                _enemySpriteRenderer.sprite = _enemySpritesStates[3];
                EnemyDie();
                break;
        }

        if (_currentState != EnemyState.DIE && EnemyData.EnemyTarget != null)
        {
            if (_currentState == EnemyState.CHASE || _currentState == EnemyState.ATTACK)
            {
                // Guarda verso il Player
                Vector2 direction = EnemyData.EnemyTarget.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle + 270f);
            }
            else if (_currentState == EnemyState.PATROLLING)
            {
                // Guarda verso la direzione in cui si muove
                Vector2 movementDir = (transform.position - (Vector3)_prevPos).normalized;
                if (movementDir.sqrMagnitude > 0.01f)
                {
                    float angle = Mathf.Atan2(movementDir.y, movementDir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, angle + 270f);
                }
            }
        }

        _prevPos = transform.position;
    }

    void EnemyPatrolling()
    {
        Debug.Log("EnemyPatrolling");

        Vector2 target = PatrolPoints[_currentPatrolIndex].position;
        Vector2 newPos = Vector2.MoveTowards(_enemyRb.position, target, EnemyData.EnemySpeed * Time.fixedDeltaTime);
        _enemyRb.MovePosition(newPos);

        _EnemyIsShooting = false;

        if (Vector2.Distance(transform.position, PatrolPoints[_currentPatrolIndex].position) < 0.2f)
        {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % PatrolPoints.Length;
        }

        if (_enemyDamage._enemyCurrentHP <= 0)
        {
            _currentState = EnemyState.DIE;
        }
    }
    void EnemyChasing()
    {
        Debug.Log("EnemyChasing");
        if (EnemyData.EnemyTarget == null) { _currentState = EnemyState.PATROLLING; return; }

        Vector2 dir = (EnemyData.EnemyTarget.position - transform.position).normalized;
        _enemyRb.velocity = dir * EnemyData.EnemySpeed;

        _EnemyIsShooting = false;

        if (_enemyConeVision != null && !_enemyConeVision._canSeePlayer)
        {
            _currentState = EnemyState.PATROLLING;
            return;
        }

        if (_enemyConeVision != null && _enemyConeVision._canSeePlayer &&
            Vector2.Distance(transform.position, EnemyData.EnemyTarget.position) <= EnemyData.EnemyAttackRange)
        {
            _currentState = EnemyState.ATTACK;
        }

        if (_enemyDamage._enemyCurrentHP <= 0)
        {
            _currentState = EnemyState.DIE;
        }
    }

    void EnemyAttacking()
    {
        Debug.Log("EnemyAttacking");
        if (_playerLifeController == null || _playerLifeController._isPlayerAlive == false)
        {
            // Player morto: torna a PATROLLING
            _EnemyIsShooting = false;
            _currentState = EnemyState.PATROLLING;
            return;
        }

        if (_enemyConeVision != null && !_enemyConeVision._canSeePlayer)
        {
            _currentState = EnemyState.CHASE;
            return;
        }

        _EnemyIsShooting = true;

        if (Vector2.Distance(transform.position, EnemyData.EnemyTarget.position) > EnemyData.EnemyAttackRange)
        {
            _currentState = EnemyState.CHASE;
        }
        _enemyRb.velocity = Vector2.zero;

        if (_enemyDamage._enemyCurrentHP <= 0)
        {
            _currentState = EnemyState.DIE;
        }
    }

    void EnemyDie()
    {
        Debug.Log("EnemyDie");
        _EnemyIsShooting = false;
        _enemyCollider.enabled = false;
        _enemyRb.velocity = Vector2.zero;
        _enemyRb.isKinematic = true;
    }

    public void SetState(EnemyState newState)
    {
        if (_currentState == EnemyState.DIE) return;
        if (_currentState == newState) return;
        Debug.Log("SetState from " + _currentState + " to " + newState);
        _currentState = newState;
    }
}
