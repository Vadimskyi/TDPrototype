/* Copyright (C) 2023 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMove : MonoBehaviour
{
	public float moveSpeed = 1f;
	public Vector2 dragOffset = Vector2.zero;
	private Vector3 dragOrigin; //Where are we moving?
	private Vector3 clickOrigin = Vector3.zero; //Where are we starting?
	private Vector3 basePos = Vector3.zero; //Where should the camera be initially?

	private bool _moving;
	private Vector3 _targetPosition;
	private float _interpolateCameraTime = 0f;

	private Camera _camera;

	private void Start()
	{
		_camera = GetComponent<Camera>();
	}

	private void Update()
	{
		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0))
		#else
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
		#endif
		{
			_moving = true;
			clickOrigin = Input.mousePosition;
			_interpolateCameraTime = 0f;
			_targetPosition = transform.position;
		}

		#if UNITY_EDITOR
		else if(Input.GetMouseButtonUp(0))
		#else
		if (Input.touchCount == 0 || Input.GetTouch(0).phase == TouchPhase.Ended)
		#endif
		{
			_moving = false;
			_targetPosition = transform.position;
		}

		#if UNITY_EDITOR
		if (_moving && Input.GetMouseButton(0))
		#else
		if (_moving && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
		#endif
		{
			dragOrigin = Input.mousePosition;
		#if UNITY_EDITOR
			var dragOffset = ClampOffset(dragOrigin - clickOrigin);
		#else
			var dragOffset = ClampOffset(Input.GetTouch(0).deltaPosition);
		#endif
			_targetPosition = new Vector3(transform.position.x + (-dragOffset.x * .01f), transform.position.y + (-dragOffset.y * .01f), -10);
			clickOrigin = Input.mousePosition;
		}

		if (_moving)
			MoveTowardsTarget();
	}

	private void MoveTowardsTarget()
	{
		_interpolateCameraTime += Time.deltaTime * GetCameraMoveSpeed();
		transform.position = Vector3.Lerp(transform.position, _targetPosition, _interpolateCameraTime);
	}

	private Vector3 ClampOffset(Vector3 offset)
	{
		#if UNITY_EDITOR
		return new Vector3(Mathf.Clamp(offset.x, -(dragOffset.x * _camera.orthographicSize), dragOffset.x * _camera.orthographicSize),
							Mathf.Clamp(offset.y, -(dragOffset.y *_camera.orthographicSize), dragOffset.y * _camera.orthographicSize), 0);
		#else
		return new Vector3(Mathf.Clamp(offset.x, -(dragOffset.x * _camera.orthographicSize), dragOffset.x * _camera.orthographicSize),
							Mathf.Clamp(offset.y, -(dragOffset.y *_camera.orthographicSize), dragOffset.y * _camera.orthographicSize), 0);
		#endif
	}

	private float GetCameraMoveSpeed()
	{
		#if UNITY_EDITOR
		return moveSpeed * _camera.orthographicSize;
		#else
		return moveSpeed * _camera.orthographicSize;
		#endif
	}
}
