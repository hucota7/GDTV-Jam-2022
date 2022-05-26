using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers.Tuple;

public class SecurityCamera : Entity
{
    public Transform pivot;
	[SerializeField] Vector3[] rotationSequence; // x is yaw degrees, y is degrees/sec to change, z is seconds to delay before moving on

	int sequenceIndex = 0;
	float sequenceTime = 0;

    //disable security camera's animation/rotation when posessed?
    //or turn off the camera's ability to sense you some other way?
    //or hop into the security camera's camera-view?
    //or manually control camera's view in some way.

    //cam standby beeping sound
    // AudioManager.Play("CamBeepSFX"); 

	void RotateCamera()
	{
		(float yaw, float rate, float wait) operation = rotationSequence[sequenceIndex].Tuple();


	}
}
