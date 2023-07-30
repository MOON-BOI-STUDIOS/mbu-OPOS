using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using DG.Tweening;
using Solana.Unity.SDK.Example;
public enum ArcadeType
{
    Racing, Shooting, Fishing
}

public class ArcadeMacineManager : MonoBehaviour
{
    public static ArcadeMacineManager Inst;
    [Header("arcadeCollisionDetection")]
    [SerializeField] arcadeCollisionDetection[] ACDlist;
    [Header("Buttons")]
    [SerializeField] Button button_shooting;
    [SerializeField] Button button_fishing;
    [SerializeField] Button button_racing;
    [SerializeField] Button button_donate;
    [Header("UI")]
    [SerializeField] GameObject RepareUI;

    [Header("Wallet")]
    public TMP_Text _buttonText;
    public TMP_Text _TransferDetails;
    public Button _SendButton;
    public GameObject wallet;
    public GameObject Background;
    public PayToPlay _paytoPlay;

    public static Animator CurrentAnimator;
    public static arcadeCollisionDetection currentACD;
    public static bool isUIopen = false;

    [Space]
    public ulong requiredAmountForRacing = 2500000; // Define the required amount for the racing game

    [Header("GameUI")]
    [SerializeField] GameObject RaceGameUI;
    [SerializeField] GameObject FishingGameUI;
    [SerializeField] GameObject BattleGameUI;
    [SerializeField] GameObject ParentGameUI;
    void Awake()
    {
        PlayerPrefs.SetInt("LastLocation", 3);
        Inst = this;
        // Check if racing is active and deactivate the button if true
        if (PlayerPrefs.GetInt("isRaceActive", 0) == 1)
        {
            button_racing.gameObject.SetActive(false);
        }
        // Check if shooting is active and deactivate the button if true
        if (PlayerPrefs.GetInt("isShootingActive", 0) == 1)
        {
            button_shooting.gameObject.SetActive(false);
        }
        // Check if fishing is active and deactivate the button if true
        if (PlayerPrefs.GetInt("isFishingActive", 0) == 1)
        {
            button_fishing.gameObject.SetActive(false);
        }
        CheckButtonsState();
        for (int i = 0; i < ACDlist.Length; i++)
        {
            int arcadeTypeValue = PlayerPrefs.GetInt("arcade" + i.ToString(), -1);
            // Check the type of arcade and call the respective load function
            switch (arcadeTypeValue)
            {
                case 0: // Racing
                    LoadRacing(ACDlist[i]);
                    break;
                case 1: // Shooting
                    LoadShooting(ACDlist[i]);
                    break;
                case 2: // Fishing
                    LoadFishing(ACDlist[i]);
                    break;
            }
        }
    }

    public void openRepairUI()
    {
        isUIopen = true;
        RepareUI.SetActive(true);
        RepareUI.transform.DOScale(1, 0.3f).SetEase(Ease.InCirc);
    }

    public void CloseRepairUI()
    {
        isUIopen = false;
        RepareUI.transform.DOScale(0, 0.3f).SetEase(Ease.InCirc);
    }

    public void TryAndProcessTransactionRacing()
    {
        if (currentACD == null)
            return;

        _buttonText.text = "2.5M BONKS";
        _TransferDetails.gameObject.SetActive(true);
        _TransferDetails.text = "Upgrade Racing Game";
        wallet.SetActive(true);
        Background.SetActive(true);
        _SendButton.gameObject.SetActive(true);

        // Remove all existing listeners from the _SendButton
        _SendButton.onClick.RemoveAllListeners();

        // Add a new listener to the _SendButton to try to process the transaction for repairing the racing game
        _SendButton.onClick.AddListener(() => _paytoPlay.TryPayToPlay(requiredAmountForRacing, RepairRacing, HandleTransactionFailure));
    }


    public void TryAndProcessTransactionFishing()
    {
        if (currentACD == null)
            return;

        _buttonText.text = "2.5M BONKS";
        _TransferDetails.gameObject.SetActive(true);
        _TransferDetails.text = "Upgrade Fishing Game";
        wallet.SetActive(true);
        Background.SetActive(true);
        _SendButton.gameObject.SetActive(true);

        // Remove all existing listeners from the _SendButton
        _SendButton.onClick.RemoveAllListeners();

        // Add a new listener to the _SendButton to try to process the transaction for repairing the fishing game
        _SendButton.onClick.AddListener(() => _paytoPlay.TryPayToPlay(requiredAmountForRacing, RepairFishing, HandleTransactionFailure));
    }

    public void TryAndProcessTransactionShooting()
    {
        if (currentACD == null)
            return;

        _buttonText.text = "2.5M BONKS";
        _TransferDetails.gameObject.SetActive(true);
        _TransferDetails.text = "Upgrade Shooting Game";
        wallet.SetActive(true);
        Background.SetActive(true);
        _SendButton.gameObject.SetActive(true);

        // Remove all existing listeners from the _SendButton
        _SendButton.onClick.RemoveAllListeners();

        // Add a new listener to the _SendButton to try to process the transaction for repairing the shooting game
        _SendButton.onClick.AddListener(() => _paytoPlay.TryPayToPlay(requiredAmountForRacing, RepairShooting, HandleTransactionFailure));
    }

    public void TryAndProcessTransactionDonate()
    {
        _buttonText.text = "2.5 BONKS";
        _TransferDetails.gameObject.SetActive(true);
        _TransferDetails.text = "Donate Builder";
        wallet.SetActive(true);
        Background.SetActive(true);
        _SendButton.gameObject.SetActive(true);

        // Remove all existing listeners from the _SendButton
        _SendButton.onClick.RemoveAllListeners();

        // Add a new listener to the _SendButton to try to process the transaction for repairing the shooting game
        _SendButton.onClick.AddListener(() => _paytoPlay.TryPayToPlay(requiredAmountForRacing, donated, HandleTransactionFailure, "donated"));
    }

    public void donated()
    {

    }
    public void RepairRacing()
    {
        if (currentACD == null)
            return;

        CloseRepairUI();
        int index = Array.IndexOf(ACDlist, currentACD);
        PlayerPrefs.SetInt("arcade" + index, 0);
        PlayerPrefs.SetInt("isRaceActive", 1);
        button_racing.gameObject.SetActive(false);
        CheckButtonsState();
        LoadRacing(currentACD);
    }

    void LoadRacing(arcadeCollisionDetection ACD)
    {
        ACD._am.enabled = true;
        ACD._am.SetTrigger("isRacing");
        ACD._AT = ArcadeType.Racing;
        ACD.isbroken = false;
    }

    public void RepairShooting()
    {
        if (currentACD == null)
            return;

        CloseRepairUI();
        int index = Array.IndexOf(ACDlist, currentACD);
        PlayerPrefs.SetInt("arcade" + index, 1);
        PlayerPrefs.SetInt("isShootingActive", 1);
        button_shooting.gameObject.SetActive(false);
        CheckButtonsState();
        LoadShooting(currentACD);
    }

    void LoadShooting(arcadeCollisionDetection ACD)
    {
        ACD._am.enabled = true;
        ACD._am.SetTrigger("isBattle");
        ACD._AT = ArcadeType.Shooting;
        ACD.isbroken = false;
    }

    public void RepairFishing()
    {
        if (currentACD == null)
            return;

        CloseRepairUI();
        int index = Array.IndexOf(ACDlist, currentACD);
        PlayerPrefs.SetInt("arcade" + index.ToString(), 2);
        PlayerPrefs.SetInt("isFishingActive", 1);
        button_fishing.gameObject.SetActive(false);
        CheckButtonsState();
        LoadFishing(currentACD);
    }

    void LoadFishing(arcadeCollisionDetection ACD)
    {
        ACD._am.enabled = true;
        ACD._am.SetTrigger("isFishing");
        ACD._AT = ArcadeType.Fishing;
        ACD.isbroken = false;
    }

    public void CheckButtonsState()
    {
        if (!button_shooting.gameObject.activeSelf &&
            !button_fishing.gameObject.activeSelf &&
            !button_racing.gameObject.activeSelf)
        {
            button_donate.gameObject.SetActive(true);
        }
        else
        {
            button_donate.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs have been reset!");
        }
    }

    private void HandleTransactionFailure(string reason)
    {
        //MessageBox.SetActive(true);
        // _TextMessage.text = "Failed to transfer tokens. Cannot repair the racing game.";
        Debug.Log($"Failed to transfer tokens. Reason: {reason}");
    }

    public void openPlayButton(ArcadeType at)
    {
        // Deactivate all the UI elements first
        RaceGameUI.SetActive(false);
        FishingGameUI.SetActive(false);
        BattleGameUI.SetActive(false);

        RaceGameUI.transform.localScale=new Vector3(0, 0, 0);
        FishingGameUI.transform.localScale = new Vector3(0, 0, 0);
        BattleGameUI.transform.localScale = new Vector3(0, 0, 0);
        ParentGameUI.transform.localScale = new Vector3(1, 1, 1);
        ParentGameUI.SetActive(true);
        // Activate the selected UI element based on the ArcadeType
        switch (at)
        {
            case ArcadeType.Racing:
                RaceGameUI.SetActive(true);
                RaceGameUI.transform.DOScale(1, 0.3f).SetEase(Ease.InCirc);
                break;
            case ArcadeType.Fishing:
                FishingGameUI.SetActive(true);
                FishingGameUI.transform.DOScale(1, 0.3f).SetEase(Ease.InCirc);
                break;
            case ArcadeType.Shooting:
                BattleGameUI.SetActive(true);
                BattleGameUI.transform.DOScale(1, 0.3f).SetEase(Ease.InCirc);
                break;
            default:
                Debug.LogError("Unknown ArcadeType");
                break;
        }
        isUIopen = true;

    }

    public void ClosePlayButton()
    {
        isUIopen = false;
        ParentGameUI.transform.DOScale(0, 0.3f).SetEase(Ease.InCirc);
    }
}
