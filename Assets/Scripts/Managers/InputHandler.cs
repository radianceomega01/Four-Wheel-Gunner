using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] FixedJoystick joyStick;
    [SerializeField] Transform carParent;

    private CarController carController;
    private Gun[] guns;

    private void Start()
    {
        GameManager.Instance.OnCarSpawned += Initialize;
    }
    private void Initialize()
    {
        carController = carParent.GetChild(0).GetComponent<CarController>();
        guns = carController.GetComponentsInChildren<Gun>();
    }

    private void FixedUpdate()
    {
        carController.SetJoystickInput(joyStick.Vertical, joyStick.Horizontal);
    }

    public void SetAccelerationInput(int value)
    {
        carController.SetAccelerationInput(value);
    }

    public void SetBrakeInput(bool value)
    {
        carController.SetBrakeInput(value);
    }

    public void SetShootInput(bool value)
    {
        for(int i=0; i<guns.Length; i++)
            guns[i].PrepareToShoot(value);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCarSpawned -= Initialize;
    }
}
