using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject keyVisual;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Character character))
        {
            KeyPickup(character);
        }
    }

    public void KeyPickup(Character character)
    {
        transform.SetParent(character.keyHolder);
        transform.position = character.keyHolder.position;
        transform.rotation = character.keyHolder.rotation;
        if (TryGetComponent(out Spin spin)) spin.enabled = false;
        character.PickupKey(this);
    }
}
