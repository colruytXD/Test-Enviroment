using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    private NavMeshAgent enemyNavMesh;
    private Transform myTransform;
    private RaycastHit hit;
    private Collider[] hitColliders;
    private Vector3 lastSeenPosition = new Vector3();

    public float checkRadius;
    public LayerMask checkLayer;
    
    public float checkRate;
    private float nextCheck;

	void OnEnable() 
	{
		SetInitialReferences();
	}

	void OnDisable() 
	{

	}
	
	void Update () 
	{
        DetectEnemy();
	}

	void SetInitialReferences() 
	{
        enemyNavMesh = gameObject.GetComponent<NavMeshAgent>();
        myTransform = gameObject.transform;
	}

    void DetectEnemy()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;

            hitColliders = Physics.OverlapSphere(myTransform.position, checkRadius, checkLayer);

            if (hitColliders.Length > 0)
            {
                MoveTowardsEnemy(hitColliders);
            }
            else
            {
                Wander();
            }
        }
    }

    void MoveTowardsEnemy(Collider[] hitColliders)
    {
        enemyNavMesh.SetDestination(hitColliders[0].transform.position);
        lastSeenPosition = hitColliders[0].transform.position;
    }

    void Wander()
    {
        Vector3 randomWanderLocation = new Vector3();

        randomWanderLocation = Random.insideUnitSphere * 50 + lastSeenPosition;
        enemyNavMesh.SetDestination(randomWanderLocation);
    }
}
