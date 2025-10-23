
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObject/CharachtersData/Player", order = 2)]
public class SO_Player : ScriptableObject
{
    [Header("Base Stats")]
    public string PlayerName;
    public float PlayerMaxLifePoints;
    public float PlayerSpeed;
    public float PlayerRunningSpeed;
    public Sprite PlayerSprite;

    [Header("Advanced Stats")]
    public float PlayerAttackCoolDown;
    public float PlayerWeaponeRange;

    [Header("Weapone Damage")]
    public int PlayerKnifeDamage;

}
