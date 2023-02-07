using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider healthSlider;

private void Start(){
    healthSlider = GetComponent<Slider>();
}

    public void SetMaxHealth(int maxHealth){
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void SetHealth(int health){
        healthSlider.value = healthSlider.value - 1;
        if(healthSlider.value == 0){
            Time.timeScale = 0;
            //trigger the game over screen...
        }
    }

    public void Death(){
        healthSlider.value = 0;
        Time.timeScale = 0;
        //trigger the game over screen...
    }
}
