using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] TMP_Text dialogText;
    [SerializeField] int letterPerSecond;
    [SerializeField] GameObject actionSelector;
    [SerializeField] List<TMP_Text> spellTexts;

    void Start()
    {
        dialogText.color = Color.black;
    }

    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/letterPerSecond);
        }
    }

    public void OnSpellButtonClicked(int index)
    {
        if (battleSystem != null)
            battleSystem.OnSpellSelected(index);
        else
            Debug.LogError("BattleSystem reference is missing in BattleDialogBox.");
    }
}
