﻿using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour
{
    UnitAnimator anim;
    Rigidbody2D rb;

    IEnumerator followingPath;
    Vector3[] path;
    int targetIndex;

    float movementSpeed = 2f; //standard for npcs, make this public?


    void Start()
    {
        anim = GetComponent<UnitAnimator>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            movementSpeed = 2f;
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
        anim.ToggleMovingAnimation(true);

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    path = null;
                    targetIndex = 0;
                    anim.ToggleMovingAnimation(false);
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
            
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, movementSpeed * Time.deltaTime);
            yield return null;
        }

    }

    //used by other scripts to stop the pathfinding on command
    //may need more cleanup (see reaching the end of path in FollowPath()
    public void StopMoving()
    {
        if (CurrentNode().walkable)
        {
            //movementSpeed = 0;
            if (followingPath != null)
            {
                StopCoroutine(followingPath);
            }
            else
            {
                StopCoroutine("FollowPath");              
            }
            anim.ToggleMovingAnimation(false);
            IsKinematic(false);
        }
    }

    Node CurrentNode()
    {
        return Grid.instance.NodeAtWorldPosition(transform.position);
    }

    void IsKinematic(bool toggleOn)
    {
        if (toggleOn)
        {
            rb.isKinematic = true; 
        }
        else
        {
            rb.isKinematic = false;
        }
    }

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
}

