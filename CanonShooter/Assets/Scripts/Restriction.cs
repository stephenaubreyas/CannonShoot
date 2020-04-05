using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restriction : MonoBehaviour
{
	[Range(0, 50)] public int segments = 50;
	[SerializeField] [Range(0, 1.5f)] float xradius = 0.93f, yradius = 0.93f;
	[SerializeField] LineRenderer line;
	[SerializeField] GameObject Enemy;
	private void Awake()
	{
		if (line == null)
			line = GetComponent<LineRenderer>();

		line.enabled = true;
	}
	private void Start()
	{
		//line.SetVertexCount(segments + 1);
		line.positionCount = segments + 1;
		line.useWorldSpace = false;
		VisualRestriction();	
	}
	void VisualRestriction()//Drawing a circle around player
	{
		float x;
		//float y = 0;
		float z;

		float angle = 20f;

		for (int i = 0; i < (segments + 1); i++)
		{
			x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
			z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

			line.SetPosition(i, new Vector2(x, z));
			//line.SetPosition(i, new Vector3(x, z, y));
			angle += (360f / segments);
		}
	}
}