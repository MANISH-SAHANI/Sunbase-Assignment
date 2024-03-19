using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public static class api_helper
{
    public static MS_API ApiRead()
    {
        string apiUrl = "https://qa.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";

        // Initialize the MS_API object
        MS_API msApi = new MS_API();

        try
        {
            UnityWebRequest request = UnityWebRequest.Get(apiUrl);

            // Send the request
            request.SendWebRequest();

            // Wait until the request is done
            while (!request.isDone)
            {

                //Wait
            }

            // Check for errors
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching data from API: " + request.error);
            }
            else
            {
                // Deserialize the JSON response into MS_API object
                string json = request.downloadHandler.text;
                msApi = ParseJson(json);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error fetching data from API: " + ex.Message);
        }

        return msApi;
    }

    private static MS_API ParseJson(string json)
    {
        MS_API msApi = new MS_API();
        try
        {
            JsonData jsonData = JsonUtility.FromJson<JsonData>(json);

            // Check if jsonData is null
            if (jsonData == null)
            {
                Debug.LogError("Error parsing JSON data: JsonData object is null");
                return null;
            }

            // Set clients
            msApi.clients = jsonData.clients;

            // Set label
            msApi.label = jsonData.label;

            // Initialize data dictionary
            msApi.data = new Dictionary<string, Data>();

            //// Check if jsonData.data is null
            //if (jsonData.data == null)
            //{
            //    Debug.LogWarning("No additional data found in JSON.");
            //    return msApi;
            //}

            // Populate data dictionary
            foreach (var dataEntry in jsonData.data)
            {
                msApi.data[dataEntry.Key] = dataEntry.Value;
            }
        }
        catch (System.Exception ex)
        {
            //Debug.LogError("Error parsing JSON data: " + ex.Message);
        }

        return msApi;
    }


    [System.Serializable]
    private class JsonData
    {
        public List<ClientData> clients;
        public Dictionary<string, Data> data;
        public string label;
    }
}
