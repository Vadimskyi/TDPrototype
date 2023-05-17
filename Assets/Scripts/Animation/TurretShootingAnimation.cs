using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VadimskyiLab.UiExtension;

[RequireComponent(typeof(SpriteRenderer))]
public class TurretShootingAnimation : MonoBehaviour
{
	public Transform nozzle;
	public Transform nozzleFlash;
	public SpriteRenderer nozzleFlashSpriteRenderer;
	public Sprite[] nozzleFlashSprites;
	public float animationDuration;
	public float nozzleDisplacementOffset;

	private ITweenRemoteControl _nozzleDisplacementTween;

	private void Start()
	{
		TurrentShotEvent.Subscribe(OnTurretShot);
	}

	private void OnTurretShot(TurrentShotEvent.Args args)
	{
		if(args.turret.gameObject != gameObject) return;
		StopAllCoroutines();
		StartCoroutine(AnimateNozzleFlash());
		_nozzleDisplacementTween?.Kill(true);
		_nozzleDisplacementTween = nozzle.TweenMove2D(new Vector2(nozzle.localPosition.x, nozzle.localPosition.y - nozzleDisplacementOffset), animationDuration, TweenerPlayStyle.PingPong);
	}

	private void OnDestroy()
	{
		TurrentShotEvent.Unsubscribe(OnTurretShot);
	}

	private IEnumerator AnimateNozzleFlash()
	{
		int index = 0;
		nozzleFlashSpriteRenderer.enabled = true;
		while (index < nozzleFlashSprites.Length)
		{
			nozzleFlashSpriteRenderer.sprite = nozzleFlashSprites[index++];
			yield return new WaitForSeconds(animationDuration / nozzleFlashSprites.Length);
		}
		nozzleFlashSpriteRenderer.enabled = false;
	}
}
