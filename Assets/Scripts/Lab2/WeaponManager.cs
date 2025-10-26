// WeaponManager.cs
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public enum WeaponType { Handgun = 1, MachineGun = 2, Grenade = 3 }
    public WeaponType startWeapon = WeaponType.Handgun;

    public Handgun handgun;
    public MachineGun machineGun;
    public GrenadeThrower grenadeThrower;

    private IWeapon activeWeapon;
    private WeaponType current;

    // 解锁状态（可在 Inspector 勾选初始可用武器）
    public bool unlockedHandgun = true;
    public bool unlockedMachineGun = false;
    public bool unlockedGrenade = false;

    public bool IsUnlocked(WeaponType wt)
    {
        return wt switch
        {
            WeaponType.Handgun => unlockedHandgun,
            WeaponType.MachineGun => unlockedMachineGun,
            WeaponType.Grenade => unlockedGrenade,
            _ => false
        };
    }


    void Start()
    {
        SwitchTo(startWeapon);
    }

    void Update()
    {
        // Weapon switch (1,2,3)
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchTo(WeaponType.Handgun);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchTo(WeaponType.MachineGun);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchTo(WeaponType.Grenade);

        if (Input.GetButtonDown("Fire1")) activeWeapon?.OnPress();
        if (Input.GetButton("Fire1")) activeWeapon?.OnHold();
        if (Input.GetButtonUp("Fire1")) activeWeapon?.OnRelease();
    }

    void SwitchTo(WeaponType wt)
    {
        if (!IsUnlocked(wt)) { Debug.Log($"Weapon {wt} not unlocked."); return; }
        current = wt;

        Debug.Log($"Switched to weapon: {current}");

        // optional: toggle visuals here
        switch (current)
        {
            case WeaponType.Handgun: activeWeapon = handgun; break;
            case WeaponType.MachineGun: activeWeapon = machineGun; break;
            case WeaponType.Grenade: activeWeapon = grenadeThrower; break;
        }
        Debug.Log("Switched to " + current);
    }

    public void Unlock(WeaponType wt, bool autoSwitch = true)
    {
        switch (wt)
        {
            case WeaponType.Handgun: unlockedHandgun = true; break;
            case WeaponType.MachineGun: unlockedMachineGun = true; break;
            case WeaponType.Grenade: unlockedGrenade = true; break;
        }
        Debug.Log($"Unlocked {wt}");
        if (autoSwitch) SwitchTo(wt);
    }

}
