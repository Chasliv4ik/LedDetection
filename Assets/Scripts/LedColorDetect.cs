using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using Image = Vuforia.Image;

public class LedColorDetect : MonoBehaviour
{



    public RawImage ImageRaw;
    public GameObject FirstLed;
    public GameObject SecondLed;
    public GameObject ThirdLed;
    public GameObject FourLed;
    public static LedColorDetect Instance;

    public List<GameObject> LedList = new List<GameObject>();
    public List<Color32> LedColor = new List<Color32>();
   

   // public Color colTest;
    

    
    public MeshRenderer VideoTexture;

    public Text FirstLedText;
    public Text SecondLedText;
    public Text ThirdLedText;
    public Text FourLedText;
    public Text HeightWeight;
    public GameObject BackGroundPlane;
    private Texture2D _videoText;
    public Texture2D tex;
    public bool detectPositionLed = false;



    public Camera cam;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
        FirstLedText.text = "Start";
        cam = Camera.main;
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
    }

    private void OnVuforiaStarted()
    {
    
    }


    void Update()
    {
        

        SecondLedText.text = "Update";
        if (detectPositionLed)
        {
            
            ThirdLedText.text = "Detect";
            _videoText = (Texture2D)BackGroundPlane.GetComponent<MeshRenderer>().material.mainTexture;
            DetectArrayLed();
          
        }

     
    }

    private void DetectArrayLed()
    {


        if (ImageRaw.texture != null)
        {

            
            FirstLedText.text = ImageRaw.texture.width + " " + ImageRaw.texture.height + " " +
                                ImageRaw.texture.GetType().Name;
            Texture2D TextureMap = new Texture2D(ImageRaw.texture.width, ImageRaw.texture.height);
            RenderTexture.active = (RenderTexture)ImageRaw.texture;
            TextureMap.ReadPixels(new Rect(0, 0, ImageRaw.texture.width, ImageRaw.texture.height), 0, 0);
            TextureMap.Apply();
            int i = 0;
            foreach (var led in LedList)
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(cam.WorldToScreenPoint(led.transform.position));
                if (Physics.Raycast(ray, out hit))
                {
                    var pixelUV = hit.textureCoord;
                    pixelUV.x *= TextureMap.width;
                    pixelUV.y *= TextureMap.height;

                    //  LedColor[i] = TextureMap.GetPixels32()[10];
                    HeightWeight.text = TextureMap.format.ToString();
                    switch (i)
                    {
                        case 0:
                            FirstLedText.text = (int)pixelUV.x + " " + (int)pixelUV.y;
                            break;
                        case 1:
                            SecondLedText.text = (int)pixelUV.x + " " + (int)pixelUV.y;
                            break;
                        case 2:
                            ThirdLedText.text = (int)pixelUV.x + " " + (int)pixelUV.y;
                            break;
                        case 3:
                            FourLedText.text = (int)pixelUV.x + " " + (int)pixelUV.y;
                            break;
                    }
                    LedColor[i] = TextureMap.GetPixel(TextureMap.width-(int)pixelUV.x, TextureMap.height-(int)pixelUV.y);

                    Debug.DrawLine(ray.origin, ray.direction * 5000, LedColor[i]);

                    i++;

                }
        FirstLedText.color = LedColor[0];
        SecondLedText.color = LedColor[1];
        ThirdLedText.color = LedColor[2];
        FourLedText.color = LedColor[3];
    
            }
            Destroy(TextureMap);
        }
    }


    IEnumerator GetTexture(int width,int height)
    {
         tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame();
        tex.ReadPixels(new Rect(0, 0,width, height), 0, 0);
        tex.Apply();

    }
}
