using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private int acceleration;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBraking;

    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody>();
        rigidBody.centerOfMass = Vector3.forward * -0.1f;
    }

    private void FixedUpdate()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
            GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

        /*if (verticalInput == 0)
        {
            rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, Vector3.zero, Time.deltaTime * 3);
        }*/
    }

    private void GetInput()
    {
        verticalInput = Input.GetAxis(VERTICAL);
        horizontalInput = Input.GetAxis(HORIZONTAL);
        acceleration = Convert.ToInt32(Input.GetKey(KeyCode.LeftShift));
        isBraking = Input.GetKey(KeyCode.Space);

    }

    public void SetJoystickInput(float vertical, float horizontal)
    {
        if(vertical >=0)
            verticalInput = 1;
        else
            verticalInput = -1;
        horizontalInput = horizontal;
    }

    public void SetBrakeInput(bool brake) => isBraking = brake;
    public void SetAccelerationInput(int acceleration) => this.acceleration = acceleration;

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * acceleration * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * acceleration * motorForce;
        currentbreakForce = isBraking ? brakeForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
