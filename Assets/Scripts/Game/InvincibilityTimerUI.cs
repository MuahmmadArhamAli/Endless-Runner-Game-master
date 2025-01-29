using TMPro;
using UnityEngine;

public class InvincibilityTimerUI : MonoBehaviour{

    [SerializeField] private TextMeshProUGUI invincibilityText;

    private InvincibiltyPowerUp player;

    private void Start() {
        invincibilityText.gameObject.SetActive(false);

        player = FindObjectOfType<InvincibiltyPowerUp>();
    }

    private void Update() {
        if (player.timer == 0f) {
            invincibilityText.gameObject.SetActive(false);
        }
        else {
            invincibilityText.gameObject.SetActive(true);   
            invincibilityText.text = player.timer.ToString();
        }
    }
}
