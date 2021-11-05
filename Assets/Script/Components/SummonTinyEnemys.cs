using UnityEngine;

public class SummonTinyEnemys : MonoBehaviour
{
    #region Serialized Fields

    [Header("Prefab")]
    [SerializeField] private GameObject _tinyEnemyPrefab;

    [Header("Summon Settings")]
    [SerializeField, Range(1,100)] private int _summonThreshold = 1;
    [SerializeField] private int _amountToSummon;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;

    #endregion

    #region Private Fields

    // Components
    private HealthController _healthController;
    private Animator _animator;

    #endregion

    #region Unity Methods

    private void Start()
    {
        GetHealthControllerComponent();
        GetAnimatorComponent();
    }

    #endregion

    #region Private Methods

    private void GetHealthControllerComponent()
    {
        _healthController = GetComponent<HealthController>();

        if (_healthController == null)
        {
            Debug.LogError($"{gameObject.transform.parent.name} no tiene el HealthController que necesita el SummonTinyEnemys");
        }
        else
        {
            _healthController.OnTakeDamage += OnTakeDamageHandler;
        }
    }

    private void GetAnimatorComponent()
    {
        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError($"{gameObject.transform.parent.name} no tiene el Animator que necesita el SummonTinyEnemys");
        }
    }

    private void OnTakeDamageHandler()
    {
        var currentLifePercentage = ((float)_healthController.CurrentHealth / (float)_healthController.MaxHealth) * 100f;

        if (currentLifePercentage <= _summonThreshold)
        {
            _animator.Play("Cast Spell");

            for (int i = 0; i < _amountToSummon; i++)
            {
                var x = Random.Range(_minDistance, _maxDistance);
                var z = Random.Range(_minDistance, _maxDistance);
                var position = transform.position + new Vector3(x, 0f, z);

                Instantiate(_tinyEnemyPrefab, position, transform.rotation);
            }

            _healthController.OnTakeDamage -= OnTakeDamageHandler;
        }
    }

    #endregion
}
