using UnityEngine;
using UnityEngine.Advertisements;
public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public static AdsManager Instance;
    [SerializeField] string _androidGameId = "4992331"; // Senin kendi gameId 'in bunu şuan boşver boş kalabilir.
    [SerializeField] string _iOSGameId = "4992330";  // Senin kendi gameId 'in bunu şuan boşver boş kalabilir.
    [SerializeField] bool _testMode = true;
    private string _gameId;
    private RewardedAds rewardedAd;
    private InterstitialAds interstitialAd;

    // public int isPremium; // Bu alan premium mu değlmi için boolean da yapabilirsin.
    
    private void Awake()
    {
        Instance = this;
        // isPremium = PlayerPrefsManager.Instance.GetInteger("isPremium"); // Bu alanda premium olup olmadığını playerprefsden getiriyorum ona göre reklamları göstericem
        InitializeAds();
    }
    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}