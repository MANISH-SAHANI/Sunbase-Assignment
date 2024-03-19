using System.Collections.Generic;

[System.Serializable]
public class ClientData
{
    public bool isManager;
    public int id;
    public string label;
}

[System.Serializable]
public class MS_API
{
    public List<ClientData> clients;
    public Dictionary<string, Data> data;
    public string label;
}

[System.Serializable]
public class Data
{
    public int id; // Add the id property
    public string address;
    public string name;
    public int points;
}