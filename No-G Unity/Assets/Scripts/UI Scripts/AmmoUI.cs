﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AmmoUI : MonoBehaviour
{

    public float fireRate = 15f;

    public int maxAmmo = 10;
    private int currentAmmo;

    public float reloadTime = 1f;
    private bool isReloading = false;

    private float nextTimetoFire = 3f;
    private float cooldown = 3f;

    //public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        //Statement to return 
        if (isReloading)
            return;

        //Automatic Reload when ammo hits 0
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        //Rate of fire
        if (Input.GetButton("Fire1") && Time.time >= nextTimetoFire)
        {
            //nextTimetoFire = Time.time + 1f / fireRate;
            Debug.Log("Shooting...");
            currentAmmo--;
            Debug.Log(currentAmmo);
        }
    }

    //Setting current ammo to maxAmmo
    IEnumerator Reload()

    {
        isReloading = true;

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);
        //might be a small time delay bug
        //animator.SetBool("Reloading", false);

        currentAmmo = maxAmmo;
        Debug.Log(currentAmmo);
        isReloading = false;

    }

   
}
