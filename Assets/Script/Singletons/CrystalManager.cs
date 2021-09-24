using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalManager : MonoBehaviour
{
    [SerializeField] private AudioClip soundAlarm;
    [SerializeField] private float soundtimerCD = 1f;
    private List<CrystalGroups> crystalsGroups = new List<CrystalGroups>();
    private PlayerController player;
    private CrystalGroups currentPlaying;

    public static CrystalManager instance;

    public float SoundTimer => soundtimerCD;
    public Transform PlayerLocation => player.transform;
    public AudioClip Sound => soundAlarm;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        player = GameManager.instance.Player;
        
    }

    private void Update()
    {
        CheckNearCrystal();
    }

    public void AddCrystalGroupList(CrystalGroups crystals)
    {
        crystalsGroups.Add(crystals);
    }

    public void RemoveCrystalGroup(CrystalGroups crystals)
    {
        crystalsGroups.Remove(crystals);
    }

    private void CheckNearCrystal()
    {
        if(crystalsGroups.Count > 0)
        {
            CrystalGroups nearest = crystalsGroups[0];
            float shortestDistance = Vector3.Distance(crystalsGroups[0].transform.position, PlayerLocation.position);

            for (int i = crystalsGroups.Count - 1; i >= 0; i--) //Cuando termine este for, deberia tener la posicion más cercana seguro. 
            {
                var distance = Vector3.Distance(crystalsGroups[i].transform.position, PlayerLocation.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearest = crystalsGroups[i];
                }
            }

            if(nearest != currentPlaying)
            {
                currentPlaying.ShowLocation(false);
                nearest.ShowLocation(true); // y a esa le dice mostrate.
                currentPlaying = nearest;
            }

        }
    }
}
