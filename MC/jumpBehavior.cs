using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpBehavior : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerMask;
    private Rigidbody2D _rbody;
    private RaycastHit2D rayHit;
    private CapsuleCollider2D boxCollider;
    public Animator _anim;

    [SerializeField]
    private float _jumpPower;
    [SerializeField]
    private float _jumpFallGravityMultiplier;
    [SerializeField]
    private float _groundCheckHeight;
    private float _initialGravityScale;
    public bool _canJump = true;
    private Vector2 _jumpTransform;


    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (!groundCheck())
        {
            _canJump = false;
            _anim.SetBool("Jumping", true);
        }
        else if (groundCheck())
        {
            _canJump = true;
            _anim.SetBool("Jumping", false);
        }
    }
    
    public void onJump()
    {
        if (groundCheck())
        {
        _jumpTransform = new Vector2(_rbody.velocity.x / 2, 1);
        _rbody.AddForce(_jumpTransform * _jumpPower, ForceMode2D.Impulse);   
        }
    }




    public bool groundCheck()
    {
        RaycastHit2D rayHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, -Vector2.up, _groundCheckHeight, playerMask);
        
        if (rayHit.collider != null)
        {
        return true;
        }
        else
        {
        return false;
        }
    }

}
