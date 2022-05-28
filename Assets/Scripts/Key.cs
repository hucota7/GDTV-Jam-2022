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
        keyVisual.transform.SetParent(character.keyHolder);
        keyVisual.transform.position = character.keyHolder.position;
        keyVisual.transform.rotation = Quaternion.identity;
        character.PickupKey(this);
    }
}
