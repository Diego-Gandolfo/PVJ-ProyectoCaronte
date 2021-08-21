using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        RecognizePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
}
