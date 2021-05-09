using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObjectVert : MonoBehaviour
{
	public GameObject objectRotateVert;

	//public float rotateSpeed = 50f;
	bool rotateStatusVert = false;

	//rotate object function
	public void RotateObjectVert()
	{

		if (rotateStatusVert == false)
		{
			rotateStatusVert = true;
		}
		else
		{
			rotateStatusVert = false;
		}
	}

	void Update()
	{
		if (rotateStatusVert == false)
		{
			//rotate object with speed
			objectRotateVert.transform.rotation = Quaternion.Euler(0,0,0);
		}
		
		else if(rotateStatusVert==true)
		{
			//rotate object with speed
			objectRotateVert.transform.rotation = Quaternion.Euler(180,0,0);
		}
		
	}
}
