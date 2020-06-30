using Gamekit2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    CharacterController2D controller2D;
    PlayerTransformator playerTransformator;
    Animator animator;

    float Hinput = 0f;
    float RunSpeed
    {
        get
        {
            float newValue = Hinput > 0 ? Hinput : -Hinput;
            return newValue;
        }
    }


    public float runSpeed = 40f;
    public float jumpHeight;
    public bool _canCrouch;

    bool jump;
    public bool crouch;
    private void Awake()
    {
        controller2D = GetComponent<CharacterController2D>();
        playerTransformator = GetComponent<PlayerTransformator>();
        animator = GetComponent<Animator>();
        playerTransformator.HasTransformed += ChangeMovementVariables;
    }

    private void OnDestroy()
    {
        playerTransformator.HasTransformed -= ChangeMovementVariables;
    }

    private void ChangeMovementVariables(float moveSpd, float jumpHght, bool canCrouch)
    {
        jumpHeight = jumpHght;
        runSpeed = moveSpd;
        _canCrouch = canCrouch;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Hinput = Input.GetAxis("Horizontal") * runSpeed;

        float RunValue = controller2D.m_Grounded == true ? RunSpeed : 0;
        animator.SetFloat("RunSpeed", RunValue);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (_canCrouch)
        {
            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
                
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }

/*        Debug.Log(crouch);*/
    }

    private void FixedUpdate()
    {
        controller2D.Move(Hinput * Time.fixedDeltaTime, crouch, jump, jumpHeight);
        jump = false;
    }

}
