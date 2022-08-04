using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] ResourceManager sourceManager;
    WaitForSeconds delayAutoCollect = new WaitForSeconds(1);
    public double currentMoney { private set; get; }

    [HideInInspector] public UnityEvent onMoneyChange;

    [SerializeField] UnityEngine.UI.Text scoreText;
    [SerializeField] UnityEngine.UI.Text autoCollectAmountText;

    [SerializeField] ClickGoldText clickTextPrefabs;
    Queue<ClickGoldText> storedClickText = new Queue<ClickGoldText>();
    [SerializeField] Transform clickTextPrefabPlace;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        onMoneyChange.AddListener(UpdateUI);
        onMoneyChange.AddListener(CheckAchivement);
        AutoCollect();
    }

    void UpdateUI()
    {
        scoreText.text = "Score : " + currentMoney.ToString("0.00");
        autoCollectAmountText.text = "Auto-Collect : " + sourceManager.GetAutoCollectGold().ToString("0.00") + " /second ";
    }

    void AutoCollect()
    {
        StartCoroutine(CollectGoldAuto());

        IEnumerator CollectGoldAuto()
        {
            while (true)
            {
                yield return delayAutoCollect;
                IncreaseMoney(sourceManager.GetAutoCollectGold());
            }
        }
    }

    internal void IncreaseMoney(double increaseAmount)
    {
        currentMoney += increaseAmount;
        onMoneyChange?.Invoke();
    }
    void CheckAchivement()
    {
        if (currentMoney >= 1000) AchivementHandle.Instance.UnlockAchivement("reach_1000");
        if (currentMoney >= 10000) AchivementHandle.Instance.UnlockAchivement("reach_10000");
        if (currentMoney >= 100000) AchivementHandle.Instance.UnlockAchivement("reach_100000");

    }

    public void ClickCollect()
    {
        var clickValue = sourceManager.GetClickGold();

        IncreaseMoney(clickValue);
        SpawnClickText(clickValue);
    }

    void SpawnClickText(double textValue)
    {
        ClickGoldText createdClick;
        if (storedClickText.Count <= 0)
        {
            createdClick = Instantiate(clickTextPrefabs);
            createdClick.transform.SetParent(clickTextPrefabPlace);
        }
        else createdClick = storedClickText.Dequeue();

        createdClick.Spawn(textValue);
    }
    internal void StoreClickObjectText(ClickGoldText clickGoldText)
    {
        storedClickText.Enqueue(clickGoldText);
    }
}
