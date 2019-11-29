using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTouch : MonoBehaviour
{

    public float moveSpeed;
    public Camera cam;

    private Animator anim;
    private Rigidbody2D myRigidBody;

    private float maxWidth;
    private Vector3 targetWidth;

    private float currentMoveSpeed;

    private Vector2 touchSide;

    // Start is called before the first frame update
    void Start()
    {
        //Get components for the animator and rigidbody
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();

        //Find the edges of the screen and store to (-maxWidth, maxWidth)
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        targetWidth = cam.ScreenToWorldPoint(upperCorner);
        maxWidth = targetWidth.x;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            //Reset the player move speed this frame. Move speed is a factor of the screensize
            currentMoveSpeed = moveSpeed * targetWidth.x;

            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0.0f;

            if (touchPosition.x < 0.0f)
            {
                touchSide = new Vector2(-1.0f, 0.0f);
            }

            if (touchPosition.x > 0.0f)
            {
                touchSide = new Vector2(1.0f, 0.0f);
            }

            //Check if player is outside the bounds (left)
            if (myRigidBody.transform.position.x < -maxWidth)
            {
                //If he's out of bounds, set speed to stop from moving
                currentMoveSpeed = 0;
                //Push player back slightly so we don't get stuck on the edge
                myRigidBody.transform.position = new Vector2(-maxWidth + 0.01f, myRigidBody.position.y);
            }

            //Check if player is outside the bounds (right)
            if (myRigidBody.transform.position.x > maxWidth)
            {
                currentMoveSpeed = 0;
                myRigidBody.transform.position = new Vector2(maxWidth - 0.01f, myRigidBody.position.y);
            }

            myRigidBody.velocity = new Vector2(touchSide.x * currentMoveSpeed, myRigidBody.velocity.y);

        }
        else
        {
            touchSide.x = 0.0f;
            myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
        }

        //Tell the animator which way we're inputting
        anim.SetFloat("MoveX", touchSide.x);
    }
}
