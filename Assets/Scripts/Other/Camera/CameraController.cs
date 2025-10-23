using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform _player;
    public Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - _player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            transform.position = _player.position + _offset;
        }
    }
}
