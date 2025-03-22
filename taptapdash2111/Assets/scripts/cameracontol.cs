using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontol : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private Vector3 _leprOffset;

    void Update()
    {
        transform.position = Vector3.Lerp(_player.position + _cameraOffset,
            _player.position + _cameraOffset + _leprOffset, 1);
    }
}
