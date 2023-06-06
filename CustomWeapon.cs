using UnityEngine;
using System.Collections.Generic;

public class CustomWeapon : MonoBehaviour
{
    public MouseOrbitCS mouseOrbit;

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

    [Header("Sniper Scope UI Effect")]
    public SniperEffect sniperEffect;

    [Header("UI Components")]
    public GameObject crosshairUI;

    [SerializeField]
    private GameObject flamethrowerScript;

    [SerializeField]
    private GameObject freezeRayScript;

    private void Start()
    {
        for (int i = 0; i < magazineTypes.Count; i++) isMagazineUnlocked[i] = true;
        for (int i = 0; i < barrelTypes.Count; i++) isBarrelUnlocked[i] = true;
        for (int i = 0; i < scopeTypes.Count; i++) isScopeUnlocked[i] = true;

        ApplyPreset();
    }

    private void OnEnable()
    {
        weaponBehaviour.weaponUnzoomXPosition = -0.04f;
        weaponBehaviour.weaponUnzoomYPosition = 0;
        weaponBehaviour.weaponUnzoomZPosition = -0.1f;

        scopeTypes[1].transform.localPosition = new(0.0f, 0.08f, 0.1874954f);
        barrelTypes[0].transform.GetChild(0).localPosition = new(0, 0, -0.348f);
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
            weaponBehaviour.bulletsPerClip = (((index+1) * 10)*(index+1)) * 3;
            weaponBehaviour.bulletsToReload = weaponBehaviour.bulletsPerClip;

            if(magazineTypes[index])
                mouseOrbit.target = magazineTypes[index].transform;

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

            currentBarrel = index;

            if (barrelTypes[index])
                mouseOrbit.target = barrelTypes[index].transform;

            if (index == 0)
            {
                weaponBehaviour.semiAuto = false;
                weaponBehaviour.fireRate = 0.097f;
                weaponBehaviour.damage = 55;
            }

            if (index == 1)
            {
                weaponBehaviour.semiAuto = true;
                weaponBehaviour.fireRate = 0.097f;
                weaponBehaviour.damage = 60;
            }

            if (index == 2)
            {
                weaponBehaviour.semiAuto = false;
                weaponBehaviour.fireRate = 0.0485f;
                weaponBehaviour.damage = 15;
            }
        }
    }

    private bool isHamr = false;

    public void OnHAMR()
    {
        if(isHamr)
        {
            Game.instance.Discover("PRESS [" + Game.instance.settings.controls.holsterWeapon.ToString() + "] TO SWITCH HAMR SCOPE TYPE");
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
            if (index == 1)
            {
                crosshairUI.GetComponent<UnityEngine.UI.RawImage>().enabled = false; 
                sniperEffect.switchEnabled = true;
                isHamr = true;
            }
            else
            {
                crosshairUI.GetComponent<UnityEngine.UI.RawImage>().enabled = true; 
                sniperEffect.switchEnabled = false;
                sniperEffect.ForceReset();
                isHamr = false;
            }

            // Check for scope and enable sniper UI
            if (index == 1 || index == 0)
            {
                sniperEffect.enabled = true;
            }
            else
            {
                sniperEffect.enabled = false;
            }

            // Check for flamethrower/scope/freezeray/shrinkray 
            if (index == 2)
            {
                flamethrowerScript.SetActive(true);
                freezeRayScript.SetActive(false);
                weaponBehaviour.canZoom = false;
            }
            else if(index == 3)
            {
                flamethrowerScript.SetActive(false);
                freezeRayScript.SetActive(true);
                weaponBehaviour.canZoom = false;
            }
            else
            {
                flamethrowerScript.SetActive(false);
                freezeRayScript.SetActive(false);
                weaponBehaviour.canZoom = true;
            }

            if (scopeTypes[index])
                mouseOrbit.target = scopeTypes[index].transform;

            currentScope = index;
        }
    }

    // Example preset combination
    public void ApplyPreset()
    {
        SwitchMagazine(2);
        SwitchBarrel(0);
        SwitchScope(1);
    }

    public void NextMagazine()
    {
        int newIndex = (currentMagazine + 1) % magazineTypes.Count;
        SwitchMagazine(newIndex);
    }

    public void PreviousMagazine()
    {
        int newIndex = (currentMagazine - 1 + magazineTypes.Count) % magazineTypes.Count;
        SwitchMagazine(newIndex);
    }

    public void NextBarrel()
    {
        int newIndex = (currentBarrel + 1) % barrelTypes.Count;
        SwitchBarrel(newIndex);
    }

    public void PreviousBarrel()
    {
        int newIndex = (currentBarrel - 1 + barrelTypes.Count) % barrelTypes.Count;
        SwitchBarrel(newIndex);
    }

    public void NextScope()
    {
        int newIndex = (currentScope + 1) % scopeTypes.Count;
        SwitchScope(newIndex);
    }

    public void PreviousScope()
    {
        int newIndex = (currentScope - 1 + scopeTypes.Count) % scopeTypes.Count;
        SwitchScope(newIndex);
    }
}
