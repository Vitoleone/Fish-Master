using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;

    private GameObject currentScreen;

    public GameObject endScreen;
    public GameObject gameScreen;
    public GameObject mainScreen;
    public GameObject returnScreen;

    public Button lengthButton;
    public Button offlineButton;
    public Button strengthButton;

    public TextMeshProUGUI gameScreenMoney;
    public TextMeshProUGUI lengthCostText;
    public TextMeshProUGUI lengthValueText;
    public TextMeshProUGUI strengthCostText;
    public TextMeshProUGUI stregthValueText;
    public TextMeshProUGUI offlineCostText;
    public TextMeshProUGUI offlineValueText;
    public TextMeshProUGUI endScreenMoney;
    public TextMeshProUGUI returnScreenMoney;

    private int gameCount;
   
    void Awake()
    {
        if(ScreenManager.instance)
        {
            Destroy(base.gameObject);
        }
        else
        {
            ScreenManager.instance = this;
        }
        currentScreen = mainScreen;
    }

    private void Start()
    {
        CheckIdles();
        UpdateTexts();
    }
    public void ChangeScreen(Screens screen)
    {
        currentScreen.SetActive(false);
        switch(screen)
        {
            case Screens.MAIN:
                currentScreen = mainScreen;
                UpdateTexts();
                CheckIdles();
                break;
            case Screens.GAME:
                currentScreen = gameScreen;
                gameCount++;
                break;
            case Screens.END:
                currentScreen = endScreen;
                SetEndScreenMoney();
                break;
            case Screens.RETURN:
                currentScreen = returnScreen;
                SetReturnScreenMoney();
                break;
        }
        currentScreen.SetActive(true);
    }
    public void SetEndScreenMoney()
    {
        endScreenMoney.text = "$" + IdleManager.instance.totalGain;
    }
    public void SetReturnScreenMoney()
    {
        returnScreenMoney.text = "$" + IdleManager.instance.totalGain + " gained while offline!";
    }
   
    public void UpdateTexts()
    {
        gameScreenMoney.text = "$" + IdleManager.instance.wallet;
        lengthCostText.text = "$" + IdleManager.instance.lengthCost;
        lengthValueText.text = -IdleManager.instance.length + "m";
        strengthCostText.text = "$" + IdleManager.instance.strenghtCost;
        stregthValueText.text = IdleManager.instance.strength + " fishes";
        offlineCostText.text = "$" + IdleManager.instance.offlineEarningCost;
        offlineValueText.text = "$" + IdleManager.instance.offlineEarnings + "/min";

    }
    public void CheckIdles()
    {
        int lengthCost = IdleManager.instance.lengthCost;
        int strengthCost = IdleManager.instance.strenghtCost;
        int offlineEarningsCost = IdleManager.instance.offlineEarningCost;
        int wallet = IdleManager.instance.wallet;
        if(wallet < lengthCost)
        {
            lengthButton.interactable = false;
        }
        else
        {
            lengthButton.interactable = true;
        }

        if (wallet < strengthCost)
        {
            strengthButton.interactable = false;
        }
        else
        {
            strengthButton.interactable = true;
        }

        if (wallet < offlineEarningsCost)
        {
            offlineButton.interactable = false;
        }
        else
        {
            offlineButton.interactable = true;
        }


    }
}
