using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _healthRegenerationRate = 0.5f;
    private float _health;
    public event Action OnDie;

    public bool IsDead => _health == 0;

    private void Start()
    {
        ResetHealth();
        StartHealthRegeneration();
    }

    private void Update()
    {
        if (_health < _maxHealth)
        {
            StartHealthRegeneration();
        }
        Debug.Log(_health);
    }

    private void ResetHealth()
    {
        _health = _maxHealth;
    }

    public void StartHealthRegeneration()
    {
        StartCoroutine(HealthRegeneration());
    }

    private IEnumerator HealthRegeneration()
    {
        while (_health < _maxHealth)
        {
            if (_health > 0)
            {
                _health += Time.deltaTime * _healthRegenerationRate;
                if (_health >= _maxHealth)
                {
                    _health = 100f;
                }
                //float amountToRegenerate = Time.deltaTime / _regenerationRate;
                //_health = Mathf.Min(_health + amountToRegenerate, _maxHealth);
                //Debug.Log("체력 회복: " + _health);
            }
            yield return new WaitForSeconds(1f); // 1초마다 체크
        }
    }

    public void TakeDamage(int damage)
    {
        if (_health <= 0) return;

        _health = Mathf.Max(_health - damage, 0);

        if (_health <= 0)
        {
            OnDie?.Invoke();
            Debug.Log("죽었어요.");
        }
        //else if (_health > 0 && _health < _maxHealth)
        //{
        //    StartHealthRegeneration();
        //}
    }

    // 테스트용
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(50);
            Debug.Log("적과 충돌! 현재 체력은 : " + _health);
        }
    }
}
