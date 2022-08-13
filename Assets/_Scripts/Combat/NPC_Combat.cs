using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Combat : MonoBehaviour
{
    public float maxPatience; //patience is health, whoever runs out first quits, though for employee it's really stubborness
    public float patience;

    [Tooltip("critical damage vs reason because you are so bold")]
    [Range(1, 10)] public int aggro;   //critical damage vs reason because you are so bold
    [Tooltip("critical damage vs kindness lvl because they will comply")]
    [Range(1, 10)] public int reason;
    [Tooltip("critical damage to aggro because who could be mad at you?")]
    [Range(1, 10)] public int kindness;

    [Tooltip("Accuracy out of 100, determines a hit or a blunder")]
    [Range(0,100)]public float accuracy;  //accuracy out of 100, determines blunder or hit

    private HealthBar healthBar;

    private void Start()
    {
        if (transform.Find("GUI/HealthBar").gameObject.TryGetComponent(out HealthBar health))
            healthBar = health;
        else
            Debug.LogError("Could not find health bar for " + name);
    }
    public void TakeDamage(float damage)
    {
        patience -= damage;
        healthBar.SetSize(patience/maxPatience);
    }
}
