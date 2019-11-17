using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulets : MonoBehaviour
{
    [SerializeField] private GameObject[] _bults;
    private Vector3[] _startPos;
    private Vector3[] _endtPos;
    private float _liveTime = 0;

    private void Awake()
    {
        _startPos = new Vector3[_bults.Length];
        _endtPos = new Vector3[_bults.Length];
    }

    public void SetBulet(int index, Vector3 start, Vector3 end)
    {
        if (index > _bults.Length - 1)
            return;

        _bults[index].SetActive(true);
        _startPos[index] = start;
        _endtPos[index] = end;
        _liveTime = 1;
        Update();
    }

    void Update()
    {
        if (_liveTime > 0)
        {
            _liveTime -= Time.deltaTime;
            if (_liveTime < 0)
            {
                for (int i = 0; i < _bults.Length; i++)
                {
                    _bults[i].SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < _bults.Length; i++)
                {
                    _bults[i].transform.position = _endtPos[i] + (_startPos[i] - _endtPos[i]) * _liveTime;
                }
            }
        }
    }
}
