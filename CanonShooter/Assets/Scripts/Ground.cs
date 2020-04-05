using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
	[SerializeField] GameObject BDesP;
	GameObject temp;
	void Start()
	{
		gameObject.SetActive(true);
		gameObject.GetComponent<Renderer>().material.color = new Color(2.25f,.91f,.32f);
	}
	private void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.tag == "Bullet")
		{
			temp = Instantiate(BDesP, hit.gameObject.transform.position, Quaternion.identity, this.transform) as GameObject;
			//hit.gameObject.SetActive(false);
			Destroy(hit.gameObject);
			Destroy(temp, 1.05f);
		}
	}
}
