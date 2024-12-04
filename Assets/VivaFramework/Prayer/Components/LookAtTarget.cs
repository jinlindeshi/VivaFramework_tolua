using System;
using BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target;

    public bool lockX = false;
    public bool lockY = false;
    public bool lockZ = false;

    private float _lockX;
    private float _lockY;
    private float _lockZ;
    private void Start()
    {
        if (lockX == true)
        {
            _lockX = transform.localEulerAngles.x;
        }
        if (lockY == true)
        {
            _lockY = transform.localEulerAngles.y;
        }
        if (lockZ == true)
        {
            _lockZ = transform.localEulerAngles.z;
        }
    }

    public void TestLog()
    {
        print("你妹啊~~~");
    }

    private void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
            Vector3 euler = transform.localEulerAngles;
            if (lockX == true)
            {
                euler.x = _lockX;
            }
            if (lockY == true)
            {
                euler.y = _lockY;
            }
            if (lockZ == true)
            {
                euler.z = _lockZ;
            }

            transform.localEulerAngles = euler;
        }
    }
}