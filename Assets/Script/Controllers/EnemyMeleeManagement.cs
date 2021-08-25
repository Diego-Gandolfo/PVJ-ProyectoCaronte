using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeManagement : EnemyController
{
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        RecognizePlayer();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) >= distance)
        {
            transform.LookAt(player.transform.position);
            this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            animator.SetBool("Walk Forward", true);
        }
    }
}
