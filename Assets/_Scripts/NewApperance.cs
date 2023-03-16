using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewApperance : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> skins;
    void Start()
    {
        int randInt = Random.Range(0, skins.Count);
        skins[0].SetActive(false); //TODO remove this for final build, wasteful. Only nice for debug to have a preset skinn
        skins[randInt].SetActive(true);
    }
}
