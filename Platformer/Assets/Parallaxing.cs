using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    [SerializeField] Transform[] backgrounds;    //our list of back and fore grounds to be parallaxed;
    private float[] parallaxScales;             //proportion of the camera's movement to move the backgrounds by
    [SerializeField] float smoothing = 1f;     //How smooth the parallax is going to be (Has to be above 0 to work)

    private Transform cam;                   //Reference to main camera's transform
    private Vector3 previousCamPos;         //position of camera in the previous frame

    void Awake() {                        //Does everything before the Start() but after all game objects are set up
        //setting up camera refernce
        cam = Camera.main.transform;
    }
    
    void Start()
    {
        //the previous frame had the current frame's camera position
        previousCamPos = cam.position;

        //assigning coresponding parallax scales
        parallaxScales = new float[backgrounds.Length];
        for(int i = 0; i< backgrounds.Length; i++) {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }

    }

    void Update()
    {
        for(int i = 0; i< backgrounds.Length; i++) {
            // the parallax is the oposite of camera movement bc previous frame multiplied by the scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            //set a target x position which = current position + parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // create a target position which = background current position with its target x position
            Vector3 backgroundTargetPOs = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current position and target position using LERP
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPOs, smoothing * Time.deltaTime);
        }
        //set previousCamPos to thecameras position at the end of frame
        previousCamPos = cam.position;
    }
}
