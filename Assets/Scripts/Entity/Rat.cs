using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Character
{
	[SerializeField] private AudioClip[] squeeks;
	public void Squeek()
    {
		AudioSource.PlayClipAtPoint(squeeks[Random.Range(0, squeeks.Length)], transform.position, Random.Range(0.3f, 0.95f));
    }
}
