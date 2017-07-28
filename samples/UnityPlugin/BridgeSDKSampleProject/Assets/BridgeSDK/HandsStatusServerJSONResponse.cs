using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class HandsStatusServerJSONResponse
{
    public int error_code;
    public HandsStatusNestedJSON status;
}

[System.Serializable]
public class HandsStatusNestedJSON
{
    public bool visible;
    public int hands_representation;
    public int[] color;
}