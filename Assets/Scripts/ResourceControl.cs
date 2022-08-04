using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ResourceControl : MonoBehaviour
{
    public int resourceLevel { private set; get; } = 0;
    [SerializeField] ResourceConfig config;
    public double output => config.output + config.output * (0.5f * resourceLevel);
    public double upgradeCost => (config.upgradeCost + (config.upgradeCost * ((resourceLevel + 1) * 0.5f)));
    public string resourceName => config.resourceName;

    public bool canUpgrade => GameManager.Instance.currentMoney >= upgradeCost;
    public bool canUnlock => GameManager.Instance.currentMoney >= config.unlockCost;
    public bool hasUnlock = false;

    [Header("UI")]
    [SerializeField] Button actionBtn;
    [SerializeField] Text resourceTextA;
    [SerializeField] Text resouceTextB;

    private void Start()
    {
        GameManager.Instance.onMoneyChange.AddListener(SetButtonEvent);
        GameManager.Instance.onMoneyChange.AddListener(ChangeButtonText);

        SetButtonEvent();
        ChangeButtonText();
    }

    void SetButtonEvent()
    {
        switch (hasUnlock)
        {
            case false:
                if (actionBtn.onClick.GetPersistentEventCount() <= 0)
                {
                    actionBtn.onClick.AddListener(Unlock);
                }
                actionBtn.interactable = canUnlock;
                break;

            case true:
                if (actionBtn.onClick.GetPersistentEventCount() <= 0)
                {
                    actionBtn.onClick.AddListener(Upgrade);
                }
                actionBtn.interactable = canUpgrade;
                break;
        }
    }
    void ChangeButtonText()
    {
        resourceTextA.text = !hasUnlock ?
            "Unlock " + config.resourceName :

            config.resourceName + " LV. " + resourceLevel +
            "\n+" + ((config.output + config.output * (1.2f * resourceLevel + 1)) - output).ToString("0.000");

        resouceTextB.text = !hasUnlock ?
            "Unlock Cost\n" + config.unlockCost.ToString("0.00") :
            "Upgraed Cost\n" + upgradeCost.ToString("0.00");
    }

    void Unlock()
    {
        double playerMoney = GameManager.Instance.currentMoney;

        if (canUnlock)
        {
            GameManager.Instance.IncreaseMoney(-config.unlockCost);
            hasUnlock = true;
            resourceLevel++;
            actionBtn.onClick.RemoveListener(Unlock);

            CheckAchivement();
        }
    }

    void CheckAchivement()
    {
        switch (config.resourceName)
        {
            case "Intern":
                AchivementHandle.Instance.UnlockAchivement("unlock_intern");
                break;
            case "Junior":
                AchivementHandle.Instance.UnlockAchivement("unlock_junior");
                break;

        }
    }
    void Upgrade()
    {

        if (canUpgrade)
        {
            GameManager.Instance.IncreaseMoney(-upgradeCost);
            resourceLevel++;
        }
    }



    [System.Serializable]
    public struct ResourceConfig
    {
        public string resourceName;
        public double unlockCost;
        public double upgradeCost;
        public double output;
    }
}
