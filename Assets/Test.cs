using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
   public Texture2D _texture;
	void Start ()
	{
	    _texture = GetComponent<SpriteRenderer>().sprite.texture;
	 //int countRedPixel;
	    for (int i = 0; i < _texture.width;i++)
	            {
	                for (int j = 0; j < _texture.height; j++)
	                {

	                    if ((_texture.GetPixel(i, j) == new Color(0.655f, 0.659f, 0.612f, 1)))
	                    {
                           Debug.Log("Yes");
	                        _texture.SetPixel(i, j, Color.gray);
	                    }

	                }
	            }
        _texture.Apply();
       
	}
	


	// Update is called once per frame
	void Update () {
		
	}
}
