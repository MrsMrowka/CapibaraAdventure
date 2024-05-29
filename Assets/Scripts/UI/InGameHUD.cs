using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsCollectedAmount;
    [SerializeField] TextMeshProUGUI timerText;

    int maxCoinsAmount;

    private void Start()
    {
        CalculateCoinsAmount();
        coinsCollectedAmount.text = GlobalVariables.CoinsCollected.ToString() + "/" + maxCoinsAmount.ToString();
    }

    private void Update()
    {
        UpdateHUD();
    }

    void UpdateHUD()
    {
        coinsCollectedAmount.text = GlobalVariables.CoinsCollected.ToString() + "/" + maxCoinsAmount.ToString();
        //timerText.text = GlobalVariables.Timer.ToString();
    }

    void CalculateCoinsAmount()
    {
        GameObject gameObject = GameObject.Find("Coins");
        Coin[] coinsList = gameObject.GetComponentsInChildren<Coin>();
        foreach (Coin coin in coinsList)
        {
            maxCoinsAmount += coin.value;
        }
    }
}
