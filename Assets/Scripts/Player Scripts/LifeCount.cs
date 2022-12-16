using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{

    public Image[] lives;
    public int livesRemaining;
    public PlayerHealth playerHealth;

    public void LoseLife() {
        livesRemaining--;
        lives[livesRemaining].enabled = false;
        if(livesRemaining ==0) {
            GameplayController.instance.GameOver();
        } else {
            playerHealth.health =100;
            GameplayController.instance.PauseGame();
        }
    }
}
