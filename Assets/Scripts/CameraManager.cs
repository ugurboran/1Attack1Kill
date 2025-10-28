using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera cam;

    public static CameraManager Instance;

    private void Awake() {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //cam = Camera.main;
    }
    
    public void CameraZoomOut(){
        cam.fieldOfView = 120;
    }

    public void CameraZoomIn(){
        cam.fieldOfView = 60;
    }
}
