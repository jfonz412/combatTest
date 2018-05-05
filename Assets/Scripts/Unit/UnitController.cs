using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour
{
    [HideInInspector]
    public float baseSpeed; //who needs to see this?

    private UnitAnimController anim;
    private Rigidbody2D rb;
    private SpriteRenderer sp;

    private IEnumerator followingPath;
    private Vector3[] path;
    private int targetIndex;

    private float currentMoveSpeed;

    void Start()
    {
        anim = GetComponent<UnitAnimController>();
        rb = GetComponent<Rigidbody2D>();
        sp = transform.GetChild(0).GetComponent<SpriteRenderer>();
        baseSpeed = GetComponent<Stats>().speed; //down the line make this a callback where everytime speed is modified this will update as well
        SetDepth();
    }

    void Update()
    {
        SetDepth();
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            currentMoveSpeed = baseSpeed;
            targetIndex = 0;
            path = newPath;

            if (followingPath != null)
            {
                StopCoroutine(followingPath);
                followingPath = FollowPath();
                StartCoroutine(followingPath);
            }
            else
            {
                StopCoroutine("FollowPath");
                followingPath = FollowPath();
                StartCoroutine(followingPath);
            }
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        anim.FaceDirection(transform.position, currentWaypoint);
        anim.Walking(true);

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    path = null;
                    targetIndex = 0;

                    //anim.ToggleMovingAnimation(false);
                    anim.Walking(false);

                    IsKinematic(false);
                    yield break;
                }
                currentWaypoint = path[targetIndex];
                anim.FaceDirection(transform.position, currentWaypoint);
            }

            if (!rb.isKinematic)
            {
                IsKinematic(true);
            }
       
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, currentMoveSpeed * Time.deltaTime);
            yield return null;
        }
    }


    //used by other scripts to stop the pathfinding on command
    //may need more cleanup (like setting path = null, see reaching the end of path in FollowPath()
    public void StopMoving()
    {
        if (CurrentNode().walkable)
        {
            //currentMoveSpeed = 0;
            if (followingPath != null)
            {
                StopCoroutine(followingPath);
            }
            else
            {
                StopCoroutine("FollowPath");              
            }

            path = null;
            targetIndex = 0;

            anim.Walking(false);
            IsKinematic(false);
        }
    }

    Node CurrentNode()
    {
        return Grid.instance.NodeAtWorldPosition(transform.position);
    }

    //diabling for now...if a unit gets stuck, figure out a way to alert UnitController and then turn OFF Kinematic AND turn OFF Trigger
    void IsKinematic(bool toggleOn)
    {
        if (toggleOn)
        {
            //rb.isKinematic = true; 
        }
        else
        {
            //rb.isKinematic = false;
        }
    }

    void SetDepth()
    {
        sp.sortingOrder = (int)Mathf.RoundToInt(-transform.position.y * 1000);
    }

    /*
    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one / 2);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
    */
}

