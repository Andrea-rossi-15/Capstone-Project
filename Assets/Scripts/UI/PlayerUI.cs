using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    // Start is called before the first frame update public Image _enemyLifeBar;
    [SerializeField] SO_Player PlayerData;
    [SerializeField] SO_Weapone WeaponeData;
    [SerializeField] public Canvas _playerUICanvas;
    public Image _playerLifeBar;
    private PlayerLifeController _playerDamage;

    [SerializeField] Image _weaponeImage;
    [SerializeField] TextMeshProUGUI _currentBullet;


    // Start is called before the first frame update
    void Start()
    {
        _playerDamage = GetComponent<PlayerLifeController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLifeBar();
    }

    void UpdateLifeBar()
    {
        if (_playerDamage != null && _playerLifeBar != null)
        {
            float _barFill = (float)_playerDamage._playerCurrentHP / PlayerData.PlayerMaxLifePoints;
            _playerLifeBar.fillAmount = _barFill;
        }
    }
    public void SetCurrentWeapone(SO_Weapone currentWeapon)
    {
        _weaponeImage.sprite = currentWeapon != null ? currentWeapon.WeaponeSprite : null;
    }
    public void SetCurrentBullet(SO_Weapone _currentWeapon, PlayerInventory _inventory)
    {
        {
            if (_currentWeapon == null || _inventory == null)
            {
                _currentBullet.text = "0";
                return;
            }

            int _ammo = _inventory.GetCurrentAmmo(_currentWeapon);
            _currentBullet.text = _ammo.ToString();
        }

    }
}
