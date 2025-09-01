using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _walkingForwardName;
    [SerializeField] private string[] _attackingNames;

    private bool _animationEnded;
    private const float ANIMATION_COMPLETION_TIME = 0.95f;

    private void Update()
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if(stateInfo.normalizedTime < ANIMATION_COMPLETION_TIME)
        {
            _animationEnded = false;
            return;
        }

        if (_animationEnded)
            return;

        if (stateInfo.IsName(_walkingForwardName))
        {
            _animationEnded = true;
            EventBus.OnWalkingComplete?.Invoke();
        }
        else
        {
            for (int i = 0; i < _attackingNames.Length; i++)
            {
                if (stateInfo.IsName(_attackingNames[i]))
                {
                    _animationEnded = true;
                    EventBus.OnAttackingComplete?.Invoke();
                    break;
                }
            }
        }
    }
}