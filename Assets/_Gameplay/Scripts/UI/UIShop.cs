using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UICanvas
{
    public enum ShopType { Pant, Hat, Accessory, Color, Weapon}

    [SerializeField] ShopData shopData;
    [SerializeField] ShopItem shopItem;
    [SerializeField] Transform content;
    [SerializeField] ShopBar[] shopBars;
    [SerializeField] TextMeshProUGUI txtCoin;
    [SerializeField] ButtonState buttonState;
    [SerializeField] TextMeshProUGUI txtCoinShop;

    MiniPool<ShopItem> miniPool = new MiniPool<ShopItem>();

    private ShopBar currentBar;
    private ShopItem currentItem;
    private ShopItem itemEquiped;

    public ShopType shopType => currentBar.Type;

    private void Awake()
    {
        miniPool.OnInit(shopItem, 10, content);
        for(int i = 0; i < shopBars.Length; i++)
        {
            shopBars[i].SetShop(this);
        }

    }

    public override void Open()
    {
        base.Open();
        SelectBar(shopBars[0]);
        CameraFollow.Ins.ChangeState(CameraFollow.State.Shop);
        txtCoin.SetText(UserData.Ins.coin.ToString());
    }

    public override void CloseDirectly()
    {
        base.CloseDirectly();

        UIManager.Ins.OpenUI<UIMainMenu>();
        LevelManager.Ins.player.TakeOffClothes();
        LevelManager.Ins.player.OnTakeClothsData();
        LevelManager.Ins.player.WearClothes();
    }

    internal void SelectBar(ShopBar shopBar)
    {
        if(currentBar != null)
        {
            currentBar.Active(false);
        }
        currentBar = shopBar;
        currentBar.Active(true);
        miniPool.Collect();
        itemEquiped = null;

        switch (currentBar.Type)
        {
            case ShopType.Hat:
                InitShipItems(shopData.hats.Ts, ref itemEquiped);
                break;
            case ShopType.Pant:
                InitShipItems(shopData.pants.Ts, ref itemEquiped);
                break;
            case ShopType.Accessory:
                InitShipItems(shopData.accessories.Ts, ref itemEquiped);
                break;
            case ShopType.Color:
                InitShipItems(shopData.colors.Ts, ref itemEquiped);
                break;
            default:
                break;
        }
    }

    private void InitShipItems<T>(List<ShopItemData<T>> items, ref ShopItem itemEquiped) where T : Enum
    {
        for(int i = 0; i < items.Count; i++)
        {
            ShopItem.State state = UserData.Ins.GetEnumData(items[i].type.ToString(), ShopItem.State.Buy);
            ShopItem item = miniPool.Spawn();
            item.SetData(i, items[i], this);
            item.SetState(state);

            if(state == ShopItem.State.Equipped)
            {
                SelectItem(item);
                itemEquiped = item;
            }
        }
    }

    public ShopItem.State GetState(Enum t) => UserData.Ins.GetEnumData(t.ToString(), ShopItem.State.Buy);


    internal void SelectItem(ShopItem shopItem)
    {
        if(currentItem != null)
        {
            currentItem.SetState(GetState(currentItem.Type));
        }

        currentItem = shopItem;

        switch (currentItem.state)
        {
            case ShopItem.State.Buy:
                buttonState.SetState(ButtonState.State.Buy);
                break;
            case ShopItem.State.Bought:
                buttonState.SetState(ButtonState.State.Equip);
                break;
            case ShopItem.State.Equipped:
                buttonState.SetState(ButtonState.State.Equiped);
                break;
            case ShopItem.State.Selecting:
                break;
            default:
                break;
        }
        LevelManager.Ins.player.TryClothes(shopType, currentItem.Type);
        currentItem.SetState(ShopItem.State.Selecting);

        //check data
        txtCoinShop.text = shopItem.data.cost.ToString();
    }

    public void EquipButton()
    {
        if (currentItem != null)
        {
            UserData.Ins.SetEnumData(currentItem.Type.ToString(), ShopItem.State.Equipped);

            switch (shopType)
            {
                case ShopType.Hat:
                    //reset trang thai do dang deo ve bought
                    UserData.Ins.SetEnumData(UserData.Ins.playerHat.ToString(), ShopItem.State.Bought);
                    //save id do moi vao player
                    UserData.Ins.SetEnumData(UserData.Key_Player_Hat, ref UserData.Ins.playerHat, (HatType)currentItem.Type);
                    break;
                case ShopType.Pant:
                    UserData.Ins.SetEnumData(UserData.Ins.playerPant.ToString(), ShopItem.State.Bought);
                    UserData.Ins.SetEnumData(UserData.Key_Player_Pant, ref UserData.Ins.playerPant, (PantType)currentItem.Type);
                    break;
                case ShopType.Accessory:
                    UserData.Ins.SetEnumData(UserData.Ins.playerAccessory.ToString(), ShopItem.State.Bought);
                    UserData.Ins.SetEnumData(UserData.Key_Player_Accessory, ref UserData.Ins.playerAccessory, (AccessoryType)currentItem.Type);
                    break;
                case ShopType.Color:
                    UserData.Ins.SetEnumData(UserData.Ins.playerColor.ToString(), ShopItem.State.Bought);
                    UserData.Ins.SetEnumData(UserData.Key_Player_Color, ref UserData.Ins.playerColor, (ColorType)currentItem.Type);
                    break;
                case ShopType.Weapon:
                    break;
                default:
                    break;
            }

        }

        if (itemEquiped != null)
        {
            itemEquiped.SetState(ShopItem.State.Bought);
        }

        currentItem.SetState(ShopItem.State.Equipped);
        SelectItem(currentItem);
    }

    public void BuyButton()
    {
        //TODO: check xem du tien hay k

        UserData.Ins.SetEnumData(currentItem.Type.ToString(), ShopItem.State.Bought);
        SelectItem(currentItem);
    }
}
