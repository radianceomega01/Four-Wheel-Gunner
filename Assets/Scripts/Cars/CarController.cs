using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
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
        rigidBody.centerOfMass = Vector3.up * -0.9f;
    }

    private void FixedUpdate()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
            GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            CarReset();
        if (verticalInput == 0)
        {
            rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, Vector3.zero, Time.deltaTime * 3);
            //print("done");

        }
        //else if(verticalInput == 1)
        //{
        //    carRb.velocity = transform.forward * 10;

        //}

    }

    public void SetHorizontalInput(int value)
    {
        horizontalInput = value;
    }
    public void SetVerticalInput(int value)
    {
        verticalInput = value;
    }
    private void GetInput()
    {
        verticalInput = Input.GetAxis(VERTICAL);
        // print(verticalInput);
        horizontalInput = Input.GetAxis(HORIZONTAL);
        isBreaking = Input.GetKey(KeyCode.Space);

    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
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

    void CarReset()
    {

        transform.rotation = new Quaternion(0, 0, 0, 0);

    }


    public void SetStartGamePos(Transform pos)
    {
        this.transform.position = pos.position;
        this.transform.rotation = pos.rotation;
    }
}
