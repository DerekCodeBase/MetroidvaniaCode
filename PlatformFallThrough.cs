using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFallThrough : MonoBehaviour
{
    private Transform _mcTransform;
    private GameObject _mc;
    private new Transform transform;
    private bool noFallThrough = true;
    private BoxCollider2D _collider;


    void Start()
    {
        transform = GetComponent<Transform>();
        _mc = GameObject.FindWithTag("Player");
        _collider = GetComponent<BoxCollider2D>();
    }
    void FixedUpdate()
    {
        _mcTransform = _mc.transform;

        if (transform.position.y > _mcTransform.position.y && noFallThrough)
        {
            GetComponent<PlatformEffector2D>().rotationalOffset = 0f;
        }
        if (transform.position.y < _mcTransform.position.y )
        {
            GetComponent<PlatformEffector2D>().rotationalOffset = 180f;
        }
    }

    public void noPassThrough()
    {
        noFallThrough = true;
    }

    public void PassThrough()
    {
        Debug.Log("Called");
        noFallThrough = false;
        GetComponent<PlatformEffector2D>().rotationalOffset = 180f;
        StartCoroutine(reset());
    }

    private IEnumerator reset()
    {
        if (transform.position.y < _mcTransform.position.y)
        {
            noFallThrough = true;
        }
        else
        {
            yield return new WaitForSeconds(.1f);
        }
    }


    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.GetComponent<enableDisable>().passThrough)
        {
            PassThrough();
        }
        else
        {
            noPassThrough();
        }
    }


}
