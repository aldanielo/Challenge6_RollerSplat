using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody ballRb;
    public float speed = 15f;

    private bool isTraveling;
    private Vector3 travelDirection;
    private Vector3 nextCollissionPosition;

    public int minSwipeRecognition = 500;
    private Vector2 swipePosLastFrame;
    private Vector2 swipePosCurrentFrame;
    private Vector2 currentSwipe;

    private Color solveColor;
    
    public AudioSource gameSound;
    public AudioClip ballSwipe;


    private void Start()
    {
        solveColor = Random.ColorHSV(0.5f, 1);
        GetComponent<MeshRenderer>().material.color = solveColor;
    }

    private void FixedUpdate()
    {
        if (isTraveling)
        {
            ballRb.velocity = travelDirection * speed;
        }

        Collider[] hitCollider = Physics.OverlapSphere(transform.position - (Vector3.up / 2), 0.05f);
        int i = 0;
        while (i < hitCollider.Length)
        {
            GroundPiece ground = hitCollider[i].transform.GetComponent<GroundPiece>();
            if(ground && !ground.isColored)
            {
                
                ground.ChangeColor(solveColor);
                
            }
            i++;
        }


        if (nextCollissionPosition != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, nextCollissionPosition) < 1)
            {
                isTraveling = false;
                travelDirection = Vector3.zero;
                nextCollissionPosition = Vector3.zero;
                
            }

        }

        if (isTraveling)
        {
            gameSound.PlayOneShot(ballSwipe);
            return;
        }

        if (Input.GetMouseButton(0))
        {
            swipePosCurrentFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if(swipePosLastFrame != Vector2.zero)
            {
                currentSwipe = swipePosCurrentFrame - swipePosLastFrame;
                //ballSwipe

                if(currentSwipe.sqrMagnitude < minSwipeRecognition)
                {
                    return;
                }

                currentSwipe.Normalize();

                // UP/Down
                if(currentSwipe.x > -0.5f && currentSwipe.x < 0.5)
                {
                    // GO UP/DOWN
                    SetDestination(currentSwipe.y > 0 ? Vector3.forward : Vector3.back);
                }

                // LEDT/RIGHT
                if (currentSwipe.y > -0.5f && currentSwipe.y < 0.5)
                {
                    // GO LEFT/RIGHT
                    SetDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                }
            }

            swipePosLastFrame = swipePosCurrentFrame;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipePosLastFrame = Vector2.zero;
            currentSwipe = Vector2.zero;
        }
    }

    private void SetDestination(Vector3 direction)
    {
        travelDirection = direction;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit, 100f))
        {
            nextCollissionPosition = hit.point;
        }

        isTraveling = true;
    }
}
