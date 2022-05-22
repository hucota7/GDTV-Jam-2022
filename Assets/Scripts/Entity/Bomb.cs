using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Entity
{
    public Explosion explosionObject;
    public int timeUntilExplosion = 3;//How long it takes to explode.
    private float explodeTime; //Time in-game that the bomb explodes.

    private Vector3 startScale;
    public Color[] startColors;
    public bool explosionTriggered = false;
    [Space]
    [SerializeField] public PossessionManager possessionManager;
    [SerializeField] public GenericPossessable genericPossessable;

    public override void Start()
    {
        base.Start();
        startScale = transform.localScale;
        explodeTime = Time.time + timeUntilExplosion;
    }
    void Update()
    {
        BombEffects();

        if (Time.time > explodeTime)
        {
            if (explosionTriggered == false)
            {
                Explode();
            }
        }
    }
    private void BombEffects()
    {
        //When the bomb is nearly gonna explode, do some effects to make it stand out!

        //Creates a new scale using that increases and decreases over time.
        float xSpeed = (Time.time < explodeTime - (timeUntilExplosion / 3f)) ? 12f : 30f;
        float ySpeed = (Time.time < explodeTime - (timeUntilExplosion / 3f)) ? 24f : 50f;

        float xScale = Mathf.Cos(Time.time * xSpeed) * 0.15f;
        float yScale = Mathf.Sin(Time.time * ySpeed) * 0.15f;

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
    private void Explode()
    {
        Debug.Log("Explosion!");
        explosionTriggered = true;
        Instantiate(explosionObject, transform.position, Quaternion.identity);
        
        if (possessionManager.CurrentPossessed == (IPossessable)genericPossessable)
            possessionManager.Possess();
        
        base.Die();
    }
}