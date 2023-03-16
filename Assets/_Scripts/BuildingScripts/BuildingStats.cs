using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStats : MonoBehaviour
{
    public int level;
    public double upgCost;


    public void Upgrade()
    {
        //TODO Upgrade will take argument Money and check if you have enough
        level++;
        //TODO change gfx, play animation, sfx, etc
        upgCost *= 2;
    }
}
