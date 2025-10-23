using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletCOntroller : MonoBehaviour
{
    [SerializeField] SO_Weapone WeaponeData;
    private Rigidbody2D _bulletrb;


    private EnemyBaseDamageController _enemyDamage;
    private PlayerLifeController _playerDamage;


    void Start()
    {
        _bulletrb = GetComponent<Rigidbody2D>();
        _bulletrb.velocity = transform.up * WeaponeData.WeaponeBulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            _enemyDamage = collision.collider.GetComponent<EnemyBaseDamageController>();//Danneggia nemico
            _enemyDamage.EnemyTakeDamage(WeaponeData.WeaponeBulletDamage);
            gameObject.SetActive(false);
            SoundManager.PlaySound(SoundType.HIT);
        }
        if (collision.collider.CompareTag("Player"))
        {
            _playerDamage = collision.collider.GetComponent<PlayerLifeController>();//Danneggia Player
            _playerDamage.PlayerTakeDamage(WeaponeData.WeaponeBulletDamage);
            gameObject.SetActive(false);
            SoundManager.PlaySound(SoundType.HIT);
        }
        gameObject.SetActive(false);
    }
}
