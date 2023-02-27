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
        skins[randInt].SetActive(true);
    }
}
