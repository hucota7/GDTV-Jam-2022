using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ViewconeTrigger : MonoBehaviour
{
    public Renderer rend;
    public Material discoveredMat;
    private Material defaultMat;

    private void Awake()
    {
        defaultMat = rend.material;
    }

    private void OnEnable()
    {
        rend.enabled = true;
        GetComponent<Collider>().enabled = true;
    }

    private void OnDisable()
    {
        rend.enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Entity>(true) is Entity e && e.CompareTag("Player"))
        {
            rend.material = discoveredMat;
            AudioManager.Play("WarningAlertSFX");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Entity>(true) is Entity e && e.CompareTag("Player"))
        {
            rend.material = defaultMat;
        }
    }
}
