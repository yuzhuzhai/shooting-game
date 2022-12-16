using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    [HideInInspector]
    public int damage;

    private float speed = 60f;

    private WaitForSeconds wait_For_Time_Alive = new WaitForSeconds(0.5f);
    private IEnumerator coroutineDeactivate;

    private Vector3 direction;

    public GameObject rocket_Explosion;

	void Start () {
        if (this.tag == TagManager.ROCKET_MISSILE_TAG) {
            speed = 8f;
        }
	}

	void Update () {
        transform.Translate(direction * speed * Time.deltaTime);
	}


	void OnEnable() {
        coroutineDeactivate = WaitForDeactivate();
        StartCoroutine(coroutineDeactivate);
    }

    void OnDisable()  {
        if(coroutineDeactivate != null) {
            StopCoroutine(coroutineDeactivate);
        }
	}

    public void SetDirection(Vector3 dir) {
        direction = dir;
    }

    IEnumerator WaitForDeactivate() {
        yield return wait_For_Time_Alive;
        gameObject.SetActive(false);
    }

    public void ExplosionFX() {
        AudioManager.instance.FenceExplosion();
        Instantiate(rocket_Explosion, transform.position, Quaternion.identity);
    }

} // class










































