using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : Entity, IMoveable, IUseable
{
    [Header("Door Variables")]
    public Transform[] pivots; //The pivots that rotate. Best to use an empty gameobject as a pivot point and child the door model to it.
	public Vector3 rotationAxis = Vector3.up; //The axis in local space around which the door rotates
	public float[] openAngle; //The angle at which the door is fully open
    public float openSpeed = 4f; //Speed at which the door opens (and closes).
	private NavMeshObstacle nav;
	bool finishedOpening = false;
    public bool requiresKey = false; //Does this door require a key?
	public GameObject lockVisual;

	[field: SerializeField] public bool isOpen { get; protected set; } = false;

    public override void Start()
    {
		base.Start();
		lockVisual.SetActive(requiresKey);
		nav = GetComponentInChildren<NavMeshObstacle>();
    }

    private void Update()
	{
		if (isOpen)
			OpenDoors();
		else
			CloseDoors();
	}

	public virtual void OpenDoors()
    {
		if(nav && nav.carving != true) nav.carving = true;
		for (int i = 0; i < pivots.Length; i++)
        {
			pivots[i].localRotation = Quaternion.Lerp(
				pivots[i].localRotation,
				Quaternion.AngleAxis(openAngle[i], rotationAxis),
				openSpeed * Time.deltaTime);
		}
    }

    public virtual void CloseDoors()
    {
		if (nav && nav.carving != false) nav.carving = false;
		for (int i = 0; i < pivots.Length; i++)
        {
			pivots[i].localRotation = Quaternion.Lerp(
				pivots[i].localRotation,
				Quaternion.AngleAxis(0, rotationAxis),
				openSpeed * Time.deltaTime);
		}     
    }

	public virtual void Move(Vector3 direction) { }
	public void ShowPrompt()
	{
		//ThoughtBubble.PlayThought("F", Color.white);
		if (!requiresKey)
		{
			prompt.ShowInteractPrompt(true, "E");
		}
        else 
		{
			prompt.ShowInteractPrompt(true, "?"); 
		}
	}
	public void HidePrompt()
	{
		//ThoughtBubble.PlayThought("F", Color.white);
		prompt.ShowInteractPrompt(false, "E");
	}
	public virtual void Use()
	{
		if (!requiresKey)
		{
			isOpen = !isOpen;
            if (isOpen) 
			{ 
				AudioManager.Play("DoorOpeningSFX"); 
			}
			else 
			{
				AudioManager.Play("DoorClosingSFX");
			}
		}
	}
}
