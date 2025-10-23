
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/CharachtersData/Enemy", order = 2)]

public class SO_Enemie : ScriptableObject
{
    [Header("Base Stats")]
    public string EnemyName;
    public float EnemyMaxLifePoints;
    public float EnemySpeed;
    public Sprite EnemySprite;
    public Transform EnemyTarget;

    [Header("Advance Stats")]
    public float EnemyAttackDamage;
    public float EnemyChaseRange;
    public float EnemyAttackRange;
}
