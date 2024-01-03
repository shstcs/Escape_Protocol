using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _healthRegenerationRate = 0.5f;
    [SerializeField] private float _alphaDecreaseRate = 30f;
    [SerializeField] private Image _hitImage;
    private bool _isDecreasingAlpha = false;
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
        //Debug.Log(_health);

        if (_isDecreasingAlpha)
        {
            StartDecreaseAlpha();
        }
    }

    private void ResetHealth()
    {
        _health = _maxHealth;
    }

    private void StartHealthRegeneration()
    {
        StartCoroutine(HealthRegeneration());
    }

    private void StartDecreaseAlpha()
    {
        StartCoroutine(DecreaseAlphaOverTimeCoroutine());
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
            }
            yield return new WaitForSeconds(1f); // 1초마다 체크
        }
    }

    private void SetHitImageAlpha(float alpha)
    {
        if (_hitImage != null)
        {
            Color imageColor = _hitImage.color;
            imageColor.a = alpha;
            _hitImage.color = imageColor;
        }
    }


    private void TakeDamage(int damage)
    {
        if (_health <= 0) return;

        _health = Mathf.Max(_health - damage, 0);

        if (_health <= 0)
        {
            OnDie?.Invoke();
            Debug.Log("죽었어요.");
        }
    }

    // 테스트용
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(50);

            // 스프라이트 알파값을 70/255에서 줄어들게 설정
            SetHitImageAlpha(0.27f);
            _isDecreasingAlpha = true;

            Debug.Log("적과 충돌! 현재 체력은 : " + _health);
        }
    }

    private IEnumerator DecreaseAlphaOverTimeCoroutine()
    {
        while (_hitImage.color.a > 0)
        {
            float alphaValue = Mathf.Clamp01(_hitImage.color.a - Time.deltaTime * (_alphaDecreaseRate / 255f));
            SetHitImageAlpha(alphaValue);

            yield return null;
        }

        _isDecreasingAlpha = false; // 알파값이 0이 되면 코루틴 종료
    }
}
