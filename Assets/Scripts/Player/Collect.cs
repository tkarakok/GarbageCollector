using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collect : MonoBehaviour
{
    public Transform parent;
    private int _level = 0;

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
                //other.gameObject.transform.position = GameManager.Instance.firstPosition.position + (GameManager.Instance.GarbageCounter * GameManager.Instance.newPositionForGarbage);
                other.gameObject.transform.position = GameManager.Instance.garbages[GameManager.Instance.garbages.Count - 1].transform.position + GameManager.Instance.newPositionForGarbage;
                MovementManager.Instance.StartWaveMoney();
            }
            GameManager.Instance.GarbageCounter++;
            GameManager.Instance.garbages.Add(other.gameObject);
            other.transform.SetParent(parent);
            
            
            other.gameObject.AddComponent<Collect>().parent = parent;
            
        }
        else if (other.CompareTag("Obstacle"))
        {
            
            if (gameObject.layer != 0)
            {
                MovementManager.Instance.StopMovement(gameObject,false);
                MovementManager.Instance.gameObject.transform.DOMoveZ(MovementManager.Instance.gameObject.transform.position.z - 2, .5f);
            }
            else
            {
                MovementManager.Instance.gameObject.transform.DOMoveZ(MovementManager.Instance.gameObject.transform.position.z - 2, .5f);
            }
            
        }
        else if (other.CompareTag("Slice"))
        {
            if (gameObject.layer != 0)
            {
                MovementManager.Instance.StopMovement(gameObject, true);
                MovementManager.Instance.gameObject.transform.DOMoveZ(MovementManager.Instance.gameObject.transform.position.z - 2, .5f);
            }
            else
            {
                MovementManager.Instance.gameObject.transform.DOMoveZ(MovementManager.Instance.gameObject.transform.position.z - 2, .5f);
            }
        }
        else if (other.CompareTag("Converter"))
        {
            if (_level < 3)
            {
                transform.GetChild(_level).gameObject.SetActive(false);
                _level++;
                transform.GetChild(_level).gameObject.SetActive(true);
            }

        }
        else if (other.CompareTag("RecycleBox"))
        {
            if (gameObject.layer != 0)
            {
                GameManager.Instance.RecycleGarbagePoint(gameObject);
            }
            else
            {
                other.transform.DOMoveX(10,.25f);
                return;

            }

        }
        else if (other.CompareTag("Finish"))
        {
            if (gameObject.layer != 0)
            {
                GameManager.Instance.Finish(gameObject);
            }
            else
            {
                StateManager.Instance.State = State.EndGame;
                GameManager.Instance.Multiplier = GameManager.Instance.Material + 1;
                UiManager.Instance.EndGame();
                GameManager.Instance.FinishBoxAnim();
                GameManager.Instance.animator.SetBool("Run",false);
                GameManager.Instance.animator.SetBool("Win",true);
            }
        }
    }

}
