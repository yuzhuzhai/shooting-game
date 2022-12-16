using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {

    private WeaponManager weaponManager;

    [HideInInspector]
    public bool canShoot;

    private bool isHoldAttack;
    Vector3 mousePostion;
    [SerializeField] Transform armL, armR;

	void Awake () {
        weaponManager = GetComponent<WeaponManager>();
        canShoot = true;
	}
	
	void Update () {
	
        if(Input.GetKeyDown(KeyCode.Q)) {
            weaponManager.SwitchWeapon();
        }

        if (Input.GetKey(KeyCode.Mouse0)) {

            isHoldAttack = true;

        } else {

            weaponManager.ResetAttack();
            isHoldAttack = false;
        }

        if(isHoldAttack && canShoot) {
            weaponManager.Attack();
        }
        AimForMouse();
    }
    void AimForMouse()
    {
        mousePostion = Input.mousePosition;
        mousePostion = Camera.main.ScreenToWorldPoint(mousePostion);
        Vector3 aimDirection = (mousePostion - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        angle = angle + 160;
        if (transform.localScale.x == -1f) angle = angle-140;
        armL.eulerAngles = new Vector3(0, 0, angle);
        armR.eulerAngles = new Vector3(0, 0, angle);
    }


} // class





























