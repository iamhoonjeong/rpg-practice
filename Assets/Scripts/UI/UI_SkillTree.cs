using UnityEngine;

public class UI_SkillTree : MonoBehaviour
{

    [SerializeField] int skillPoints;
    [SerializeField] private UI_TreeConnectHandler[] parentNodes;
    public Player_SkillManager skillManager { get; private set; }

    private void Awake()
    {
        skillManager = FindFirstObjectByType<Player_SkillManager>();
    }

    private void Start()
    {
        UpdateAllConnections();
    }


    [ContextMenu("Reset skill tree")]
    public void RefundAllSkills()
    {
        UI_TreeNode[] skillNodes = GetComponentsInChildren<UI_TreeNode>();

        foreach (var node in skillNodes)
        {
            node.Refund();
        }
    }

    public bool EnooughSkillPoint(int cost) => skillPoints >= cost;
    public void RemoveSkillPoint(int cost) => skillPoints -= cost;
    public void AddSkillPoints(int points) => skillPoints += points;

    [ContextMenu("Update All Connections")]
    public void UpdateAllConnections()
    {
        foreach (var node in parentNodes)
        {
            node.UpdateAllConnections();
        }
    }
}
