using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI gemsText;
    public TextMeshProUGUI gems2Text;

    public Animator messageAnim;

    [SerializeField] private WatchAdd watchAdd;
    [SerializeField] private PaystackConverter paystackConverter;

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
        PlayerPrefs.SetFloat(watchAdd.timeExitedKey, (float)System.DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        PlayerPrefs.SetString(paystackConverter.lastQuitTime, DateTime.Now.ToString());
        PlayerPrefs.SetFloat(watchAdd.timeExitedKey, (float)System.DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        PlayerPrefs.Save();
        Application.Quit();
    }
}
