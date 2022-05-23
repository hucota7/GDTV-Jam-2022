using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class StatusBarUI : MonoBehaviour
{
    public Image background, difference, fill;
    [HideInInspector] public CanvasGroup visuals;
    public float animationSpeed = 10;

    private void Awake()
    {
        visuals = GetComponent<CanvasGroup>();
    }
    public virtual void Update()
    {
        difference.fillAmount = Mathf.MoveTowards(difference.fillAmount, fill.fillAmount, animationSpeed * Time.deltaTime);
    }
    public virtual void Init(float currentValue, float maxValue)
    {
		difference.fillAmount = fill.fillAmount = 1;
    }
    public virtual void UpdateBar(float normalizedValue)
    {
        fill.fillAmount = normalizedValue;
    }
    public virtual void ShowBar()
    {
        visuals.alpha = 1;
    }
    public virtual void HideBar()
    {
        visuals.alpha = 0;
    }
}
