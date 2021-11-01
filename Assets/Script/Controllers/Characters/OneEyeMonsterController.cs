using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneEyeMonsterController : EnemyController
{
    [SerializeField] private Transform[] randomSpots;
    private int currentRandomSpot;

    private float minDistance = 0.2f;
    private float idleTime = 2.0f;
    private float currentIdleTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentIdleTime = idleTime;
        currentRandomSpot = Random.Range(0, randomSpots.Length);    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, randomSpots[currentRandomSpot].position, _actorStats.OriginalSpeed * Time.deltaTime);

        if (!isHostile)
        {
            if (Vector2.Distance(transform.position, randomSpots[currentRandomSpot].position) < minDistance)
            {
                if (idleTime <= 0)
                {
                    currentRandomSpot = Random.Range(0, randomSpots.Length);
                    idleTime = currentIdleTime;
                }
            
                else idleTime -= Time.deltaTime;
            }
        }
        else
        {
            print("enemy has become hostile ! ! !");
        } 

    }

    private void FollowPlayer(PlayerController player)
    {
        transform.LookAt(player.transform.position);
    }
}
