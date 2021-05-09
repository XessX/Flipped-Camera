using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObjectHori : MonoBehaviour
{
	public GameObject objectRotateHori;

	//public float rotateSpeed = 50f;
	bool rotateStatusHori = false;

	//rotate object function
	public void RotateObjectHori()
	{

		if (rotateStatusHori == false)
		{
			rotateStatusHori = true;
		}
		else
		{
			rotateStatusHori = false;
		}
	}

	void Update()
	{
		if (rotateStatusHori == true)
		{
			// GameObject.Find("Preview").GetComponent<RotateObjectVert>().enabled = false;

			//rotate object with speed
			objectRotateHori.transform.rotation = Quaternion.Euler(0,0,0);
		}
		else if(rotateStatusHori==false)
		{
			//rotate object with speed
			objectRotateHori.transform.rotation = Quaternion.Euler(0,180,0);
		}
	}
}
