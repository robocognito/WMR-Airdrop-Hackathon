using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour, IInputHandler, ISourcePositionHandler, ISourceRotationHandler, ISourceStateHandler, ISelectHandler, IInputClickHandler
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
            //Vector3 move2D = new Vector3(GazeManager.Instance.Rays[0].Direction.x, 0, GazeManager.Instance.Rays[0].Direction.z).normalized;
            Vector3 move3D = GazeManager.Instance.Rays[0].Direction;
            float moveSpeed = TopSpeed * Time.deltaTime * _vel;
            transform.position += move3D * moveSpeed;
        }


    }

    //move forward and accelerate based on the depth of press
    public void OnInputClicked(InputClickedEventData eventData)
    {

    }

    public void OnInputDown(InputEventData eventData)
    {
    }

    public void OnInputUp(InputEventData eventData)
    {
    }

    public void OnPositionChanged(SourcePositionEventData eventData)
    {
    }

    public void OnRotationChanged(SourceRotationEventData eventData)
    {
    }


    public void OnSelectPressedAmountChanged(SelectPressedEventData eventData)
    {
        _lastPressAmount = (float)eventData.PressedAmount;
    }

    public void OnSourceDetected(SourceStateEventData eventData)
    {
    }

    public void OnSourceLost(SourceStateEventData eventData)
    {
    }
}
