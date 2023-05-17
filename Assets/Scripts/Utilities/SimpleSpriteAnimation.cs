using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpriteAnimation : MonoBehaviour
{
	public float duration;
	public bool loop;
	public bool startAutomatically;
	public SpriteRenderer spriteRenderer;
	public Sprite[] sprites;

	// Start is called before the first frame update
	void Start()
	{
		if(startAutomatically)
			StartCoroutine(Animate());
	}

	public void StartAnimation()
	{
		StartCoroutine(Animate());
	}

	public void StopAnimation()
	{
		StopAllCoroutines();
	}

	private IEnumerator Animate()
	{
		int index = 0;
		float waitTime = duration / sprites.Length;
		while (index < sprites.Length)
		{
			spriteRenderer.sprite = sprites[index++];
			yield return new WaitForSeconds(waitTime);
			if(loop && index >= sprites.Length) index = 0;
		}
	}
}
