using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : GameUnit
{
    [SerializeField] Transform rightHand;
    [SerializeField] Transform leftHand;
    [SerializeField] Transform head;
    [SerializeField] Renderer pantRenderer;
    [SerializeField] Renderer bodyRenderer;
    [SerializeField] PantData pantData;
    [SerializeField] ColorData colorData;

    Hat currentHat;
    Accessory currentAccessory;
    Weapon currentWeapon;

    public Weapon Weapon => currentWeapon;


    [SerializeField] bool IsCanChange = false;


    public void ChangeWeapon(WeaponType weaponType)
    {
        currentWeapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, rightHand);
    }

    public void ChangeHat(HatType hatType)
    {
        if (IsCanChange && hatType != HatType.HAT_None)
        {
            currentHat = SimplePool.Spawn<Hat>((PoolType)hatType, head);
        }
    }

    public void ChangeAccessory(AccessoryType accessoryType)
    {
        if(IsCanChange && accessoryType != AccessoryType.ACC_None)
        {
            currentAccessory = SimplePool.Spawn<Accessory>((PoolType)accessoryType, leftHand);
        }
    }

    public void ChangePant(PantType pantType)
    {
        pantRenderer.material = pantData.GetMaterial(pantType);
    }

    public void ChangeColor(ColorType colorType)
    {
        bodyRenderer.material = colorData.GetMaterial(colorType);
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(currentWeapon);
        if (currentHat)
        {
            SimplePool.Despawn(currentHat);
        }
        if (currentAccessory)
        {
            SimplePool.Despawn(currentAccessory);
        }
    }

    public void DespawnHat()
    {
        if (currentHat)
        {
            SimplePool.Despawn(currentHat);
        }
    }

    public void DespawnAccessory()
    {
        if (currentAccessory)
        {
            SimplePool.Despawn(currentAccessory);
        }
    }

    public void DespawnWeapon()
    {
        if (currentWeapon)
        {
            SimplePool.Despawn(currentWeapon);
        }
    }
}
