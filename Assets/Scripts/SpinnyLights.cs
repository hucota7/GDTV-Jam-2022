using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyLights : MonoBehaviour
{
    [SerializeField] GameObject Pivot;    
    [SerializeField] public bool isActivated = false;
    [SerializeField] public Light Light1;
    [SerializeField] public Light Light2;

	private void Awake() {
        GameManager.Instance.AlarmRaised += () => isActivated = true;
        GameManager.Instance.AlarmLowered += () => isActivated = false;
	}

	void Update()
    {
        LightControls();
    }

    private void LightControls()
    {
        if (GameManager.HighAlert)
        {

            //spin on pivot z axis (use spin script)
            GetComponentInChildren<Spin>().enabled = true;
            
            //turn on spotlights within spinnylights
            Light1.enabled = true;
            Light2.enabled = true;
            //trigger gamemanagers red-alert and sfx
        }
        else
        {
            GetComponentInChildren<Spin>().enabled = false;
            Light1.enabled = false;
            Light2.enabled = false;
            //do not play red-alert... etc etc
        }
    }
}
