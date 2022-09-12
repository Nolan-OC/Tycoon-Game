using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform bar;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        bar = transform.Find("Bar");
    }
    private void Update()
    {
        //TODO check performance on this
        Vector3 toCam = new Vector3(cam.transform.position.x, 2f, cam.transform.position.z);
        this.transform.LookAt(toCam);
    }
    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}
