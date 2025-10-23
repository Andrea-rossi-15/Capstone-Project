using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{
    [SerializeField] SO_Player PlayerData;//SO Class

    public float _playerCurrentHP;

    public bool _isPlayerAlive = true;

    PlayerUI _playerUI;



    void Awake()
    {
        _playerCurrentHP = PlayerData.PlayerMaxLifePoints;
        _playerUI = GetComponent<PlayerUI>();
    }

    public void PlayerTakeDamage(int amount)
    {
        _playerCurrentHP = Mathf.Clamp(_playerCurrentHP - amount, 0, PlayerData.PlayerMaxLifePoints);
        if (_playerCurrentHP <= 0)
        {
            Die();
        }
    }

    public void PlayerHealing(int amount)
    {
        _playerCurrentHP = Mathf.Clamp(_playerCurrentHP + amount, 0, PlayerData.PlayerMaxLifePoints);
    }

    public void Die()
    {
        _isPlayerAlive = false;
        gameObject.SetActive(false);
        _playerUI._playerUICanvas.gameObject.SetActive(false);
    }
}
