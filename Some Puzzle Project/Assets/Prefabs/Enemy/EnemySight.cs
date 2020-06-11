using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemySight : MonoBehaviour
{
    [Header("Field of View")]
    public float FOVAngle;
    public float FOVRadius;

    [Space(10)]
    public bool playerInSight;

    NavMeshAgent agent;
    //SphereCollider col;
    GameObject player;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameManager.Instance.getCurrentPlayer;
        target = player.transform;
    }

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

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, FOVRadius);

        Vector3 FOVline1 = Quaternion.AngleAxis(FOVAngle, transform.up) * transform.forward * FOVRadius;
        Vector3 FOVline2 = Quaternion.AngleAxis(-FOVAngle, transform.up) * transform.forward * FOVRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, FOVline1);
        Gizmos.DrawRay(transform.position, FOVline2);

        if(playerInSight)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, (target.position-transform.position).normalized*FOVRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward*FOVRadius);
    }

    bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {
        Collider[] overlaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, FOVRadius, overlaps);
        for(int i = 0; i< count;i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == target)
                {
                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;
                    float angle = Vector3.Angle(checkingObject.forward, directionBetween);
                    if (angle <= maxAngle)
                    {
                        Ray ray = new Ray (checkingObject.position, target.position-checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius))
                        {
                            if (hit.transform == target)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
}
