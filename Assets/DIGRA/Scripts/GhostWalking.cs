using UnityEngine;
using UnityEngine.AI;

public class Wanderer : MonoBehaviour
{
    private NavMeshAgent agent;
    public float wanderRadius = 10f;
    public float pointReachDistance = 0.5f;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        GoToRandomPoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < pointReachDistance)
        {
            GoToRandomPoint();
        }
    }

    void GoToRandomPoint()
    {
        Vector3 randomPos = Random.insideUnitSphere * wanderRadius + transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}