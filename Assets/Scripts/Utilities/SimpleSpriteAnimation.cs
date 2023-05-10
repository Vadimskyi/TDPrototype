using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpriteAnimation : MonoBehaviour
{
	public SpriteRenderer spriteRenderer;
	public Sprite[] sprites;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(Animate());
	}


	private IEnumerator Animate()
	{
		int index = 0;
		while (true)
		{
			spriteRenderer.sprite = sprites[index];
			index = (index + 1) % sprites.Length;
			yield return new WaitForSeconds(0.1f);
		}
	}
}
