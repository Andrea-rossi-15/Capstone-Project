using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseDamageController : MonoBehaviour
{
    [SerializeField] SO_Enemie _enemyData;//SO Class

    public float _enemyCurrentHP;

    private EnemyUI _enemyUI;

    void Awake()
    {
        _enemyCurrentHP = _enemyData.EnemyMaxLifePoints;
        _enemyUI = GetComponent<EnemyUI>();
    }

    public void EnemyTakeDamage(int amount)//Danneggio nemico
    {
        _enemyCurrentHP -= amount;
        if (_enemyCurrentHP <= 0)
        {
            Die();
        }
    }

    public void EnemyHealing(int amount)//Curo nemico
    {
        _enemyCurrentHP += amount;
        if (_enemyCurrentHP > _enemyData.EnemyMaxLifePoints)
        {
            _enemyCurrentHP = _enemyData.EnemyMaxLifePoints;
        }
    }

    public void Die()
    {
        _enemyUI._enemyLifeBar.gameObject.SetActive(false);
        SoundManager.PlaySound(SoundType.ENEMYDIE);
    }
}
