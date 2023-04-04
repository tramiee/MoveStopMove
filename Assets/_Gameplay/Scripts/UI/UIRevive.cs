using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRevive : UICanvas
{
    [SerializeField] TextMeshProUGUI txtCounter;
    private float counter;
    public override void Setup()
    {
        base.Setup();
        GameManager.Ins.ChangState(GameState.Revive);
        counter = 5;
    }
    public override void Open()
    {
        base.Open();
    }

    private void Update()
    {
        if(counter > 0)
        {
            counter -= Time.deltaTime;
            txtCounter.SetText(counter.ToString("F0"));
            if (counter < 0)
            {
                CancelButton();
            }
        }
    }

    public void CancelButton()
    {
        UIManager.Ins.CloseUI<UIRevive>();
        UIManager.Ins.OpenUI<UIFail>();
        LevelManager.Ins.Fail();
    }

    public void ReviveButton()
    {
        GameManager.Ins.ChangState(GameState.GamePlay);
        UIManager.Ins.CloseUI<UIRevive>();
        LevelManager.Ins.OnRevive();
        UIManager.Ins.OpenUI<UIGameplay>();
    }
}
