using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LabelsAndPoints : MonoBehaviour
{
    [SerializeField] private Button m_LabelButton;
    [SerializeField] private Button m_RestartButton;
    [SerializeField] private GameObject clientLabelPrefab;
    [SerializeField] private GameObject parentObject;

    [SerializeField] private TextMeshProUGUI m_headingText;

    public delegate void PopUp(string clientId);
    public static event PopUp OnPopUpOpen;
    private void Awake()
    {
        m_LabelButton.onClick.AddListener(LabelsAndPointsPrint);
        m_RestartButton.onClick.AddListener(Restart);
        parentObject.GetComponent<GridLayoutGroup>().enabled = false;
    }

    public void LabelsAndPointsPrint()
    {

        MS_API m = api_helper.ApiRead();

        // Clear any existing client labels
        ClearClientLabels();
        parentObject.GetComponent<GridLayoutGroup>().enabled = true;

        m_headingText.text = "All Clients";


        for (int i = 0; i < m.clients.Count; i++)
        {
            InstantiateClientLabel(m.clients[i].label, parentObject.transform);
        }
    }

    private void ClearClientLabels()
    {
        GameObject[] existingLabels = GameObject.FindGameObjectsWithTag("ClientLabel");
        foreach (GameObject label in existingLabels)
        {
            Destroy(label);
        }
    }

    private void InstantiateClientLabel(string labelText, Transform parent)
    {
        GameObject clientLabelObject = Instantiate(clientLabelPrefab, parent);

        Button buttonComponent = clientLabelObject.GetComponent<Button>();
        if (buttonComponent == null)
        {
            buttonComponent = clientLabelObject.AddComponent<Button>();
        }

        if (buttonComponent.onClick.GetPersistentEventCount() == 0)
        {
            buttonComponent.onClick.AddListener(() => PrintClientLabel(labelText));
        }
 
        TMP_Text textComponent = clientLabelObject.GetComponent<TMP_Text>();

        textComponent.text = labelText;
    }

    private void PrintClientLabel(string labelText)
    {
        OnPopUpOpen.Invoke(labelText);
        print("Client Label: " + labelText);

    }


    public void Restart()
    {
        print("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
