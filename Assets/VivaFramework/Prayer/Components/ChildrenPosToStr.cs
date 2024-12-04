using System;
using UnityEngine;

public class ChildrenPosToStr : MonoBehaviour
{
    public string locStr;

    public void TraceLoc()
    {
        string str = "{";
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 pos = transform.GetChild(i).localPosition;
            str += "Vector3.New(" + pos.x + "," + pos.y + "," + pos.z + "),";
        }

        str += "}";
        Debug.Log("TraceLoc " + str);
    }
}