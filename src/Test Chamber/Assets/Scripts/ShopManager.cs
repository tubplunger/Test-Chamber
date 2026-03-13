using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public GameObject shopPanel;

    bool shopOpen = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!WaveManager.instance.waveActive && Input.GetKeyDown(KeyCode.F))
        {
            ToggleShop();
        }
    }

    public void ToggleShop()
    {
        shopOpen = !shopOpen;

        shopPanel.SetActive(shopOpen);

        if (shopOpen)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void CloseShop()
    {
        shopOpen = false;
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
