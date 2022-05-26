using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Entity, IMoveable, IUseable
{
    public Explosion explosionObject;
    public int timeUntilExplosion = 3;//How long it takes to explode.
	private float explodeTime => timeLit == -1 ? float.PositiveInfinity : timeLit + timeUntilExplosion; //Time in-game that the bomb explodes.

    private Vector3 startScale;
    public Color[] startColors;
	public bool bombLit = false;
    public bool explosionTriggered = false;

    IPossessable genericPossessable;

	float timeLit = -1;

	float litDuration => timeLit == -1 ? 0 : Time.time - timeLit;
	
	public override void Awake()
	{
		base.Awake();
		genericPossessable = GetComponent<IPossessable>();
	}

	public override void Start()
    {
        base.Start();
        startScale = transform.localScale;
    }
    void Update()
    {
		if (bombLit && timeLit == -1)
		{
			timeLit = Time.time;
		}
		else if (!bombLit && timeLit != -1)
		{
			timeLit = -1;
			ClearEffects();
		}

		if (bombLit)
		{
			BombEffects();

			if (Time.time >= explodeTime)
			{
				if (explosionTriggered == false)
				{
					Explode();
				}
			}
		}
    }
    private void BombEffects()
    {
        //When the bomb is nearly gonna explode, do some effects to make it stand out!

        //Creates a new scale using that increases and decreases over time.
        float xSpeed = (Time.time < explodeTime - (timeUntilExplosion / 3f)) ? 12f : 30f;
        float ySpeed = (Time.time < explodeTime - (timeUntilExplosion / 3f)) ? 24f : 50f;

        float xScale = Mathf.Cos(litDuration * xSpeed) * 0.15f;
        float yScale = Mathf.Sin(litDuration * ySpeed) * 0.15f;

        //Recolours it cheekily by comparing the xScale to 0.2f.

        //bombRenderer.material.color = rendColor;

        for (int i = 0; i < renderers.Length; i++)
        {
            Color rendColor = (xScale < 0.05f) ? startColors[i] : Color.white;
            renderers[i].material.color = rendColor;
        }

        //Set the new scale to the bomb, making it look bouncy and fluid when it's about to blow!
        Vector3 newScale = new Vector3(xScale, -yScale, xScale);
        transform.localScale = startScale + newScale;
    }

	private void ClearEffects()
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].material.color = startColors[i];
		}

		transform.localScale = startScale;
	}

    private void Explode()
    {
        Debug.Log("Explosion!");
        explosionTriggered = true;
        Instantiate(explosionObject, transform.position, Quaternion.identity);
        
        if (PossessionManager.Instance.CurrentPossessed == (IPossessable)genericPossessable)
			PossessionManager.Instance.Possess();
        
        base.Die();
    }

	public void Move(Vector3 direction) { }

	public void Use()
	{
		bombLit = !bombLit;
	}
}