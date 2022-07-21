using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : NPC_Combat
{
    public List<ScriptableObject> traits;

    [Header("Misc Information")]
    public string empName;
    public string age;
}
