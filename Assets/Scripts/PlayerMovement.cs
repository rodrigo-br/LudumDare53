using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Singleton<PlayerMovement>
{
    [SerializeField] float  moveSpeed = 3f;
    [SerializeField] int    numberOfLettersinGame = 10;
    int[]                   lettersReadArray;
    int                     lettersRead;
    Rigidbody2D             myRigidBody;
    Vector2                 moveInput;
    float                   defaultMoveSpeed;
    Interactible            interactible;
    bool                    isReadingLetter = false;
    bool                    isFlipped = false;
    protected override void Awake()
    {
        base.Awake();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        lettersReadArray = new int[numberOfLettersinGame];
        lettersRead = 0;
        defaultMoveSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        myRigidBody.velocity = moveInput * moveSpeed;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.x < 0 && isFlipped || moveInput.x > 0 && !isFlipped)
        {
            isFlipped = !isFlipped;
            transform.localScale = new Vector2(-transform.localScale.x, 1f);
        }
    }

    void OnRun(InputValue value)
    {
        if (value.isPressed)
        {
            moveSpeed *= 2;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
        }
    }

    void OnInteract(InputValue value)
    {
        if (isReadingLetter)
        {
            interactible.StopReading();
            isReadingLetter = false;
        }
        else if (myRigidBody.IsTouchingLayers(LayerMask.GetMask("Interactible")) && !isReadingLetter)
        {
            if (interactible.OnInteract(lettersRead))
            {
                isReadingLetter = true;
                if (lettersReadArray[interactible.GetIndex()]++ == 0)
                {
                    lettersRead++;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Interactible")
        {
            interactible = other.GetComponent<Interactible>();
        }
    }
}
