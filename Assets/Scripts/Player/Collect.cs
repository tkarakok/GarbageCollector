using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            }
            GameManager.Instance.GarbageCounter++;
            GameManager.Instance.garbages.Add(other.gameObject);
            other.transform.SetParent(parent);
            
            
            other.gameObject.AddComponent<Collect>().parent = parent;
            
        }
    }

}
