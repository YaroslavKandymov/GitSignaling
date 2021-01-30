using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _deltaX;
    private float _deltaZ;

    void Update()
    {
        _deltaX = Input.GetAxis("Horizontal") * _speed;
        _deltaZ = Input.GetAxis("Vertical") * _speed;
        transform.Translate(_deltaX * Time.deltaTime, 0 , _deltaZ * Time.deltaTime);
    }
}
