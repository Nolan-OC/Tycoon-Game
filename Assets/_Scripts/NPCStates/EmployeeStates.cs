using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeStates : MonoBehaviour
{
    public enum EmployeeState
    {
        battle,
        onBreak,
        idle,
        victory,
        defeat
    }
    public EmployeeState state;
}
