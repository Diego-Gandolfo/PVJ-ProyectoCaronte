using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneEyeMonsterController : EnemyController
{
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform[] randomSpots;
    [SerializeField] private float timeToShoot;
    private int currentRandomSpot;

    private float minDistance = 0.2f;
    private float idleTime = 2.0f;
    private float currentIdleTime = 0.0f;
    
    private float currentTimeToShoot;

    private bool canDiscountTimeToShoot;

    // Start is called before the first frame update
    void Start()
    {
        canDiscountTimeToShoot = false;
        currentTimeToShoot = timeToShoot;
        currentIdleTime = idleTime;
        currentRandomSpot = Random.Range(0, randomSpots.Length);    
    }

    // Update is called once per frame
    void Update()
    {
        CheckVisibleData();

        transform.position = Vector3.MoveTowards(transform.position, randomSpots[currentRandomSpot].position, _actorStats.OriginalSpeed * Time.deltaTime);

        if (!isHostile)
        {
            canDiscountTimeToShoot = false;

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

            canDiscountTimeToShoot = true;
            if (canDiscountTimeToShoot)
            {
                currentTimeToShoot += Time.deltaTime;
                if (currentTimeToShoot >= timeToShoot)
                {
                    AttackPlayer();
                }
            }
        } 
    }

    private void FollowPlayer(PlayerController player)
    {
        if (isHostile)
        {
            transform.LookAt(player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, _actorStats.OriginalSpeed * Time.deltaTime);
        }
    }

    private void CheckVisibleData()
    {
        if (!HealthController.IsDead)
        {
            if (isHostile)
            {
                outline.enabled = true;
            }
            else
            {
                outline.enabled = false;
            }

            if (HealthController.CurrentHealth != HealthController.MaxHealth)
            {
                lifeBar.SetBarVisible(isHostile);
            }
        }
    }

    private void AttackPlayer()
    {
        if (isHostile)
        {
            Instantiate(enemyBullet, firePoint.position, Quaternion.LookRotation(firePoint.position));
        }
        currentTimeToShoot = 0.0f;
    }
}
