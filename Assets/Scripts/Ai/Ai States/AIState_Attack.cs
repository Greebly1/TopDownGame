using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Set the looktarget, then fire at the target if it is in line of sight
/// </summary>
public class AIState_Attack : IState
{
    AiController controller;
    AiSight senses;
    int _stateID;
    Targeter moveTargeter;
    public float minDistanceToTarget = 5;
    public float maxDistanceToTarget = 10;
    float minwait;
    float maxwait;
    float waitTimer;

    public int StateID()
    {
        return _stateID;
    }

    public void OnBegin()
    {
        Debug.LogWarning("I AM ATTACKING NOW");
        controller.possessedPawn.lookTarget.setTarget(PlayerController.ClientPlayerController.possessedPawn);


        moveTargeter.transform.position = randPosAroundTarget();
    }

    public void OnEnd()
    {
        if (controller.possessedPawn == null)  {return; }
        controller.possessedPawn.lookTarget.setFollow(false);
        controller.possessedPawn.lookAtTarget = false;
    }

    public void Tick()
    {
        if (senses == null) { return; }

        //Shooting/sprinting logic
        if (senses.canSee(PlayerController.ClientPlayerController.possessedPawn))
        { //The ai sees the player
            
            isAttacking = true;
            
        } else
        { //the ai does not see the player

            isAttacking = false;
        }


        if (controller.isAtTarget)
        {
            //start a random wait timer
            moveTargeter.transform.position = randPosAroundTarget();
        }
    }



    public AIState_Attack(int ID, AiController Controller, AiSight Senses, Targeter moveTargeter, float MinDistance, float MaxDistance)
    {
        controller = Controller;
        _stateID = ID;
        this.moveTargeter = moveTargeter;
        this.minDistanceToTarget = MinDistance;
        this.maxDistanceToTarget = MaxDistance;
        senses = Senses;
    }

    Vector3 randPosAroundTarget()
    {
        Vector3 targetPos;

        if (PlayerController.ClientPlayerController.possessedPawn != null)
        {
            targetPos = PlayerController.ClientPlayerController.possessedPawn.transform.position;
        } else
        {
            targetPos = new Vector3(0, 0, 0);
        }

        int MAX_ITERATIONS = 100;
        int ITERATIONS = 0;
        while (true && ITERATIONS < MAX_ITERATIONS)
        {
            //Generate a random direction
            Vector2 dir = Vector2.zero.Randomize();

            float randDistance = Random.Range(minDistanceToTarget, maxDistanceToTarget);
            
            Vector3 randPosition = targetPos + (new Vector3(dir.x, 0, dir.y) * randDistance);

            var agent = controller.navAgent;
            var path = new NavMeshPath();
            agent.CalculatePath(randPosition, path);
            switch (path.status)
            {
                case NavMeshPathStatus.PathComplete:
                    return randPosition;
                case NavMeshPathStatus.PathPartial:
                    break;
                default:
                    break;
            }

            ITERATIONS++;
        }

        return new Vector3(0, 0, 0);
    }


    bool _isAttacking = false;
    bool isAttacking
    {
        get { return _isAttacking; }
        set
        {
            if (_isAttacking != value)
            {
                if (value)
                {
                    if ((controller.possessedPawn as HumanoidPawn).Sprinting == true)
                    {
                        (controller.possessedPawn as HumanoidPawn).toggleSprint(false);
                    }
                    controller.possessedPawn.lookAtTarget = true;
                    (controller.possessedPawn as HumanoidPawn).PrimaryAction(true);
                } else
                {
                    (controller.possessedPawn as HumanoidPawn).PrimaryAction(false);
                    controller.possessedPawn.lookAtTarget = false;
                    if ((controller.possessedPawn as HumanoidPawn).Sprinting == false)
                    {
                        (controller.possessedPawn as HumanoidPawn).toggleSprint(true);
                    }
                }
            }

            _isAttacking = value;
        }
    }
}
