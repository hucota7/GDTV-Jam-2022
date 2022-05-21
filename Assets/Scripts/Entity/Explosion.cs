using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers.Vector;

public class Explosion : Entity
{
    public Light pointLight;
    public Renderer rend;
	public float damage = 30;
	public float knockback = 5f;

    private Color startColor;
    private float deathTime;
    private Vector3 startScale;
    public float explosionRadius = 2;

    int timeAlive = 1;

    private void Start()
    {
        startScale = transform.localScale;
        deathTime = Time.time + timeAlive;
        startColor = rend.material.color;
    }
    void Update()
    {
        ExplosionEffects();

        if (Time.time > deathTime)
        {
            Die();
        }
    }
    private void OnTriggerEnter(Collider other)
    {

    }

    private void ExplosionEffects()
    {
        float newScale = startScale.x += Time.deltaTime * 1.5f;
        float alpha = startColor.a -= Time.deltaTime * 1.5f;

        if (newScale < explosionRadius)
        {
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
        if (alpha >= 0)
        {
            pointLight.intensity = alpha;
            rend.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        }
        else
        {
            rend.enabled = false;
        }
        if (alpha <= 0.1f)
        {
            GetComponent<Collider>().enabled = false;
        }
    }
	
    //public override void Die()
    //{
    //    Destroy(gameObject);
    //}
}
