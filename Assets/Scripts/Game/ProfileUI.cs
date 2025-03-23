using TMPro;
using UnityEngine;

public class ProfileUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ngnText;

    private void Update(){
        ngnText.text = "NGN : " + PlayerPrefs.GetInt("NGN", 0);
    }
}
