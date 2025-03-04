using UnityEngine;
using UnityEngine.UI;

public class WatchAdd : MonoBehaviour
{
    [SerializeField] private AndroidNotification androidNotification;
    [SerializeField] private IOSNotification iOSNotification;
    [SerializeField] private RewardedAds rewardedAds;
    private Button button;

    public string timeExitedKey = "timerExited";
    private float elapsedTime = 0f;
    private bool oneHourPassed = false;
    private const float testThreshold = 3600f; // Change this to 3600f for 1 hour

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            button.interactable = false;
            rewardedAds.ShowRewardedAd();
            #if UNITY_ANDROID
            androidNotification.SendNotification("Ad Avaialable", "Watch Ad to Get reward", (int)testThreshold);
            #endif

            #if UNITY_IOS
            iOSNotification.SendNotification("Ad Avaialable", "A new ad is available to watch", "Watch to get reward",(int)testThreshold);
            #endif
        });

        CheckTimeElapsed();
    }

    private void Update()
    {
        if (!oneHourPassed) // Only check until the threshold has passed
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= testThreshold)
            {
                oneHourPassed = true;
                button.interactable = true;
            }
        }
    }

    private void CheckTimeElapsed()
    {
        if (PlayerPrefs.HasKey(timeExitedKey))
        {
            float lastExitTime = PlayerPrefs.GetFloat(timeExitedKey);
            float currentTime = (float)System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            float timeSinceExit = currentTime - lastExitTime;

            if (timeSinceExit >= testThreshold) // 50 seconds for testing
            {
                oneHourPassed = true;
                button.interactable = true;
            }
            else{
                elapsedTime = timeSinceExit;
            }
        }
    }
}
