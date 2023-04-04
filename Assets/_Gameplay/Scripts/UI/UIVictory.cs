using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIVictory : UICanvas
{
    private int coin;
    [SerializeField] TextMeshProUGUI txtCoin;
    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangState(GameState.Finish);
    }

    public void x2CoinButton()
    {

    }

    public void NextLevelButton()
    {

    }

    internal void SetCoin(int coin)
    {
        this.coin = coin;
        txtCoin.SetText(coin.ToString());
    }
}
