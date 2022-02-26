using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Vector3 _offset;

    private void Start()
    {
        _offset = gameObject.transform.position - _target.position;
    }

    private void LateUpdate()
    {
        if (StateManager.Instance.State == State.InGame)
        {
            
            Vector3 targetPosition = _target.position + _offset;
            // targetPosition += new Vector3(0,_offsetY,0);
            transform.position = new Vector3(transform.position.x , transform.position.y, targetPosition.z);
        }

    }
}
