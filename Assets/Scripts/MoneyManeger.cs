using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoneyManeger : MonoBehaviour
{
    public static MoneyManeger Instance;

    [SerializeField] Text moneyText;
    [SerializeField] GameObject warningText;

    [SerializeField] Text reportMoneyText;
    [SerializeField] Text reportRentText;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Text gameOverRentText;

    int money;

    int rent = 100;

    private void Awake()
    {
        Instance = this;
        ChangeMoney(70);
    }

    public void ChangeMoney(int index)
    {
        money += index;
        moneyText.text = "Money : " + money;
    }

    public bool ChechMoney(int index)
    {
        if (money-index >= 0) 
        {
            return true;
        }

        OpenWarning();
        return false;
    }

    public void DaySkip()
    {
        money -= rent;

        if (money < 0)
        {
            gameOverPanel.SetActive(true);
            gameOverPanel.transform.parent.GetChild(0).gameObject.SetActive(false);
            gameOverPanel.transform.parent.GetChild(1).gameObject.SetActive(false);
            gameOverRentText.text = "Congratulations you were able to run the shop for " + DayManeger.instance.currentDay + " days in this economy.";
        }

        moneyText.text = "Money : " + money;
        reportMoneyText.text = "Money : " + money;
        rent = (int)(rent * 1.3f);
        reportRentText.text = "Rent : " + rent;
    }

    void OpenWarning()
    {
        if (warningText.activeSelf)
        {
            return;
        }

        warningText.SetActive(true);
        Invoke("CloseWarning",2);
    }

    void CloseWarning()
    {
        warningText.SetActive(false);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
