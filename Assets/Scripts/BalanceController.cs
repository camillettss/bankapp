using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceController : MonoBehaviour
{
    public Text balance;
    public Transform historyContent;
    [SerializeField] MenuController menuController;

    [SerializeField] Text historyTextPrefab;

    List<HEvent> History = new List<HEvent>();
    float myBalance = 0;
    
    public static BalanceController instance;

    private void Start()
    {
        instance = this;

        var data = SaveSystem.Load();

        History = data.history;
        myBalance = data.latestAmount;

        UpdateUI();
    }

    public void onAdd()
    {
        menuController.Activate(true);
    }

    public void onSub()
    {
        menuController.Activate(false);
    }

    void UpdateUI()
    {
        balance.text = $"{myBalance}€";

        foreach(Transform child in historyContent) 
            Destroy(child.gameObject);

        for(int i = 0; i < History.Count; i++)
        {
            var slot = Instantiate(historyTextPrefab, historyContent);
            slot.text = $"{History[i].amount}, {History[i].motivation}";
        }
    }

    public void UpdateBalance(float amount, bool adding, string motivation)
    {
        if (!adding) amount = -amount;

        myBalance += amount;

        HEvent ev = new HEvent(amount, motivation);
        History.Add(ev);

        UpdateUI();
    }

    private void OnDestroy()
    {
        print("saving");
        SaveSystem.Save(History, myBalance);
    }
}

public class HEvent
{
    public float amount;
    public string motivation;

    public HEvent(float amount, string motivation)
    {
        this.amount = amount;
        this.motivation = motivation;
    }
}