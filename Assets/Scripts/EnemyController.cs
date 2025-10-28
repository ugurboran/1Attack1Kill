using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public GameObject[] enemies;

    public BoxCollider xcollider;
    public int enemyCount;

    public int damageFromPlayer = 100;

    public void AddEnemyToList()
    {
        enemies = GameObject.FindGameObjectsWithTag("EnemyBase"); // EnemyBase tagindeki düşmanların enemies dizisine eklenmesi
    }

    public void ActivateBoxEnemy(int enemyCount) // Düşmanın box collider componentini yok etme
    {
        xcollider = enemies[enemyCount].GetComponentInChildren<BoxCollider>(); // Componenti bulma ve xcollider değişkenine eşitleme
        xcollider.enabled = true;; // Değişkeni inaktif hale getirme
    }

    public void DeActivateBoxEnemy(int enemyCount) // Düşmanın box collider componentini yok etme
    {
        xcollider = enemies[enemyCount].GetComponentInChildren<BoxCollider>(); // Componenti bulma ve xcollider değişkenine eşitleme
        xcollider.enabled = false; // Değişkeni inaktif hale getirme
    }
    
    public IEnumerator EnemyAttack2Player() // Düşmanın oyuncumuza saldırması
    {
        PlayerController.Instance.animator.SetTrigger("Defeated"); // Oyuncumuzun Üzülme animasyonunu aktif hale getirme
        yield return new WaitForSeconds(2f);
        enemies[enemyCount].GetComponent<Animator>().SetTrigger("ActiveAttack"); // Düşman saldırısını aktif hale getirme
        yield return new WaitForSeconds(2f);
        PlayerController.Instance.animator.SetTrigger("DeathActivate");
        if(!ShopManager.Instance.anotherLife && !ShopManager.Instance.thereisArmour){
            EnemyWinScenario();
        }
    }

    public void EnemyWinScenario(){
        if(UIManager.Instance.choosenOne){
            SoundManager.Instance.GameOverSoundOn();
        }
        else if(!UIManager.Instance.choosenOne){
            SoundManager.Instance.FemaleGameOverSoundOn();
        }
        StartCoroutine(PlayerController.Instance.TakeDamage(1)); // Oyuncumuzun 1 değerinde vuruş(hasar) almasını belirten fonksiyonu çağırma
        enemies[enemyCount].GetComponent<Animator>().SetTrigger("EnemyDanceActivate"); // Düşman dans animasyonunu aktif hale getirme
        CameraManager.Instance.CameraZoomOut();
        GameManager.Instance.TryAgainButtonActivate();
    }
    
    public IEnumerator EnemyDeathActivate(){ // Düşman ölüm animasyonu ve düşman objesini yok eden fonksiyon
        enemies[enemyCount].GetComponent<Animator>().SetTrigger("EnemyDeathActivate"); // Düşman ölümü animasyonunu aktifleştirme
        yield return new WaitForSeconds(0.5f);  
        yield return new WaitForSeconds(1f); 
        StartCoroutine(PlayerController.Instance.DestroyEnemy());
        enemyCount++; // Düşman x değerinin düşman öldükçe arttırılması
        if(enemyCount == 3){ // Düşman x değeri 3 değerine ulaştığında sayacımız ile ilgili fonksiyonu çağır
            CountDownChange(); // İlgili fonksiyon sayacın bir birim azaltılıp o değerden geriye doğru sayımın başlaması
        }
        else if(enemyCount == 6){ // Düşman x değeri 6 değerine ulaştığında sayacımız ile ilgili fonksiyonu çağır
            CountDownChange(); // İlgili fonksiyon sayacın bir birim azaltılıp o değerden geriye doğru sayımın başlaması
        } 
        else if(enemyCount == 9){ // Düşman x değeri 6 değerine ulaştığında sayacımız ile ilgili fonksiyonu çağır
            CountDownChange(); // İlgili fonksiyon sayacın bir birim azaltılıp o değerden geriye doğru sayımın başlaması
        }    
    }
    public IEnumerator TakeDamage()
    {
        CountDownTimer.Instance.CountDownImmediateRestart(); // Sayacı yeniden başlangıç değerine atama
        AttackSoundWithCharacterChoose();
        MusicManager.Instance.MusicOff(); // Saldırı esnasında müziğin durdurulması
        yield return new WaitForSeconds(1f);
        TakeDamageFromLeft(); // Düşmanın hasar alması ve bar değerini değiştiren fonksiyonu çağırma
        if(enemies[enemyCount].GetComponentInChildren<Slider>().value == 0){ // Eğer düşmanın can barı 0 değerine eşitse
            StartCoroutine(EnemyDeathActivate()); // Düşman ölüm animasyonu ve düşman objesini yok eden fonksiyonu çağırma
            //PlayerController.Instance.MoveOn(); // Oyuncumuzun hareketine devam etmesi
            StartCoroutine(PlayerController.Instance.MoveOnLate()); // Oyuncumuzun hareketine belli bir süre gecikmenin ardından devam etmesi
            MusicManager.Instance.MusicOn(); // Müziğin yeniden başlatılması
            CountDownTimer.Instance.CountDownTimerDeactivate();  // Sayacın görünmez hale gelmesi
        }
        else{ // Düşmanımızın can barı 0 değerine eşit olmadığı durumlarda
            CountDownTimer.Instance.CountDownImmediateRestart(); // Sayacın saldırı sonrası yenilenmesi
            StartCoroutine(CountDownTimer.Instance.CountDownLate()); // Sayacın belli bir süre sonra yeniden başlatılması 
            PlayerController.Instance.StopActivate(); // Oyuncunun durması
            PlayerController.Instance.ButtonsActivate(); // Butonların yeniden rastgele renk üretmesini ve aktif yani görünür hale gelmesini çağıran fonksiyon     
        }
        
    }

    public void AttackSoundWithCharacterChoose(){
        if(UIManager.Instance.choosenOne){
            SoundManager.Instance.SoundOn(); // Saldırı sesinin başlatılması
        }
        else if(!UIManager.Instance.choosenOne){
            SoundManager.Instance.FemaleAttackSoundOn();
        } 
    }

    public void TakeDamageFromLeft(){ // Düşmanın aldığı hasarla bar değerini değiştiren fonksiyon
        enemies[enemyCount].GetComponentInChildren<Slider>().value = enemies[enemyCount].GetComponentInChildren<Slider>().value - damageFromPlayer; // Düşmanın can değerinden hasarın çıkartılıp düşmanın can değerine eşitlenmesi
    }

    public void CountDownChange(){
        CountDownTimer.Instance.startingTime -= 1; // Her spawn gerçekleştiğinde, chapter değiştiğinde Sayacımız 1 birim azalacak ve zorluk artacak.
        CountDownTimer.Instance.CountDownRestart(); // Sayacın yeniden başlatılması
    }

    public void EnemyDisappoint(){
        enemies[enemyCount].GetComponent<Animator>().SetTrigger("EnemyDisappointActivate"); // Düşman dans animasyonunu aktif hale getirme
    }

    public void EnemyPassiveAttack(){
        enemies[enemyCount].GetComponent<Animator>().SetTrigger("PassiveAttack"); // Düşman dans animasyonunu aktif hale getirme
    }
}
