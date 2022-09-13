using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleManager : MonoBehaviour
{
    [HideInInspector]
    public int length;
    [HideInInspector]
    public int strength;
    [HideInInspector]
    public int offlineEarnings;
    [HideInInspector]
    public int lengthCost;
    [HideInInspector]
    public int strenghtCost;
    [HideInInspector]
    public int offlineEarningCost;
    [HideInInspector]
    public int wallet;
    [HideInInspector]
    public int totalGain;
    public int[] costs = new int[50];
    public static IdleManager instance;

    private void Awake()
    {
        costs = CreateCosts(50);
        if (IdleManager.instance)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
        else
        {
            IdleManager.instance = this;
        }
        length = -PlayerPrefs.GetInt("Length", 30);
        strength = PlayerPrefs.GetInt("Strength", 3);
        offlineEarnings = PlayerPrefs.GetInt("Offline", 3);
        lengthCost = costs[-length / 10 - 3];
        strenghtCost = costs[strength - 3];
        offlineEarningCost = costs[offlineEarnings - 3];
        wallet = PlayerPrefs.GetInt("Wallet", 0);
        

    }

    private void OnApplicationPause(bool paused)
    {
        if(paused)
        {
            DateTime now = DateTime.Now;
            PlayerPrefs.SetString("Date", now.ToString());
            
        } 
        else
        {
            string @string = PlayerPrefs.GetString("Date", string.Empty);
            if(@string != string.Empty)
            {
                DateTime d = DateTime.Parse(@string);
                totalGain = (int)((DateTime.Now - d).TotalMinutes * offlineEarnings + 1.0f);
                ScreenManager.instance.ChangeScreen(Screens.RETURN);
            }
        }
    }
    private void OnApplicationQuit()
    {
        OnApplicationPause(true);
    }
    public void BuyLength()
    {
        
        length -= 10;
        wallet -= lengthCost;
        lengthCost = costs[-length / 10 - 3];
        
        PlayerPrefs.SetInt("Length", -length);
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.ChangeScreen(Screens.MAIN); 
    }
    public void BuyOffline()
    {
        offlineEarnings++;
        wallet -= offlineEarningCost;
        offlineEarningCost = costs[offlineEarnings- 3];
        PlayerPrefs.SetInt("Offline", offlineEarnings);
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.ChangeScreen(Screens.MAIN);
    }
    public void BuyStrength()
    {
        strength++;
        wallet -= strenghtCost;
        strenghtCost = costs[strength - 3];
        PlayerPrefs.SetInt("Strength", strength);
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.ChangeScreen(Screens.MAIN);
    }
    public void CollectMoney()
    {
        wallet += totalGain;
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.ChangeScreen(Screens.MAIN);
    }
    public void CollectDoubleMoney()
    {
        wallet += totalGain*2;
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.ChangeScreen(Screens.MAIN);


    }

    int[] CreateCosts(int length)
    {
        int[] costs = new int[length];
        costs[0] = 120;
        for(int i = 1; i < length; i++)
        {
            costs[i] = (int)(costs[i-1] + costs[i-1] * 0.15f);
        }
        return costs;
    }
}
