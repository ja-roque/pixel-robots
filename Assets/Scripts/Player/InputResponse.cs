using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputResponse
{
    public string Type { get; set; }
    public string Side { get; set; }
    public InputResponse(string type = "none", string side = "none")
    {
        Type = type;
        Side = side;
    }
}
