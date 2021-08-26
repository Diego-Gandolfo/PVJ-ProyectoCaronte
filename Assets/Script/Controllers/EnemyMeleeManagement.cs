using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeManagement : EnemyController
{
    #region Basic parameters
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    #endregion Basic parameters

    #region Animator settings
    private Animator animator;
    public bool hasAnimator;
    #endregion Animator settings

    #region Damage management
    private float timeToDamageAgain = 2.0f;
    private float currentTimeToDamage = 0.0f;
    public bool canDamage;
    #endregion Damage management

    // Start is called before the first frame update
    void Start()
    {
        RecognizePlayer();
        animator = GetComponentInChildren<Animator>();
        canDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();

        if (!canDamage)
        {
            currentTimeToDamage += Time.deltaTime;
            if (currentTimeToDamage >= timeToDamageAgain)
            {
                canDamage = true;
                currentTimeToDamage = 0.0f;
            }
        }
    }

    private void FollowPlayer()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) >= distance)
        {
            transform.LookAt(player.transform.position);
            this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            if (hasAnimator) animator.SetBool("Walk Forward", true);
        }
    }

    public override void AttackPlayer()
    {
        base.AttackPlayer();
    }
}
