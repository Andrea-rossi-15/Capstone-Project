using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponeShooting : MonoBehaviour
{
    [SerializeField] public SO_Weapone WeaponeData;
    [SerializeField] private Transform _firePoint;
    private float _lastShoot;
    private PlayerInventory _playerInventory;

    private void Awake()
    {
        _playerInventory = GetComponentInParent<PlayerInventory>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= _lastShoot + WeaponeData.WeaponeFireRate && _playerInventory != null && _playerInventory.TryConsumeAmmo(WeaponeData))
        {
            Shoot();
            _lastShoot = Time.time;
            SoundManager.PlaySound(SoundType.RIFLEBULLET);
        }
    }
    void Shoot()
    {
        GameObject bullet = ObjectPolling._sharedIstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = _firePoint.transform.position;
            bullet.transform.rotation = _firePoint.transform.rotation;
            bullet.SetActive(true);
        }
        Rigidbody2D _bulletRb = bullet.GetComponent<Rigidbody2D>();
        _bulletRb.velocity = WeaponeData.WeaponeBulletSpeed * _firePoint.up;
    }
}
