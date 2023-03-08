using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class NavDestination : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navAgent;

    private Queue<NavTarget> agentTargets;
    private NavTarget currentTarget;

    public delegate void MoveDelegate();
    public delegate void StandStillDelegate();
    public delegate void ReachedDestinationDelegate();
    public event MoveDelegate MoveActions;
    public event StandStillDelegate StandStillActions;
    public event ReachedDestinationDelegate ReachedDestinationActions;


    void Awake() => agentTargets = new Queue<NavTarget>();
    private void OnTriggerEnter(Collider other)
    {
        if (currentTarget == null)
        {
            return;
        }
        if (other.TryGetComponent(out NavMeshAgent otherAgent) && otherAgent == navAgent)
        {
            ReachedDestinationActions?.Invoke();
            currentTarget.Actions?.Invoke();

            MoveToNextTarget();
        }
    }
    public void MoveToNextTarget()
    {
        if (agentTargets.Count > 0)
        {
            currentTarget = agentTargets.Dequeue();

            transform.position = currentTarget.Position;
            navAgent.SetDestination(currentTarget.Position);

            MoveActions?.Invoke();
            return;
        }

        StandStillActions?.Invoke();
    }
    public void AddTargetPosition(Vector3 position, ReachedDestinationDelegate whenReachedDestination = null)
    {
        agentTargets.Enqueue(new NavTarget(position, whenReachedDestination));
        if (currentTarget == null)
        {
            MoveToNextTarget();
        }
    }
    public void ClearTargets()
    {
        if (agentTargets == null)
        {
            return;
        }
        navAgent.SetDestination(navAgent.transform.position);
        currentTarget = null;
        agentTargets.Clear();
    }

    public class NavTarget
    {
        public Vector3 Position;
        public ReachedDestinationDelegate Actions;

        public NavTarget(Vector3 position, ReachedDestinationDelegate actions)
        {
            Position = position;
            Actions = actions;
        }
    }
}
