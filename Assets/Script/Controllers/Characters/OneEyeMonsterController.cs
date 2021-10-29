using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneEyeMonsterController : EnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Mathf.Sin(0) * 2, 0) * _actorStats.OriginalSpeed * Time.deltaTime;
    }
}
