using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneEyeMonsterGrowl : MonoBehaviour
{
    private float timeToReproduceSound = 5.0f;
    private float currentTimeToReproduceSound = 0.0f;

    private EnemyController oneEyeMonsterEnemy;

    // Start is called before the first frame update
    void Start()
    {
        currentTimeToReproduceSound = timeToReproduceSound;
        oneEyeMonsterEnemy = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            if (oneEyeMonsterEnemy.HasTakenDamage)
            {
                currentTimeToReproduceSound += Time.deltaTime;
                if (currentTimeToReproduceSound >= timeToReproduceSound)
                {
                    AudioManager.instance.PlaySound(SoundClips.MonsterGrowl);
                    currentTimeToReproduceSound = 0.0f;
                }
            }
        }
    }
}
