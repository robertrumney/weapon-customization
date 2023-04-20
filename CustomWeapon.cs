using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CustomWeapon : MonoBehaviour
{
    public GameObject baseGun;
    public List<GameObject> magazineTypes;
    public List<GameObject> barrelTypes;
    public List<GameObject> scopeTypes;

    public WeaponBehavior weaponBehaviour;
    public AudioClip[] barrelAudioClips;
    public GameObject crosshairUI;

    public int currentMagazine;
    public int currentBarrel;
    public int currentScope;

    public bool[] isMagazineUnlocked;
    public bool[] isBarrelUnlocked;
    public bool[] isScopeUnlocked;

    private AudioSource audioSource;

    [SerializeField]
    private FlameThrowerBehaviour flamethrowerScript;

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
            magazineTypes[currentMagazine].SetActive(false);
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
            barrelTypes[currentBarrel].SetActive(false);
            barrelTypes[index].SetActive(true);
            audioSource.clip = barrelAudioClips[index];
            currentBarrel = index;
        }
    }

    public void SwitchScope(int index)
    {
        if (isScopeUnlocked[index])
        {
            scopeTypes[currentScope].SetActive(false);
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
                flamethrowerScript.enabled = true;
            }
            else
            {
                flamethrowerScript.enabled = false;
            }

            currentScope = index;
        }
    }

    // Example preset combination
    public void ApplyPreset1()
    {
        SwitchMagazine(0);
        SwitchBarrel(1);
        SwitchScope(2);
    }
}
