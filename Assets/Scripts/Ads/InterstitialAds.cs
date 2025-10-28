using UnityEngine;
using UnityEngine.Advertisements;
public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId = "Interstitial_Android";
    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
    }
    private void Start()
    {
        LoadAd();
    }
    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        // if (AdsManager.Instance.isPremium == 0) // sadece premium değilse reklamları gösteriyorum
        // {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load("Interstitial_Android", this);
        // }
    }
    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        // if (AdsManager.Instance.isPremium == 0) // sadece premium değilse reklamları gösteriyorum
        // {
            Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
        // }
    }
    // Implement Load Listener and Show Listener interface methods:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Insterstitial Loaded");
    }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }
    public void OnUnityAdsShowStart(string adUnitId)
    {
        // TimeManager.Instance.PauseGame();
    }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        // TimeManager.Instance.ResumeGame();
    }
}