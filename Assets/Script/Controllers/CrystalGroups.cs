using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGroups : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform crystalGroup;

    private List<CrystalController> crystalList = new List<CrystalController>();
    private bool canSound;
    private float timer;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++) //conta y agrega a la lista, pero solo si el objeto esta activo.
        {
            if (transform.GetChild(i).gameObject.activeSelf) //si el hijo esta activo
            {
                CrystalController crystal = transform.GetChild(i).gameObject.GetComponent<CrystalController>();
                if (crystal != null)
                {
                    crystalList.Add(crystal);
                    crystal.OnDie += OnCrystalDead;
                }
            }
        }

        CrystalManager.instance.AddCrystalGroupList(this);

        if (crystalGroup == null)
            print("Falta agregar el punto de deteccion del crystal group en " + gameObject.name);
    }

    private void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            if (timer >= 0)
                timer -= Time.deltaTime;
            if (canSound)
            {
                DoEffects();
            }
        }
    }

    private void OnCrystalDead(CrystalController crystal)
    {
        crystal.OnDie -= OnCrystalDead;
        crystalList.Remove(crystal);
        CheckIfEmpty();
    }
    
    private void CheckIfEmpty()
    {
        if(crystalList.Count <= 0)
        {
            CrystalManager.instance.RemoveCrystalGroup(this);
            canSound = false;
        }
    }

    private void DoEffects()
    {
        CheckIfEmpty();
        var distance = Vector3.Distance(crystalGroup.position, CrystalManager.instance.PlayerLocation.position);
        if (timer <= 0)
        {
            audioSource.Play();
            timer = CrystalManager.instance.SoundTimer;
            HUDManager.instance.SonarManager.TriggerLevel(distance);
        }
    }

    public void ShowLocation(bool value)
    {
        canSound = value;
    }


}
