using UnityEngine;

[CreateAssetMenu(fileName = "Weapone", menuName = "ScriptableObject/CharactersData/Weapone", order = 3)]
public class SO_Weapone : ScriptableObject
{
    [Header("Base Stats")]
    public string WeaponeName;
    public Sprite WeaponeSprite;
    public int WeaponeCapacity;
    public int WeaponeBulletDamage;
    public float WeaponeFireRate;
    public float WeaponeFireRange;
    public float WeaponeReloadingTime;
    public float WeaponeBulletSpeed;

    [Header("References")]
    public GameObject WeaponeBulletPrefab;
}
