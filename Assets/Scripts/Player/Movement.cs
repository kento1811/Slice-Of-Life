using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private bool isMoving = false;
    private bool isJumping = false;
    [SerializeField] private float speed = 4f;
    private Vector3 velocityVec;


    [SerializeField] private float jumpAccer = 4f;
    [SerializeField] private float jumpTime = 1f;
    private float jumpVec;

    [SerializeField] private Transform spriteTransform;

    void Awake()
    {
        if(!spriteTransform)
        {
            spriteTransform = transform.GetChild(0);
        }
    }
    void Start()
    {
        velocityVec = new Vector3(0,0,0);
        jumpVec = 0;
    }

    // Update is called once per frame
    void Update()
    {
        HandelInputAction();

        transform.position += velocityVec.normalized * Time.deltaTime * speed;
    }


    private void HandelInputAction()
    {
        if(Keyboard.current != null)
        {
            Move();
            Jump();
        }
    }

    private void Move()
    {
        velocityVec.x = 0;
        velocityVec.y = 0;
        if(Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            isMoving = true;
            velocityVec.x = -1;
        }
        if(Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            isMoving = true;
            velocityVec.x = 1;
        }
        if(Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            isMoving = true;
            velocityVec.y = 1;
        }
        if(Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            isMoving = true;
            velocityVec.y = -1;
        }
    }
    private void Jump()
    {
        if (Keyboard.current.spaceKey.isPressed && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpCoroutine());
        }
    }

    private IEnumerator JumpCoroutine()
    {
        float BeginTimer = Time.time;
        float Timer = Time.time - BeginTimer;
        while(Timer <= jumpTime)
        {
            jumpVec += Mathf.Sin(Mathf.PI/2*(1 + 2*Timer/jumpTime))*jumpAccer*Time.deltaTime;
            spriteTransform.localPosition = new Vector3(0, jumpVec, 0);
            yield return null;
            Timer = Time.time - BeginTimer;
        }
        isJumping = false;
        spriteTransform.localPosition = Vector3.zero;
    }

}
