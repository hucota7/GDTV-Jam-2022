using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilhouetteReveal : MonoBehaviour
{
	public RawImage image;
	public float revealDelay = 1;
	public float revealDuration = 2;

	private void OnEnable()
	{
		StartCoroutine(Reveal());
	}

	IEnumerator Reveal()
	{
		yield return new WaitForSeconds(revealDelay);

		Color c = image.color;
		float t = 0;
		while (t < revealDuration)
		{
			c.a = Mathf.Lerp(1, 0, t / revealDuration);
			image.color = c;

			t += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		c.a = 0;
		image.color = c;
	}
}
