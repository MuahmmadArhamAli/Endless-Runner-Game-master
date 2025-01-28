using TMPro;
using UnityEngine;

public class ScoreMultiplierUI : MonoBehaviour{

    [SerializeField] private TileManager tileManager;
    [SerializeField] private TextMeshProUGUI scoreMultText;

    private void Update(){
        if (tileManager.scoreMultiplier == 1){
            scoreMultText.gameObject.SetActive(false);
            return;
        }

        scoreMultText.gameObject.SetActive(true);
        scoreMultText.text = "X" + tileManager.scoreMultiplier.ToString();
    }

}