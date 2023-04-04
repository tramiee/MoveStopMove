using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    [SerializeField] Transform minPoint, maxPoint;
    public int botInTable = 5;
    public int botTotal = 20;

    public Vector3 RandomPoint()
    {
        Vector3 randomPoint = Random.Range(minPoint.position.x, maxPoint.position.x) * Vector3.right + Random.Range(minPoint.position.z, maxPoint.position.z) * Vector3.forward;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, float.PositiveInfinity, 1);
        return hit.position;
    }
}
