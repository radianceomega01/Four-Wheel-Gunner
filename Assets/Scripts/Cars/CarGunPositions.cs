
using UnityEngine;

public class CarGunPositions : MonoBehaviour
{
    [Header("Gun Positions")]
    [SerializeField] Transform gunPositions;

    public Transform GetPosition(int index)
    {
        if (index < gunPositions.childCount)
        {
            return gunPositions.GetChild(index);
        }
        else 
        {
            return gunPositions.GetChild(0);
        }
    }

    public Transform GetGunPositionTransform() => gunPositions;
}
