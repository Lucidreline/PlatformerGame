using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] //makes sure that this object has a sprite renderer if it doesnt then it will make one

public class Tiling : MonoBehaviour
{
    // ~ ~ ~ Buddies = another foreground to extend the floor onx axis ~ ~ ~

    [SerializeField] int offsetX         = 2;           //avoids getting weird errors, creates buddy before we need it;
    [SerializeField] bool hasARightBuddy = false,      // checks if we need to instatiate buddy
                          hasALeftBuddy  = false;    

    [SerializeField] bool reverseScale   = false;   //used if object is not tilable

    private float spriteWidth = 0f;               //width of element
    private Camera cam;
    private Transform myTransform;

    void Awake() {
        cam         = Camera.main;
        myTransform = transform;
    }
    
    void Start()
    {
        //check width
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth              = sRenderer.sprite.bounds.size.x;
    }

    void Update(){
        //does it still need buddies
        if(!hasALeftBuddy || !hasARightBuddy) {
            //calc the cameras half width of wha camera could see in world coordinates
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            //calculate the X position where camera can seeedge of sprite
            float edgeVisablePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisablePositionLeft  = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            //check if we can see the edge of the element, if we can then we make a new buddy
            if(cam.transform.position.x >= edgeVisablePositionRight - offsetX  && !hasARightBuddy) {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }else if(cam.transform.position.x <= edgeVisablePositionLeft + offsetX && !hasALeftBuddy) {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
    }

    void MakeNewBuddy(int rightOrLeft) { //makes buddy on required side
        //calculate new pos for new buddy
        Vector3 newPostion = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        //instatiating new buddy and storing him in a variable
        Transform newBuddy = (Transform)Instantiate(myTransform, newPostion, myTransform.rotation);

        //reverses the x scale on every other object so that you cant tell that its just repeating
        if (reverseScale)
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);

        newBuddy.parent = myTransform.parent;
        if(rightOrLeft > 0) 
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        else 
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
    }
}
