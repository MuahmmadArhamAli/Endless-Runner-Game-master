using UnityEngine;
using UnityEngine.UI;

public class WatchAdd : MonoBehaviour
{
    [SerializeField] private RewardedAds rewardedAds;
    Button button;

    private void Awake(){
        button = GetComponent<Button>();

        button.onClick.AddListener(()=>{
            rewardedAds.ShowRewardedAd();
        });
    }
}
