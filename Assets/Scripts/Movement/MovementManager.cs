using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementManager : Singleton<MovementManager>
{
    [SerializeField] private float _limitX = 2;
    [SerializeField] private float _xSpeed = 25;
    [SerializeField] private float _forwardSpeed = 2;
    [SerializeField] private float _waveSpeed = .1f;

    public Vector3 waveScale;
   
    void Update()
    {
        if (StateManager.Instance.State == State.InGame)
        {
            float _touchXDelta = 0;
            float _newX = 0;
            if (Input.GetMouseButton(0))
            {
                _touchXDelta = Input.GetAxis("Mouse X");
            }
            _newX = transform.position.x + _xSpeed * _touchXDelta * Time.deltaTime;
            _newX = Mathf.Clamp(_newX, -_limitX, _limitX);



            Vector3 newPosition = new Vector3(_newX, transform.position.y, transform.position.z + _forwardSpeed * Time.deltaTime);
            transform.position = newPosition;


            for (int i = 1; i < GameManager.Instance.garbages.Count; i++)
            {
                Transform garbage = GameManager.Instance.garbages[i].transform;
                garbage.DOMoveX(transform.position.x, (i * .2f));
            }
        }

    }

    #region Money Wave
    
    public void StartWaveMoney()
    {
        StartCoroutine(WaveMoney());
    }
    
    IEnumerator WaveMoney()
    {
        for (int i = 0; i < GameManager.Instance.garbages.Count; i++)
        {
            Transform garbage = GameManager.Instance.garbages[i].transform;
            garbage.localScale += waveScale;
            yield return new WaitForSeconds(_waveSpeed);
            garbage.localScale -= waveScale;
        }
    }
    #endregion
}
