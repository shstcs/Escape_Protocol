using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private MonsterHealth _monsterHealth;


    private void Start()
    {
        _monsterHealth = GameObject.FindWithTag("Enemy").GetComponent<MonsterHealth>();
    }

    public void Attack()
    {
        _monsterHealth.TakeDamage(Main.Player.GunController.CurrentGun.Damage);
        Debug.Log(_monsterHealth.Health);
        Debug.Log("몬스터가 죽었나요? : " + _monsterHealth.IsDead);
    }

}
