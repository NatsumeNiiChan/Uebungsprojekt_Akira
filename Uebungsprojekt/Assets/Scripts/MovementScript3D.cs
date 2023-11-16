using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript3D : MonoBehaviour
{
    public Rigidbody MyRigidbody;
    private Camera mainCamera;
    private Animator playerAnimator;
    
    public float MoveSpeed = 2f;
    
    private void Awake()
    {
        mainCamera = Camera.main;
        MyRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponentInChildren<Animator>();

        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        Vector2 movementInput;

        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        Move(movementInput);
    }


    private void Move(Vector2 movementInput)
    {
        Vector3 newVelocity = MyRigidbody.velocity;

        newVelocity = CalculateHorizontalMovement(newVelocity, movementInput);
        
        newVelocity.y = MyRigidbody.velocity.y;

        MyRigidbody.velocity = newVelocity;

        if (newVelocity.magnitude > 0.5)
        {
            transform.forward = GetCameraForward();
            playerAnimator.SetBool("IsWalking", true);
        }

        else playerAnimator.SetBool("IsWalking", false);
    }

    private Vector3 CalculateHorizontalMovement(Vector3 newVelocity, Vector2 movementInput)
    {
        //Berechnung
        Vector3 forwardDirection = movementInput.y * GetCameraForward();
        Vector3 sidewardDirection = movementInput.x * GetCameraSideward();

        Vector3 generalDirection = forwardDirection + sidewardDirection;
        generalDirection = generalDirection.normalized;

        newVelocity = generalDirection * MoveSpeed;

        return newVelocity;
    }

    public Vector3 GetCameraForward()
    {
        Vector3 cameraForward = GetPlaneDirection(mainCamera.transform.forward);
        return cameraForward;
    }

    public Vector3 GetCameraSideward()
    {
        Vector3 cameraSidewards = GetPlaneDirection(mainCamera.transform.right);
        return cameraSidewards;
    }

    private Vector3 GetPlaneDirection(Vector3 vector)
    {
        vector.y = 0f;
        vector = vector.normalized;
        return vector;
    }
}
