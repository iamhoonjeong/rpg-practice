using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entityVfx;
    private Entity_Stats entityStats;
    private Entity_Health entityHealth;
    private ElementalType currentEffect = ElementalType.None;

    [Header("Electrify effect details")]
    [SerializeField] private GameObject lightningStrikeVfx;
    [SerializeField] private float currentCharge;
    [SerializeField] private float maximumCharge = 1;
    private Coroutine electrifyCo;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        entityHealth = GetComponent<Entity_Health>();
        entityStats = GetComponent<Entity_Stats>();
        entityVfx = GetComponent<Entity_VFX>();
    }

    public void ApplyElectrifyEffect(float duration, float damage, float charge)
    {
        float lightningResistance = entityStats.GetElementalResistance(ElementalType.Lightning);
        float finalCharge = charge * (1 - lightningResistance);
        currentCharge = currentCharge + finalCharge;

        if (currentCharge >= maximumCharge)
        {
            DoLightningStrike(damage);
            StopElectrifyEffect();
            return;
        }

        if (electrifyCo != null)
            StopCoroutine(electrifyCo);

        electrifyCo = StartCoroutine(ElectrifyEffectCo(duration));
    }

    private IEnumerator ElectrifyEffectCo(float duration)
    {
        currentEffect = ElementalType.Lightning;
        entityVfx.PlayerOnStatusVfx(duration, ElementalType.Lightning);

        yield return new WaitForSeconds(duration);
        StopElectrifyEffect();
    }

    private void StopElectrifyEffect()
    {
        currentEffect = ElementalType.None;
        currentCharge = 0;
        entityVfx.StopAllVfx();
    }

    private void DoLightningStrike(float damage)
    {
        Instantiate(lightningStrikeVfx, transform.position, Quaternion.identity);
        entityHealth.ReduceHealth(damage);
    }

    public void ApplyBurnEffect(float duration, float fireDamage)
    {
        float fireResistance = entityStats.GetElementalResistance(ElementalType.Fire);
        float finalDamage = fireDamage * (1 - fireResistance);

        StartCoroutine(BurnEffectCo(duration, finalDamage));
    }

    private IEnumerator BurnEffectCo(float duration, float totalDamage)
    {
        currentEffect = ElementalType.Fire;
        entityVfx.PlayerOnStatusVfx(duration, ElementalType.Fire);

        int ticksPerSecond = 2;
        int tickCount = Mathf.RoundToInt(ticksPerSecond * duration);

        float damagePerTick = totalDamage / tickCount;
        float tickInterval = 1f / ticksPerSecond;

        for (int i = 0; i < tickCount; i++)
        {
            entityHealth.ReduceHealth(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }

        currentEffect = ElementalType.None;
    }

    public void ApplyChillEffect(float duration, float slowMultiplier)
    {
        float iceResistance = entityStats.GetElementalResistance(ElementalType.Ice);
        float finalDuration = duration * (1 - iceResistance);

        StartCoroutine(ChillEffectCo(finalDuration, slowMultiplier));
    }

    private IEnumerator ChillEffectCo(float duration, float slowMultiplier)
    {
        entity.SlowDownEntity(duration, slowMultiplier);
        currentEffect = ElementalType.Ice;
        entityVfx.PlayerOnStatusVfx(duration, ElementalType.Ice);

        yield return new WaitForSeconds(duration);
        currentEffect = ElementalType.None;
    }

    public bool CanBeApplied(ElementalType element)
    {
        if (element == ElementalType.Lightning && currentEffect == ElementalType.Lightning)
            return true;

        return currentEffect == ElementalType.None;
    }
}
