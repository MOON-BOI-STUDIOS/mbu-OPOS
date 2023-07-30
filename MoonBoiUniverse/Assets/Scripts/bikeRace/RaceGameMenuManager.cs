using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Solana.Unity.SDK.Example;
using System;

public class RaceGameMenuManager : MonoBehaviour
{
    [Header("Bike Details")]
    [SerializeField] private Image bikeImage;
    [SerializeField] private GameObject lives;
    [SerializeField] private TMP_Text BoostTimeText;
    [SerializeField] private TMP_Text ButtonText;
    [SerializeField] private Image ButtonBg;
    [Header("Buttons")]
    [SerializeField] private Button buyButton; // Reference to the buy button
    [SerializeField] private Button PlayButton;

    [Header("Bike Data")]
    [SerializeField] private BikeData[] bikeDataArray = new BikeData[5];

    private int currentIndex = 0;
    private static readonly string CurrentBikePrefKey = "CurrentBikeIndex";


    [Header("Wallet")]
    public TMP_Text _buttonText;
    public TMP_Text _TransferDetails;
    public Button _SendButton;
    public GameObject wallet;
    public GameObject Background;
    public PayToPlay _paytoPlay;

    void Start()
    {
        currentIndex = PlayerPrefs.GetInt(CurrentBikePrefKey, 0);
        LoadBikeData(currentIndex);
        UpdateBuyButtonState();
    }

    void Update()
    {
        // You can add any per-frame logic here
    }

    public void MoveLeft()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = bikeDataArray.Length - 1;

        LoadBikeData(currentIndex);
        UpdateBuyButtonState();
    }

    public void MoveRight()
    {
        currentIndex++;
        if (currentIndex >= bikeDataArray.Length)
            currentIndex = 0;

        LoadBikeData(currentIndex);
        UpdateBuyButtonState();
    }

    public void TryAndProcessTransactionBikeUpgrade()
    {
        if (currentIndex == PlayerPrefs.GetInt(CurrentBikePrefKey, 0))
        {
            BikeData bikeData = bikeDataArray[currentIndex];
            _buttonText.text = bikeData.UpgradePrice.ToString()+" BONKS";
            _TransferDetails.gameObject.SetActive(true);
            _TransferDetails.text = "Upgrade Bike";
            wallet.SetActive(true);
            Background.SetActive(true);
            _SendButton.gameObject.SetActive(true);

            // Remove all existing listeners from the _SendButton
            _SendButton.onClick.RemoveAllListeners();

            // Add a new listener to the _SendButton to try to process the transaction for repairing the racing game
            _SendButton.onClick.AddListener(() => _paytoPlay.TryPayToPlay((ulong)bikeData.UpgradePrice, Buy, HandleTransactionFailure));
        }
    }

    private void HandleTransactionFailure(string obj)
    {
        throw new NotImplementedException();
    }

    public void Buy()
    {
        if (currentIndex == PlayerPrefs.GetInt(CurrentBikePrefKey, 0))
        {
            PlayerPrefs.SetInt(CurrentBikePrefKey, currentIndex + 1);
            PlayerPrefs.Save();
            Debug.Log("Bike bought! Current bike index: " + (currentIndex + 1));
            UpdateBuyButtonState();
        }
        else
        {
            Debug.LogWarning("You can only buy the next bike in the sequence!");
        }
    }

    private void LoadBikeData(int index)
    {
        BikeData bikeData = bikeDataArray[index];
        bikeImage.sprite = bikeData.BikeImage;

        for (int i = 0; i < lives.transform.childCount; i++)
        {
            lives.transform.GetChild(i).gameObject.SetActive(i < bikeData.Health);
        }

        BoostTimeText.text = bikeData.Boost.ToString()+" SEC";
        ButtonText.text = bikeData.UpgradePrice + " BONK";
    }

    private void UpdateBuyButtonState()
    {
        buyButton.interactable = currentIndex == PlayerPrefs.GetInt(CurrentBikePrefKey, 0);
        buyButton.gameObject.SetActive(currentIndex == PlayerPrefs.GetInt(CurrentBikePrefKey, 0));
        PlayButton.gameObject.SetActive(currentIndex <= PlayerPrefs.GetInt(CurrentBikePrefKey, 0));
    }

    public void Play()
    {
        PlayerPrefs.SetInt("CurrentPlayBikeIndex", currentIndex);
    }

   
}
