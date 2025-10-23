using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<SO_Weapone> weapons = new List<SO_Weapone>();

    // Dizionario per gestire le munizioni correnti di ogni arma (caricatore)
    private Dictionary<SO_Weapone, int> currentAmmo = new Dictionary<SO_Weapone, int>();

    // Dizionario per gestire le munizioni extra di ogni arma (inventario / riserva)
    private Dictionary<SO_Weapone, int> inventoryAmmo = new Dictionary<SO_Weapone, int>();

    private int currentWeaponIndex = -1;

    /// <summary>
    /// Aggiunge un'arma all'inventario, oppure aggiunge munizioni se già presente.
    /// Ritorna true se il pickup è stato consumato (arma nuova o munizioni aggiunte), false se munizioni già al massimo.
    /// </summary>
    public bool AddWeapon(SO_Weapone newWeapon)
    {
        if (newWeapon == null) return false;

        if (!weapons.Contains(newWeapon))
        {
            weapons.Add(newWeapon);
            currentAmmo[newWeapon] = 0; // caricatore vuoto all'inizio
            inventoryAmmo[newWeapon] = 0; // riserva iniziale
            if (currentWeaponIndex == -1)
            {
                currentWeaponIndex = 0;
                EquipWeapon(weapons[currentWeaponIndex]);
            }
            return true;
        }
        else
        {
            // Se le munizioni in riserva sono già al massimo (non definito un massimo per riserva, quindi sempre aggiungibili)
            bool ammoAdded = AddAmmoToWeapon(newWeapon);
            return ammoAdded;
        }
    }

    public void EquipWeapon(SO_Weapone weapon)
    {
        if (weapon == null) return;

        int ammo = currentAmmo.ContainsKey(weapon) ? currentAmmo[weapon] : 0;
        // TODO: qui potrai aggiungere la logica di equip (aggiornare sprite, stats, UI ecc.)
    }

    public SO_Weapone GetCurrentWeapon()
    {
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            return weapons[currentWeaponIndex];
        }
        return null;
    }

    public void NextWeapon()
    {
        if (weapons.Count == 0) return;

        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        EquipWeapon(weapons[currentWeaponIndex]);
    }

    /// <summary>
    /// Aggiunge munizioni all'arma specificata. Ritorna true se sono state aggiunte munizioni.
    /// </summary>
    private bool AddAmmoToWeapon(SO_Weapone weapon)
    {
        if (!currentAmmo.ContainsKey(weapon))
        {
            currentAmmo[weapon] = 0;
        }
        if (!inventoryAmmo.ContainsKey(weapon))
        {
            inventoryAmmo[weapon] = 0;
        }

        int ammoToAdd = Random.Range(3, weapon.WeaponeCapacity + 1);

        int spaceInMagazine = weapon.WeaponeCapacity - currentAmmo[weapon];
        int toMagazine = Mathf.Min(spaceInMagazine, ammoToAdd);
        int toReserve = ammoToAdd - toMagazine;

        currentAmmo[weapon] += toMagazine;
        inventoryAmmo[weapon] += toReserve;

        Debug.Log("Raccolte " + ammoToAdd + " munizioni per " + weapon.WeaponeName +
                  ". Caricatore: " + currentAmmo[weapon] + "/" + weapon.WeaponeCapacity +
                  " | Riserva: " + inventoryAmmo[weapon]);

        return ammoToAdd > 0;
    }

    public bool TryConsumeAmmo(SO_Weapone weapon)
    {
        if (weapon == null) return false;

        if (!currentAmmo.ContainsKey(weapon)) return false;

        if (currentAmmo[weapon] > 0)
        {
            currentAmmo[weapon]--;
            Debug.Log("Munizione consumata da " + weapon.WeaponeName + ". Munizioni rimanenti: " + currentAmmo[weapon] + "/" + weapon.WeaponeCapacity);
            return true;
        }
        else
        {
            Debug.Log("Nessuna munizione disponibile per " + weapon.WeaponeName);
            return false;
        }
    }

    public int GetCurrentAmmo(SO_Weapone weapon)
    {
        if (weapon == null) return 0;
        if (!currentAmmo.ContainsKey(weapon)) return 0;
        return currentAmmo[weapon];
    }

    public int GetInventoryAmmo(SO_Weapone weapon)
    {
        if (weapon == null) return 0;
        if (!inventoryAmmo.ContainsKey(weapon)) return 0;
        return inventoryAmmo[weapon];
    }

    public void SetCurrentAmmo(SO_Weapone weapon, int value)
    {
        if (weapon == null) return;
        currentAmmo[weapon] = Mathf.Clamp(value, 0, weapon.WeaponeCapacity);
    }

    public void ConsumeInventoryAmmo(SO_Weapone weapon, int amount)
    {
        if (weapon == null) return;
        if (!inventoryAmmo.ContainsKey(weapon)) return;
        inventoryAmmo[weapon] = Mathf.Max(0, inventoryAmmo[weapon] - amount);
    }
}
