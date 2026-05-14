using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AINavigation : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkRadius = 6f;   // Slightly larger radius to explore
    [SerializeField] private float minWaitTime = 2f;   // Less waiting so they move more
    [SerializeField] private float maxWaitTime = 5f;
    [SerializeField] private float movementSpeed = 0.8f; // Super slow, cozy stroll

    [Header("Rotation Settings")]
    [SerializeField] private float turnSpeed = 120f;   // How fast they spin in place
    [SerializeField] private float faceTargetAngle = 20f; // Must be within 20 degrees of target before stepping forward

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        // Apply speeds
        agent.speed = movementSpeed; 
        agent.angularSpeed = turnSpeed;

        // CRITICAL: We uncheck this because we want to manually control when they step forward
        agent.updatePosition = false; 
        
        StartCoroutine(LivetoBeAliveRoutine());
    }

    private IEnumerator LivetoBeAliveRoutine()
    {
        while (true) 
        {
            float randomWait = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(randomWait);

            // Increased to a 75% chance to take a walk so they move around much more frequently
            if (Random.value < 0.75f)
            {
                Vector3 targetPos = GetNearbyLocation();
                agent.SetDestination(targetPos);

                // Wait a split second for the agent to calculate its path
                yield return new WaitUntil(() => !agent.pathPending);

                // --- THE FORWARD-ONLY MOVEMENT LOGIC ---
                while (agent.remainingDistance > agent.stoppingDistance)
                {
                    // 1. Calculate the direction to the next corner on the path
                    Vector3 nextWaypoint = agent.steeringTarget;
                    Vector3 directionToTarget = (nextWaypoint - transform.position).normalized;
                    directionToTarget.y = 0; // Keep it on a flat plane

                    if (directionToTarget != Vector3.zero)
                    {
                        // 2. Rotate toward the target point smoothly
                        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
                        
                        // 3. FIXED: Using Mathf.Abs to prevent negative/inverted angle math confusion
                        // NOTE: If your 3D model is still walking backwards, change 'transform.forward' to '-transform.forward' right below!
                        float angleDifference = Mathf.Abs(Vector3.Angle(transform.forward, directionToTarget));

                        // 4. ONLY move forward if we are facing the target! No backing up or sliding sideways.
                        if (angleDifference <= faceTargetAngle)
                        {
                            // Manually pull the character forward along the NavMesh path
                            transform.position = Vector3.MoveTowards(transform.position, agent.nextPosition, movementSpeed * Time.deltaTime);
                        }
                    }

                    // Force the agent's internal simulation to stay glued to our manual position
                    agent.nextPosition = transform.position;

                    yield return null; 
                }
            }
        }
    }

    private Vector3 GetNearbyLocation()
    {
        Vector3 randomPoint = transform.position + (Random.insideUnitSphere * walkRadius);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, walkRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkRadius);
    }
}