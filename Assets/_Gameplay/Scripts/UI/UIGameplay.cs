using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameplay : UICanvas
{
    public TextMeshProUGUI txtCharacterAmount;

    public override void Setup()
    {
        base.Setup();
        UpdateTotalCharacter();
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangState(GameState.GamePlay);
        LevelManager.Ins.SetTargetIndicatorAlpha(1);

    }

    public override void CloseDirectly()
    {
        base.CloseDirectly();
        LevelManager.Ins.SetTargetIndicatorAlpha(0);
    }

    public void SettingButton()
    {
        UIManager.Ins.OpenUI<UISetting>();
    }

    public void UpdateTotalCharacter()
    {
        txtCharacterAmount.text = LevelManager.Ins.TotalCharacter.ToString();
    }
}
