using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CrystalGroups : MonoBehaviour
{
    [SerializeField] private Transform crystalGroup;
    [SerializeField] private float radious = 1f;

    private List<CrystalController> crystalList = new List<CrystalController>();
    private bool canSound;
    private float timer;
    private AudioSource audioSource;

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
        if (timer >= 0)
            timer -= Time.deltaTime;

        if (canSound )
        {
            
            var distance = Vector3.Distance(crystalGroup.position, CrystalManager.instance.PlayerLocation.position); //Check Player Distance from radius around crystals so if he there, the alarm doesn't sound. 
            print(distance);
            if (distance > radious && timer <= 0)
            {
                //TODO: Sound from crystals and UI animation
                print(gameObject.name);
                audioSource.PlayOneShot(CrystalManager.instance.Sound);
                timer = CrystalManager.instance.SoundTimer;
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
        }
    }

    public void ShowLocation(bool value)
    {
        canSound = value;
        if (value)
            timer = CrystalManager.instance.SoundTimer;
    }
}
