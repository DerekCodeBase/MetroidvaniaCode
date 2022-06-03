using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackBehavior : MonoBehaviour
{

   /* private PlayerControls GameActions;
    public Animator _anim;


    [SerializeField]
    private float _attackDistance;
    [SerializeField]
    private LayerMask _enemyLayer;
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private Transform _attackPointLeft;

    private bool _lightAttacking;
    private bool _inLightCombo;
    private bool _heavyAttacking;
    private bool _inHeavyCombo;

    
    private GameObject _enemyHealth;



    public void OnEnable()
    {
        GameActions.GeneralControls.Enable();
    }

    public void OnDisable()
    {
        GameActions.GeneralControls.Disable();
    }

    void Awake()
    {
        GameActions = new PlayerControls();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    GameActions.GeneralControls.LightAttack.performed += _ => LightAttack();
    GameActions.GeneralControls.HeavyAttack.performed += _ => HeavyAttack();
    }


    //All light attack functions
    private void LightAttack()
    {
        _lightAttacking = true;
        Light1();
    }

    void Light1()
    {
        if(!_inLightCombo)
        {
            _anim.SetTrigger("Light Attack 1");
            _inLightCombo = true;
        }
    }

    void Light2()
    {
        if(_lightAttacking)
        {
            _anim.SetTrigger("Light Attack 2");
        }
        else
        {
            StartCoroutine(noSpam());
            disableCombo();
        }
    }

    void Light3()
    {
        if (_lightAttacking)
        {
            _anim.SetTrigger("Light Attack 3");
            disableCombo();
            StartCoroutine(pauseAttack());
        }
        else
        {
            disableCombo();
            StartCoroutine(pauseAttack());
        }
    }

    //All Heavy attack functions
    private void HeavyAttack()
    {
        _heavyAttacking = true;
        Heavy1();
    }

    void Heavy1()
    {
        if(!_inHeavyCombo)
        {
            _anim.SetTrigger("Heavy Attack 1");
            _inHeavyCombo = true;
        }
    }

    void Heavy2()
    {
        if (_heavyAttacking)
        {
            _anim.SetTrigger("Heavy Attack 2");
        }
        else
        {
            disableCombo();
            StartCoroutine(pauseAttack());

        }
    }

    void Heavy3()
    {
        if (_heavyAttacking)
        {
            _anim.SetTrigger("Heavy Attack 3");
            disableCombo();
            StartCoroutine(pauseAttack());
        }
        else
        {
            disableCombo();
            StartCoroutine(pauseAttack());
        }
    }


    void disableCombo()
    {
        _inLightCombo = false;
        _inHeavyCombo = false;
    }

    void disableAttack()
    {
        _lightAttacking = false;
        _heavyAttacking = false;
    }

    private IEnumerator noSpam()
    {
        OnDisable();
        yield return new WaitForSeconds(.2f);
        OnEnable();
    }

    private IEnumerator pauseAttack()
    {
        OnDisable();
        yield return new WaitForSeconds(1f);
        OnEnable();
        
    }

    void HeavyCollide()
    {
        if(GetComponent<basicMovement>()._facingRight)
        {
            Collider2D[] _hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackDistance, _enemyLayer);
            foreach(Collider2D enemy in _hitEnemies)
            {
                StartCoroutine(enemy.GetComponent<EnemyHealth>().HeavyDamage());
                Debug.Log("HitMe");
            }
        }
        else
        {
            Collider2D[] _hitEnemies = Physics2D.OverlapCircleAll(_attackPointLeft.position, _attackDistance, _enemyLayer);
            foreach(Collider2D enemy in _hitEnemies)
            {
                StartCoroutine(enemy.GetComponent<EnemyHealth>().HeavyDamage());
                Debug.Log("HitMe");
            }   
        }
    }

    void LightCollide()
    {
        if(GetComponent<basicMovement>()._facingRight)
        {
            Collider2D[] _hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackDistance, _enemyLayer);
            foreach(Collider2D enemy in _hitEnemies)
            {
                StartCoroutine(enemy.GetComponent<EnemyHealth>().LightDamage());
                Debug.Log("HitMe");
            }
        }
        else
        {
            Collider2D[] _hitEnemies = Physics2D.OverlapCircleAll(_attackPointLeft.position, _attackDistance, _enemyLayer);
            foreach(Collider2D enemy in _hitEnemies)
            {
                StartCoroutine(enemy.GetComponent<EnemyHealth>().LightDamage());
                Debug.Log("HitMe");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackDistance);
        Gizmos.DrawWireSphere(_attackPointLeft.position, _attackDistance);
    }*/
}
