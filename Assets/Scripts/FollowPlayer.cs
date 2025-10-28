using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
    {
    // KAMERA TAKİP FONKSİYONU
    public Transform playerTransform;

    public Transform davidTransform;
    public Transform mariaTransform;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;

    void Start(){    
    }
    private void Update()
    {
        Follow();
        if(UIManager.Instance.choosenOne){
            playerTransform = davidTransform;
        }
        else{
            playerTransform = mariaTransform;
        }    
    }
    private void Follow()
    {
        Vector3 desiredPosition = playerTransform.position + playerTransform.rotation * locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        Quaternion desiredrotation = playerTransform.rotation * Quaternion.Euler(rotationOffset);
        Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
        //transform.rotation = smoothedrotation;
    }
    }
