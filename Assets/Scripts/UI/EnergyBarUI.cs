using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarUI : StatusBarUI
{
    [Header("Assigned at Runtime")]
    public Transform target;
    private void LateUpdate()
    {
        transform.position = target.transform.position;
        transform.forward = Camera.main.transform.forward;
    }
}
