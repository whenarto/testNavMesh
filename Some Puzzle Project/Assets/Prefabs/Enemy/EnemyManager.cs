using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : EnemySight
{
    // Update is called once per frame
    void Update()
    {
        playerInSight = inFOV(transform, target, FOVAngle, FOVRadius); 
        float distance = Vector3.Distance(target.position, transform.position);

        if ((distance <= FOVRadius) && playerInSight)
        {
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position-transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
