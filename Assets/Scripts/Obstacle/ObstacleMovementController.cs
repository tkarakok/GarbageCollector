using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleMovementController : MonoBehaviour
{
    public bool moveable = false;
    public float duration;
    public Vector3 targetPosition;

    private Vector3 _firstPosition;

    void Start()
    {
        _firstPosition = transform.position;
        if (moveable)
        {
            StartCoroutine(Move());
        }
    }
    IEnumerator Move()
    {
        while (true)
        {
            transform.DOMove(targetPosition, duration).OnComplete(() => transform.DOMove(_firstPosition, duration));

            yield return new WaitForSeconds(duration * 2);
        }
    }
}
