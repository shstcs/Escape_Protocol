using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 10;
    private float _health;
    public event Action OnDie;

    public bool IsDead => _health == 0;

    [SerializeField] private float _regenerationRate = 1f;

    private void Start()
    {
        _health = _maxHealth;
        StartCoroutine(HealthRegeneration());
    }

    private IEnumerator HealthRegeneration()
    {
        while (_health < _maxHealth)
        {
            yield return new WaitForSeconds(_regenerationRate);
            float amountToRegenerate = 1f * Time.deltaTime / _regenerationRate;
            _health = Mathf.Min(_health + amountToRegenerate, _maxHealth);
            
            Debug.Log("체력 회복: " + _health);
        }
    }

    public void TakeDamage(int damage)
    {
        if (_health == 0) return;
        _health = Mathf.Max(_health - damage, 0);

        if (_health == 0)
            OnDie?.Invoke();
    }



    // 테스트용
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(5);
            Debug.Log("적과 충돌! 현재 체력은 : " + _health);
        }
    }
}
