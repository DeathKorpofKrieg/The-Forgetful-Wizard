using UnityEngine;
using UnityEngine.UI;  // Required for UI elements
using TMPro;  // Required for TMP_Text (TextMeshPro)

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject healthBar;  // The actual health bar image (UI Image)
    [SerializeField] TMP_Text hpText;   // Text component to show current health
    [SerializeField] float maxHP = 500f; // Max health
    private float currentHP;            // Current health

    void Start()
    {
        // Set the initial health to maxHealth
        currentHP = maxHP;

        // Update the health bar and text on startup
        SetHP(currentHP, maxHP);
    }

    public void TakeDamage(float damage)
    {
        // Reduce health by damage, but make sure it doesn't go below 0
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        // Update the health bar and text after taking damage
        SetHP(currentHP, maxHP);

        // Check if game over
        if (currentHP == 0)
        {
            GameOver();
        }
    }

    public void SetHP(float current, float max)
    {
        // Update health bar scale (1 corresponds to full health)
        float healthPercentage = current / max;
        healthBar.transform.localScale = new Vector3(healthPercentage, 1f, 1f);

        // Update health text (e.g., "100/500")
        hpText.text = current.ToString("0") + "/" + max.ToString("0");
    }

    private void GameOver()
    {
        // Implement game over logic here (this can trigger UI, animations, etc.)
        Debug.Log("Game Over! Your health reached 0.");
    }
}
