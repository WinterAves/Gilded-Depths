using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _submarine;
    [SerializeField] private Vector3 _offset;
    [SerializeField][Range(0f, 1f)] private float _smoothing;

    private Camera _mainCam;

    void Start()
    {
        _mainCam = Camera.main;    
    }

    
    void FixedUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 submarineDestination = _submarine.position + _submarine.velocity / 2f;
        transform.position = Vector3.Lerp(transform.position, submarineDestination, _smoothing) + _offset;
    }
}
