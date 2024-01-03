using System;
using System.Collections;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _staminaRegenerationRate = 0.5f;
    [SerializeField] private float _staminaConsumptionRate = 20f;

    public float CurrentStamina { get; private set; }

    public float StaminaConsumptionRate
    {
        get { return _staminaConsumptionRate; }
        private set { _staminaConsumptionRate = value; }
    }

    private void Start()
    {
        ResetStamina();
        StartStaminaRegeneration();
    }

    private void Update()
    {
        if (CurrentStamina < _maxStamina)
        {
            StartStaminaRegeneration();
        }
        //Debug.Log(CurrentStamina);
    }

    private void ResetStamina()
    {
        CurrentStamina = _maxStamina;
    }

    public bool CanConsumeStamina(float amount)
    {
        return CurrentStamina - amount >= 0;
    }

    public void ConsumeStamina(float amount)
    {
        if (CanConsumeStamina(amount))
        {
            CurrentStamina -= amount;
        }
    }

    public void StartStaminaRegeneration()
    {
        StartCoroutine(StaminaRegeneration());
        //Debug.Log("코루틴시작");
    }

    private IEnumerator StaminaRegeneration()
    {
        while (CurrentStamina < _maxStamina)
        {
            CurrentStamina += Time.deltaTime * _staminaRegenerationRate;
            if (CurrentStamina >= _maxStamina)
            {
                CurrentStamina = 100f;
            }
            //float amountToRegenerate = Time.deltaTime * _staminaRegenerationRate;
            //CurrentStamina = Mathf.Min(CurrentStamina + amountToRegenerate, _maxStamina);
            yield return new WaitForSeconds(1f);
        }
    }
}
