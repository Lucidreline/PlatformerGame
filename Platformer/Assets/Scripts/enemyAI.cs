using Pathfinding;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class enemyAI : MonoBehaviour
{
    //what to chase
    [SerializeField] Transform target;

    //how many times a second we will update path
    [SerializeField] float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb;

    //calculatedPath
    [SerializeField] Path path;

    //AI Speed per second
    [SerializeField] float speed = 300f;

    //way to control how force is appied to rigidBody
    [SerializeField] ForceMode2D fmode;

    [HideInInspector]
    public bool pathIsEnded = false;

    //max distance from AI to waypoint for it to continue to next waypoint
    public float nextWayPointDistance = 3;

    //waypoint we are currently moving towards
    private int currentWayPoint = 0;

    void Start() {
        seeker = GetComponent<Seeker>();
        rb     = GetComponent<Rigidbody2D>();

        if (target == null) {
            Debug.LogError("THERE IS NOOOO TARGET! PANIC!");
            return;
        }
        //Start a new path and return results in the function onPathComplete
        seeker.StartPath(transform.position, target.position, onPathComplete);

        StartCoroutine(UpdatePath());

    }

    public void onPathComplete(Path p) {
        Debug.Log("WE GOT A PATH MATE! error? " + p.error);
        if (!p.error) {
            path = p;
            currentWayPoint = 0; //0 so we dont start in the middle or end of the path
        }
    }

    IEnumerator UpdatePath() {
        if (target == null) {
            Debug.Log("NEED TARGET!");
        }
            seeker.StartPath(transform.position, target.position, onPathComplete);
        yield return new WaitForSeconds(1 / updateRate);
        StartCoroutine(UpdatePath());
    }

    void FixedUpdate() {
        if(target == null) {
                return;
        }
            
        if (path == null) {
            return;
        }
        if(currentWayPoint >= path.vectorPath.Count) {
            if (pathIsEnded) {
                return;
            }
            Debug.Log("END OF PATH REACH");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        //Direction to next waypoint
        Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //now we FINALLY move the AI
        rb.AddForce(dir, fmode);
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);
        if (dist < nextWayPointDistance) {
            currentWayPoint++;
            return;
        }

    }
}
