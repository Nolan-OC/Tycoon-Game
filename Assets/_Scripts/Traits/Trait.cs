using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Scriptable Object/Trait")]
//[CreateAssetMenu(menu)]
public class Trait : ScriptableObject
{
    public enum SpecialTrait
    {
        [Tooltip("regen patience on a crit")]
        sadistic,
        [Tooltip("extra breaks")]
        lazy,
        [Tooltip("being damaged by aggro attacks will gain ballistic meter")]
        triggered,
        [Tooltip("higher patience level")]
        zen,
    }

    public SpecialTrait specialTrait;
    public Sprite icon;
}
