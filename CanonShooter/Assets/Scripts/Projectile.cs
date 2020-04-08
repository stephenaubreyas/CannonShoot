﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[HideInInspector] public static bool isAlive;
	public GameObject Sphere, Pointer;
	public Rigidbody bulletPrefabs;
	public Transform shootPoint;
	public LayerMask layer;
	public LineRenderer lineVisual;
	public int lineSegment = 10;
	public ParticleSystem muzzleFlash;
	[SerializeField] readonly float speed = 10.0f;
	float radius, distance;
	Vector3 centerPosition, allowedPos, pos;

	void Awake()
	{
        GameManager.shoot += LaunchProjectile;

        GameManager.playerMovement += Movements;

		isAlive = true;
	}
	void Start()
	{
		radius = 9.75f;
		centerPosition = new Vector3(0f, 0.5f, -40);
		transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
		Sphere.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
		lineVisual.positionCount = lineSegment;
	}
	void Update()
	{
		Movekey();
	}
	void LateUpdate()
	{
		OnMouseClick();
	}
	void Movekey()
    {


		//pos = transform.position;
        if (Input.GetKeyDown(KeyCode.UpArrow) && GameManager.Instance.movement == PlayerMovements.Left)
        {
            GameManager.Instance.movement = PlayerMovements.ForwardLeft;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && GameManager.Instance.movement == PlayerMovements.Forward)
        {
            GameManager.Instance.movement = PlayerMovements.LeftForward;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && GameManager.Instance.movement == PlayerMovements.Right)
        {
            GameManager.Instance.movement = PlayerMovements.ForwardRight;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && GameManager.Instance.movement == PlayerMovements.Forward)
        {
            GameManager.Instance.movement = PlayerMovements.RightForward;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && GameManager.Instance.movement == PlayerMovements.Left)
        {
            GameManager.Instance.movement = PlayerMovements.BackwardLeft;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && GameManager.Instance.movement == PlayerMovements.Backward)
        {
            GameManager.Instance.movement = PlayerMovements.LeftBackward;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && GameManager.Instance.movement == PlayerMovements.Right)
        {
            GameManager.Instance.movement = PlayerMovements.BackwardRight;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && GameManager.Instance.movement == PlayerMovements.Backward)
        {
            GameManager.Instance.movement = PlayerMovements.RightBackward;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameManager.Instance.movement = PlayerMovements.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameManager.Instance.movement = PlayerMovements.Right;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManager.Instance.movement = PlayerMovements.Forward;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameManager.Instance.movement = PlayerMovements.Backward;
        }

        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            GameManager.Instance.movement = PlayerMovements.None;
        }

        //Movements(GameManager.Instance.movement);

        GameManager.InvokePlayerMethod(GameManager.Instance.movement);
	}

    void Movement()
    {
        pos = transform.position;

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            pos.x += speed * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            pos.x -= speed * Time.deltaTime;
        }

        distance = Vector3.Distance(pos, new Vector3(centerPosition.x, 0.5f, pos.z));

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            pos.z += speed * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            pos.z -= speed * Time.deltaTime;
        }

        distance = Vector3.Distance(pos, new Vector3(centerPosition.x, 0.5f, centerPosition.z));

        if (distance < radius)
        {
            transform.position = pos;
        }
        else
        {
            allowedPos = pos - centerPosition;
            allowedPos *= radius / distance;
            pos = centerPosition + allowedPos;
            transform.position = pos;
        }
    }

    void Movements(PlayerMovements movements)
    {
        pos = transform.position;

        if(movements == PlayerMovements.None)
        {
            return;
        }
       else if (movements == PlayerMovements.Left)
        {
            pos.x -= speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.Right)
        {
            pos.x += speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.Forward)
        {
            pos.z += speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.Backward)
        {
            pos.z -= speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.RightForward)
        {
            pos.x += speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.LeftForward)
        {
            pos.x -= speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.RightBackward)
        {
            pos.x += speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.LeftBackward)
        {
            pos.x -= speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.ForwardLeft)
        {
            pos.z += speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.ForwardRight)
        {
            pos.z += speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.BackwardLeft)
        {
            pos.z -= speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.BackwardRight)
        {
            pos.z -= speed * Time.deltaTime;
        }

        transform.position = pos;
    }

 //   void Movement()
	//{
	//		pos = transform.position;

	//		if (Input.GetAxisRaw("Horizontal") > 0)
	//		{
	//			pos.x += speed * Time.deltaTime;
	//		}
	//		else if (Input.GetAxisRaw("Horizontal") < 0)
	//		{
	//			pos.x -= speed * Time.deltaTime;
	//		}

	//		distance = Vector3.Distance(pos, new Vector3(centerPosition.x, 0.5f, pos.z));

	//		if (Input.GetAxisRaw("Vertical") > 0)
	//		{
	//			pos.z += speed * Time.deltaTime;
	//		}
	//		else if (Input.GetAxisRaw("Vertical") < 0)
	//		{
	//			pos.z -= speed * Time.deltaTime;
	//		}

	//		distance = Vector3.Distance(pos, new Vector3(centerPosition.x, 0.5f, centerPosition.z));

	//		if (distance < radius)
	//		{
	//			transform.position = pos;
	//		}
	//		else
	//		{
	//			allowedPos = pos - centerPosition;
	//			allowedPos *= radius / distance;
	//			pos = centerPosition + allowedPos;
	//			transform.position = pos;
	//		}
	//}

  

	void OnMouseClick()
	{
		if(Input.GetMouseButton(1) == false && Input.GetMouseButtonDown(1) == false && Input.GetMouseButtonUp(1) == false && Input.GetMouseButtonDown(0) == false)
		{
			return;
		}
		if (Input.GetMouseButton(1))
		{
            GameManager.InvokeShootMethod();
		}
		else if (Input.GetMouseButtonUp(1))
		{
			lineVisual.enabled = false;
			Pointer.SetActive(false);
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		else if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1) == false && Input.GetMouseButtonUp(1) == false)
		{
			muzzleFlash.Play();
			Rigidbody R = Instantiate(bulletPrefabs, shootPoint.transform.position, shootPoint.transform.rotation);
			Vector3 D = shootPoint.forward;
			R.AddForce(D * 1000);
		}
	}
	#region Projectile
	void LaunchProjectile()
	{
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        lineVisual.enabled = true;
        Pointer.SetActive(true);

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(camRay, out hit, 100f, layer))
		{
			Pointer.transform.position = hit.point + Vector3.up * 0.1f;
			Vector3 Vo = CalculateVelocity(hit.point, shootPoint.position, 1f);

			Visualize(Vo);

			transform.rotation = Quaternion.LookRotation(Vo);

            if (Input.GetMouseButtonDown(0))
            {
                muzzleFlash.Play();
                Rigidbody obj = Instantiate(bulletPrefabs, shootPoint.transform.position, Quaternion.identity);
                obj.velocity = Vo;
            }
        }
	}
	void Visualize(Vector3 vo)
	{
		for (int i = 0; i < lineSegment; i++)
		{
			Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
			lineVisual.SetPosition(i, pos);
		}
	}
	Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
	{
		//define the distance x and y first
		Vector3 distance = target - origin;
		Vector3 distanceXZ = distance;
		distanceXZ.y = 0f;

		//create a float that represent our distance
		float Sy = distance.y;
		float Sxz = distanceXZ.magnitude;

		//Vx = x/t;
		float Vxz = Sxz / time;
		//Vy0 = y/t + 1/2 * g * t
		float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

		Vector3 result = distanceXZ.normalized;
		result *= Vxz;
		result.y = Vy;

		return result;
	}
	Vector3 CalculatePosInTime(Vector3 vo, float time)
	{
		Vector3 Vxz = vo;
		Vxz.y = 0f;

		Vector3 result = shootPoint.position + vo * time;
		float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + shootPoint.position.y;

		result.y = sY;

		return result;
	}
	#endregion
}