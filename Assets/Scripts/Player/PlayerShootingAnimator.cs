using UnityEngine;

public class PlayerShootingAnimator : MonoBehaviour
{
    private PlayerInventory _playerInventory;

    [SerializeField] GameObject[] _firearms;
    [SerializeField] Animator[] _firearmAnimators;
    [SerializeField] WeaponeShooting[] _weaponShootings;
    [SerializeField] GameObject _knife;
    public int _currentWeaponIndex = -1;

    void Awake()
    {
        _playerInventory = GetComponentInParent<PlayerInventory>();

        for (int i = 0; i < _firearms.Length; i++)
        {
            _firearms[i].SetActive(false);
        }

        _knife.SetActive(true);
    }
    void Update()
    {
        Shoot();
        ReloadRifle();
        ChangeGun();
    }

    void Shoot()
    {
        if (_currentWeaponIndex >= 0 && _currentWeaponIndex < _firearmAnimators.Length && _playerInventory != null)
        {
            var ws = _weaponShootings[_currentWeaponIndex];
            var data = ws != null ? ws.WeaponeData : null;
            bool hasAmmo = data != null && _playerInventory.GetCurrentAmmo(data) > 0;
            _firearmAnimators[_currentWeaponIndex].SetBool("IsShooting", hasAmmo && Input.GetMouseButton(0));
        }
    }

    void ReloadRifle()
    {
        if (_currentWeaponIndex < 0 || _currentWeaponIndex >= _firearmAnimators.Length || _playerInventory == null) return;
        var ws = _weaponShootings[_currentWeaponIndex];
        var data = ws != null ? ws.WeaponeData : null;
        if (data == null) return;

        int currentAmmo = _playerInventory.GetCurrentAmmo(data);
        int maxAmmo = data.WeaponeCapacity;
        int inventoryAmmo = _playerInventory.GetInventoryAmmo(data);

        bool canReload = currentAmmo < maxAmmo && inventoryAmmo > 0;

        if (canReload && Input.GetKeyDown(KeyCode.R))
        {
            SoundManager.PlaySound(SoundType.RELOAD);
            _firearmAnimators[_currentWeaponIndex].SetTrigger("IsReloading");
        }
    }

    void ChangeGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { EquipWeapon(0); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { EquipWeapon(1); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { UnequipWeapons(); }
    }

    void EquipWeapon(int index)
    {
        if (index < 0 || index >= _firearms.Length) return;

        _knife.SetActive(false);

        // Disattiva tutte le armi
        for (int i = 0; i < _firearms.Length; i++)
        {
            _firearms[i].SetActive(false);
        }

        // Attiva l'arma selezionata
        _firearms[index].SetActive(true);
        _currentWeaponIndex = index;

        for (int i = 0; i < _weaponShootings.Length; i++)
        {
            _weaponShootings[i].enabled = (i == index);
        }
    }

    void UnequipWeapons()
    {
        for (int i = 0; i < _firearms.Length; i++)
        {
            _firearms[i].SetActive(false);
        }
        _knife.SetActive(true);
        _currentWeaponIndex = -1;

        for (int i = 0; i < _weaponShootings.Length; i++)
        {
            _weaponShootings[i].enabled = false;
        }
    }

    // Da chiamare con un Animation Event alla fine dell'animazione di ricarica
    public void OnReloadFinished()
    {
        if (_currentWeaponIndex < 0 || _currentWeaponIndex >= _firearmAnimators.Length || _playerInventory == null) return;
        var ws = _weaponShootings[_currentWeaponIndex];
        var data = ws != null ? ws.WeaponeData : null;
        if (data == null) return;

        int currentAmmo = _playerInventory.GetCurrentAmmo(data);
        int maxAmmo = data.WeaponeCapacity;
        int inventoryAmmo = _playerInventory.GetInventoryAmmo(data);

        int needed = maxAmmo - currentAmmo;
        int toLoad = Mathf.Min(needed, inventoryAmmo);

        _playerInventory.SetCurrentAmmo(data, currentAmmo + toLoad);
        _playerInventory.ConsumeInventoryAmmo(data, toLoad);
    }
}
