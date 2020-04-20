using System.Collections;
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
	private float radius, distance;
	private Vector3 centerPosition, allowedPos, pos;

	void Awake()
	{
        GameManager.shoot += LaunchProjectile;

        GameManager.playerMovement += Movements;

        GameManager.mouseMovement += MouseMovements;

		isAlive = true;
	}

	void Start()
	{
		radius = 9.75f;
		centerPosition = new Vector3(0f, 0.5f, -120);
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
        BasicMovement(false);

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            BasicMovement(true);
        }

        GameManager.InvokePlayerMethod(GameManager.Instance.movement);
	}

    void BasicMovement(bool needNoneCondition)
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                GameManager.Instance.movement = PlayerMovements.LeftForward;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                GameManager.Instance.movement = PlayerMovements.LeftBackward;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (GameManager.Instance.movement == PlayerMovements.Left)
                {
                    GameManager.Instance.movement = PlayerMovements.LeftRight;
                }
                else if (GameManager.Instance.movement == PlayerMovements.Right)
                {
                    GameManager.Instance.movement = PlayerMovements.RightLeft;
                }
            }
            else
            {
                GameManager.Instance.movement = PlayerMovements.Left;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                GameManager.Instance.movement = PlayerMovements.RightForward;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                GameManager.Instance.movement = PlayerMovements.RightBackward;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                GameManager.Instance.movement = PlayerMovements.RightLeft;
            }
            else
            {
                GameManager.Instance.movement = PlayerMovements.Right;
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (GameManager.Instance.movement == PlayerMovements.Forward)
                {
                    GameManager.Instance.movement = PlayerMovements.ForwadBackward;
                }
                else if (GameManager.Instance.movement == PlayerMovements.Backward)
                {
                    GameManager.Instance.movement = PlayerMovements.BackwardForward;
                }
            }
            else
            {
                GameManager.Instance.movement = PlayerMovements.Forward;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            GameManager.Instance.movement = PlayerMovements.Backward;
        }
        else if (needNoneCondition)
        {
            GameManager.Instance.movement = PlayerMovements.None;
        }
    }

    void Movements(PlayerMovements movements)
    {
        pos = transform.position;

        if (movements == PlayerMovements.None)
        {
            return;
        }
        else if (movements == PlayerMovements.LeftForward)
        {
            pos.x -= speed * Time.deltaTime;

            pos.z += speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.RightForward)
        {
            pos.x += speed * Time.deltaTime;

            pos.z += speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.LeftBackward)
        {
            pos.x -= speed * Time.deltaTime;

            pos.z -= speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.RightBackward)
        {
            pos.x += speed * Time.deltaTime;

            pos.z -= speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.Left || movements == PlayerMovements.RightLeft)
        {
            pos.x -= speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.Right || movements == PlayerMovements.LeftRight)
        {
            pos.x += speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.Forward || movements == PlayerMovements.BackwardForward)
        {
            pos.z += speed * Time.deltaTime;
        }
        else if (movements == PlayerMovements.Backward || movements == PlayerMovements.ForwadBackward)
        {
            pos.z -= speed * Time.deltaTime;
        }

        distance = Vector3.Distance(pos, centerPosition);

        if (distance > radius)
        {
            allowedPos = pos - centerPosition;
            allowedPos *= radius / distance;
            pos = centerPosition + allowedPos;
        }

        transform.position = pos;
    }

    void OnMouseClick()
    {
        if (Input.GetMouseButton(1))
        {
            GameManager.Instance.mouseMovements = global::MouseMovements.RightClick;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            GameManager.Instance.mouseMovements = global::MouseMovements.RightClickReleased;
        }
        else if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1) && !Input.GetMouseButtonUp(1))
        {
            GameManager.Instance.mouseMovements = global::MouseMovements.LeftClick;
        }
        else if (!Input.GetMouseButton(1) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonUp(1) && !Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.mouseMovements = global::MouseMovements.None;
        }

        GameManager.InvokeMouseMethod(GameManager.Instance.mouseMovements);
    }

    void MouseMovements(MouseMovements move)
    {
        if(move == global::MouseMovements.None)
        {
            return;
        }
        else if(move == global::MouseMovements.RightClick)
        {
            GameManager.InvokeShootMethod();
        }
        else if(move == global::MouseMovements.RightClickReleased)
        {
            lineVisual.enabled = false;
            Pointer.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(move == global::MouseMovements.LeftClick)
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
        else
        {
            StopShowingLineRenderer();
        }
	}

    void StopShowingLineRenderer()
    {
        lineVisual.enabled = false;

        Pointer.SetActive(false);

        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;
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