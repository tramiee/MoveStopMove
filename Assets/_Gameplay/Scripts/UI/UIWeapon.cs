using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIWeapon : UICanvas
{
    public Transform weaponPoint;
    private Weapon currentWeapon;
    private WeaponType weaponType;

    [SerializeField] WeaponData weaponData;
    [SerializeField] ButtonState buttonState;
    [SerializeField] TextMeshProUGUI txtNameWeapon;
    [SerializeField] TextMeshProUGUI txtCoinWeapon;
    [SerializeField] TextMeshProUGUI txtCoin;

    public override void Setup()
    {
        base.Setup();
        ChangeWeapon(UserData.Ins.playerWeapon);
        txtCoin.SetText(UserData.Ins.coin.ToString());

    }


    public override void CloseDirectly()
    {
        base.CloseDirectly();
        if(currentWeapon != null)
        {
            SimplePool.Despawn(currentWeapon);
            currentWeapon = null;
        }
        UIManager.Ins.CloseUI<UIMainMenu>();
    }


    public void NextButton()
    {
        ChangeWeapon(weaponData.NextType(weaponType));
    }

    public void PrevButton()
    {
        ChangeWeapon(weaponData.PrevType(weaponType));
    }

    public void BuyButton()
    {
        if (true)
        {
            UserData.Ins.SetEnumData(weaponType.ToString(), ShopItem.State.Bought);
            ChangeWeapon(weaponType);
        }
    }

    public void EquipButton()
    {
        UserData.Ins.SetEnumData(weaponType.ToString(), ShopItem.State.Equipped);
        UserData.Ins.SetEnumData(UserData.Ins.playerWeapon.ToString(), ShopItem.State.Bought);
        UserData.Ins.SetEnumData(UserData.Key_Player_Weapon, ref UserData.Ins.playerWeapon, weaponType);
        ChangeWeapon(weaponType);
        LevelManager.Ins.player.TryClothes(UIShop.ShopType.Weapon, weaponType);
    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        this.weaponType = weaponType;
        if(currentWeapon != null)
        {
            SimplePool.Despawn(currentWeapon);
        }

        currentWeapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, Vector3.zero, Quaternion.identity, weaponPoint);

        //Check data dong

        ButtonState.State state = (ButtonState.State)UserData.Ins.GetDataState(weaponType.ToString(), 0);
        buttonState.SetState(state);

        WeaponItem item = weaponData.GetWeaponItem(weaponType);
        txtNameWeapon.SetText(item.name);
        txtCoinWeapon.text = item.cost.ToString();
    }
}
