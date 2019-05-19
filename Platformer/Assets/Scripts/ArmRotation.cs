using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    [SerializeField] int rotationOffset = 0;

    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();     // makes all sum of vector = to 1;

        float rotationZ         = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //finding the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + rotationOffset);
    }
}
