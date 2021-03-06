using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour {
	[SerializeField] private Transform colliderRoot;
	[SerializeField] private Transform bodyCentre;

	private Character _character;
    private Animator _animator;      // reference to animator. It must be deactivated in order to make sure the ragdoll works

    // ragdoll rigidbodies
    private List<Rigidbody> _ragdollRigidbodies = new List<Rigidbody>();
    private List<Collider> _ragdollColliders = new List<Collider>();

	private void Awake()
    {
		_character = GetComponent<Character>();
        _animator = GetComponent<Animator>();
        GetRagdollReferences();
		enabled = false;
    }

	private void GetRagdollReferences()
	{
		if (_animator == null) return;
		for (int i = 0; i < 18; i++)
		{
			var bone = _animator.GetBoneTransform((HumanBodyBones)i);

			if (bone == null) continue;

			// try get rigidbody component
			if (bone.TryGetComponent(out Rigidbody rb))
			{
				rb.isKinematic = true; // deactivate physics
				_ragdollRigidbodies.Add(rb);
			}

			// try get collider component
			if (bone.TryGetComponent(out Collider coll))
			{
				coll.enabled = false;
				_ragdollColliders.Add(coll);
			}
		}
	}

    public void ActivateRagdoll()
    {
        if (_animator == null) return;

        _animator.enabled = false;
		_character.enabled = false;

        // activate rigidbodies
        _ragdollRigidbodies.ForEach(r =>
        {
            r.isKinematic = false;
            r.useGravity = true;
        });

        // activate colliders
        _ragdollColliders.ForEach(c => c.enabled = true);

		enabled = true;
    }

	public IEnumerable<Collider> Colliders => _ragdollColliders;

	public void Update() {
		Vector3 movement = bodyCentre.position - transform.position;
		movement.y = 0;

		transform.position += movement;
		colliderRoot.position -= movement;
	}
}
