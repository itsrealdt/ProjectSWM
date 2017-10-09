using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class BikeAnimation : MonoBehaviour
{


    protected Animator animator;


    public bool ikActive = false;
    public float RestTime = 5;

    public IKPointsClass IKPoints;

    public Transform myBike;
    public Transform player;
    public Transform eventPoint;

    public AudioSource crashSound;

   



    private Rigidbody bikeRigidbody;

    private BikeControl BikeScript;
    private Vector3 myPosition;
    private Quaternion myRotation;

    private float timer=0.0f;
    private float steer = 0.0f;
    private float speed = 0.0f;
    private float groundedTime = 0.0f;

    private bool grounded = true;


    

    [System.Serializable]
    public class IKPointsClass
    {
        public Transform rightHand, leftHand;
        public Transform rightFoot, leftFoot;
    }




    void Awake()
    {


        BikeScript = myBike.GetComponent<BikeControl>();
        animator = player.GetComponent<Animator>();

        myPosition = player.localPosition;
        myRotation = player.localRotation;
        DisableRagdoll(true);

        bikeRigidbody = myBike.GetComponent<Rigidbody>();

    }




    void Update()
    {

        Vector3 direction;

        if (timer!=0.0f)
        timer = Mathf.MoveTowards(timer, 0.0f, Time.deltaTime);



        if (BikeScript.grounded)
        {
            direction = eventPoint.TransformDirection(Vector3.forward);
        }
        else
        {
            direction = eventPoint.TransformDirection(0, -0.25f, 1);
        }



        Debug.DrawRay(eventPoint.position, direction, Color.red);


        RaycastHit hit;

        if (Physics.Raycast(eventPoint.position, direction, out hit, 1.0f) && BikeScript.speed > 50)
        {
            if (hit.collider.transform.root != transform.root)
            {
                if (player.parent != null)
                {
                    crashSound.GetComponent<AudioSource>().Play();
                    player.parent = null;
                }


                DisableRagdoll(true);
                player.GetComponent<Animator>().enabled = false;

                BikeScript.crash = true;
                timer = RestTime;
            }
        }





        if (timer == 0.0f)
        {

            player.GetComponent<Animator>().enabled = true;
            DisableRagdoll(false);

            player.parent = BikeScript.bikeSetting.MainBody.transform;

            player.localPosition = myPosition;
            player.localRotation = myRotation;

            if (BikeScript.crash)
            {
                bikeRigidbody.AddForce(Vector3.up * 10000);
                bikeRigidbody.MoveRotation(Quaternion.Euler(0, transform.eulerAngles.y, 0));
                BikeScript.crash = false;
            }



        }




        if (player.GetComponent<Animator>().enabled != true) return;



        if (BikeScript.speed > 50 && grounded)
        {
            steer = BikeScript.steer;
        }
        else
        {
            steer = Mathf.MoveTowards(steer, 0.0f, Time.deltaTime * 10.0f);


        }



        if (BikeScript.grounded)
        {
            grounded = true;
            groundedTime = 2.0f;
        }
        else
        {
            groundedTime = Mathf.MoveTowards(groundedTime, 0.0f, Time.deltaTime * 10.0f);

            if (groundedTime == 0)
                grounded = false;
        }





        if (BikeScript.currentGear > 0 || !BikeScript.Backward)
        {
            speed = BikeScript.speed;
        }
        else
        {
            speed = -BikeScript.speed;
        }



        animator.SetFloat("speed", speed);
        animator.SetFloat("right", steer);
        animator.SetBool("grounded", grounded);


    }



    void DisableRagdoll(bool active)
    {


        Component[] Rigidbodys = player.GetComponentsInChildren(typeof(Rigidbody));

        foreach (Rigidbody RigidbodyChild in Rigidbodys)
        {
            RigidbodyChild.isKinematic = !active;
        }


        Component[] Colliders = player.GetComponentsInChildren(typeof(Collider));

        foreach (Collider ColliderChild in Colliders)
        {
            ColliderChild.enabled = active;
        }

    }




    //a callback for calculating IK
    void OnAnimatorIK()
    {




        if (player.GetComponent<Animator>().enabled != true) return;




        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {


                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);



                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1.0f);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1.0f);




                if (IKPoints.leftHand != null)
                {
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, IKPoints.leftHand.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, IKPoints.leftHand.rotation);


                }


                if (speed > -1)
                {

                    //set the position and the rotation of the right hand where the external object is
                    if (IKPoints.rightHand != null)
                    {
                        animator.SetIKPosition(AvatarIKGoal.RightHand, IKPoints.rightHand.position);
                        animator.SetIKRotation(AvatarIKGoal.RightHand, IKPoints.rightHand.rotation);
                    }

                    if (IKPoints.rightFoot != null)
                    {
                        animator.SetIKPosition(AvatarIKGoal.RightFoot, IKPoints.rightFoot.position);
                        animator.SetIKRotation(AvatarIKGoal.RightFoot, IKPoints.rightFoot.rotation);
                    }

                    if (IKPoints.leftFoot != null && BikeScript.speed > 30.0f)
                    {

                        animator.SetIKPosition(AvatarIKGoal.LeftFoot, IKPoints.leftFoot.position);
                        animator.SetIKRotation(AvatarIKGoal.LeftFoot, IKPoints.leftFoot.rotation);
                    }


                }



            }

                //if the IK is not active, set the position and rotation of the hand back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }
        }
    }
}