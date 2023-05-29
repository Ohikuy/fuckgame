using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }
    public override void LoginUpdate()
    {
        
    }



    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        
    }
}
