using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PaystackManager : MonoBehaviour
{
    [Header("Paystack API Keys")]
    private string secretKey = "sk_test_f645e3354e753f88424f08af56e27ce4364737d5"; // Replace with your secret key

    [Header("UI Elements")]
    public TMP_Dropdown bankDropdown;
    public TMP_InputField accountNumberInput;
    public TextMeshProUGUI accountNameText;
    public Button verifyButton;
    public Button withdrawButton;

    public TextMeshProUGUI somethingWentWrongText;

    private Dictionary<string, string> bankCodeMap = new Dictionary<string, string>();
    private string recipientCode = "";

    void Start()
    {
        withdrawButton.interactable = false;
        somethingWentWrongText.gameObject.SetActive(false);
        StartCoroutine(GetBankList());
        verifyButton.onClick.AddListener(VerifyAccount);
        withdrawButton.onClick.AddListener(InitiateWithdrawal);
    }

    // ✅ Step 1: Fetch Bank List and Populate Dropdown
    private IEnumerator GetBankList()
    {
        Debug.Log("Getting Request -------------------------");
        string url = "https://api.paystack.co/bank";
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authorization", "Bearer " + secretKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Bank List: " + request.downloadHandler.text);
            
            // Corrected JSON parsing
            string jsonText = request.downloadHandler.text;
            var json = JsonUtility.FromJson<BankListResponse>(jsonText);

            bankDropdown.ClearOptions();

            List<string> bankNames = new List<string>();
            foreach (var bank in json.data)
            {
                bankNames.Add(bank.name);
                bankCodeMap[bank.name] = bank.code;
            }

            bankDropdown.AddOptions(bankNames);
        }
        else
        {
            Debug.LogError("Failed to Get Banks: " + request.error);
            somethingWentWrongText.gameObject.SetActive(true);
            StartCoroutine(WaitForAnimation());
        }
    }

    // ✅ Step 2: Verify Account Number
    private void VerifyAccount()
    {
        string selectedBank = bankDropdown.options[bankDropdown.value].text;
        string bankCode = bankCodeMap[selectedBank];
        string accountNumber = accountNumberInput.text;

        Debug.Log($"Bank: {selectedBank}, Bank Code: {bankCode}, Account Number: {accountNumber}");

        if (string.IsNullOrEmpty(accountNumber))
        {
            Debug.LogError("Enter a valid account number!");
            somethingWentWrongText.gameObject.SetActive(true);
            StartCoroutine(WaitForAnimation());
            return;
        }

        StartCoroutine(VerifyBankAccount(bankCode, accountNumber));
    }

    private IEnumerator VerifyBankAccount(string bankCode, string accountNumber)
    {
        string url = $"https://api.paystack.co/bank/resolve?account_number={accountNumber}&bank_code={bankCode}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authorization", "Bearer " + secretKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Account Verified: " + request.downloadHandler.text);

            var response = JsonUtility.FromJson<AccountVerificationResponse>(request.downloadHandler.text);
            accountNameText.text = response.data.account_name;
            
            // ✅ Step 3: Create a Transfer Recipient
            StartCoroutine(CreateTransferRecipient(accountNumber, bankCode, response.data.account_name));
        }
        else
        {
            Debug.LogError("Account Verification Failed: " + request.error);
            somethingWentWrongText.gameObject.SetActive(true);
            StartCoroutine(WaitForAnimation());
        }
    }

    // ✅ Step 3: Create Transfer Recipient
    private IEnumerator CreateTransferRecipient(string accountNumber, string bankCode, string playerName)
    {
        string url = "https://api.paystack.co/transferrecipient";
        string jsonPayload = $"{{\"type\":\"nuban\",\"name\":\"{playerName}\",\"account_number\":\"{accountNumber}\",\"bank_code\":\"{bankCode}\",\"currency\":\"NGN\"}}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + secretKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Recipient Created: " + request.downloadHandler.text);
            var response = JsonUtility.FromJson<RecipientResponse>(request.downloadHandler.text);
            recipientCode = response.data.recipient_code;

            withdrawButton.interactable = true;
        }
        else
        {
            Debug.LogError("Failed to Create Recipient: " + request.error);
            somethingWentWrongText.gameObject.SetActive(true);
            StartCoroutine(WaitForAnimation());
        }
    }

    // ✅ Step 4: Initiate Withdrawal
    private void InitiateWithdrawal()
    {
        if (string.IsNullOrEmpty(recipientCode))
        {
            Debug.LogError("Recipient not created! Verify account first.");
            somethingWentWrongText.gameObject.SetActive(true);
            StartCoroutine(WaitForAnimation());
            return;
        }

        int amountInKobo = PlayerPrefs.GetInt("NGN", 0) * 100;
        StartCoroutine(InitiateTransfer(recipientCode, amountInKobo, "Game earnings withdrawal"));
    }

    private IEnumerator InitiateTransfer(string recipientCode, int amountInKobo, string reason)
    {
        string url = "https://api.paystack.co/transfer";
        string jsonPayload = $"{{\"source\":\"balance\",\"amount\":{amountInKobo},\"recipient\":\"{recipientCode}\",\"reason\":\"{reason}\",\"currency\":\"NGN\"}}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + secretKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Withdrawal Successful: " + request.downloadHandler.text);
            PlayerPrefs.SetInt("NGN", 0);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("Withdrawal Failed: " + request.error);
            Debug.LogError("Error Details: " + request.downloadHandler.text);
            somethingWentWrongText.gameObject.SetActive(true);
            StartCoroutine(WaitForAnimation());
        }
    }

    private IEnumerator WaitForAnimation(){
        yield return new WaitForSeconds(4);
        somethingWentWrongText.gameObject.SetActive(false);
    }

    // JSON Data Classes
    [System.Serializable]
    private class Bank
    {
        public string name;
        public string code;
    }

    [System.Serializable]
    private class BankListResponse
    {
        public Bank[] data;
    }

    [System.Serializable]
    private class AccountVerificationResponse
    {
        public VerificationData data;
    }

    [System.Serializable]
    private class VerificationData
    {
        public string account_name;
    }

    [System.Serializable]
    private class RecipientResponse
    {
        public RecipientData data;
    }

    [System.Serializable]
    private class RecipientData
    {
        public string recipient_code;
    }
}
