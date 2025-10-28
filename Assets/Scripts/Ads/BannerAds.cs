using UnityEngine;
using UnityEngine.Advertisements;
public class BannerAds : MonoBehaviour
{
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.TOP_CENTER;
    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null;
    void Start()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
                _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
        Advertisement.Banner.SetPosition(_bannerPosition);
        LoadBanner();
    }
    public void LoadBanner()
    {
        // if (AdsManager.Instance.isPremium == 0) // Sadece premium değilse banner reklamları gösteriyorum
        // {
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };
            // Load the Ad Unit with banner content:
            Advertisement.Banner.Load(_adUnitId, options);
        // }
    }
    public void OnBannerLoaded()
    {
        Debug.Log("Banner loaded and ready to show.");
        ShowBannerAd();
    }
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }
    void ShowBannerAd()
    {
        // if (AdsManager.Instance.isPremium == 0)
        // {
            BannerOptions options = new BannerOptions
            {
                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHidden,
                showCallback = OnBannerShown
            };
            Advertisement.Banner.Show(_adUnitId, options);
        // }
    }
    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }
}