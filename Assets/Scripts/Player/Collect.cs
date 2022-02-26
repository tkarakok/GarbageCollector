using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collect : MonoBehaviour
{
    public Transform parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Garbage"))
        {
            other.tag = "Untagged";
            
            if (GameManager.Instance.GarbageCounter == 0)
            {
                other.gameObject.transform.position = GameManager.Instance.firstPosition.position;
            }
            else
            {
                other.gameObject.transform.position = GameManager.Instance.firstPosition.position + (GameManager.Instance.GarbageCounter * GameManager.Instance.newPositionForGarbage);
                MovementManager.Instance.StartWaveMoney();
            }
            GameManager.Instance.GarbageCounter++;
            GameManager.Instance.garbages.Add(other.gameObject);
            other.transform.SetParent(parent);
            
            
            other.gameObject.AddComponent<Collect>().parent = parent;
            
        }
        if (other.CompareTag("Obstacle"))
        {
            other.tag = "Untagged";
            MovementManager.Instance.StopMovement(gameObject);
            MovementManager.Instance.gameObject.transform.DOMoveZ(MovementManager.Instance.gameObject.transform.position.z - 2,.5f);
        }
    }

}
