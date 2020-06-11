using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    Vector3 offSet;
    public GameObject player;
    public float smoothSpeed;

    // Update is called once per frame
    void Start () {
       
        offSet = transform.position - player.transform.position;
       
    }
   
    void LateUpdate () {
        Vector3 desiredPosition = player.transform.position + offSet;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
