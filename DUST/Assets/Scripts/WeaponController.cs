using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform weaponTransform;
    private Vector3 _weaponDirection;
    private Vector3 _mouseDistance;
    private float _weaponDistance;
    
    void Start()
    {
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;

        _weaponDirection  = transform.rotation * Vector3.right;
        
        _mouseDistance = mousePosition - transform.position;
        float distanceMagnitude = _mouseDistance.magnitude;
        _mouseDistance /= distanceMagnitude;
        
        WeaponMouseRotation();
    }

    private void WeaponMouseRotation()
    {
        Vector3 axisToRotateAround = Vector3.forward;
        float cosineHalfAlpha = Vector3.Dot(_weaponDirection, _mouseDistance);
        Vector3 cross = Vector3.Cross(_weaponDirection, _mouseDistance);
        float sineHalfAlpha = cross.z;
        float angle = Mathf.Atan2(sineHalfAlpha, cosineHalfAlpha) * Mathf.Rad2Deg;
        
        Quaternion rotateToMouse = Quaternion.AngleAxis(angle, axisToRotateAround);
        transform.rotation *= rotateToMouse;
    }
    
    private void WeaponMousePosition()
    {
        if(weaponTransform == null)
            _weaponDistance = (transform.position - transform.GetChild(0).position).magnitude;
        transform.GetChild(0).position = transform.position + (_mouseDistance * _weaponDistance);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + _weaponDirection);
        Gizmos.DrawLine(transform.position, transform.position + _mouseDistance);
    }
}
