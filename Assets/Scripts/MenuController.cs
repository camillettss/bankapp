using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] InputField BalanceInput;
    [SerializeField] InputField MotivationInput;
    bool adding;

    public void Activate(bool isAdding)
    {
        gameObject.SetActive(true);
        adding = isAdding;
    }

    public void OnButtonDown()
    {
        var val = float.Parse(BalanceInput.text);
        BalanceController.instance.UpdateBalance(val, adding, MotivationInput.text);

        gameObject.SetActive(false);
    }
}
