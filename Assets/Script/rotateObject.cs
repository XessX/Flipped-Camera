﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObject : MonoBehaviour
{
	public GameObject objectRotate;

	//public float rotateSpeed = 50f;
	bool rotateStatus = false;

	//rotate object function
	public void RotateObject()
	{

		if (rotateStatus == false)
		{
			rotateStatus = true;
		}
		else
		{
			rotateStatus = false;
		}
	}

	void Update()
	{
		if (rotateStatus == true)
		{
			//rotate object with speed
			objectRotate.transform.rotation = Quaternion.Euler(180,0,0);
		}
		else if(rotateStatus==true)
		{
			//rotate object with speed
			objectRotate.transform.rotation = Quaternion.Euler(0,0,0);
		}
	}
}
