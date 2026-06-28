using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    [Header("Heneral details")]
    [SerializeField] protected SkillType skillType;
    [SerializeField] protected SkillUpgradeType upgradeType;
    [SerializeField] private float cooldown;
    private float lastTimeused;

    protected virtual void Awake()
    {
        lastTimeused = lastTimeused - cooldown;
    }

    public void SetSkillUpgrade(UpgradeData upgrade)
    {
        upgradeType = upgrade.upgradeType;
        cooldown = upgrade.cooldown;
    }

    public bool CanUseSkill()
    {
        if (OnCooldown())
        {
            print("ON cooldown");
            return false;
        }

        return true;
    }

    protected bool Unlocked(SkillUpgradeType upgradeToCheck) => upgradeType == upgradeToCheck;

    private bool OnCooldown() => Time.time < lastTimeused + cooldown;
    public void SetSkillOnCooldown() => lastTimeused = Time.time;
    public void ResetCooldownBy(float cooldownReduction) => lastTimeused = lastTimeused + cooldownReduction;
    public void ResetCooldown() => lastTimeused = Time.time;
}
