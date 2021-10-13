using UnityEngine;

public class AnimatorSpeedChanger : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private float _animatorSpeed = 1;

    #endregion

    #region Private Fields

    // Components
    private Animator _animator;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _animator = GetComponent<Animator>();

        if (_animator != null)
        {
            _animator.speed = _animatorSpeed;
        }
        else 
        {
            Debug.LogError($"{this}: {gameObject.name} no tiene un Animator");
        }

        this.enabled = false;
    }

    #endregion
}
