using UnityEngine;
using System.Collections.Generic;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] HPBar hpBar;
    public float MaxHP = 500;
    private float currentHP;
    private float damageReduction = 0f;
    private int armorTurns = 0;
    private string lastUsedSpell;

    public List<Spell> spellList;  // For AI use

    void Start()
    {
        currentHP = MaxHP;
        hpBar.SetHP(currentHP, MaxHP);
    }

    public void SetSpellList(List<Spell> spells)
    {
        spellList = spells;
    }

    public float SimulateDamageTaken(float incomingDamage)
    {
        return incomingDamage * (1 - damageReduction);
    }

    public void TakeDamage(float damage)
    {
        float finalDamage = SimulateDamageTaken(damage);
        currentHP -= finalDamage;
        if (currentHP < 0) currentHP = 0;
        hpBar.SetHP(currentHP, MaxHP);

        if (armorTurns > 0)
        {
            armorTurns--;
            if (armorTurns == 0) damageReduction = 0;
        }
    }

    public void ApplySpellEffect(Spell spell, BattleUnit source)
    {
        lastUsedSpell = spell.Name;

        if (this == source)
        {
            if (spell.Name == "Stone Armour")
            {
                damageReduction = 0.5f;
                armorTurns = 2;
            }

            if (spell.Name == "Frostbite" & damageReduction == 0f)
            {
                damageReduction = 0.25f;
                armorTurns = 1; 
            }

            if (spell.Name == "Frostbite" & damageReduction == 0.5f)
            {
                damageReduction = 0.62f;
                armorTurns = 1;
            }
        }
        
        if (spell.Name == "Earthquake")
        {
            if (armorTurns > 0)
            {
                armorTurns = 0;
                damageReduction = 0f;
            }
        }
    }

    public string GetLastUsedSpell() => lastUsedSpell;
    public bool IsAlive() => currentHP > 0;
}