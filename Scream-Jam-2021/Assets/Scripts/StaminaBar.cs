using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;

    [Header("Stamina")]
    [SerializeField] private int maxStamina = 1000;
    private int currentStamina;

    [Header("Regeneration & Cost")]
    [Range(1, 5)] [SerializeField] private int cost = 1;
    [SerializeField] private Coroutine regen;

    public static StaminaBar instance;

    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }
    public int CheckStamina() 
    {
        return currentStamina;
    }
    public void Sprinting() 
    {
        if (currentStamina - cost >= 0)
        {
            currentStamina -= cost;
            staminaBar.value = currentStamina;

            if (regen != null) {
                StopCoroutine(regen);
            }
            regen = StartCoroutine(RegenStamina());
        }
    }
    private IEnumerator RegenStamina() 
    {
        yield return new WaitForSeconds(1);

        while (currentStamina < maxStamina) 
        {
            currentStamina += maxStamina / 100;
            staminaBar.value = currentStamina;
            yield return new WaitForSeconds(0.1f);
        }
        regen = null;
    }
}
