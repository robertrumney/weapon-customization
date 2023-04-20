using UnityEngine;
using UnityEngine.UI;

public class WeaponCustomizationUI : MonoBehaviour
{
    public CustomWeapon customWeapon;
    public Camera customizationCamera;

    public Transform[] cameraPositions;
    public Text categoryText;
    public Text modText;

    private int currentCategory;
    private int currentMod;

    private string[] categories = { "Magazine", "Barrel", "Scope" };

    private void Start()
    {
        UpdateCameraPosition();
        UpdateUI();
    }

    public void EnterCustomizationMode()
    {
        customizationCamera.gameObject.SetActive(true);
    }

    public void ExitCustomizationMode()
    {
        customizationCamera.gameObject.SetActive(false);
    }

    public void CycleCategory(bool next)
    {
        if (next)
        {
            currentCategory = (currentCategory + 1) % categories.Length;
        }
        else
        {
            currentCategory = (currentCategory - 1 + categories.Length) % categories.Length;
        }

        currentMod = GetCurrentMod();
        UpdateCameraPosition();
        UpdateUI();
    }

    public void CycleMod(bool next)
    {
        int maxMods = GetMaxMods();

        if (next)
        {
            currentMod = (currentMod + 1) % maxMods;
        }
        else
        {
            currentMod = (currentMod - 1 + maxMods) % maxMods;
        }

        if (IsModUnlocked())
        {
            UpdateWeaponMod();
        }
        else
        {
            CycleMod(next);
        }

        UpdateUI();
    }

    private void UpdateCameraPosition()
    {
        customizationCamera.transform.position = cameraPositions[currentCategory].position;
        customizationCamera.transform.rotation = cameraPositions[currentCategory].rotation;
    }

    private void UpdateUI()
    {
        categoryText.text = categories[currentCategory];
        modText.text = "Mod " + (currentMod + 1);
    }

    private int GetCurrentMod()
    {
        switch (currentCategory)
        {
            case 0:
                return customWeapon.currentMagazine;
            case 1:
                return customWeapon.currentBarrel;
            case 2:
                return customWeapon.currentScope;
            default:
                return 0;
        }
    }

    private int GetMaxMods()
    {
        switch (currentCategory)
        {
            case 0:
                return customWeapon.magazineTypes.Count;
            case 1:
                return customWeapon.barrelTypes.Count;
            case 2:
                return customWeapon.scopeTypes.Count;
            default:
                return 0;
        }
    }

    private bool IsModUnlocked()
    {
        switch (currentCategory)
        {
            case 0:
                return customWeapon.isMagazineUnlocked[currentMod];
            case 1:
                return customWeapon.isBarrelUnlocked[currentMod];
            case 2:
                return customWeapon.isScopeUnlocked[currentMod];
            default:
                return false;
        }
    }

    private void UpdateWeaponMod()
    {
        switch (currentCategory)
        {
            case 0:
                customWeapon.SwitchMagazine(currentMod);
                break;
            case 1:
                customWeapon.SwitchBarrel(currentMod);
                break;
            case 2:
                customWeapon.SwitchScope(currentMod);
                break;
        }
    }
}
