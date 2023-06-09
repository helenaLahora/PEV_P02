using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking_NPC3 : StateMachineBehaviour
{
    Transform _player;
    Transform _enemy;

    float _timer;

    Transform CastPoint;

    [SerializeField]
    public LayerMask WhatIsGround;

    [SerializeField]
    float Speed = 30f;

    public float maxDistToGround = 10f;

    public float DetectionJump = 10;
    public float DetectionDmg = 10;
    public float WaitTime = 3;


    //______________________________________________________________________________________________________________________________________________________________//


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _enemy = GameObject.FindGameObjectWithTag("EnemyWay").transform;

        CastPoint = GameObject.FindGameObjectWithTag("Castpoints").transform;

        _timer = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Execute(animator);
        CheckTriggers(animator);
    }

    //______________________________________________________________________________________________________________________________________________________________//

    private void Execute(Animator animator)
    {
        _timer += Time.deltaTime;

        if (EdgeDetected())
        {
            Rotate();
        }
        else
        {
            Move();
        }
    }

    private void CheckTriggers(Animator animator)
    {
        if (IsPlayerClose(_player, animator.transform))
        {
            animator.SetBool("Jump", true);
        }
        

        bool timeUp = IsTimeUp();
        animator.SetBool("Walk", !timeUp); //invertim el valor de patrolling a false quan el temps de wander s'ha acabat

        if (Vector3.Distance(_player.position, _enemy.position) < DetectionDmg) 
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) //get mouse button down 0 = boto esq
            {
                animator.SetBool("Damage", true);
            }
        }
    }

    private bool IsPlayerClose(Transform player, Transform mySelf)
    {
        return Vector3.Distance(player.position, mySelf.position) < DetectionJump;
    }


    private bool IsTimeUp()
    {
        return _timer > WaitTime;
    }

    //______________________________________________________________________________________________________________________________________________________________//

    private void Move()
    {
        _enemy.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    private void Rotate()
    {
        float rot = UnityEngine.Random.Range(90, 270);
        _enemy.transform.Rotate(new Vector3(0, rot, 0));
    }

    private bool EdgeDetected()
    {
        if (Physics.Raycast(CastPoint.position, Vector3.down, maxDistToGround, WhatIsGround))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
