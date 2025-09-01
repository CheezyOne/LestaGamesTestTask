using UnityEngine;

public class WalkForward : MonoBehaviour
{
    [SerializeField] private float _walkTime;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _transform;
    [SerializeField] private bool _isReversedModel;

    private void Update()
    {
        if (_walkTime <= 0)
            return;

        _walkTime-=Time.deltaTime;
        Vector3 targetPosition;

        if(!_isReversedModel)
        {
            targetPosition = _transform.position + _transform.forward;
        }
        else
        {
            targetPosition = _transform.position - _transform.forward;
        }

        _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, _moveSpeed * Time.deltaTime);
    }
}