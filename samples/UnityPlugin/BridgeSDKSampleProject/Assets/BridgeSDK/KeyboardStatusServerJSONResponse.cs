[System.Serializable]
public class KeyboardStatusServerJSONResponse
{
    public int error_code;
    public KeyboardStatusNestedJSON status;
}

[System.Serializable]
public class KeyboardStatusNestedJSON
{
    public bool visible;
    public string paired_tracker_id;
}