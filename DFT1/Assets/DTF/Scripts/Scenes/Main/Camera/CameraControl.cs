using UnityEngine;
using UnityEditor;

public class CameraControl : MonoBehaviour
{

    public void UpdatePos(float pos)
    {
        if (pos < 0)
            pos = 0;

        pos = 6;

        transform.localPosition = new Vector3(pos, 0, -10);
    }
}