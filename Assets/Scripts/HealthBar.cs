using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider playerSlider; // Can değeri için kaydırıcı component (Oyuncu)
    
    public void SetHealth(int health){
        Debug.Log($"playerSlider.value : {playerSlider.value} \t = health : {health}");
        playerSlider.value = health; // can değerini kaydırıcıya eşitleme
    }

    public void SetMaxHealth(int health){
        playerSlider.maxValue = health; // can değerini kaydırıcının maximum değerine eşitleme
        playerSlider.value = health; // can değerini kaydırıcıya eşitleme
    }
}
