using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Image _enemyLifeBar;
    [SerializeField] SO_Enemie EnemyData;

    private EnemyBaseDamageController _enemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        _enemyDamage = GetComponent<EnemyBaseDamageController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyDamage != null && _enemyLifeBar != null)
        {
            float _barFill = (float)_enemyDamage._enemyCurrentHP / EnemyData.EnemyMaxLifePoints;
            _enemyLifeBar.fillAmount = _barFill;
        }
    }
}
