using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Plugins;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] public Joystick _joystick;

    private Rigidbody2D _rigidbody;

    public Rigidbody2D GetPlayerRb()
    {
        return _rigidbody;}

    private int numOfSlimeTouching = 0;


    private float xInput;
    private float yInput;

    [SerializeField] private Collider2D _collider;

    [SerializeField] private BackgroundScroll quad;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Animator _animator;


    private float _ghostTimeRemaining = 0;

    float _GhostTimeRemaining
    {
        get { return _ghostTimeRemaining; }
        set
        {
            if (_ghostTimeRemaining <= 0 && value > 0)      //유체화 시작
            {
                SetPlayerCollider(false);
            }
            else if (_ghostTimeRemaining > 0 && value <= 0) //유체화 끝
            {
                SetPlayerCollider(true);
            }
            _ghostTimeRemaining = value;
        }
    }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (!instance)
            instance = this;
    }

    private void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    public void AddGhostTime(float t)
    {
        if (_GhostTimeRemaining < t) _GhostTimeRemaining = t;
    }
    private void FixedUpdate()
    {
        Vector2 velo;
        if (xInput != 0 || yInput != 0)
        {
            velo = new Vector2(xInput, yInput).normalized * (1.8f * ((100 + PlayerStatManager.instance.Speed) / 100));
                
            _rigidbody.velocity = velo;
            if (xInput < 0) _sr.flipX = true;
            else if (xInput != 0) _sr.flipX = false;
        }
        else
        {
            velo = (Vector3) _joystick.Direction.normalized * (2.2f * ((100 + PlayerStatManager.instance.Speed) / 100));
            _rigidbody.velocity = velo;

            if (_joystick.Direction.x < 0) _sr.flipX = true;
            else if (_joystick.Direction.x != 0) _sr.flipX = false;
        }

        if (xInput != 0 || yInput != 0 || _joystick.Direction != Vector2.zero)
        {
            
            _animator.SetBool("IsMoving",true);
        }
        else
        {
            _animator.SetBool("IsMoving",false);
        }
        
        
        if (_GhostTimeRemaining > 0)
        {
            _GhostTimeRemaining -= Time.fixedUnscaledDeltaTime;
        }
        quad.SetOffset(transform.position.x * 0.033f,transform.position.y * 0.05f);
    }

    public void SetPlayerCollider(bool t)
    {
        _collider.enabled = t;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Slime"))
            numOfSlimeTouching++;

        PlayerStatManager.instance.IsBeingHit = numOfSlimeTouching > 0;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Slime"))
            numOfSlimeTouching--;
        
        PlayerStatManager.instance.IsBeingHit = numOfSlimeTouching > 0;
    }
}
