using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour
{
    private UnitAnimController anim;
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private CombatSkills skills;
    private Brain myBrain;
    private IEnumerator followingPath;
    private Vector3[] path;
    private int targetIndex;

    private float currentMoveSpeed;

    private Brain.State[] impairingStates = new Brain.State[] 
    {
        Brain.State.Downed,
        Brain.State.Rocked,
        Brain.State.Shock,
        Brain.State.Unconscious,
        Brain.State.Vomitting,
        Brain.State.Dead,
        Brain.State.CantBreathe
    };

    void Start()
    {
        anim = GetComponent<UnitAnimController>();
        myBrain = GetComponent<Brain>();
        rb = GetComponent<Rigidbody2D>();
        sp = transform.GetChild(0).GetComponent<SpriteRenderer>();
        skills = GetComponent<CombatSkills>(); //down the line make this a callback where everytime speed is modified this will update as well
        SetDepth();
    }

    void Update()
    {
        SetDepth();
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful && !Impaired())
        {
            currentMoveSpeed = skills.GetCurrentMoveSpeed();
            //Debug.Log("movespeed is " + currentMoveSpeed);
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

        while (true && !Impaired()) //expensive but it might take care of all movement stoppage
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    Stop();
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

        Stop(); //if we are knocked out of while loop by Impaired, we must stop movement
    }


    //used by other scripts to stop the pathfinding on command
    //may need more cleanup 
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

            Stop();
        }
    }

    private void Stop()
    {
        path = null;
        targetIndex = 0;

        anim.Walking(false);
        IsKinematic(false);
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

    private void SetDepth()
    {
        sp.sortingOrder = (int)Mathf.RoundToInt(-transform.position.y * 1000);
    }

    private bool Impaired()
    {
        if (myBrain.ActiveStates(impairingStates))
        {
            return true;
        }
        else
        {
            return false;
        }
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

