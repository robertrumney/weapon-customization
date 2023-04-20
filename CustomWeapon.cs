using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomWeapon : MonoBehaviour
{
    [Header("Base Gun")]
    public GameObject baseGun;

    [Header("Modules")]
    public List<GameObject> magazineTypes;
    public List<GameObject> barrelTypes;
    public List<GameObject> scopeTypes;

    [Header("Module Unlocked States")]
    public bool[] isMagazineUnlocked;
    public bool[] isBarrelUnlocked;
    public bool[] isScopeUnlocked;

    [Header("Barrel Audio Clips")]
    public AudioClip[] barrelAudioClips;

    [Header("Current Modules")]
    public int currentMagazine;
    public int currentBarrel;
    public int currentScope;

    [Header("Weapon Behavior")]
    public WeaponBehavior weaponBehaviour;

    [Header("UI Components")]
    public GameObject crosshairUI;

    [Header("Private Components")]
    private AudioSource audioSource;

    [SerializeField]
    private GameObject flamethrowerScript;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < magazineTypes.Count; i++) isMagazineUnlocked[i] = true;
        for (int i = 0; i < barrelTypes.Count; i++) isBarrelUnlocked[i] = true;
        for (int i = 0; i < scopeTypes.Count; i++) isScopeUnlocked[i] = true;

        // Set default values
        currentMagazine = 0;
        currentBarrel = 0;
        currentScope = 0;

        SwitchMagazine(currentMagazine);
        SwitchBarrel(currentBarrel);
        SwitchScope(currentScope);
    }

    public void SwitchMagazine(int index)
    {
        if (isMagazineUnlocked[index])
        {
            if (magazineTypes[currentMagazine])
                magazineTypes[currentMagazine].SetActive(false);

            if (magazineTypes[index])
                magazineTypes[index].SetActive(true);

            // Example of adjusting ammo based on magazine type
            weaponBehaviour.bulletsPerClip = index * 10;

            currentMagazine = index;
        }
    }

    public void SwitchBarrel(int index)
    {
        if (isBarrelUnlocked[index])
        {
            if (barrelTypes[currentBarrel])
                barrelTypes[currentBarrel].SetActive(false);

            if (barrelTypes[index])
                barrelTypes[index].SetActive(true);

            audioSource.clip = barrelAudioClips[index];
            currentBarrel = index;
        }
    }

    public void SwitchScope(int index)
    {
        if (isScopeUnlocked[index])
        {
            if (scopeTypes[currentScope])
                scopeTypes[currentScope].SetActive(false);

            if (scopeTypes[index])
                scopeTypes[index].SetActive(true);

            // Check for "no scope" and enable UI crosshair
            if (index == 0)
            {
                crosshairUI.SetActive(true);
            }
            else
            {
                crosshairUI.SetActive(false);
            }

            // Check for flamethrower scope and enable flamethrower script
            if (index == 2)
            {
                flamethrowerScript.SetActive(true);
            }
            else
            {
                flamethrowerScript.SetActive(false);
            }

            currentScope = index;
        }
    }

    // Example preset combination
    public void ApplyPreset()
    {
        SwitchMagazine(0);
        SwitchBarrel(1);
        SwitchScope(2);
    }
}
