using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool isCrash; // Karakterin bir nesneye çarpıp durması ve durmamasının simgeleyen bool değer
    public float speed;
    public Animator animator;
    public Transform parentTransform;
    public GameObject lastEnemy;
    public int maxHealth = 4;
    public int currentHealth;
    public HealthBar healthBar;
    private GameObject trailSword;

    public bool healthUsed;
    public EnemyController enemyController;

    private GameObject seller;

    public static PlayerController Instance;

    private void Awake() {
        Instance = this;
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth); // Oyuncu can barı objesinin maximum can değerine eşitlenmesi
        animator = GetComponent<Animator>(); // Oyuncu oyun objesinin animator componentini animator adlı değişkene atama
        healthUsed = false;
        trailSword = GameObject.FindGameObjectWithTag("Trail");
    }

    void Update()
    {
        if (!isCrash) // Bu değer yanlış ise
        {
            parentTransform.Translate(Vector3.forward * speed * Time.deltaTime); // Karakterin ileri doğru hareketi (Koşması)
            trailSword.SetActive(false);
        }
        else{
            trailSword.SetActive(true);
        }    
    }

    public void MoveOn() // Karakterin hareketinin devam etmesi fonksiyonu
    {
        isCrash = false; // Bu durumda çarpma yok, karakter hareket eder.
    }

    public IEnumerator MoveOnLate(){
        yield return new WaitForSeconds(1f);
        isCrash = false;
    }

    public IEnumerator MoveOnLateForRewardAd(){
        yield return new WaitForSeconds(2f);
        isCrash = false;
    }

    private void OnTriggerEnter(Collider other) // Trigger Kontrolü
    {
        if (other.gameObject.tag == "Enemy" && isCrash == false) //Triggerlanan objenin tagi Enemy ise ve çarpma değeri çarpma henüz yok ise
        {
            
            if(!UIManager.Instance.choosenOne){
                SoundManager.Instance.FemaleReadySoundOn();
            }
            else if(UIManager.Instance.choosenOne){
                SoundManager.Instance.ReadySoundOn();
                StartCoroutine(FightSoundOnLate());
            }
            lastEnemy = other.gameObject; // Bu triggerlanan obje son düşman değişkenine atanır
            isCrash = true; // Artık çarpma değeri doğru değerini dönderir
            animator.SetTrigger("StopActivate"); // Karakterin durma animasyonunu çağıran trigger
            StartCoroutine(CountDownTimer.Instance.CountDownCoroutine());
            StartCoroutine(CountDownTimer.Instance.CountDownTimerActivateCoroutine()); 
            ButtonsActivate();
            
        }
        else if (other.gameObject.tag == "MileStone" && isCrash == false) ////Triggerlanan objenin tagi MileStone ise ve çarpma değeri çarpma henüz yok ise
        {
            isCrash = true; // Artık çarpma değeri doğru değerini dönderir
            animator.SetTrigger("StopActivate"); // Karakterin durma animasyonunu çağıran trigger
            ShopManager.Instance.ActiveShop(true); // Satıcı ekranını aktif eden fonksiyonunu çağırma
        }
    }

    public IEnumerator FightSoundOnLate(){
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.FightSoundOn(); 
    }
    public void DestroySeller() // Satıcı oyun objesini yok etme
    {
        GameObject seller = GameObject.FindGameObjectWithTag("TheBoss");
        Destroy(seller);// Satıcı oyun objesini yok etme
    }

    public IEnumerator AttackEnemy() // Oyuncumuz ile Düşmana saldırma
    {
        CountDownTimer.Instance.CountDownRestart();
        yield return new WaitForSeconds(1f);
        DestroyEnemyWithoutCoroutine(); // Düşmanı yok eden fonksiyonu çağırma
        yield return new WaitForSeconds(3f);
        isCrash = false; // Ardından çarpma değeri yanlış hale getirilir, böylece oyuncumuzun hareketi koşusu devam eder ???????????
        MusicManager.Instance.MusicOn(); // Müziğin aktivasyonu
    }
    
    public IEnumerator DestroyEnemy(){ // Düşman oyun objesinin yok edilmesi
        if(UIManager.Instance.choosenOne){
            SoundManager.Instance.EnemyDefeatSoundOn();
        }
        else if(!UIManager.Instance.choosenOne){
            SoundManager.Instance.FemaleEnemyDefeatSoundOn();
        }
        
        yield return new WaitForSeconds(0.5f);
        Destroy(lastEnemy); // Düşmanı yok etme
    }

    public void DestroyEnemyWithoutCoroutine(){ // Düşman oyun objesinin yok edilmesi
        Destroy(lastEnemy); // Düşmanı yok etme    
    }

    public IEnumerator TakeDamage(int damageFromEnemy) // Hasar alma fonksiyonu
    {
        yield return new WaitForSeconds(2f);
        Debug.Log(currentHealth);
        currentHealth -= damageFromEnemy; // Hasar değerinin anlık değerden düşmesi
        if(currentHealth <= 0){ // Oyuncumuzun canı 0 dan küçük veya eşit olduğu durumlarda 
            currentHealth = 0; // Değerimiz negatife inmesin, 0'a eşitle
            }
        healthBar.SetHealth(currentHealth); // Yeni değerin can değerimize atanması fonksiyonu çağırma            
    }

    public void RebirthCharacter(){
        animator.SetTrigger("StandUp"); // Karakterin canlanıp yeniden ayağa kalkma animasyonu
    }

    public void RunActivate(){
        animator.SetTrigger("RunActivate"); // Koşu animasyonunu çağırma aktivasyonu
    }

    public void RunStart()
    {
        animator.Play("Base Layer.sword_run"); // Deneme
    }

    public void StopActivate(){
        animator.SetTrigger("StopActivate"); // Karakterin durma animasyonunu çağıran trigger
    }

    public void ButtonsActivate(){
        ButtonManager.Instance.GenerateRandomColor(); // Butonlar için rastgele renk üreten fonksiyonu çağırma
        StartCoroutine(ButtonManager.Instance.ActivateButtons()); // Butonların aktif edilmesi fonksiyonunu çağırma
        MusicManager.Instance.MusicOff(); // Müziğin durdurulması
    }
}
