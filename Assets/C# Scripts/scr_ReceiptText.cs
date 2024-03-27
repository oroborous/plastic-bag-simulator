using UnityEngine;
using UnityEngine.UI;

public class scr_ReceiptText : MonoBehaviour {

    public Text receiptText;

    void Start ()
    {
        int count = scr_PlayerMovement.getReceipts();
        receiptText.text = "Receipts " + count.ToString("n0");
	}
	
}
