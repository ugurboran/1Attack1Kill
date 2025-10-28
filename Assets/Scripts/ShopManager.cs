using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    private void Awake() {
        Instance = this;
    }

    public Button buttonShop1;
    public Button buttonShop2; 
    public Button buttonShop3;
    
    public GameObject armour;

    public GameObject shop;

    public EnemyController enemyController;

    private int shieldValue = 0;
    private int swordValue = 0;
    private int hearthValue = 0;


    [SerializeField] TextMeshProUGUI swordtext;
    [SerializeField] TextMeshProUGUI shieldtext;
    [SerializeField] TextMeshProUGUI hearthtext;
    

    public bool anotherLife; // Oyuncumuzun fazladan bir canı olup olmadığını gösteren boolean değer

    public bool thereisArmour;

    // Start is called before the first frame update
    void Start()
    {
        shieldtext.text = shieldValue.ToString();
        hearthtext.text = hearthValue.ToString();
        swordValue = enemyController.damageFromPlayer;
        swordtext.text = swordValue.ToString();
        buttonShop1.onClick.AddListener(() => ShopButtons(buttonShop1)); // Sol seçenek seçildiğinde
        buttonShop2.onClick.AddListener(() => ShopButtons(buttonShop2)); // Orta seçenek seçildiğinde
        buttonShop3.onClick.AddListener(() => ShopButtons(buttonShop3)); // Sağ seçenek seçildiğinde
        anotherLife = false; // Oyuncunun fazladan bir canı yok
        thereisArmour = false; 
    }

    // Update is called once per frame
    void Update()
    {
        ShieldValueControl();
        HearthValueControl();
    }

    public void ShopButtons(Button buttonPressed){
        if(buttonPressed == buttonShop1) // Eğer tıklanan buton leftbutton tagine sahip ise 
        {
            SoundManager.Instance.ShopSoundOn();
            thereisArmour = true;
            UIManager.Instance.ImageShieldActivate();
            shieldValue = shieldValue + 1;
            shieldtext.text = shieldValue.ToString();
            armour.SetActive(true); // Armour yani zırh oyun objesini aktif hale getirme
            ActiveShop(false);// Butonlara sahip olan oyun objesini inaktif eden fonksiyonu çağırma
            PlayerController.Instance.RunStart(); // Oyuncumuzun koşu animasyonunu devam ettiren fonksiyonu çağırma
            PlayerController.Instance.MoveOn(); // Oyuncunun ileri gidişini devam ettiren fonksiyonu çağırma
            PlayerController.Instance.DestroySeller();
        }

        else if(buttonPressed == buttonShop2) 
        {
            SoundManager.Instance.ShopSoundOn();
            anotherLife = true; // Oyuncunun fazladan bir canı var
            UIManager.Instance.ImageHealthActivate();
            hearthValue = hearthValue + 1;
            hearthtext.text = hearthValue.ToString();
            PlayerController.Instance.currentHealth += PlayerController.Instance.currentHealth;
            PlayerController.Instance.healthBar.SetHealth(PlayerController.Instance.currentHealth);
            ActiveShop(false);// Butonlara sahip olan oyun objesini inaktif eden fonksiyonu çağırma
            PlayerController.Instance.RunStart(); // Oyuncumuzun koşu animasyonunu devam ettiren fonksiyonu çağırma
            PlayerController.Instance.MoveOn(); // Oyuncunun ileri gidişini devam ettiren fonksiyonu çağırma
            PlayerController.Instance.DestroySeller();
        }

        else if(buttonPressed == buttonShop3)
        {
            SoundManager.Instance.ShopSoundOn();
            UIManager.Instance.ImageSwordActivate();
            enemyController.damageFromPlayer = enemyController.damageFromPlayer * 2 ; // Oyuncudan düşmana gelen hasarı iki katına çıkarma işlemi, Bir nevi kılıç hasarı iki katına çıkartıldı.
            swordValue = enemyController.damageFromPlayer;
            swordtext.text = swordValue.ToString();
            ActiveShop(false);// Butonlara sahip olan oyun objesini inaktif eden fonksiyonu çağırma
            PlayerController.Instance.RunStart(); // Oyuncumuzun koşu animasyonunu devam ettiren fonksiyonu çağırma
            PlayerController.Instance.MoveOn(); // Oyuncunun ileri gidişini devam ettiren fonksiyonu çağırma
            PlayerController.Instance.DestroySeller();
        }
    }

    public void ShieldValueControl(){
        if(shieldValue == 1){
            buttonShop1.enabled = false;
        }
        else{
            buttonShop1.enabled = true;
        }
    }

    public void HearthValueControl(){
        if(hearthValue == 1){
            buttonShop2.enabled = false;
        }
        else{
            buttonShop2.enabled = true;
        }
    }

    public void ActiveShop(bool active) // Satış seçeneklerini aktif eden fonksiyon
    {
        shop.SetActive(active); // Shop satış oyun objemizi aktif hale getirme (Böylece satıştaki butonlar ekranda görünecek)
    }

    public void ArmourCharacterChoose(){
        armour = GameObject.FindGameObjectWithTag("Armour");
        armour.SetActive(false);
    }

    public void ArmourValueDown(){
        shieldValue = shieldValue - 1;
        shieldtext.text = shieldValue.ToString();
    }

    public void HearthValueDown(){
        hearthValue = hearthValue - 1;
        if(hearthValue <= 0){
            hearthValue = 0;
        }
        hearthtext.text = hearthValue.ToString();    
    }
}
