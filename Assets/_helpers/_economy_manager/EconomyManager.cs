using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "EconomyManager", menuName = "Game/EconomyManager")]
public class EconomyManager : ScriptableObject
{
    [SerializeField] private List<CurrencyData> currencies = new List<CurrencyData>();

    [SerializeField] private PlayerData playerData;
    

    private const string SaveKey = "EconomyData";

    private static EconomyManager instance;

    public static EconomyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<EconomyManager>("EconomyManager");
                if (instance == null)
                {
                    Debug.LogError("EconomyManager asset not found in Resources folder!");
                }
                else
                {
                    instance.LoadEconomy();
                }
            }
            return instance;
        }
    }

    public int GetCurrencyAmount(CurrencyType currencyType)
    {
        var currency = currencies.Find(c => c.Type == currencyType);
        return currency != null ? currency.Amount : 0;
    }

    public void SetCurrencyAmount(CurrencyType currencyType, int amount)
    {
        var currency = currencies.Find(c => c.Type == currencyType);
        if (currency != null)
        {
            currency.Amount = amount;
        }
        else
        {
            currencies.Add(new CurrencyData { Type = currencyType, Amount = amount });
        }
    }

    public void AddCurrency(CurrencyType currencyType, int amount)
    {
        var currentAmount = GetCurrencyAmount(currencyType);
        SetCurrencyAmount(currencyType, currentAmount + amount);
    }

    public void SubtractCurrency(CurrencyType currencyType, int amount)
    {
        var currentAmount = GetCurrencyAmount(currencyType);
        SetCurrencyAmount(currencyType, Mathf.Max(0, currentAmount - amount));
    }

    public void SaveEconomy()
    {
        string jsonData = JsonUtility.ToJson(this, true);
        File.WriteAllText(GetSavePath(), jsonData);
    }

    public void LoadEconomy()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(jsonData, this);
        }
    }

    public void ResetEconomy()
    {
        currencies.Clear();
        File.Delete(GetSavePath());
    }

    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "economy_data.json");
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    #if UNITY_EDITOR
    private void OnDisable()
    {
        if (!Application.isPlaying)
        {
            ResetEconomy();
        }
    }
    #endif
}
