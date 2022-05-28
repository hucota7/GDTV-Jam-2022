using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class KeyPrompt : MonoBehaviour
{
    [Header("Assigned at Runtime")]
    public Transform target;
    [Space]
    [Header("References & Settings")]
    public string interactKey = "E";
    public GameObject visual;
    public Image keyIcon;
    public TMP_Text keyText;
    public Vector3 offset = new Vector3(0, -0.5f, 0);

    bool hasInit = false;

    public void Init(Transform target)
    {
        this.target = target;
        hasInit = true;
        keyText.text = interactKey;
    }

    public void ShowPrompt(bool show, string key = "E")
    {
        keyText.text = key;
        visual.SetActive(show);
    }



    private void LateUpdate()
    {
        if (!hasInit) return;

        if (target)
        {
            transform.position = target.position + offset;
            transform.forward = Camera.main.transform.forward;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
