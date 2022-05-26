using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtBubble : MonoBehaviour
{
    [Header("Assigned at Runtime")]
    public Transform target;
    [Space]
    [Header("References & Settings")]
    public Animator animator;
    public Image thoughtImage;
    public Vector3 offset = new Vector3(0, -0.5f, 0);

	bool hasInit = false;

    public void Init(Transform target)
    {
        this.target = target;
		hasInit = true;
    }

    public void PlayThought(string stateName, Color color, float transitionDuration = 0.1f)
    {
        thoughtImage.color = color;
        animator.CrossFadeInFixedTime(stateName, transitionDuration);
    }
    public void Exclamation(Color color)
    {
        PlayThought("Exclamation", color);
        AudioManager.Play("WarningAlertSFX");
    }
    public void Question()
    {
        PlayThought("Question", Color.white);
        AudioManager.Play("QuestionSFX");
        
    }
    public void Sleep()
    {
        PlayThought("Resting", Color.white);
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
