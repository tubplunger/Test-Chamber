using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Text moneyText;
    public TMP_Text waveText;

    void Awake()
    {
        instance = this;
    }

    public void UpdateMoney(int amount)
    {
        moneyText.text = "Money: " + amount;
    }

    public void UpdateWave(int wave)
    {
        waveText.text = "Wave: " + wave;
    }
}
