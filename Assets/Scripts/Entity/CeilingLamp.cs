using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLamp : Entity, IMoveable, IUseable      
{
    public bool isOn = true;
    public Light ceilingLight;

    public void Move(Vector3 direction)
    {
       
    }

    public void Use()
    {
        isOn = !isOn;
        
        ceilingLight.enabled = isOn;
        AudioManager.Play("LightSwitchSFX");
    }
}
