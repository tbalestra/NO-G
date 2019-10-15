﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthandAmmo : MonoBehaviour
{
	private Canvas canvas;

    public float maxHealth = 100;
    public float currentHealth;

    public Slider healthbar;
    public Text healthtext;

    ShootingGun gun;

    public float currentAmmo;
    public float maxAmmo;

    private float timeSpent;
    private float reloadFill;

    public bool isReloading = false;

    public Slider ammobar;
    public Slider reloadbar;

    public Text ammotext;

    // Start is called before the first frame update
    void Start()
    {
		healthbar = GameObject.Find("Healthbar").GetComponent<Slider>();
		healthtext = GameObject.Find("HealthText").GetComponent<Text>();

		ammobar = GameObject.Find("Ammobar").GetComponent<Slider>();
		reloadbar = GameObject.Find("Reloadbar").GetComponent<Slider>();

		ammotext = GameObject.Find("AmmoText").GetComponent<Text>();

		if (maxHealth == 0)
        {
            maxHealth = 100f;
        }
        currentHealth = maxHealth;

        healthbar.value = CalculateHealth();
        healthtext.text = ConvertHealthFloattoString();

        gun = GetComponentInChildren<ShootingGun>();
        maxAmmo = gun.maxAmmo;
        currentAmmo = gun.currentAmmo;

        ammobar.value = CalculateAmmo();
        ammotext.text = ConvertAmmoFloattoString();

        isReloading = gun.isReloading;
        reloadbar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if(ammotext == null)
		{
			healthbar = GameObject.Find("Healthbar").GetComponent<Slider>();
			healthtext = GameObject.Find("HealthText").GetComponent<Text>();

			ammobar = GameObject.Find("Ammobar").GetComponent<Slider>();
			reloadbar = GameObject.Find("Reloadbar").GetComponent<Slider>();

			ammotext = GameObject.Find("AmmoText").GetComponent<Text>();
		}

        isReloading = gun.isReloading;

        if (gun == null)
        {
            gun = GetComponentInChildren<ShootingGun>();
        }
        if (currentAmmo != gun.currentAmmo)
        {
            currentAmmo = gun.currentAmmo;
            ammobar.value = CalculateAmmo();
            ammotext.text = ConvertAmmoFloattoString();
        }
        if (isReloading)
        {
            if (timeSpent < gun.reloadTime || currentAmmo == 0)
            {
                timeSpent += Time.deltaTime;
                reloadFill += (1f / gun.reloadTime) * Time.deltaTime;
                reloadbar.value = reloadFill;

            }
        }
        else
        {
            timeSpent = 0;
            reloadFill = 0;
            reloadbar.value = reloadFill;
        }
    }

    private float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }

    private string ConvertHealthFloattoString()
    {
        float converthealth = CalculateHealth() * 100;
        return converthealth.ToString("f00");
    }

    public void DealDamage(float damagevalue)
    {
        //Minus player health w/ damage value
        currentHealth -= damagevalue;
        healthbar.value = CalculateHealth();
        healthtext.text = ConvertHealthFloattoString();

        //If player health =0, trigger death
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        currentHealth = 0;
        Debug.Log("Die");
        healthtext.text = "Dead";

    }

    float CalculateAmmo()
    {
        return currentAmmo / maxAmmo;
    }


    string ConvertAmmoFloattoString()
    {
        float convertammo = CalculateAmmo() * 10;
        return convertammo.ToString("f0");
    }

}
