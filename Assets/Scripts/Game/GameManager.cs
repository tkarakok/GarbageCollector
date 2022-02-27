using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> garbages;
    public Vector3 newPositionForGarbage;
    public Transform firstPosition;
    public GameObject finishLine;
    public Slider levelProgressBar;
    [Header("Finish")]
    public Material[] finishMaterials;
    public Renderer[] multiplierRenderer;
    public GameObject finishBox;
    public GameObject finishCap;
    public Transform finishPoint;
    [Header("Efekts")]
    public GameObject obstacleEfekts;
    public GameObject coinEfekts;
    public GameObject confettiEfects;
    public GameObject smokeEfect;
    [Header("Player Animator")]
    public Animator animator;


    private int _finishTurn = 0;
    private int _material = 0;
    private int _multiplier = 0;
    private float _maxDistance;

    private int garbageCounter = 0;
    private int recycleGarbage = 0;

    public int GarbageCounter { get => garbageCounter; set => garbageCounter = value; }
    public int RecycleGarbage { get => recycleGarbage; set => recycleGarbage = value; }
    public int Material { get => _material; set => _material = value; }
    public int Multiplier { get => _multiplier; set => _multiplier = value; }

    private void Start()
    {
        _maxDistance = finishLine.transform.position.z - MovementManager.Instance.transform.position.z;
    }
    private void Update()
    {
        float distance = finishLine.transform.position.z - MovementManager.Instance.transform.position.z;
        levelProgressBar.value = 1 - (distance / _maxDistance);
    }
    public void GetPoint(GameObject garbage)
    {
        for (int i = 0; i < garbage.transform.childCount; i++)
        {
            if (garbage.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                recycleGarbage += (i + 1);
                garbageCounter--;
            }
        }
    }

    public void RecycleGarbagePoint(GameObject garbage)
    {
        GetPoint(garbage);
        UiManager.Instance.InGameCoinUpdate();
        garbages.Remove(garbage);
        garbage.SetActive(false);
        garbage.transform.SetParent(null);
        for (int i = 0; i < garbages.Count; i++)
        {
            if (i == 0)
            {
                garbages[i].transform.position = firstPosition.position;
            }
            else
            {
                garbages[i].transform.position = garbages[i-1].transform.position + newPositionForGarbage;
            }
            
        }
    }

    public void Finish(GameObject garbage)
    {
        _finishTurn++;
        UiManager.Instance.InGameCoinUpdate();
        PlayerPrefs.SetInt("Level", (LevelManager.Instance.CurrentLevel));
        confettiEfects.SetActive(true);
        garbage.transform.SetParent(null);
        garbages.Remove(garbage);
        DOTween.Kill(garbage);
        garbage.transform.DOMove(finishPoint.position, .25f).OnComplete(()=> garbage.SetActive(false));
        smokeEfect.transform.position = finishPoint.position;
        smokeEfect.SetActive(true);
        GetPoint(garbage);
        if (_finishTurn %3 == 0)
        {
            _material++;
            finishBox.transform.DOScale(finishBox.transform.localScale += new Vector3(.25f, .25f, .25f), .25f);
            for (int i = 0; i < multiplierRenderer.Length; i++)
            {
                multiplierRenderer[i].material = finishMaterials[_material];
            }
        }
    }

    public void FinishBoxAnim()
    {
        
        finishCap.transform.DOMoveY(10,.5f);
        confettiEfects.SetActive(false);
        coinEfekts.SetActive(true);
    }
}
