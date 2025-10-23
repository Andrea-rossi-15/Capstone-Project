using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public SO_Weapone weaponData; // riferimento allo ScriptableObject dell'arma

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null && weaponData != null)
            {
                // AddWeapon ora ritorna un bool: true se pickup consumato, false se ignorato
                bool collected = inventory.AddWeapon(weaponData);

                if (collected)
                {
                    SoundManager.PlaySound(SoundType.TAKEBULLET);
                    Destroy(gameObject); // pickup consumato
                }
                else
                {
                    Debug.Log("Pickup ignorato: munizioni già al massimo per " + weaponData.WeaponeName);
                }
            }
        }
    }
}
