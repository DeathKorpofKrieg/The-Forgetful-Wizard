using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BattleState { PlayerMove, BossMove, GameOver }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleDialogBox dialogBox;
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit bossUnit;

    BattleState state;
    List<Spell> spells;

    void Start()
    {
        spells = new List<Spell>()
        {
            new Spell("Fireball", 60),
            new Spell("Lightning", 55, true),
            new Spell("Riptide", 30),
            new Spell("Frostbite", 40, true),
            new Spell("Earthquake", 25),
            new Spell("Stone Armour", 0, false, true)
        };

        StartCoroutine(dialogBox.TypeDialog("Defeat the dungeon boss."));
        state = BattleState.PlayerMove;
    }

    public void OnSpellSelected(int spellIndex)
    {
        if (state != BattleState.PlayerMove) return;

        Spell selectedSpell = spells[spellIndex];
        StartCoroutine(PlayerTurn(selectedSpell));
    }

    private IEnumerator PlayerTurn(Spell spell)
    {
        dialogBox.SetDialog($"You used {spell.Name}!");
        yield return new WaitForSeconds(1f);

        ApplySpellEffects(playerUnit, bossUnit, spell);
        
        if (!bossUnit.IsAlive())
        {
            EndGame(true);
            yield break;
        }

        state = BattleState.BossMove;
        StartCoroutine(BossTurn());
    }

    private IEnumerator BossTurn()
    {
        Spell chosenSpell = spells[Random.Range(0, spells.Count)];
        dialogBox.SetDialog($"Boss used {chosenSpell.Name}!");
        yield return new WaitForSeconds(1f);

        ApplySpellEffects(bossUnit, playerUnit, chosenSpell);

        if (!playerUnit.IsAlive())
        {
            EndGame(false);
            yield break;
        }

        state = BattleState.PlayerMove;
        dialogBox.SetDialog("Choose your spell!");
    }

    private void ApplySpellEffects(BattleUnit caster, BattleUnit target, Spell spell)
    {
        float damage = spell.Damage;

        // Check for spell interactions
        if (spell.Name == "Lightning" || spell.Name == "Frostbite")
        {
            if (caster.GetLastUsedSpell() == "Riptide")
                damage *= 2; // Double damage if Riptide was used last turn
        }

        if (spell.Name == "Frostbite")
        {
            target.ApplySpellEffect(new Spell("Frostbite Effect", 0)); // Apply debuff
        }

        if (spell.Name == "Earthquake")
        {
            if (target.GetLastUsedSpell() == "Stone Armour")
            {
                target.ApplySpellEffect(new Spell("Stone Armour Removed", 0));
            }
        }

        target.TakeDamage(damage);
        caster.ApplySpellEffect(spell);
    }

    private void EndGame(bool playerWins)
    {
        state = BattleState.GameOver;
        dialogBox.SetDialog(playerWins ? "You win!" : "Game Over!");
    }
}
