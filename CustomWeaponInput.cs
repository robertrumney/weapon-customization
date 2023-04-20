using UnityEngine;

public class CustomWeaponInput : MonoBehaviour
{
    public CustomWeapon customWeapon;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            customWeapon.SwitchMagazine(0);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            customWeapon.SwitchMagazine(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            customWeapon.SwitchMagazine(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            customWeapon.SwitchBarrel(0);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            customWeapon.SwitchBarrel(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            customWeapon.SwitchBarrel(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            customWeapon.SwitchScope(0);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            customWeapon.SwitchScope(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            customWeapon.SwitchScope(2);
        }
    }
}
