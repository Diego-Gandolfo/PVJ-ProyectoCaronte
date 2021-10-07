using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestsManager : MonoBehaviour
{
    #region Static

    public static QuestsManager Instance { get; private set; }

    #endregion

    #region SerializeFields

    [Header("HUD")]
    [SerializeField] private GameObject _questBox;
    [SerializeField] private Text _titleMessage;
    [SerializeField] private Text _questMessage;

    [Header("Animator")]
    [SerializeField] private Animator _animator;

    [Header("Oxigen Quest")]
    [SerializeField] private float _oxigenTimeToStart;
    [SerializeField] private string _oxigenTitle;
    [SerializeField, TextArea] private string _oxigenQuest;

    [Header("Crystal Quest")]
    [SerializeField] private float _crystalTimeToStart;
    [SerializeField] private string _crystalTitle;
    [SerializeField, TextArea] private string _crystalQuest;

    #endregion

    #region Private Fields

    private float _questTimerCounter;
    private bool _canCount;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _questTimerCounter = _oxigenTimeToStart;

        _titleMessage.text = _oxigenTitle;
        _questMessage.text = _oxigenQuest;

        _questBox.SetActive(false);

        _canCount = true;
    }


    private void Update()
    {
        if (_canCount) RunTimer();
    }

    #endregion

    #region Private Fields

    private void RunTimer()
    {
        _questTimerCounter -= Time.deltaTime;

        if (_questTimerCounter <= 0f)
        {
            _canCount = false;
            _questBox.SetActive(true);
            _animator.SetTrigger("Show");
        }
    }

    #endregion

    #region Public Methods

    public void StartCrystalQuest()
    {
        if(_animator != null)
        {
            _animator.SetTrigger("Hide");

            _questTimerCounter = _crystalTimeToStart;

            _titleMessage.text = _crystalTitle;
            //_questMessage.text = _crystalQuest;
            
            string message = "Find  \n <b>" + LevelManager.instance.CrystalsNeeded + " CRYSTALS </b> \n <size=20><i> (shoot'em to break)</i></size>";
            _questMessage.text = message;

            _questBox.SetActive(false);

            _canCount = true;
        }
    }

    #endregion
}
