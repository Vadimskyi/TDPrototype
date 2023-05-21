/* Copyright (C) 2023 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBounds : MonoBehaviour
{
	 public SpriteRenderer spriteBounds;
	 // Pad the bounded area to 'paddingRatio'. Such that 1 means the camera can fully reach the bounds and 0.95 provides a 5% padding on all ends
	 public float paddingRatio = 1f;
	 public float maxZoom;
	 public float minZoom;

	 private Camera _camera;

	 private void Start()
	 {
		 _camera = GetComponent<Camera>();

		var bounds = GetSpriteBounds();
		 maxZoom = ((bounds.x / 2.0f) / ((float)Screen.width / Screen.height)) - 0.1f;
		 minZoom = maxZoom / 2f;
	 }

	 private void LateUpdate()
	 {
		var bounds = GetSpriteBounds();

		float verticalExtent = _camera.orthographicSize;
		float horizontalExtent = verticalExtent * Screen.width / Screen.height;
		float spriteWidth = bounds.x / 2.0f;
		float spriteHeight = bounds.y / 2.0f;

		var leftBound = spriteBounds.transform.position.x + paddingRatio * (horizontalExtent - spriteWidth);
		var rightBound = spriteBounds.transform.position.x + paddingRatio * (spriteWidth - horizontalExtent);
		var bottomBound = spriteBounds.transform.position.y + paddingRatio * (verticalExtent - spriteHeight);
		var topBound = spriteBounds.transform.position.y + paddingRatio * (spriteHeight - verticalExtent);

		var pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
		pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);

		_camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, minZoom, maxZoom);

		transform.position = pos;
	 }

	private Vector2 GetSpriteBounds()
	{
		var sprite = spriteBounds.sprite;
		var spriteWidth = sprite.bounds.size.x;
		var spriteHeight = sprite.bounds.size.y;
		return new Vector2(spriteWidth * spriteBounds.transform.localScale.x, spriteHeight * spriteBounds.transform.localScale.y);
	}
}
