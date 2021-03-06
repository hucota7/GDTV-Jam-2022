using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarUI : StatusBarUI
{
    [Header("Assigned at Runtime")]
    public Entity target;
    public Vector3 offset = new Vector3(0, 2, 0);

    public override void Init(float currentValue, float maxValue)
    {
        base.Init(1, 1);
    }

    public void TempPain(Entity tempity) {
        target = tempity;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.transform.position + offset;
            transform.forward = Camera.main.transform.forward;
        }
        else
        {
            //Debug.LogWarning("No target, deleting energy bar!");
            Destroy(gameObject);
        }
    }
}
