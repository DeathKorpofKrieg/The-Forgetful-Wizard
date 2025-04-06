using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] HPBar hpBar;
    public float MaxHP = 500;
    private float currentHP;
    private float damageReduction = 0f;
    private int armorTurns = 0;
    private string lastUsedSpell;

    void Start()
    {
        currentHP = MaxHP;
        hpBar.SetHP(currentHP, MaxHP);
    }

    public void TakeDamage(float damage)
    {
        // Apply damage reduction if any
        float finalDamage = damage * (1 - damageReduction);

        // Reduce HP
        currentHP -= finalDamage;
        if (currentHP < 0) currentHP = 0;

        // Update UI
        hpBar.SetHP(currentHP, MaxHP);

        // Decrease armor turns
        if (armorTurns > 0)
        {
            armorTurns--;
            if (armorTurns == 0) damageReduction = 0; // Remove armor effect
        }
    }

    public void ApplySpellEffect(Spell spell)
    {
        lastUsedSpell = spell.Name;

        if (spell.Name == "Stone Armour")
        {
            damageReduction = 0.5f; // Reduce damage by 50%
            armorTurns = 2; // Lasts for 2 turns
        }
    }

    public string GetLastUsedSpell() => lastUsedSpell;
    public bool IsAlive() => currentHP > 0;
}