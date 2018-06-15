using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class crazyTest : MonoBehaviour, IInputHandler, ISourcePositionHandler, ISourceRotationHandler, ISourceStateHandler, ISelectHandler, IInputClickHandler
{
    public Transform CameraTransform;
    private Rigidbody _rigidbody;
    public float movementSpeed = 10;
    public float rotationSpeed = 1;

    public float deadZoneX = 10;
    public float deadZoneZ = 10;
    
    private float _lastPressAmount;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        RotateRigidbody();
        MoveRigidbody();



    }

    private void RotateRigidbody()
    {
        float xRot = Mathf.DeltaAngle(CameraTransform.localEulerAngles.x,0);
        if (xRot > deadZoneX)
        {
            xRot -= deadZoneX;
        }
        else if (xRot < -deadZoneX)
        {
            xRot += deadZoneX;
        }
        else
        {
            xRot = 0;
        }
        Vector3 rot =  CameraTransform.rotation* Vector3.right*xRot;
        _rigidbody.AddTorque(-rot * rotationSpeed* Time.deltaTime);

        float zRot = Mathf.DeltaAngle(CameraTransform.localEulerAngles.z, 0);
        if (zRot > deadZoneZ)
        {
            zRot -= deadZoneZ;
        }
        else if (zRot < -deadZoneZ)
        {
            zRot += deadZoneZ;
        }
        else
        {
            zRot = 0;
        }
        rot = CameraTransform.rotation * Vector3.forward * zRot;
        _rigidbody.AddTorque(-rot * rotationSpeed * Time.deltaTime);
    }

    void MoveRigidbody()
    {
        Vector3 move3D = GazeManager.Instance.Rays[0].Direction;
        _rigidbody.AddForce(move3D * movementSpeed * _lastPressAmount);
    }

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
