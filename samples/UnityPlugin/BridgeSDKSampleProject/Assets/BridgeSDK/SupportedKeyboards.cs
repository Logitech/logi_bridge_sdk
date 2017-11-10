using System.Collections.Generic;


public class SupportedKeyboards
{
    public List<SupportedKeyboard> keyboards;

    public void LoadFromJSON(SupportedKeyboardsServerJSONResponse serverJSONResponse)
    {
        this.keyboards = new List<SupportedKeyboard>();

        for (int i = 0; i < serverJSONResponse.keyboards.Length; i++)
        {
            SupportedKeyboard aKeyboard = new SupportedKeyboard(serverJSONResponse.keyboards[i].name);

            for (int j = 0; j < serverJSONResponse.keyboards[i].skins.Length; j++)
            {
                SupportedSkin aSkin = new SupportedSkin(serverJSONResponse.keyboards[i].skins[j].name);
                aKeyboard.skins.Add(aSkin);
            }

            this.keyboards.Add(aKeyboard);
        }
    }

    /// <summary>
    /// Function to print the list of supported keyboards and their respective skins
    /// </summary>
    public string Print()
    {
        string resultAsString = "";
        resultAsString += "Supported keyboards: (select this log to expland)\n";

        for (int i = 0; i < keyboards.Count; i++)
        {
            resultAsString += ("\tKeyboard name: " + keyboards[i].name + "\n");
            for (int j = 0; j < keyboards[i].skins.Count; j++)
            {
                resultAsString += ("\t\tSkin name: " + keyboards[i].skins[j].name + "\n");
            }
        }

        return resultAsString;
    }
}

public class SupportedKeyboard
{
    public string name;
    public List<SupportedSkin> skins;

    public SupportedKeyboard(string name)
    {
        this.name = name;
        skins = new List<SupportedSkin>();
    }
}

public class SupportedSkin
{
    public string name;
    
    public SupportedSkin(string name)
    {
        this.name = name;
    }
}