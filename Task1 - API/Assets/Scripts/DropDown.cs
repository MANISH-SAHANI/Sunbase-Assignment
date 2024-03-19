using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DropDown : MonoBehaviour
{
    [SerializeField] private GameObject textPrefab; // Prefab for the text object
    [SerializeField] private Transform textParent; // Parent transform for the instantiated text objects
    private List<TMP_Text> clientTexts = new List<TMP_Text>(); // List to keep track of the instantiated text objects

    [SerializeField] private TMP_Dropdown dropdown;

    [SerializeField] private TextMeshProUGUI m_headingText;


    public delegate void PopUp(string clientId);
    public static event PopUp OnPopUpOpen;

    private void Awake()
    {
        dropdown.onValueChanged.AddListener(Dropdown);
        textParent.GetComponent<GridLayoutGroup>().enabled = false;

    }
    public void Dropdown(int index)
    {
        MS_API m = api_helper.ApiRead();

        // Ensure m.clients is not null and index is within bounds
        if (m != null && index >= 0 && index < 3)
        {
            // Destroy previous text objects
            foreach (TMP_Text clientText in clientTexts)
            {
                Destroy(clientText.gameObject);
            }
            clientTexts.Clear();

            switch (index)
            {
                case 0:
                    print("All Clients");
                    ClearClientLabels();
                    textParent.GetComponent<GridLayoutGroup>().enabled = true;

                    // All Clients
                    for (int i = 0; i < m.clients.Count; i++)
                    {
                        TMP_Text newText = Instantiate(textPrefab, textParent).GetComponent<TMP_Text>();
                        newText.text = m.clients[i].label;
                        clientTexts.Add(newText);

                        Button buttonComponent1 = newText.GetComponent<Button>();
                        if (buttonComponent1 == null)
                        {
                            buttonComponent1 = newText.gameObject.AddComponent<Button>();
                        }

                        // Check if onClick listener already exists
                        if (buttonComponent1.onClick.GetPersistentEventCount() == 0)
                        {
                            // If onClick listener doesn't exist, add it
                            int clientIndex = i; // Store the value of i in a local variable
                            buttonComponent1.onClick.AddListener(() => PrintClientLabel(m.clients[clientIndex].label));
                        }

                        m_headingText.text = "All Clients";
                    }
                    break;

                case 1:
                    print("All Managers");
                    ClearClientLabels();
                    textParent.GetComponent<GridLayoutGroup>().enabled = true;

                    // All Managers
                    if (m.clients.Count > 0)
                    {
                        TMP_Text managerText = Instantiate(textPrefab, textParent).GetComponent<TMP_Text>();
                        managerText.text = m.clients[0].label;
                        clientTexts.Add(managerText);

                        Button buttonComponent = managerText.GetComponent<Button>();
                        if (buttonComponent == null)
                        {
                            buttonComponent = managerText.gameObject.AddComponent<Button>();
                        }

                        // Check if onClick listener already exists
                        if (buttonComponent.onClick.GetPersistentEventCount() == 0)
                        {
                            // If onClick listener doesn't exist, add it
                            buttonComponent.onClick.AddListener(() => PrintClientLabel(m.clients[0].label));
                        }
                    }

                    m_headingText.text = "Managers";
                    break;

                case 2:
                    print("All Non-Managers");
                    ClearClientLabels();
                    textParent.GetComponent<GridLayoutGroup>().enabled = true;

                    // All Non-Managers
                    for (int i = 0; i < m.clients.Count; i++)
                    {
                        if (!m.clients[i].isManager)
                        {
                            TMP_Text newText = Instantiate(textPrefab, textParent).GetComponent<TMP_Text>();
                            newText.text = m.clients[i].label;
                            clientTexts.Add(newText);

                            Button buttonComponent2 = newText.GetComponent<Button>();
                            if (buttonComponent2 == null)
                            {
                                buttonComponent2 = newText.gameObject.AddComponent<Button>();
                            }

                            // Check if onClick listener already exists
                            if (buttonComponent2.onClick.GetPersistentEventCount() == 0)
                            {
                                // If onClick listener doesn't exist, add it
                                int clientIndex = i; // Store the value of i in a local variable
                                buttonComponent2.onClick.AddListener(() => PrintClientLabel(m.clients[clientIndex].label));
                            }

                            m_headingText.text = "Non-Managers";
                        }
                    }
                    break;
            }
        }
        else
        {
            Debug.LogError("Invalid index or null data received.");
        }
    }


    private void PrintClientLabel(string labelText)
    {
        // Print the client label text
        print("Client Label: " + labelText);
        OnPopUpOpen.Invoke(labelText);

    }

    private void ClearClientLabels()
    {
        // Destroy any existing client label game objects
        GameObject[] existingLabels = GameObject.FindGameObjectsWithTag("ClientLabel");
        foreach (GameObject label in existingLabels)
        {
            Destroy(label);
        }
    }

}

