using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] private GameObject m_PopUp;
    [SerializeField] private Button m_BlankClick;

    [SerializeField] private Animator m_PopUpAnimation;


    [SerializeField] private TextMeshProUGUI clientIdText;
    [SerializeField] private TextMeshProUGUI m_message;

    private void Awake()
    {
        m_PopUp.SetActive(false);
        LabelsAndPoints.OnPopUpOpen += PopUpToggleOnn;
        DropDown.OnPopUpOpen += PopUpToggleOnn;
        m_BlankClick.onClick.AddListener(PopUpToggleOff);
    }

    public void PopUpToggleOnn(string clientId)
    {
        print("PopUpToggleOnn");
        m_PopUp.SetActive(true);

        m_PopUpAnimation.SetBool("scaleUp", true);

        clientIdText.text = clientId;

        switch (clientId)
        {

            case "Client1":
                PrintClientData(1);
                break;

            case "Client2":
                PrintClientData(2);
                break;

            case "Client3":
                PrintClientData(3);
                break;

            case "Client5":
                m_message.text = "No Data Found";
                break;
        }

    }

    public void PopUpToggleOff()
    {
        print("PopUpToggleOff");

        m_PopUpAnimation.SetBool("scaleUp", false);

        StartCoroutine(PopUpToggleOffCoroutine());
        

    }

    public void PrintClientData(int clientId)
    {
        // Call the ApiRead method to get the MS_API object
        MS_API msApi = api_helper.ApiRead();

        // Check if the MS_API object is valid
        if (msApi == null)
        {
            Debug.LogError("Failed to fetch data from API.");
            return;
        }

        print(msApi);

        // Convert the clientId to string
        string clientIdStr = clientId.ToString();

        // Check if the data dictionary contains the requested clientId
        if (msApi.data.ContainsKey(clientIdStr))
        {
            // Get the Data object associated with the clientId
            Data clientData = msApi.data[clientIdStr];

            // Print the data
            Debug.Log("Address: " + clientData.address);
            Debug.Log("Name: " + clientData.name);
            Debug.Log("Points: " + clientData.points);
        }
        else
        {
            
            Debug.LogWarning("No data found for client with ID: " + clientId);

            m_message.text = $"No data found for client with ID: " + clientId;
        }
    }
    IEnumerator PopUpToggleOffCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        m_PopUp.SetActive(false);
    }
}
