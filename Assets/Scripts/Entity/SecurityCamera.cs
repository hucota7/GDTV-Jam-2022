using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers.Tuple;

public class SecurityCamera : Entity
{
    public Transform pivot;
	[SerializeField] Vector3[] rotationSequence; // x is yaw degrees, y is rotation duration, z is seconds to delay before moving on

	float[] durations;
	float totalDuration;

	float yaw = 0;

	//disable security camera's animation/rotation when posessed?
	//or turn off the camera's ability to sense you some other way?
	//or hop into the security camera's camera-view?
	//or manually control camera's view in some way.

	//cam standby beeping sound
	// AudioManager.Play("CamBeepSFX"); 

	public override void Awake()
	{
		base.Awake();

		durations = new float[rotationSequence.Length];

		for (int i = 0; i < durations.Length; i++)
		{
			durations[i] = rotationSequence[i].y + rotationSequence[i].z;
		}

		totalDuration = 0;
		foreach (float d in durations)
			totalDuration += d;
	}

	void RotateCamera()
	{
		//float t = Time.time % totalDuration;
		//int sequenceIndex = ;
		//(float yaw, float rate, float wait) operation = rotationSequence[sequenceIndex].Tuple();

		
		pivot.localRotation = Quaternion.AngleAxis(yaw, Vector3.up);
	}
}
