using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LedColorDetect : MonoBehaviour
{



    public RawImage ImageRaw;
    public GameObject FirstLed;
    public GameObject SecondLed;
    public GameObject ThirdLed;
    public GameObject FourLed;
    public static LedColorDetect Instance;

    public List<GameObject> LedList = new List<GameObject>();
    public List<Color> LedColor = new List<Color>();
   

   // public Color colTest;
    

    
    public MeshRenderer VideoTexture;

    public Text FirstLedText;
    public Text SecondLedText;
    public Text ThirdLedText;
    public Text FourLedText;
    public Text HeightWeight;
    public GameObject BackGroundPlane;
    private Texture2D _videoText;

    public bool detectPositionLed = false;
    


    public Camera cam;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        cam = Camera.main;
    }

    
    void Update()
    {
     
        if (detectPositionLed)
        {
            _videoText = (Texture2D)BackGroundPlane.GetComponent<MeshRenderer>().material.mainTexture;
            DetectArrayLed();
          
        }

     
    }

   private void DetectArrayLed()
    {
        int i = 0;

       foreach (var _led in LedList)
       {
        
           if (_videoText != null)
           {
            
               RaycastHit hit;
               Ray ray = cam.ScreenPointToRay(cam.WorldToScreenPoint(_led.transform.position));
               BackGroundPlane.GetComponent<MeshCollider>().sharedMesh = BackGroundPlane.GetComponent<MeshFilter>().mesh;
               if (Physics.Raycast(ray, out hit))
               {
                   Texture2D TextureMap = (Texture2D) hit.transform.GetComponent<MeshRenderer>().material.mainTexture;
                   ImageRaw.texture = TextureMap;
                    HeightWeight.text = TextureMap.width+ " " + TextureMap.height;
                  var pixelUV = hit.textureCoord;
                   pixelUV.x *= TextureMap.width;
                   pixelUV.y *= TextureMap.height;
#if UNITY_ANDROID
             
#endif
                    switch (i)
                   {
                        case 0:
                           FirstLedText.text = (int) pixelUV.x + " " + (int) pixelUV.y;
                            break;
                        case 1:
                          SecondLedText.text = (int)pixelUV.x + " " + (int) pixelUV.y;
                            break;
                        case 2:
                            ThirdLedText.text =  (int)pixelUV.x + " " + (int) pixelUV.y;
                            break;
                        case 3:
                          FourLedText.text =  (int) pixelUV.x+ " " + (int) pixelUV.y;
                          break;
                   }
                   LedColor[i] = TextureMap.GetPixel((int) pixelUV.x, (int) pixelUV.y);
                
                   Debug.DrawLine(ray.origin, ray.direction*5000, LedColor[i]);
                
                   i++;
                
               }
           }
       }
       
      FirstLedText.color = LedColor[0];
        SecondLedText.color = LedColor[1];
        ThirdLedText.color = LedColor[2];
        FourLedText.color = LedColor[3];
    }

}
