using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public HealthBar healthBar;
    public EnemyController enemyController;

    public bool rewardedAdsUsed = false;
    [SerializeField] private Button _showAdButton;
    [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] private string _iOSAdUnitId = "Rewarded_iOS";
    private string _adUnitId = "Rewarded_Android"; // This will remain null for unsupported platforms
    private string _adState = "WAITING";
    private void Start()
    {
        rewardedAdsUsed = false;
#if UNITY_IOS
                _adUnitId = "Rewarded_iOS";
#elif UNITY_ANDROID
        _adUnitId = "Rewarded_Android";
#endif
        //Disable the button until the ad is ready to show:
        _showAdButton.interactable = false;
        LoadAd();
        Debug.Log("Rewarded Ads Start");
    }
    // Load content to the Ad Unit:
    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
        if (adUnitId.Equals(_adUnitId))
        {
            _showAdButton.onClick.AddListener(ShowAd);
            _showAdButton.interactable = true;
        }
    }
    public void ShowAd()
    {
        _showAdButton.interactable = true;
        Advertisement.Show(_adUnitId, this);
    }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) // Reklamı izlediyse bu fonksiyon çalışır
    {
        
        if (_adState != "WAITING")
            return;
        _adState = "IN_PROGRESS";
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))  // Reklamı tamamen izlediyse bu fonksiyon çalışır
        {
            // Bu kısımda reklamı tamamen izledi ve ödülü hakettiği için ödüllendirilir .
            Debug.Log("Unity Ads Rewarded Ad Completed");
            PlayerController.Instance.DestroyEnemyWithoutCoroutine();
            enemyController.enemyCount++;
            GameManager.Instance.TryAgainButtonDeactivate();
            GameManager.Instance.UpdateGameState(GameState.GamePlay);
            //StartCoroutine(ButtonManager.Instance.AnotherLifeOnCharacter());
            ButtonManager.Instance.AnotherLifeWithoutCoroutine();
            CameraManager.Instance.CameraZoomIn();
            healthBar.SetMaxHealth(1);
            MusicManager.Instance.MusicOn();
            _showAdButton.onClick.RemoveAllListeners();
            // Load another ad:
            Advertisement.Load(_adUnitId, this);
            rewardedAdsUsed = true;
            if(rewardedAdsUsed){
                UIManager.Instance.RewardedAdsButtonDeactivate();
            }
            
        }
        else // reklamı skip yaptıysa buraya girer ödüllendirilmez.
        {
            // dont give the rewards
        }
        // TimeManager.Instance.ResumeGame();
        // GameManager.Instance.UpdateGameState(GameState.MAINMENU);
    }
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        // TimeManager.Instance.ResumeGame();
        // GameManager.Instance.UpdateGameState(GameState.MAINMENU);
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        // TimeManager.Instance.ResumeGame();
        // GameManager.Instance.UpdateGameState(GameState.MAINMENU);
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
    public void OnUnityAdsShowStart(string adUnitId)
    {
        // TimeManager.Instance.PauseGame();
        _adState = "WAITING";
    }
    public void OnUnityAdsShowClick(string adUnitId)
    {
    }
    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
}