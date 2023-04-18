using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopItem : MonoBehaviour
{
    public enum State { Buy, Bought, Equipped, Selecting}
    [SerializeField] GameObject[] stateObjs;

    private UIShop shop;

    [SerializeField] Color[] colorBG;
    [SerializeField] Image icon;
    [SerializeField] Image bgIcon;

    public int id;
    public State state;

    public Enum Type;
    internal ShopItemData data;

    public void SetShop(UIShop shop)
    {
        this.shop = shop;
    }

    public void SetData<T>(int id, ShopItemData<T> itemData, UIShop shop) where T : Enum
    {
        this.id = id;
        Type = itemData.type;
        this.data = itemData;
        this.shop = shop;
        icon.sprite = itemData.icon;
        bgIcon.color = colorBG[(int)shop.shopType];
    }

    public void SelectButton()
    {
        shop.SelectItem(this);
    }

    public void SetState(State state)
    {
        for(int i = 0; i < stateObjs.Length; i++)
        {
            stateObjs[i].SetActive(false);
        }
        stateObjs[(int)state].SetActive(true);
        this.state = state;
    }
}
