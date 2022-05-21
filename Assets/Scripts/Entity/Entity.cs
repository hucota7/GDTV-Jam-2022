using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	public bool possessable = true;
	public Renderer[] renderers;
	public EnergyBarUI energyBar;

    private void Awake()
    {
		energyBar = Instantiate(InterfaceManager.Instance.energyBarPrefab, InterfaceManager.Instance.worldCanvas.transform);
		energyBar.Init(this);
	}

    private void Reset()
	{
		renderers = GetComponentsInChildren<Renderer>();
	}
	public virtual void Die()
	{
		Debug.Log(gameObject.name + " has died!");
		Destroy(gameObject);
	}
	public void DoColorFlash(float duration)
	{
		StartCoroutine(ColorFlash(duration));
	}

	IEnumerator ColorFlash(float duration)
	{
		Color red = new Color(1, 0, 0, 0.5f);
		Color transparent = new Color(1, 1, 1, 0);
		bool on = true;

		float t = Time.time;
		while (Time.time - t < duration)
		{
			foreach (Renderer renderer in renderers)
			{
				foreach (Material m in renderer.materials)
				{
					m.SetColor("_Tint", on ? red : transparent);
				}
			}
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			on = !on;
		}
		foreach (Renderer renderer in renderers)
		{
			foreach (Material m in renderer.materials)
			{
				m.SetColor("_Tint", transparent);
			}
		}
	}
}
