using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConeVision : MonoBehaviour
{
    public float _angle;
    public float _radius;

    public GameObject _playerRef;

    public LayerMask _targetMask;
    public LayerMask _obstructionMask;

    public bool _canSeePlayer;

    private EnemyController _enemyController;

    void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds _wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return _wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, _radius, _targetMask);
        bool detected = false;

        for (int i = 0; i < rangeChecks.Length; i++)
        {
            Transform t = rangeChecks[i].transform;

            // Se hai assegnato un riferimento esplicito, filtra su quello; altrimenti accetta oggetti con tag Player
            if (_playerRef != null)
            {
                if (t.gameObject != _playerRef) continue;
            }
            else
            {
                if (!t.CompareTag("Player")) continue;
            }

            Vector2 dirToTarget = (t.position - transform.position).normalized;
            // Usa transform.up per il fronte (top-down 2D)
            if (Vector2.Angle(transform.up, dirToTarget) <= _angle * 0.5f)
            {
                float distToTarget = Vector2.Distance(transform.position, t.position);
                // Ostacoli che bloccano la vista
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToTarget, distToTarget, _obstructionMask);
                if (!hit)
                {
                    detected = true;
                    break;
                }
            }
        }

        _canSeePlayer = detected;

        // ✅ Unica responsabilità: se vedo il player → CHASE. Non forzo PATROLLING qui.
        if (detected && _enemyController != null)
        {
            _enemyController.SetState(EnemyState.CHASE);
        }
    }
}
