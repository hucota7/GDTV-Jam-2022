using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class StatusBarUI : MonoBehaviour
{
    [HideInInspector] public float currentValue, maxValue;
    public Image background, difference, fill;
    [HideInInspector] public CanvasGroup visuals;
    public float animationSpeed = 10;

    private void Awake()
    {
        visuals = GetComponent<CanvasGroup>();
    }
    public virtual void Update()
    {
        difference.fillAmount = Mathf.MoveTowards(difference.fillAmount, currentValue / maxValue, animationSpeed / maxValue * Time.deltaTime);
    }
    public virtual void Init(float currentValue, float maxValue)
    {
        this.currentValue = currentValue;
        this.maxValue = maxValue;
        difference.fillAmount = fill.fillAmount = currentValue / maxValue;
    }
    public virtual void UpdateBar()
    {
        fill.fillAmount = currentValue / maxValue;
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
