using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour, ISelectHandler
{
    public float TopSpeed = 10;
    float _vel;

    private float _lastPressAmount;

    private void Update()
    {
        if (_lastPressAmount > 0.01f)
        {
            _vel += Time.deltaTime/3f;
            if (_vel > 1) _vel = 1;
        }
        else
        {
            _vel -= Time.deltaTime;
            if (_vel < 0) _vel = 0;
        }

        if (_vel > 0)
        {
            Vector3 move3D = GazeManager.Instance.Rays[0].Direction;
            float moveSpeed = TopSpeed * Time.deltaTime * _vel;
            transform.position += move3D * moveSpeed;
        }
    }

    public void OnSelectPressedAmountChanged(SelectPressedEventData eventData)
    {
        Debug.Log(eventData.PressedAmount);
        _lastPressAmount = (float)eventData.PressedAmount;
    }
}
