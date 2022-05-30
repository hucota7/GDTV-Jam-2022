using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ViewconeTrigger : MonoBehaviour
{
    public Renderer rend;
    public Material discoveredMat;
    private Material defaultMat;
    [SerializeField] private Transform cameraBody;
    [SerializeField] private SecurityCamera securityCamera;
    [SerializeField] private LayerMask lineOfSightMask;
    public bool detectedPlayer = false;

    private void Awake()
    {
        defaultMat = rend.material;
    }

    private void Update()
    {
        if(GameManager.HighAlert)
        {
            rend.material = discoveredMat;
        }
        else
        {
            rend.material = defaultMat;
        }
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

    private void OnTriggerStay(Collider other)
    {
        if (!(other.GetComponentInParent<Entity>(true) is Entity e && e.CompareTag("Player")))
            return;

        bool canSeePlayer = true;

        Vector3 otherDirection = other.transform.position - cameraBody.position;

        if (Physics.Raycast(cameraBody.position, otherDirection, out RaycastHit hit, otherDirection.magnitude, lineOfSightMask)) {
            if (!(hit.transform.GetComponentInParent<Entity>(true) is Entity entity && entity.CompareTag("Player")))
                canSeePlayer = false;
        }

        if (detectedPlayer == canSeePlayer)
            return;

        if (canSeePlayer) {
            SeePlayer();
        }
        else {
            UnseePlayer();
        }
    }
    private void OnTriggerExit(Collider other) {
        if (!detectedPlayer)
            return;

        if (other.GetComponentInParent<Entity>(true) is Entity e && e.CompareTag("Player")) {
            UnseePlayer();
        }
    }

    private void SeePlayer() {
        if (!GameManager.HighAlert)
        {
            rend.material = discoveredMat;
            AudioManager.Play("WarningAlertSFX");
        }
        detectedPlayer = true;

        if (!GameManager.HighAlert)
        {
            GameManager.Instance.RaiseAlarm();
        }
    }

    private void UnseePlayer() {
        rend.material = defaultMat;
        detectedPlayer = false;
        securityCamera.StopCameraRotation(1);

        //if (GameManager.AlarmLevel > 0)
        //{
        //    GameManager.Instance.LowerAlarm();
        //}
    }
}
