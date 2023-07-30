using Solana.Unity.Rpc.Core.Http;
using Solana.Unity.Rpc.Models;
using Solana.Unity.Wallet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Solana.Unity.Rpc.Types;
using System.Linq;
using System;


// ReSharper disable once CheckNamespace

namespace Solana.Unity.SDK.Example
{
    public class PayToPlay : MonoBehaviour
    {
        public Button payToPlayBtn;
        public GameObject MessageBox;
        public TMP_Text _TextMessage;
        private string bonkMintAddress = "DezXAZ8z7PnrnRJjz3wXBoRgixCa6xjnB7YaB1pPB263"; // BONK
        private string destinationAddress = "B2Vh4JS8Q5eQawJZUq7JbmNdnyDRvBmDsFHas7havGxu";
        private ulong requiredAmount = 20000;

        private void Start()
        {
           // payToPlayBtn.onClick.AddListener(TryPayToPlay);
        }

        public async void TryPayToPlay(ulong requiredAmount, Action onSuccess, Action<string> onFailure, string actionType = "")
        {
            // Get the user's BONK token account
            var tokenAccounts = await Web3.Wallet.GetTokenAccounts(Commitment.Confirmed);
            var bonkTokenAccount = tokenAccounts.FirstOrDefault(t => t.Account.Data.Parsed.Info.Mint == bonkMintAddress);
            if (bonkTokenAccount == null)
            {
                MessageBox.SetActive(true);
                _TextMessage.text = "You do not own any BONK tokens.";
                Debug.Log("You do not own any BONK tokens.");
                onFailure?.Invoke("You do not own any BONK tokens.");
                return;
            }

            // Check if the user has enough BONK tokens
            var userBonkAmount = bonkTokenAccount.Account.Data.Parsed.Info.TokenAmount.AmountUlong;
            if (userBonkAmount < requiredAmount)
            {
                MessageBox.SetActive(true);
                _TextMessage.text = $"You do not have enough BONK tokens. You have {userBonkAmount}, but need {requiredAmount} to play.";
                Debug.Log($"You do not have enough BONK tokens. You have {userBonkAmount}, but need {requiredAmount} to play.");
                onFailure?.Invoke($"You do not have enough BONK tokens. You have {userBonkAmount}, but need {requiredAmount} to play.");

                return;
            }

            // Transfer the BONK tokens
            RequestResult<string> result = await Web3.Instance.WalletBase.Transfer(
                new PublicKey(destinationAddress),
                new PublicKey(bonkMintAddress),
                requiredAmount);
            if (result.Result != null)
            {
                MessageBox.SetActive(true);
                _TextMessage.text = "Successfully transferred BONK tokens. You can now play the game.";
                if (actionType == "donated")
                {
                    _TextMessage.text = "Thank you for your contribution";
                }

                Debug.Log("Successfully transferred BONK tokens. You can now play the game.");
                onSuccess?.Invoke();
            }
            else
            {
                MessageBox.SetActive(true);
                _TextMessage.text = $"Failed to transfer BONK tokens. Reason: {result.Reason}";
                Debug.Log($"Failed to transfer BONK tokens. Reason: {result.Reason}");
                onFailure?.Invoke(result.Reason);
            }
        }
    }
}