using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private GameObject[] _weaponPrefabs;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            SpawnRandomWeapon();
            Destroy(gameObject);
        }
    }
    void SpawnRandomWeapon()
    {
        if (_weaponPrefabs.Length == 0) return;

        int index = Random.Range(0, _weaponPrefabs.Length);

        Instantiate(_weaponPrefabs[index], transform.position, Quaternion.identity);

        Debug.Log(_weaponPrefabs[index]);
    }
}
