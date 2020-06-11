using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager2 : EnemySight
{
    [SerializeField] Transform bullet;
    // Update is called once per frame
    void Update()
    {
        playerInSight = inFOV(transform, target, FOVAngle, FOVRadius); 
        float distance = Vector3.Distance(target.position, transform.position);

        if ((distance <= FOVRadius) && playerInSight)
        {
            //Face the target then
            FaceTarget();
            
            //Shoot
            StartCoroutine(Shoot());
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position-transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1f);
    }

    IEnumerator Shoot()
    {
        //Instantiate a bullet
        yield return new WaitForSeconds(5);
        Transform tmpbullet = Instantiate(bullet, transform.position+(transform.forward*2) , Quaternion.identity);
        Vector3 direction = (target.position-transform.position).normalized;
        tmpbullet.GetComponent<Bullet>().Setup(direction);
        yield return new WaitForSeconds(5);
    }
}
