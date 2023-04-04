using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : UICanvas
{
    public override void Setup()
    {
        base.Setup();
        GameManager.Ins.ChangState(GameState.Setting);
    }
    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
    }

    public override void CloseDirectly()
    {
        Time.timeScale = 1;
        base.CloseDirectly();
    }

    public void HomeButton()
    {
        /*UIManager.Ins.OpenUI<UIMainMenu>();
        UIManager.Ins.CloseUI<UISetting>();*/
        LevelManager.Ins.Home();
    }

    public void ContinueButton()
    {
        UIManager.Ins.OpenUI<UIGameplay>();
        UIManager.Ins.CloseUI<UISetting>();
    }
}
