using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHoriEnDis : MonoBehaviour
{
    public GameObject objectEnHori;

	//public float rotateSpeed = 50f;
	bool rotateStatusEnDis = false;
    // Start is called before the first frame update
    	public void RotateHoriEn()
	{

		if (rotateStatusEnDis == false)
		{
			rotateStatusEnDis = true;
		}
		else
		{
			rotateStatusEnDis = false;
		}
	}

    // Update is called once per frame
    void Update()
    {  if(rotateStatusEnDis== true){
        GameObject.Find("Preview").GetComponent<rotateObjectVert>().enabled = false;
    }
    else 
          GameObject.Find("Preview").GetComponent<rotateObjectVert>().enabled = true;


    }
}
