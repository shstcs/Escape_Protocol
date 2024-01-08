using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Fields

    private MonsterHealth _monsterHealth;

    #endregion

    #region MonoBehaviours

    private void Start()
    {
        _monsterHealth = GameObject.FindWithTag("Enemy").GetComponent<MonsterHealth>();
    }

    #endregion

    #region Methods

    public void Attack()
    {
        _monsterHealth.TakeDamage(Main.Player.GunController.CurrentGun.Damage);
        Debug.Log(_monsterHealth.Health);
        Debug.Log("몬스터가 죽었나요? : " + _monsterHealth.IsDead);
    }

    #endregion
}
