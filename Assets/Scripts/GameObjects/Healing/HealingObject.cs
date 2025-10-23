using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingObject : MonoBehaviour
{
    PlayerLifeController _playerHealing;
    public int _healAmount;
    // Start is called before the first frame update
    void Start()
    {
        if (_playerHealing == null)
        {
            _playerHealing = GameObject.FindWithTag("Player").GetComponent<PlayerLifeController>();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_playerHealing != null)
            {
                SoundManager.PlaySound(SoundType.HEALING);
                _playerHealing.PlayerHealing(_healAmount);
            }
            Destroy(gameObject);
        }
    }
}
