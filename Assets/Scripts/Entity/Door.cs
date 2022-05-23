using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Entity, IMoveable, IUseable
{
    [Header("Door Variables")]
    public Transform doorTransform; //The transform that rotates. Best to use an empty gameobject as a pivot point and parent the door model to it.
    private Vector3 startEuler; //The rotation of the door on startup. Used to determine where the new rotation will be when the door is open.
    public Vector3 doorRotation = new Vector3(0f, 90f, 0f); //The new rotation that the door will move to. Default is 90deg in the y-axis.
    public float openSpeed = 4f; //Speed at which the door opens (and closes).
    public bool requiresKey = false; //Does this door require a key?

	bool isOpen = false;

	private void Update()
	{
		if (isOpen)
			OpenDoor();
		else
			CloseDoor();
	}

	public void OpenDoor()
    {
        doorTransform.eulerAngles = Vector3.Lerp(doorTransform.eulerAngles, startEuler + doorRotation, openSpeed * Time.deltaTime);
    }

    public void CloseDoor()
    {
        doorTransform.eulerAngles = Vector3.Lerp(doorTransform.eulerAngles, startEuler, openSpeed * Time.deltaTime);
    }

	public void Move(Vector3 direction) { }

	public void Use()
	{
		isOpen = !isOpen;
	}
}
