using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    float currentTime = 0f;
    public float startingTime = 5f;

    public bool timerIsRunning = false;

    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI timetext;

    public EnemyController enemyController;

    public static CountDownTimer Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime; // başlangıç değeri ilk sayaç değeri
        countdownText.enabled = false; // Zaman sayacı görünmez halde
        timetext.enabled = false; // // Zaman sayacı yazısı görünmez halde
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning) // && GameManager.State == GameState.GamePlay
        {
            countdownText.color = Color.green; // Sayaç rengi yeşil
            if (currentTime <= 0)
            {
                currentTime = 0;
                Debug.Log("Attack of Enemy");
                SoundManager.Instance.ClockSoundOff(); // Saat sesinin durdurulması
                StartCoroutine(enemyController.EnemyAttack2Player()); // Düşmanın oyuncumuza saldırı fonksiyonunu çağırma
                ButtonManager.Instance.DeactivateButtons(); // Ardından butonların inaktif edildiği fonksiyonu çağırma
                CountDownTimerDeactivate();
                timerIsRunning = false;
                if(ShopManager.Instance.thereisArmour){
                    StartCoroutine(ButtonManager.Instance.ShieldOn());
                }
                else if(ShopManager.Instance.anotherLife){
                    StartCoroutine(ButtonManager.Instance.AnotherLifeOnCharacter());
                }        
            }
            else if (currentTime <= 1 && currentTime > 0)
            {
                countdownText.color = Color.red; // Süre 1 değerinin altına düştüğünde Kırmızı renk olacak
                currentTime -= 1 * Time.deltaTime; // Sürenin azalması
                countdownText.text = currentTime.ToString("0.0"); // Sayaç değişkeninin string veri yapısına dönüştürülüp ekrandaki mesaja eşitlenmesi, küsüratlı şekilde
            }
            else
            {
                currentTime -= 1 * Time.deltaTime; // Sürenin azalması
                countdownText.text = currentTime.ToString("0.0"); // Sayaç değişkeninin string veri yapısına dönüştürülüp ekrandaki mesaja eşitlenmesi, küsüratlı şekilde
            }
        }
    }
    public void CountDown() // CountDown aktif
    {
        timerIsRunning = true; // Sayacı başlatan boolean değer
        SoundManager.Instance.ClockSoundOn(); // Saat sesinin başlatılması
    }

    public void CountDownRestart(){ // Sayacı durdurma, başlangıç değerine eşitleme ve ekranda görünmez hale getirme
        countdownText.color = Color.green; // Sayaç rengi yeşil
        timerIsRunning = false; // Sayacın çalışmasını istemezsek boolean değer
        currentTime = startingTime;
        countdownText.text = currentTime.ToString("0.0");
        CountDownTimerDeactivate();     
    }

    public void CountDownTimerActivate(){ // Sayacımızın ve sayaç yazımızın görünür hale gelmesi
        countdownText.color = Color.green; // Sayaç rengi yeşil
        countdownText.enabled = true; // Sayacımızın görünür hale gelmesi
        timetext.enabled = true; // Sayaç yazımızın görünür hale gelmesi
        GameManager.Instance.PauseButtonDeactivate();
    }

    public void CountDownTimerDeactivate(){ // Sayacımızın ve sayaç yazımızın görünmez hale gelmesi
        countdownText.enabled = false; // Sayacımızın görünmez hale gelmesi
        timetext.enabled = false; // Sayaç yazımızın görünmez hale gelmesi
        SoundManager.Instance.ClockSoundOff(); // Saat sesinin durdurulması
        GameManager.Instance.PauseButtonActivate();
    }

    public void CountDownImmediateRestart(){ // Sayacı durdurma, başlangıç değerine eşitleme
        countdownText.color = Color.green; // Sayaç rengi yeşil
        timerIsRunning = false; // Sayacın çalışmasını istemezsek boolean değer
        currentTime = startingTime;
        countdownText.text = currentTime.ToString("0.0"); 
        SoundManager.Instance.ClockSoundOff(); // Saat sesinin durdurulması
    }

    public IEnumerator CountDownLate(){ // Belli bir süre sonra sayacı aktif eden fonksiyon(Bunu düşman hasar aldıktan sonra sayacın yeniden başlatılacağı durumlar için kullandım)
        countdownText.color = Color.green; // Sayaç rengi yeşil
        yield return new WaitForSeconds(2f); // 3 Saniyelik bekleyiş
        timerIsRunning = true; // Sayacı başlatan boolean değer
        SoundManager.Instance.ClockSoundOn(); // Saat sesinin başlatılması
    }

    public IEnumerator CountDownCoroutine() // CountDown aktif
    {
        yield return new WaitForSeconds(2f);
        timerIsRunning = true; // Sayacı başlatan boolean değer
        SoundManager.Instance.ClockSoundOn(); // Saat sesinin başlatılması
    }

    public IEnumerator CountDownTimerActivateCoroutine(){ // Sayacımızın ve sayaç yazımızın görünür hale gelmesi
        yield return new WaitForSeconds(2f);
        countdownText.color = Color.green; // Sayaç rengi yeşil
        countdownText.enabled = true; // Sayacımızın görünür hale gelmesi
        timetext.enabled = true; // Sayaç yazımızın görünür hale gelmesi
        GameManager.Instance.PauseButtonDeactivate();
    }

    public IEnumerator CountDownImmediateRestartCoroutine(){ // Sayacı durdurma, başlangıç değerine eşitleme
        yield return new WaitForSeconds(2f);
        countdownText.color = Color.green; // Sayaç rengi yeşil
        timerIsRunning = false; // Sayacın çalışmasını istemezsek boolean değer
        currentTime = startingTime;
        countdownText.text = currentTime.ToString("0.0"); 
        SoundManager.Instance.ClockSoundOff(); // Saat sesinin durdurulması
    }
}
