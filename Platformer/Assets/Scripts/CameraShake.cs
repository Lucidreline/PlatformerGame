using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField] Camera mainCam;
    float shakeAmmount;

    void Awake() {
        if(mainCam == null) {
            mainCam = Camera.main;
        }
    }

    void Update() {
        
    }
    public void Shake(float ammount, float length) {
        shakeAmmount = ammount;
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    void DoShake() {
        if(shakeAmmount > 0) {
            Vector3 camPos = mainCam.transform.position;

            float offSetX = Random.value * shakeAmmount * 2 - shakeAmmount;
            float offsetY = Random.value * shakeAmmount * 2 - shakeAmmount;
            camPos.x += offSetX;
            camPos.y += offsetY;

            mainCam.transform.position = camPos;
        }
    }

    void StopShake() {
        CancelInvoke("DoShake");
        mainCam.transform.localPosition = Vector3.zero;
    }
}
