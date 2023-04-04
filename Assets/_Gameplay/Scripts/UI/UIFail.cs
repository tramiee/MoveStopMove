using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFail : UICanvas
{
    [SerializeField] TextMeshProUGUI txtCoin;
    private int coin;

    public override void Open()
    {
        base.Open();
    }

    public void HomeButton()
    {

    }

    public void x2CoinButton()
    {

    }

    internal void SetCoin(int coin)
    {
        this.coin = coin;
        txtCoin.SetText(coin.ToString());
    }
}
