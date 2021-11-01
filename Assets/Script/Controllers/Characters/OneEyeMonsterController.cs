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
    private float maxDistanceToPlayer = 1.0f;

    private bool canAttack;
    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        canAttack = false;
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
            PlayerController player = LevelManager.instance.Player;
            FollowPlayer(player);
        } 
    }

    private void FollowPlayer(PlayerController player)
    {
        if (isHostile)
        {
            //canAttack = true;
            transform.LookAt(player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, _actorStats.OriginalSpeed * Time.deltaTime);
        }
    }
}
