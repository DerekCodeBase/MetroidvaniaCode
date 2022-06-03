using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MCHealth : MonoBehaviour
{
    private float _healthPoints;
    private float _startingHealth;
    public Animator _anim;
    
    [SerializeField]
    private LayerMask _mcLayer;
    [SerializeField]
    private LayerMask _enemyLayer;
    public Player player;

    void Awake()
    {
        _healthPoints = 100;   
        _startingHealth = _healthPoints;
    }
    
    void Start()
    {

    }


    void Update()
    {
     if (_healthPoints <= 0)
     {
        OnDeath();
     }   
    }

    public void TakeDamageMC(float damage)
    {
        _healthPoints = _healthPoints - damage;
        player.StateMachine.ChangeState(player.HurtState);
        StartCoroutine(IFrames());
    }

    void OnDeath()
    {
        player.StateMachine.ChangeState(player.DeathState);
    }

    private IEnumerator IFrames()
    {
        yield return true;
    }
}
