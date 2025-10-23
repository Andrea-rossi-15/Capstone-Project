using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private SO_Weapone WeaponeData;
    [SerializeField] private Transform _firePoint;
    private float _lastShoot;

    private EnemyController _enemyController;

    // Start is called before the first frame update
    void Start()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyController._EnemyIsShooting == true && Time.time >= _lastShoot + WeaponeData.WeaponeFireRate)
        {
            Shoot();
            _lastShoot = Time.time;
        }
    }
    public void Shoot()
    {
        GameObject bullet = ObjectPolling._sharedIstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = _firePoint.transform.position;
            bullet.transform.rotation = _firePoint.transform.rotation;
            bullet.SetActive(true);
            SoundManager.PlaySound(SoundType.RIFLEBULLET);
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = _firePoint.up * WeaponeData.WeaponeBulletSpeed;
    }
}
