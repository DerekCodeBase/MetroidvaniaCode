using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappleBehavior : MonoBehaviour
{
    private CircleCollider2D _collider;
    public static bool _grappleRange = false;
    public Transform _grappleTransform;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            _grappleRange = true;
            collider.GetComponent<basicMovement>().UpdateGrapple(_grappleTransform);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            _grappleRange = false;
        }
    }

    void Start()
    {
     _collider = GetComponent<CircleCollider2D>();
     _grappleTransform = transform;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
