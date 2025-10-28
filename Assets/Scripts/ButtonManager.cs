using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;

    private void Awake() {
        Instance = this;
    }

    public List<Button> button_List;

    public Button button1;
    public Button button2; 
    public Button button3;

    public bool checkdefeat;

    public EnemyController enemyController;

    public int i ;
    public bool attackIsDone = false;

    private void Start() {
        ButtonManagerActivate(); // Hangi butona tıklandığında neler olacağını çağıran fonksiyon
        DeactivateButtons(); // Butonların ekranda görünürlüğünü deaktif etme işlemi
    }

    public void buttonCallBack(Button buttonPressed)
    {        
        i++;
        if (buttonPressed == button1) // Sol seçenek
        {
            if(buttonPressed.image.color == Color.green ) // Eğer seçilen buton yeşilse
            {
                SoundManager.Instance.CorrectChoiceSoundOn();
                if(i == 1){
                    GenerateRandomColor(); // Yeniden renk ata
                }
                else if(i == 2){
                    GenerateRandomColor(); // Yeniden renk ata
                }
                else if(i == 3){
                    GreenButtonActivate(buttonPressed); // Yeşil butona tıklandığında oluşacak durumların çağrılması
                }    
            }    

            else if(buttonPressed.image.color == Color.red ) // Eğer seçilen buton kırmızı ise
            {
                RedButtonActivate();  // Kırmızı butona tıklandığında oluşacak durumların çağrılması
            }
        }

        if (buttonPressed == button2) // Orta seçenek
        {
            if(buttonPressed.image.color == Color.green ) // Eğer seçilen buton yeşilse
            {
                SoundManager.Instance.CorrectChoiceSoundOn();
                if(i == 1){
                    GenerateRandomColor(); // Yeniden renk ata
                }
                else if(i == 2){
                    GenerateRandomColor(); // Yeniden renk ata
                }
                else if(i == 3){
                    GreenButtonActivate(buttonPressed); // Yeşil butona tıklandığında oluşacak durumların çağrılması
                }
            }    
            else if(buttonPressed.image.color == Color.red ) // Eğer seçilen buton kırmızı ise
            {
                RedButtonActivate(); // Kırmızı butona tıklandığında oluşacak durumların çağrılması
            }
        }

        if (buttonPressed == button3) // Sağ seçenek
        {
            if(buttonPressed.image.color == Color.green ) // Eğer seçilen buton yeşilse
            {
                SoundManager.Instance.CorrectChoiceSoundOn();
                if(i == 1){
                    GenerateRandomColor(); // Yeniden renk ata
                }
                else if(i == 2){
                    GenerateRandomColor(); // Yeniden renk ata
                }
                else if(i == 3){
                    GreenButtonActivate(buttonPressed); // Yeşil butona tıklandığında oluşacak durumların çağrılması
                }
            }    

            else if(buttonPressed.image.color == Color.red ) // Eğer seçilen buton kırmızı ise
            {
                RedButtonActivate(); // Kırmızı butona tıklandığında oluşacak durumların çağrılması
            }
        }
        if(i >= 3){
            i = 0;
        }
        AttackControlButtonUpdate();
        ReturnToRun(); // Oyuncumuzun animasyon olarak koşmaya geri dönmesi
        
    }

    public void AttackControlButtonUpdate(){
        DeactivateButtons();
        if(!attackIsDone){
            StartCoroutine(ActivateButtonsForButtonVisualize());
            DeactivateButtons();
        }
        DeactivateButtons();
    }
        
    public void GenerateRandomColor() // Rastgele renk butonu oluşturma fonksiyonu
    {
        int x = Random.Range(0,button_List.Count); // yeşil olan
        foreach(var item in button_List){
            if(button_List[x] == item){
                button_List[x].gameObject.SetActive(true);
                button_List[x].image.color = Color.green;    
            }else{
                item.gameObject.SetActive(true);
                item.image.color = Color.red;
            }
        }
    }

    public IEnumerator WaitForButtons(){
        yield return new WaitForSeconds(2f);
    }

    public void DeactivateButtons() // Butonları inaktif etme fonksiyonu
    {
        foreach(var item in button_List) // Listedeki değerler için for döngüsü
        {
            //item.gameObject.SetActive(false);
            item.GetComponent<Image>().enabled = false; // Buton resmini inaktif yapma
            item.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
    }

    public IEnumerator DeactivateButtonsForButtonVisualize() // Butonları inaktif etme fonksiyonu
    {
        yield return new WaitForSeconds(CountDownTimer.Instance.startingTime);
        foreach(var item in button_List) // Listedeki değerler için for döngüsü
        {
            item.GetComponent<Image>().enabled = false; // Buton resmini inaktif yapma
            item.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
    }

    public IEnumerator ActivateButtons() // Butonları aktif etme fonksiyonu
    {
        yield return new WaitForSeconds(2f);
        foreach(var item in button_List) // Listedeki değerler için for döngüsü
        {
            item.GetComponent<Image>().enabled = true; // Buton resmini aktif yapma
            item.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
    }

    public IEnumerator ActivateButtonsForButtonVisualize() // Butonları aktif etme fonksiyonu
    {
        yield return new WaitForSeconds(0.25f);
        foreach(var item in button_List) // Listedeki değerler için for döngüsü
        {
            item.GetComponent<Image>().enabled = true; // Buton resmini aktif yapma
            item.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
    }
    public void GreenButtonActivate(Button buttonPressed){ // Yeşil butona basıldığında olacakları çağıran fonksiyon
        attackIsDone = true;
        if(enemyController.enemies[enemyController.enemyCount].GetComponentInChildren<Slider>().value == 0){
            AttackEnemyDeath(); // Düşmanın ölümünü çağıran fonksiyon
            if(buttonPressed.tag == "LeftButton") // Sol seçenek seçilirse
            {
                LeftAttack();
            }
            else if(buttonPressed.tag == "MiddleButton") // Ortadaki seçenek seçilirse
            {
                MiddleAttack();
            }
            else if(buttonPressed.tag == "RightButton") // Sağ seçenek seçilirse
            {
                RightAttack();
            }
            DeactivateButtons(); // Ardından butonların inaktif edildiği fonksiyonu çağırma       
        }
        else{
            StartCoroutine(enemyController.TakeDamage());
            if(buttonPressed.tag == "LeftButton") // Sol seçenek seçilirse
            {
                LeftAttack();
            }
            else if(buttonPressed.tag == "MiddleButton") // Ortadaki seçenek seçilirse
            {
                MiddleAttack();
            }
            else if(buttonPressed.tag == "RightButton") // Sağ seçenek seçilirse
            {
                RightAttack();
            }
            DeactivateButtons(); // Ardından butonların inaktif edildiği fonksiyonu çağırma  
        }        
    }

    public void RedButtonActivate(){ // Kırmızı butona basıldığında olacakları çağıran fonksiyon
        attackIsDone = true;
        SoundManager.Instance.IncorrectChoiceSoundOn();
        CountDownTimer.Instance.timerIsRunning = false;
        SoundManager.Instance.EnemyLaughSoundOn(); // Düşman gülüş sesini çalan fonksiyonu çağırma
        SoundManager.Instance.ClockSoundOff(); // Saat sesinin durdurulması
        CountDownTimer.Instance.CountDownTimerDeactivate(); // Sayacın inaktif görünmez hale getirilmesi
        checkdefeat = true;
        StartCoroutine(enemyController.EnemyAttack2Player()); // Düşmanın oyuncumuza saldırı fonksiyonunu çağırma
        MusicManager.Instance.MusicOff(); // Saldırı esnasında müziğin durdurulması
        DeactivateButtons(); // Ardından butonların inaktif edildiği fonksiyonu çağırma
        
        if(ShopManager.Instance.thereisArmour){
            StartCoroutine(ShieldOn());
        }
        else if(ShopManager.Instance.anotherLife){
            StartCoroutine(AnotherLifeOnCharacter());
        }        
    }
    public IEnumerator AnotherLifeOnCharacter(){
        enemyController.EnemyDisappoint();
        yield return new WaitForSeconds(2f);
        enemyController.EnemyPassiveAttack();
        PlayerController.Instance.StopActivate();
        yield return new WaitForSeconds(3f);
        PlayerController.Instance.RebirthCharacter();
        yield return new WaitForSeconds(2f);
        PlayerController.Instance.ButtonsActivate();
        StartCoroutine(CountDownTimer.Instance.CountDownImmediateRestartCoroutine());
        StartCoroutine(CountDownTimer.Instance.CountDownTimerActivateCoroutine());
        StartCoroutine(CountDownTimer.Instance.CountDownCoroutine());
        ShopManager.Instance.anotherLife = false;
        ShopManager.Instance.HearthValueDown();
        checkdefeat = false;
    }

    public IEnumerator ShieldOn(){
        enemyController.EnemyDisappoint();
        yield return new WaitForSeconds(2f);
        enemyController.EnemyPassiveAttack();
        PlayerController.Instance.StopActivate();
        yield return new WaitForSeconds(3f);
        PlayerController.Instance.RebirthCharacter();
        yield return new WaitForSeconds(2f);
        PlayerController.Instance.ButtonsActivate();
        StartCoroutine(CountDownTimer.Instance.CountDownImmediateRestartCoroutine());
        StartCoroutine(CountDownTimer.Instance.CountDownTimerActivateCoroutine());
        StartCoroutine(CountDownTimer.Instance.CountDownCoroutine());
        ShopManager.Instance.thereisArmour = false;
        ShopManager.Instance.armour.SetActive(false);
        ShopManager.Instance.ArmourValueDown();
        checkdefeat = false;
    }

    public void AnotherLifeWithoutCoroutine(){
        if(UIManager.Instance.choosenOne){
            SoundManager.Instance.LetsGoSoundOn();
        }
        else if(!UIManager.Instance.choosenOne){
            SoundManager.Instance.FemaleLetsDoSoundOn();
        }
        PlayerController.Instance.RebirthCharacter();
        StartCoroutine(CountDownTimer.Instance.CountDownImmediateRestartCoroutine());
        ShopManager.Instance.anotherLife = false;
        ShopManager.Instance.HearthValueDown();
        checkdefeat = false;
        StartCoroutine(PlayerController.Instance.MoveOnLateForRewardAd());
    }

    public void AttackEnemyDeath(){
        StartCoroutine(enemyController.EnemyDeathActivate()); // Düşman ölümünün aktifleştirilmesi(İçinde Animasyonunu çağırıyor)
        StartCoroutine(PlayerController.Instance.AttackEnemy()); // Düşmana saldırıyı başlatma fonksiyonunu çağırma
        
        if(UIManager.Instance.choosenOne){
            SoundManager.Instance.SoundOn(); // Saldırı sesinin başlatılması
        }
        else if(!UIManager.Instance.choosenOne){
            SoundManager.Instance.FemaleAttackSoundOn();
        }    
        
        MusicManager.Instance.MusicOff(); // Saldırı esnasında müziğin durdurulması
    }

    public void ReturnToRun(){

        PlayerController.Instance.animator.SetTrigger("PassiveAttack"); // Atak animasyonundan çıkıp ilgili eski animasyona dönme triggerı
        PlayerController.Instance.RunActivate(); // // Oyuncumuzun koşu animasyonunu devam ettiren fonksiyonu çağırma 
        attackIsDone = false; 
    }

    public void ButtonManagerActivate(){
        button1.onClick.AddListener(() => buttonCallBack(button1)); // Sol seçenek seçildiğinde
        button2.onClick.AddListener(() => buttonCallBack(button2)); // Orta seçenek seçildiğinde
        button3.onClick.AddListener(() => buttonCallBack(button3)); // Sağ seçenek seçildiğinde    
    }

    public void RightAttack(){ // Oyuncumuzun sağdan saldırıya geçme ve aşağıdan yukarı kan particle sistemini çağıran fonksiyon
        PlayerController.Instance.animator.SetTrigger("DodgeRightActivate"); // Sağa kayma animasyonunu çağırma
        PlayerController.Instance.animator.SetTrigger("ActiveAttack3"); // Saldırı animasyonunu çağırma
    }

    public void LeftAttack(){ // Oyuncumuzun soldan saldırıya geçme ve aşağıdan yukarı kan particle sistemini çağıran fonksiyon
        PlayerController.Instance.animator.SetTrigger("DodgeLeftActivate"); // Sola kayma animasyonunu çağırma
        PlayerController.Instance.animator.SetTrigger("ActiveAttack"); // Saldırı animasyonunu çağırma
    }

    public void MiddleAttack(){ // Oyuncumuzun ortadan kayarak saldırıya geçme ve soldan sağa kan particle sistemini çağıran fonksiyon
        PlayerController.Instance.animator.SetTrigger("ActiveAttack2"); // Saldırı animasyonunu çağırma
    }
}