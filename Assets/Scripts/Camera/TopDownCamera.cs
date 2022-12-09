using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform carParent;

    private bool isTargetSet;

    private void Start()
    {
        GameManager.Instance.OnCarSpawned += SetTarget;
    }
    public void SetTarget() => isTargetSet = true;

    private void FixedUpdate()
    {
        if (isTargetSet)
        { 
            target.position = carParent.GetChild(0).position;
            target.rotation = carParent.GetChild(0).rotation;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCarSpawned -= SetTarget;
    }
}
