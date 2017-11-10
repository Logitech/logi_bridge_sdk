[System.Serializable]
public class SupportedKeyboardsServerJSONResponse
{
    public int error_code;
    public SupportedKeyboardJSON[] keyboards;
}

[System.Serializable]
public class SupportedKeyboardJSON
{
    public string name;
    public SkinJSON[] skins;
}

[System.Serializable]
public class SkinJSON
{
    public string name;
}