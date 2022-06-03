using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class basicMovement : MonoBehaviour
{
    //Variables needed for walking/running

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _runSpeed;
    [SerializeField]
    private float _dodgeLength;
    private float _moveDirection = 1;
    private Rigidbody2D _rbody;
    private Vector2 _moveInput;
    public new Transform transform;

    //Animations dealing with movement
    public Animator _anim;
    public bool _facingRight = true;
    private SpriteRenderer _spriteRender;
    private Camera _mainCam;
    private bool _waitForAnim;

    [SerializeField]
    private LayerMask _mcLayer;
    [SerializeField]
    private LayerMask _enemyLayer;
    public bool isDodging = false;
    public bool _canDodge = true;

    private bool _canJump;

    public float _grappleSpeed;
    private Transform _grapplePosition;

    private float _initialGravityScale;
    public float _maxSpeed;
    private float _defaultSpeed;
    private float _currentSpeed;
    public float _decelerationRate;
    public float _slowDownSpeed;

    private float _turnSpeed;
    private float _slipSpeed;
    private float _accelerationRate;

    void Start()
    {
    _rbody = GetComponent<Rigidbody2D>();
    _spriteRender = GetComponent<SpriteRenderer>();
    _initialGravityScale = _rbody.gravityScale;     
    _defaultSpeed = _speed;    
    }

    void FixedUpdate()
    {
        if (!_canJump)
        {
            if(_rbody.velocity.x > -5 && _moveInput.x < 0)
            {
                _rbody.AddForce(new Vector2(_moveInput.x * 12, 0), ForceMode2D.Force);
                Debug.Log("Added Force");
            }
            if(_rbody.velocity.x < 5 && _moveInput.x > 0)
            {
                _rbody.AddForce(new Vector2(_moveInput.x * 12, 0), ForceMode2D.Force);
                Debug.Log("added force");
            }
        }

        //Flip Sprite based on direction
        float h = _moveInput.x;
        if (h > 0 && !_facingRight){
            flip();
            _moveDirection = 1;
        }
        else if (h < 0 && _facingRight){
            flip();
            _moveDirection = -1;
        }

        _canJump = GetComponent<jumpBehavior>()._canJump;

        if(!_canJump && _rbody.velocity.y < 0)
        {
            _rbody.gravityScale = _initialGravityScale * 1.5f;
        }

        if (_canJump && _rbody.velocity.y > 0)
        {
            _rbody.gravityScale = _initialGravityScale;
        }

        //Walk
        _moveInput = GetComponent<enableDisable>()._moveInput;

            if (!isDodging && _canJump)
            {

               
                if (Mathf.Sign(_moveInput.x)  != Mathf.Sign(_rbody.velocity.x))
                {
                    //_anim.SetBool("Turn", true);
                    _rbody.velocity = new Vector2(_turnSpeed, _rbody.velocity.y);
                }
                if (Mathf.Abs(_rbody.velocity.x) >= 0 && Mathf.Abs(_rbody.velocity.x) < _speed )
                {
                    _rbody.velocity = new Vector2(_moveInput.x * _accelerationRate, _rbody.velocity.y);
                    Debug.Log("should be movin");
                }
                if (Mathf.Abs(_rbody.velocity.x) == _speed )
                {
                    _rbody.velocity = new Vector2(_moveInput.x * _speed, _rbody.velocity.y);
                }
                if (_moveInput.x == 0 && Mathf.Abs(_rbody.velocity.x) > 0)
                {
                    _rbody.velocity = new Vector2(_slipSpeed, _rbody.velocity.y);
                }

            }

        

        //Run Animation
        _anim.SetFloat("Speed", Mathf.Abs(_moveInput.x));
    }

    private void Update()
    {
         _slipSpeed = 1 - Time.deltaTime * _slowDownSpeed;                
         _accelerationRate = Time.deltaTime * _speed;                
         _turnSpeed = _rbody.velocity.x - (Mathf.Sign(_moveInput.x) * (Time.deltaTime * _decelerationRate));

    }


    public void onDodge()
    {
        if(GetComponent<jumpBehavior>().groundCheck())
        {
            _anim.SetTrigger("Dodge");
            moveThrough();
        }
    }

    void moveThrough()
    {
        GetComponent<enableDisable>().GeneralControlsDisable();
        _rbody.velocity = new Vector2(_moveDirection * _dodgeLength, _rbody.velocity.y);
        isDodging = true;
        StartCoroutine(DodgeCooldown());
        StartCoroutine(IFrames());
    }


    //Flip Sprite
    public void flip()
    {
       _spriteRender.flipX = !_spriteRender.flipX;
       _facingRight = !_facingRight;                                                       
    }
    private IEnumerator IFrames()
    {
        Physics2D.IgnoreLayerCollision(8, 7, true);
        yield return null;
    }

    private IEnumerator DodgeCooldown()
    {
        _canDodge = false;
        yield return new WaitForSeconds(1.5f);
        _canDodge = true;
 
    }
    void EndDodge()
    {
        Physics2D.IgnoreLayerCollision(8, 7, false);
        isDodging = false;
        GetComponent<enableDisable>().GeneralControlsEnable();
    }

    public Transform UpdateGrapple(Transform newTransform)
    {
        _grapplePosition = newTransform;
        return _grapplePosition;
    }

    public void Grapple()
    {
        Vector2 _grappleDirectionRaw = new Vector2(_grapplePosition.position.x - transform.position.x, _grapplePosition.position.y - transform.position.y);
        Vector2 _grappleDirection = _grappleDirectionRaw.normalized;
        _rbody.velocity = new Vector2(0f, 0f);
        _rbody.velocity = new Vector2(_moveInput.x * _speed + _grappleSpeed * _grappleDirection.x, _rbody.velocity.y + _grappleSpeed * _grappleDirection.y);
    }
}
