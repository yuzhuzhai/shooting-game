using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int health = 100;

    public GameObject[] blood_FX;

    private PlayerAnimations playerAnim;

    public LifeCount lifeCount;
	
	void Awake () {
        playerAnim = GetComponentInParent<PlayerAnimations>();
	}
	
    public void DealDamage(int damage) {

        health -= damage;

        GameplayController.instance.PlayerLifeCounter(health);

        //playerAnim.Hurt();
        if(health <= 0) {

            lifeCount.LoseLife();

        }

    }


} // class










































