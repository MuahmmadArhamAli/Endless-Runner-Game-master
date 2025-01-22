using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour{
    [SerializeField] private GameObject earningsMenu;
    [SerializeField] private GameObject profileMenu;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private Button earningsButton;
    [SerializeField] private Button profileButton;
    [SerializeField] private Button backButton;

    private void Awake() {
        earningsButton.onClick.AddListener(() => {
            earningsMenu.SetActive(true);
            mainMenu.SetActive(false);
            backButton.gameObject.SetActive(true);
        });

        profileButton.onClick.AddListener(() => {
            profileMenu.SetActive(true);
            mainMenu.SetActive(false);
            backButton.gameObject.SetActive(true);
        });

        backButton.onClick.AddListener(() => {
            earningsMenu.SetActive(false);
            mainMenu.SetActive(true);
            backButton.gameObject.SetActive(false);
            profileMenu.SetActive(false);
        });
    }
}
