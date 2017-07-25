using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOTweenTest : MonoBehaviour {

	void Start ()
	{
	    //transform.DOMove(new Vector3(5f, 5f, 5f), 2f);
	    transform.DOShakePosition(1f, 2f, 1);
	}
	
	void Update ()
    {
		
	}
}
