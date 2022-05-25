using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSpecificThingOverTime : MonoBehaviour
{
    [SerializeField] GameObject rotatingGameObject;
    [SerializeField] float rotationSpeedX;
    [SerializeField] float rotationSpeedY;
    [SerializeField] float rotationSpeedZ;
    void Update()
    {
        float newRotationSpeedX = rotationSpeedX * Time.deltaTime;
        float newRotationSpeedY = rotationSpeedY * Time.deltaTime;
        float newRotationSpeedZ = rotationSpeedZ * Time.deltaTime;
        if (rotationSpeedX == 0) 
        { 
            newRotationSpeedX = 0; 
        };
        if (rotationSpeedY == 0)
        {
            newRotationSpeedY = 0;
        };
        if (rotationSpeedZ == 0)
        {
            newRotationSpeedZ = 0;
        };

        rotatingGameObject.transform.Rotate(newRotationSpeedX,newRotationSpeedY,newRotationSpeedZ);
    }
}
