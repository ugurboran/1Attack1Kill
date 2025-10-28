using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] chapterPrefabs;
    public float zspawn = 0;
    public float planeLength = 150f;

    public int numberofSpawn = 1;

    public int chapterNumber;

    public EnemyController enemyController;

    public Transform playerTransform;

    public Transform davidTransform;
    public Transform mariaTransform;

    // Start is called before the first frame update
    void Start()
    {
        chapterNumber = 0;
        SpawnChapter(chapterNumber); // İlk chapter üretmesi ile başla
    }

    // Update is called once per frame
    void Update()
    {
        CharacterControl();
        if(playerTransform.position.z > zspawn -(numberofSpawn * (planeLength-65f))) // 65 birim tam düzlük bittiğinde oyuncunun yeni düzlüğü aktif hale gelmesi için ideal değer
        {
            //StartCoroutine(DestroyChapter(chapterNumber)); // Destroying is not permitted because of data loss, I guess because of enemy list data.
            if(chapterNumber == chapterPrefabs.Length - 1){ // Son chapter numarasına gelindiğinde 
                chapterNumber = chapterPrefabs.Length - 1; // Değişkeni son chapterPrefabs dizisinin son indisine eşitle
                if(playerTransform.position.z > zspawn -(numberofSpawn * (planeLength-65f))) // 65 birim tam düzlük bittiğinde oyuncunun yeni düzlüğü aktif hale gelmesi için ideal değer
                {
                SpawnChapter(Random.Range(0, chapterPrefabs.Length)); // Son chapterın yüklenmesinin ardından chapterlar random olarak yükle
                } 
            }
            else{
                chapterNumber ++;
                SpawnChapter(chapterNumber); // Chapter üretimine sıra ile devam et
            }
        }
    }

    public void SpawnChapter(int chapterIndex)
    {
        Instantiate(chapterPrefabs[chapterIndex], transform.forward * zspawn, transform.rotation);
        zspawn += planeLength;
        enemyController.AddEnemyToList(); // Chapterin yüklenmesi ile ilgili düşmanları listeye ekleyen fonksiyonu çağırma    
    }

    public IEnumerator DestroyChapter(int chapterIndex){
            yield return new WaitForSeconds(5f);
            Destroy(chapterPrefabs[chapterIndex]);    
    }

    public void CharacterControl(){
        if(UIManager.Instance.choosenOne){
            playerTransform = davidTransform;
        }
        else{
            playerTransform = mariaTransform;
        }
    }
}
