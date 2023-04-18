using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    [SerializeField] Transform tf;
    public float speed;
    private void LateUpdate()
    {
        tf.Rotate(Vector3.up * speed, Space.Self);
    }
}
