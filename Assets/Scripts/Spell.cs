using UnityEngine;

public class Spell
{
    public string Name { get; private set; }
    public float Damage { get; private set; }
    public bool IsMultiplierSpell { get; private set; }
    public bool IsArmor { get; private set; }
    
    public Spell(string name, float damage, bool isMultiplierSpell = false, bool isArmor = false)
    {
        Name = name;
        Damage = damage;
        IsMultiplierSpell = isMultiplierSpell;
        IsArmor = isArmor;
    }
}