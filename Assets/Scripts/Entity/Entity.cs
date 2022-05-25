using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	public Renderer[] renderers;
	public int[] rendererLayers { get; private set; } = null;
	public EnergyBarUI EnergyBar { get; private set; } = null;
	public Transform uiPoint;

	public virtual void Awake()
	{
		rendererLayers = new int[renderers.Length];
		for (int i = 0; i < rendererLayers.Length; i++)
		{
			rendererLayers[i] = renderers[i].gameObject.layer;
		}
	}

	public virtual void Start()
    {
		if (TryGetComponent(out Energy _))
		{
			EnergyBar = Instantiate(InterfaceManager.Instance.energyBarPrefab, InterfaceManager.Instance.worldCanvas.transform);
			EnergyBar.Init(1, 1);
			EnergyBar.TempPain(this);
		}
	}

    public virtual void Reset()
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

	public void SetOutline()
	{
		foreach (var ren in renderers)
		{
			ren.gameObject.layer = LayerMask.NameToLayer("Outline");
		}
	}
	public void ClearOutline()
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].gameObject.layer = rendererLayers[i];
		}
	}
}
