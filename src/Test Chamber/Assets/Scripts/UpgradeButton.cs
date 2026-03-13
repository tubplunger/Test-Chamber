using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    public string upgradeType;

    public int baseCost = 20;
    public float costMultiplier = 1.5f;

    int currentCost;

    public TMP_Text costText;

    void Start()
    {
        currentCost = baseCost;
        UpdateText();
    }

    public void BuyUpgrade()
    {
        if (GameManager.instance.playerMoney < currentCost)
            return;

        GameManager.instance.playerMoney -= currentCost;

        ApplyUpgrade();

        currentCost = Mathf.RoundToInt(currentCost * costMultiplier);

        UIManager.instance.UpdateMoney(GameManager.instance.playerMoney);

        UpdateText();
    }

    void ApplyUpgrade()
    {
        switch (upgradeType)
        {
            case "speed":
                PlayerMovement.instance.UpgradeSpeed();
                break;

            case "meleeDamage":
                PlayerAttack.instance.UpgradeMeleeDamage();
                break;

            case "rangedDamage":
                PlayerAttack.instance.UpgradeRangedDamage();
                break;

            case "meleeRange":
                PlayerAttack.instance.UpgradeMeleeRange();
                break;

            case "rangedRange":
                PlayerAttack.instance.UpgradeRangedRange();
                break;

            case "maxHealth":
                HealthSystem.instance.UpgradeMaxHealth();
                break;

            case "money":
                HealthSystem.instance.UpgradeMoneyDrop();
                break;
        }
    }

    void UpdateText()
    {
        costText.text = "Cost: " + currentCost;
    }
}
