using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptionist : MonoBehaviour
{
    //pulll stats from npc_combat for reception build
    private Employee employeeStats;
    private int receptionSkill;

    private void Awake()
    {
        employeeStats = GetComponent<Employee>();
        receptionSkill = employeeStats.receptionSkill;
    }
}
