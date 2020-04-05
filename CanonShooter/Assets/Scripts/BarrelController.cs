using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
	public Transform nozzle;
	public float friction, lerpSpeed;
	Quaternion fromRotation, toRotation;
	public int ADSSpeed;
	public float firePower;
	float yaw, pitch;
	Vector2 centerPos;

	Vector3 mouse_pos;
	Vector3 object_pos;
	float angle;

	public Camera camera;
	public Camera cameraMain;
	public Texture2D crosshair;

	public CursorMode cursorMode = CursorMode.ForceSoftware;

	public Vector2 hotSpot = -Vector2.up;
	
	void OnMouseEnter()
	{
		Cursor.SetCursor(crosshair, hotSpot, cursorMode);
	}

	void OnMouseExit()
	{
		Cursor.SetCursor(null, Vector2.zero, cursorMode);
	}

	private void Start()
	{
		cameraMain = Camera.main;
		camera.gameObject.SetActive(false);
		camera.clearFlags = CameraClearFlags.SolidColor;
		camera.backgroundColor = new Color(Random.value, Random.value, Random.value, Random.value);
		cameraMain.gameObject.SetActive(true);
		//Cursor.SetCursor(null, Vector2.zero, cursorMode);
		//Cursor.lockState = CursorLockMode.Locked;

		//Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//Vector2 center = Nozzel.transform.position;

		//Vector2 direction = mousePosition - center; //direction from Center to Cursor
		//Vector2 normalizedDirection = direction.normalized;

		mouse_pos = Input.mousePosition;
		mouse_pos.z = 5.23f; //The distance between the camera and object
		object_pos = Camera.main.WorldToScreenPoint(transform.position);
		mouse_pos.x = mouse_pos.x - object_pos.x;
		mouse_pos.y = mouse_pos.y - object_pos.y;
		//angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
		//transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		//transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
	}

	void Update()
	{
		if (Input.GetMouseButton(1) && Input.GetMouseButtonUp(1) == false)
		{
			cameraMain.gameObject.SetActive(false);
			camera.gameObject.SetActive(true);
			//OnMouseEnter();
			Cursor.SetCursor(crosshair, hotSpot, cursorMode);
			Cursor.lockState = CursorLockMode.Locked;
			Mousemove();
		}
		if (Input.GetMouseButtonUp(1) == true)
		{
			camera.gameObject.SetActive(false);
			cameraMain.gameObject.SetActive(true);
			//OnMouseExit();
			Cursor.SetCursor(null, Vector2.zero, cursorMode);
			Cursor.lockState = CursorLockMode.None;
		}
	}
	void Mousemove()
	{
		yaw += ADSSpeed * Input.GetAxis("Mouse X");
		pitch -= ADSSpeed * Input.GetAxis("Mouse Y");
		yaw = Mathf.Clamp(yaw, -90, 90);
		pitch = Mathf.Clamp(pitch, 9, 90);
		//transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		transform.eulerAngles = new Vector2(pitch, yaw);
	}
}
