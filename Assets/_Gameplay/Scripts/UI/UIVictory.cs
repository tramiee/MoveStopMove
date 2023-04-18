using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIVictory : UICanvas
{
    [SerializeField] TextMeshProUGUI txtCoin;
    private int coin;
    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangState(GameState.Finish);
    }

    public void x2CoinButton()
    {
        LevelManager.Ins.NextLevel();
        LevelManager.Ins.Home();
    }

    public void NextLevelButton()
    {
        LevelManager.Ins.NextLevel();
        LevelManager.Ins.Home();

    }

    internal void SetCoin(int coin)
    {
        this.coin = coin;
        txtCoin.SetText(coin.ToString());
    }
}
