using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Static

    public static LevelManager instance;

    #endregion

    #region Serialized Fields

    [SerializeField] private Transform respawnPoint;

    #endregion

    #region Events

    public Action<PlayerController> OnPlayerAssing;
    public Action<int> OnCrystalsInPlayerUpdate;
    public Action<int> OnCrystalsInBankUpdate;

    #endregion

    #region Properties

    public PlayerController Player { get; private set; }
    public ShipItemsManager ShipManager { get; private set; }
    public int CrystalCounter => CrystalsInBank + CrystalsInPlayer;
    public int CrystalsInBank { get; private set; }
    public int CrystalsInPlayer { get; private set; }
    public bool HasAllShipItems { get; private set; } //Esta seria la variable para llamar al final. 

    #endregion

    #region Unity Methods

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        ShipManager = GetComponent<ShipItemsManager>();
        ShipManager.OnCompleted += OnCompleted;
    }



    #endregion

    private void OnCompleted() //Solo ocurre si el player consiguio todos los items
    {
        HasAllShipItems = true;
        //TODO: Hacer el final o algo?
    }

    #region Public Methods

    public void SetPlayer(PlayerController player)
    {
        Player = player;
        OnPlayerAssing?.Invoke(player);
        DialogueManager.Instance.SuscribeOnDie();
    }

    public void GameOver()
    {
        GameManager.instance.SetCursorActive(true);
        SceneManager.LoadScene("GameOver");
    }

    public void Victory()
    {
        GameManager.instance.SetCursorActive(true);
        SceneManager.LoadScene("Victory");
    }

    public void AddCrystalInPlayer(int number)
    {
        CrystalsInPlayer += number;
        OnCrystalsInPlayerUpdate?.Invoke(CrystalsInPlayer);
    }

    public void RemoveCrystalsInPlayer(int number)
    {
        CrystalsInPlayer -= number;
        OnCrystalsInPlayerUpdate?.Invoke(CrystalsInPlayer);
    }

    public void SetCrystalsInBank(int number)
    {
        RemoveCrystalsInPlayer(number);
        CrystalsInBank += number;
        OnCrystalsInBankUpdate?.Invoke(CrystalsInBank);
    }

    public void RemoveCrystalsInBank(int number)
    {
        CrystalsInBank -= number;
        OnCrystalsInBankUpdate?.Invoke(CrystalsInBank);
    }

    public void TakeCrystals(int number)
    {
        if(number > CrystalsInPlayer) //Si el monto es mayor que lo que tiene el player en la mochila...
        {
            int total = number - CrystalsInPlayer; //Saca lo que falta
            RemoveCrystalsInPlayer(CrystalsInPlayer); //Saca todo al player
            RemoveCrystalsInBank(total); //Saca lo que falta del banco
        } else
        {
            RemoveCrystalsInPlayer(number); //Saca todo del player
        }
    }

    public void Respawn()
    {
        Player.transform.position = respawnPoint.position;
    }
    #endregion
}
