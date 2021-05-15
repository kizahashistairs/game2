using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moucefollow: MonoBehaviour
{
    Vector3 screenPoint;

    private void Update ()
    {
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 a = new Vector3 (Input.mousePosition.x,Input.mousePosition.y,screenPoint.z);
        transform.position = Camera.main.ScreenToWorldPoint (a);
    }
}