using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private MonsterHealth _monsterHealth;


    private void Awake()
    {
        _monsterHealth = GameObject.Find("Monster").GetComponent<MonsterHealth>();
    }

    public void Attack()
    {
        _monsterHealth.TakeDamage(40); // _gun.Damage로 수정 필요
        Debug.Log(_monsterHealth.Health);
        Debug.Log("몬스터가 죽었나요? : " + _monsterHealth.IsDead);
    }

}
