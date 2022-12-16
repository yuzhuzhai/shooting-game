using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeaponController : WeaponController {

    public Transform spawnPoint;
    public GameObject bulletPrefab;
    public ParticleSystem fx_Shot;
    public GameObject fx_BulletFall;

   // private Collider2D fireCollider;

    private WaitForSeconds wait_Time = new WaitForSeconds(0.02f);
    // private WaitForSeconds fire_ColliderWait = new WaitForSeconds(0.02f);

	void Start () {
		// create bullets

        if(!GameplayController.instance.bullet_And_BulletFX_Created) {

            GameplayController.instance.bullet_And_BulletFX_Created = true;

          
                SmartPool.instance.CreateBulletAndBulletFall(bulletPrefab, fx_BulletFall, 100);
            
        }

        // if(!GameplayController.instance.rocket_Bullet_Created) {

        //     if(nameWp == NameWeapon.ROCKET) {
        //         GameplayController.instance.rocket_Bullet_Created = true;

        //         SmartPool.instance.CreateRocket(bulletPrefab, 100);

        //     }

        // }

        // if(nameWp == NameWeapon.FIRE) {
        //     fireCollider = spawnPoint.GetComponent<BoxCollider2D>();
        // }

	}

	public override void ProcessAttack() {
        //base.ProcessAttack();

        switch(nameWp) {

            case NameWeapon.PISTOL:
                AudioManager.instance.GunSound(0);
                break;

            // case NameWeapon.MP5:
            //     AudioManager.instance.GunSound(1);
            //     break;

            // case NameWeapon.M3:
            //     AudioManager.instance.GunSound(2);
            //     break;

            case NameWeapon.AK:
                AudioManager.instance.GunSound(3);
                break;

            // case NameWeapon.AWP:
            //     AudioManager.instance.GunSound(4);
            //     break;

            // case NameWeapon.FIRE:
            //     AudioManager.instance.GunSound(5);
            //     break;

            // case NameWeapon.ROCKET:
            //     AudioManager.instance.GunSound(6);
            //     break;

        } // switch and case

        // SPAWN BULLET

        if((transform != null)) {

            // if(nameWp != NameWeapon.ROCKET) {

            //     GameObject bullet_FallFX = SmartPool.instance.SpawnBulletFallFX(
            //         spawnPoint.transform.position, Quaternion.identity);

            //     bullet_FallFX.transform.localScale =
            //                      (transform.root.eulerAngles.y > 1.0f) ? new Vector3(-1f, 1f, 1f) : 
            //                      new Vector3(1f, 1f, 1f);

            //     StartCoroutine(WaitForShootEffect());

            // }

            SmartPool.instance.SpawnBullet(spawnPoint.transform.position,
                                           new Vector3(-transform.root.localScale.x, 0f, 0f),
                                           spawnPoint.rotation, nameWp);

        } 


	} // process attack

    IEnumerator WaitForShootEffect() {
        yield return wait_Time;
        fx_Shot.Play();
    }
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(new Vector3(-transform.root.localScale.x, 0f, 0f)) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
    // IEnumerator ActiveFireCollider() {
    //     fireCollider.enabled = true;

    //     fx_Shot.Play();

    //     yield return fire_ColliderWait;

    //     fireCollider.enabled = false;
    // }

} // class













































