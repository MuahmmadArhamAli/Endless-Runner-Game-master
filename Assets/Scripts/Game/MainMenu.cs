using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI gemsText;
    public TextMeshProUGUI gems2Text;

    public Animator messageAnim;

    private void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        highScoreText.text = "High Score\n" + PlayerPrefs.GetInt("HighScore", 0);
        gemsText.text = PlayerPrefs.GetInt("TotalGems", 0).ToString();
        gems2Text.text = gemsText.text;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
