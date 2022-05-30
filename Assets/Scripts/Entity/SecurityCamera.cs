using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers.Tuple;
using Helpers.Array;

public class SecurityCamera : Entity, IMoveable ,IUseable
{
	[SerializeField] bool isTurnedOn = true;
	[SerializeField] ViewconeTrigger viewConeTrigger;
    public Transform pivot;
	// x is yaw degrees, y is rotation duration, z is seconds to delay before moving on
	[SerializeField, VectorLabels("Yaw", "Time", "Delay")] Vector3[] rotationSequence;

	[SerializeField, ReadOnly] AnimationCurve curve;
	float yaw = 0;

	//disable security camera's animation/rotation when posessed? <---
	//or turn off the camera's ability to sense you some other way?
	//or hop into the security camera's camera-view?
	//or manually control camera's view in some way.

	//cam standby beeping sound
	// AudioManager.Play("CamBeepSFX"); 

	private void FixedUpdate()
	{
		if (isTurnedOn)
		{
			//would be nice to play that turn on sound when you turn it on
			//AudioManager.Play("CamTurnOnSFX");
			if (!viewConeTrigger.detectedPlayer)
			{
				RotateCamera();
				
			}
				if (!viewConeTrigger.gameObject.activeInHierarchy)
				{
					viewConeTrigger.gameObject.SetActive(true);
				}
			
		}
		else 
		{
			//would be nice to play that turn off sound when you turn it off
			//AudioManager.Play("CamTurnOffSFX");
			if (viewConeTrigger.gameObject.activeInHierarchy)
			{
				viewConeTrigger.gameObject.SetActive(false);
			}
		}
	}

	void RotateCamera()
	{
			yaw = curve.Evaluate(Time.time);
			pivot.localRotation = Quaternion.AngleAxis(yaw, Vector3.up);	
	}

#if UNITY_EDITOR
	private void OnValidate()
	{
		//some of this probably needs to be frozen if isTurnedOn = false 
		if (rotationSequence.Length == 0)
		{
			curve = new AnimationCurve(new Keyframe(0, 0));
			return;
		}
		
		if (rotationSequence.Length == 1)
		{
			curve = new AnimationCurve(new Keyframe(0, rotationSequence[0].x));
			return;
		}

		foreach (var el in rotationSequence)
			if (el == null) return;

		for (int i = 0; i < rotationSequence.Length; i++)
		{
			Vector3 el = rotationSequence[i];
			el.y = el.y < 0.01f ? 0.01f : el.y;
			el.z = el.z < 0 ? 0 : el.z;
			rotationSequence[i] = el;
		}

		float t = 0;
		curve = new AnimationCurve() { preWrapMode = WrapMode.Loop, postWrapMode = WrapMode.Loop };

		curve.AddKey(t, rotationSequence.At(-1).x);

		for (int i = 0; i < rotationSequence.Length; i++)
		{
			(float yaw, float time, float delay) curr = rotationSequence[i].Tuple();
			
			t += curr.time;
			curve.AddKey(t, curr.yaw);

			if (curr.delay > 0)
			{
				t += curr.delay;
				curve.AddKey(t, curr.yaw);
			}
		}

		for (int i = 0; i < curve.keys.Length; i++)
		{
			UnityEditor.AnimationUtility.SetKeyLeftTangentMode(curve, i, UnityEditor.AnimationUtility.TangentMode.Linear);
			UnityEditor.AnimationUtility.SetKeyRightTangentMode(curve, i, UnityEditor.AnimationUtility.TangentMode.Linear);
		}
	}

    public void Move(Vector3 direction)
    {
        
    }

    public void Use()
    {
		isTurnedOn = !isTurnedOn;
    }
#endif
}
