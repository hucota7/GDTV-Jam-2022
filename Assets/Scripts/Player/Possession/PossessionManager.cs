using System;
using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PossessionManager : MonoBehaviour
{
	public static PossessionManager Instance { get; private set; }

	public Action<IPossessable> InitialPossession;
	public Action<IPossessable> Possessed;

	[Tooltip("Only used if closestPossesableAsStartingPossessable is false")]
	[SerializeField] private Possessable startingPossable;
	[SerializeField] private bool closestPossesableAsStartingPossessable;
	[Tooltip("Only used if closestPossesableAsStartingPossessable is true")]
	[SerializeField] private LayerMask possessableMask;
	[Space]
	[SerializeField] private bool runPossessOnStartingPossessable;
	[Space]
	[SerializeField] private PossessionBehaviourConcreteImplementation possessionBehaviour;

	Possessable nearPossessable = null;

	public IPossessable CurrentPossessed { get; private set; }

	private void Awake()
	{
		if (Instance)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	private void Start()
	{
		if (closestPossesableAsStartingPossessable)
		{
			int searchRadious = 5;
			Collider[] possessableObjects;

			do
			{
				possessableObjects = Physics.OverlapSphere(transform.position, searchRadious, possessableMask, QueryTriggerInteraction.Collide);
				searchRadious += 5;
			}
			while (possessableObjects.Length == 0 && searchRadious < 100);

			if (possessableObjects.Length == 0)
			{
				Debug.LogWarning("Could not find possessable object within 100 units of PossessionManager! Instead setting it to " + startingPossable);

				CurrentPossessed = startingPossable;
				InitialPossession?.Invoke(CurrentPossessed);

				if (runPossessOnStartingPossessable)
					CurrentPossessed.Possess(null);

				return;
			}

			IPossessable[] possessables = new IPossessable[possessableObjects.Length];

			for (int i = 0; i < possessableObjects.Length; i++)
			{
				possessables[i] = possessableObjects[i].GetComponent<IPossessable>();
			}

			IPossessable closestPossesable = null;
			float closestDistance = searchRadious;

			foreach (var possessable in possessables)
			{
				if (possessable == null) continue;
				if (possessable.GetEntity() is Entity e)
				{
					float distance = Vector3.Distance(transform.position, e.transform.position);

					if (distance < closestDistance)
					{
						closestPossesable = possessable;
						closestDistance = distance;
					}
				}
				else
				{
					Debug.LogWarning($"{possessable} has no entity");
				}
			}

			CurrentPossessed = closestPossesable;
		}
		else
			CurrentPossessed = startingPossable;

		InitialPossession?.Invoke(CurrentPossessed);

		if (runPossessOnStartingPossessable)
			CurrentPossessed.Possess(null);
	}

	private void Update()
	{
		if (Input.GetKeyDown(Keymap.Possess))
			Possess();

		if (CurrentPossessed is GhostPossessable)
		{
			IPossessable near = possessionBehaviour.GetPossessable(new PossessionBehaviourContext() { currentPossessed = CurrentPossessed });

			if (near == null && nearPossessable != null)
			{
				nearPossessable.GetEntity().ClearOutline();
				nearPossessable = null;
			}
			else if (near is Possessable p)
			{
				if (nearPossessable == null)
				{
					nearPossessable = p;
					nearPossessable.GetEntity().SetOutline();
				}
				else if (nearPossessable != p)
				{
					nearPossessable.GetEntity().ClearOutline();
					nearPossessable = p;
					nearPossessable.GetEntity().SetOutline();
				}
			}
		}
	}

	public void Possess()
	{
		PossessionBehaviourContext context = new PossessionBehaviourContext()
		{
			currentPossessed = CurrentPossessed
		};

		IPossessable newPossessable = possessionBehaviour.GetPossessable(context);

		if (newPossessable == null)
			return;

		newPossessable.Possess(CurrentPossessed);
		if (CurrentPossessed != null) CurrentPossessed.Unpossess(newPossessable);
		CurrentPossessed = newPossessable;

		Possessed?.Invoke(CurrentPossessed);
	}
}