using UnityEngine;
using UnityEngine.UI;

public class ProfileWithdrawMenuEnable : MonoBehaviour{
    [SerializeField] private Button withdrawButton;
    [SerializeField] private GameObject withdrawMenu;
    [SerializeField] private Button backButton;

    private void Start(){
        withdrawMenu.SetActive(false);

        withdrawButton.onClick.AddListener(()=>{
            withdrawMenu.SetActive(true);
        });

        backButton.onClick.AddListener(()=>{
            withdrawMenu.SetActive(false);
        });
    }
}