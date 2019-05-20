using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour
{
    [SerializeField] int moveSpeed = 230;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        //translate is good for when you only want something to go in one dirrection 
        //and doesnt need a ridged body

        Destroy(gameObject, 1);
    }
}
