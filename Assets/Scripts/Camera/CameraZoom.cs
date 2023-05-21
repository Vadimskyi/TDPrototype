/* Copyright (C) 2023 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */

using UnityEngine;

[RequireComponent(typeof(Camera), typeof(CameraBounds))]
public class CameraZoom : MonoBehaviour
{
	public float zoomSpeed;

	private Camera _camera;
	private CameraBounds _cameraBounds;

	private void Start()
	{
		_camera = GetComponent<Camera>();
		_cameraBounds = GetComponent<CameraBounds>();
	}

	private void Update()
	{
		#if UNITY_EDITOR
		var scroll = Input.GetAxis("Mouse ScrollWheel");
		if (scroll != 0.0f)
		{
			_camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize + -scroll * zoomSpeed, _cameraBounds.minZoom, _cameraBounds.maxZoom);
		}
		#else
		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			deltaMagnitudeDiff /= 400f;

			// ... change the orthographic size based on the change in distance between the touches.
			_camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize + deltaMagnitudeDiff * zoomSpeed, _cameraBounds.minZoom, _cameraBounds.maxZoom);
		}
		#endif
	}
}
