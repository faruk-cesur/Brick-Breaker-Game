using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener
{
    
    // Setting Up Phone Type
    [SerializeField] private string _androidId;
    [SerializeField] private string _appleId;
    private string _gameId;

    private const string _bannerAdId = "Banner_Android";

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            _gameId = _androidId;
        }
        else
        {
            _gameId = _appleId;
        }

        InitializeAds();
    }

    
    // Initialize Ads And Show
    public void InitializeAds()
    {
        Advertisement.Initialize(_gameId, true);
        ShowBannerAd();
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        print("Failed " + message);
    }

    
    // Showing Banner Ad
    private void ShowBannerAd()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        BannerLoadOptions options = new BannerLoadOptions();
        options.loadCallback = OnLoad;
        options.errorCallback = OnError;
        Advertisement.Banner.Load(_bannerAdId, options);
        Advertisement.Banner.Show();
    }

    private void OnError(string message)
    {
        print("OnError " + message);
        Advertisement.Banner.Hide();
    }


    private void OnLoad()
    {
        print("OnLoad");
    }
}