using UnityEngine;

public class PlayerMovementPath : MonoBehaviour
{
    public float laneChangeSpeed = 5f;
    private int _currentLane = 1; // 0=left, 1=center, 2=right
    private Vector3 _targetPosition;
    private readonly float[] _lanePositions = { -2f, 0f, 2f };

    void Start()
    {
        _targetPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _targetPosition,
            laneChangeSpeed * Time.deltaTime
        );

        HandleInput();
    }

    private void HandleInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft))
        {
            MoveLeft();
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight))
        {
            MoveRight();
        }
    }

    public void MoveLeft()
    {
        if (_currentLane > 0)
        {
            _currentLane--;
            UpdateTargetPosition();
        }
    }

    public void MoveRight()
    {
        if (_currentLane < 2)
        {
            _currentLane++;
            UpdateTargetPosition();
        }
    }

    private void UpdateTargetPosition()
    {
        _targetPosition = new Vector3(
            _lanePositions[_currentLane],
            transform.position.y,
            transform.position.z
        );
    }
}