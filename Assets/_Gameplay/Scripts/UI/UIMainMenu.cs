using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMainMenu : UICanvas
{
    [SerializeField] TextMeshProUGUI txtCoin;
    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangState(GameState.MainMenu);
        CameraFollow.Ins.ChangeState(CameraFollow.State.MainMenu);
        txtCoin.SetText(UserData.Ins.coin.ToString());


    }

    public void PlayButton()
    {
        UIManager.Ins.OpenUI<UIGameplay>();
        UIManager.Ins.CloseUI<UIMainMenu>();
       
        LevelManager.Ins.OnPlay();
        CameraFollow.Ins.ChangeState(CameraFollow.State.Gameplay);
    }

    public void WeaponButton()
    {
        UIManager.Ins.OpenUI<UIWeapon>();
        UIManager.Ins.CloseUI<UIMainMenu>();
    }

    public void ShopButton()
    {
        UIManager.Ins.OpenUI<UIShop>();
        UIManager.Ins.CloseUI<UIMainMenu>();
    }
}
