using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

//Written by Ryan Jacob
public class cameraMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target; //targetPlayer
    Vector3 targetPosition;

    public Vector3 offset;

    public Vector2 max;
    public Vector2 min;

    public int smooth;

    private Bounds cameraBounds;
    private Camera theCamera;

    //private Vector3 

    void Awake()
    {
        theCamera = Camera.main;
    }

    void Start()
    {
       

    }

    private void Update()
    {
       
    }

    public void LateUpdate()
    {
        if (target != null)
        {
            if (transform.position != targetPosition)
            {
                Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -8.8f);

                targetPosition.x = Mathf.Clamp(targetPosition.x, min.x, max.x); //setting clamp for bounds
                targetPosition.y = Mathf.Clamp(targetPosition.y, min.y, max.y);

                transform.position = Vector3.Lerp(transform.position, targetPosition, smooth);


            }
        }

    }
}
