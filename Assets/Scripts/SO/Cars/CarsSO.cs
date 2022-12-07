
using UnityEngine;

[CreateAssetMenu(fileName = "Car", menuName = "ScriptableObjects/Cars", order = 1)]
public class CarsSO : ScriptableObject
{
    public string carName;

    [Header("Stats values(0 to 100)")]
    public float maxHP;
    public float maxSpeed;
    public float controls;

    [Header("Implementational values")]
    public float actualHP;
    public float actualMaxSpeed;
    public float actualControls;
}
