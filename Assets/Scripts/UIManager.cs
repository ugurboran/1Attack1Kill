using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake() {
        Instance = this;
    }
    public Image imageHealth;

    public Image imageSword;

    public Image imageShield;

    public GameObject playerDavid;
    public GameObject playerMaria;

    public GameObject rewardedAdsButton;

    public bool choosenOne;
    // Start is called before the first frame update
    void Start()
    {
        ImageHealthActivate();
        ImageSwordActivate();
        ImageShieldActivate();

    }
    

    public void ImageHealthActivate(){
        imageHealth.enabled = true;
    }

    public void ImageHealthDeactivate(){
        imageHealth.enabled = false;
    }

    public void ImageSwordActivate(){
        imageSword.enabled = true;
    }

    public void ImageSwordDeactivate(){
        imageSword.enabled = false;
    }

    public void ImageShieldActivate(){
        imageShield.enabled = true;
    }

    public void ImageShieldDeactivate(){
        imageShield.enabled = false;
    }

    public void PlayerMariaSetActive(){
        playerMaria.SetActive(true);
        SoundManager.Instance.FemaleLetsDoSoundOn();
    }

    public void PlayerDavidSetActive(){
        playerDavid.SetActive(true);
        choosenOne = true;
        SoundManager.Instance.LetsGoSoundOn();
    }

    public void RewardedAdsButtonDeactivate(){
        rewardedAdsButton.SetActive(false);
    }


}
