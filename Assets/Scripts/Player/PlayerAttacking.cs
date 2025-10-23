using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    [SerializeField] SO_Player _playerData;//SO Class
    [SerializeField] Animator _playerAnimator;

    private float _timer = 0;

    [SerializeField] Transform _attackPoint;
    [SerializeField] LayerMask _enemyLayer;




    void Awake()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (_timer > 0)//Timer attacco
        {
            _timer -= Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0))
        {
            KnifeAttack();//Attacco base
        }
    }


    public void KnifeAttack()//attacco base 
    {
        if (_timer <= 0)
        {
            _playerAnimator.SetBool("isAttacking", true);
            _timer = _playerData.PlayerAttackCoolDown;
        }
    }
    public void DealDamage()//Serve per i keyframe
    {
        Collider2D[] _enemieInRange = Physics2D.OverlapCircleAll(_attackPoint.position, _playerData.PlayerWeaponeRange, _enemyLayer);
        if (_enemieInRange.Length > 0)
        {
            _enemieInRange[0].GetComponent<EnemyBaseDamageController>().EnemyTakeDamage(_playerData.PlayerKnifeDamage);
        }
    }
    public void StopAttacking()
    {
        _playerAnimator.SetBool("isAttacking", false);
    }
}
