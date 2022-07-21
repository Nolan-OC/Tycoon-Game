using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : NPC_Combat
{
    [Header("For Reception")]
    [Range(0,100)]public int receptionSkill;

    [Header("Utility Info")]
    public int breaksRemaining;    //employees need breaks to regen patience before closing time or they'll get wore down!
    [Range(0,100)] public float ballisticMeter; //countown to ballistic mode, once ballistc meter reaches 100 get strong stat buff for a few min

    public List<ScriptableObject> traits;

    [Header("Misc Information")]
    public string empName;
    public string age;
}
