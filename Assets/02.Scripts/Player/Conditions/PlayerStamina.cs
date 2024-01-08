using System;
using System.Collections;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    #region Fields

    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _staminaRegenerationRate = 0.5f;
    [SerializeField] private float _staminaConsumptionRate = 20f;

    #endregion

    #region Properties

    public float CurrentStamina { get; private set; }

    public float StaminaConsumptionRate
    {
        get { return _staminaConsumptionRate; }
        private set { _staminaConsumptionRate = value; }
    }

    #endregion

    #region MonoBehaviours

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

    #endregion

    #region Methods

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
            yield return new WaitForSeconds(1f);
        }
    }

    #endregion
}
