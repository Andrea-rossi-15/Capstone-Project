using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

[CustomEditor(typeof(EnemyConeVision))]
public class EnemyFOVeditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyConeVision _fov = (EnemyConeVision)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(_fov.transform.position, Vector3.forward, Vector3.right, 360, _fov._radius);

        Vector3 _viewAngle01 = DirectionFromAngle(-_fov._angle / 2);
        Vector3 _viewAngle02 = DirectionFromAngle(_fov._angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(_fov.transform.position, _fov.transform.position + _viewAngle01 * _fov._radius);
        Handles.DrawLine(_fov.transform.position, _fov.transform.position + _viewAngle02 * _fov._radius);

        if (_fov._canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(_fov.transform.position, _fov._playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float _angleInDegrees)
    {
        float rad = _angleInDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
    }
}
