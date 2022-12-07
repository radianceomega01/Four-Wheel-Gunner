
using UnityEngine;

public class CarGunPositions : MonoBehaviour
{
    [Header("Gun Positions")]
    [SerializeField] Transform topPos;
    [SerializeField] Transform leftPos;
    [SerializeField] Transform rightPos;

    public Transform GetPosition(int index)
    {
        if (index == 0)
            return topPos;
        else if (index == 1)
            return leftPos;
        else if (index == 2)
            return rightPos;
        else
            return topPos;
    }
}
