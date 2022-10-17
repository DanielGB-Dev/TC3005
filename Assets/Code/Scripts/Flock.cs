using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    internal FlockController controller;

    private new Rigidbody rigidbody;

    private Vector3 center;
    private Vector3 follow;
    private Vector3 randomize;

    public float rangeOfVision;

    public bool evade;
    private Vector3 evationPoint;

    private Ray ray_middle;
    private Ray ray_r;
    private Ray ray_l;
    private Ray ray_u;
    private Ray ray_d;

    private Vector3 angle_middle;
    private Vector3 angle_r;
    private Vector3 angle_l;
    private Vector3 angle_u;
    private Vector3 angle_d;

    private bool hit_middle;
    private bool hit_r;
    private bool hit_l;
    private bool hit_u;
    private bool hit_d;
    
    private float ray_angle;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        evade = false;
        ray_angle = 20f;
    }

    void Update()
    {
        GoToEvation(5f);

        if (!evade)
        {
            transform.LookAt(controller.target);
        }
        else
        {
            transform.LookAt(evationPoint);
        }

        CreateRays(ray_angle, rangeOfVision);
        CheckForHit();
    }

    void FixedUpdate()
    {
        if (controller)
        {
            Vector3 relativePos = Steer() * Time.deltaTime;

                if(relativePos != Vector3.zero)
                    rigidbody.velocity = relativePos;

                float speed = rigidbody.velocity.magnitude;

                if (speed > controller.maxVelocity)
                    rigidbody.velocity = rigidbody.velocity.normalized * controller.maxVelocity;
                    
                else if (speed < controller.minVelocity) 
                    rigidbody.velocity = rigidbody.velocity.normalized * controller.minVelocity;
            
        }
    }

    void CreateRays(float angle, float distance)
    {
        angle_middle = transform.TransformDirection(Vector3.forward) * distance;
        angle_r = Quaternion.Euler( 0, angle, 0) * transform.forward * distance;
        angle_l = Quaternion.Euler( 0, -angle, 0) *  transform.forward * distance;
        angle_u = Quaternion.Euler( angle, 0, 0) *  transform.forward * distance;
        angle_d = Quaternion.Euler( -angle, 0, 0) *  transform.forward * distance;

        ray_middle = new Ray(transform.position, transform.forward * distance);
        ray_r = new Ray(transform.position, angle_r);
        ray_l = new Ray(transform.position, angle_l);
        ray_u = new Ray(transform.position, angle_u);
        ray_d = new Ray(transform.position, angle_d);

        RaycastHit hitData_middle;
        hit_middle = Physics.Raycast(ray_middle, out hitData_middle, distance);
        Debug.DrawRay(transform.position, angle_middle, hit_middle ? Color.red : Color.green);
        /*
        if (hit_middle)
        {
            Debug.Log(hitData_middle.distance);
        }
        */
        RaycastHit hitData_r;
        hit_r = Physics.Raycast(ray_r, out hitData_r, distance);
        Debug.DrawRay(transform.position, angle_r, hit_r ? Color.red : Color.green);

        RaycastHit hitData_l;
        hit_l = Physics.Raycast(ray_l, out hitData_l, rangeOfVision);
        Debug.DrawRay(transform.position, angle_l, hit_l ? Color.red : Color.green);

        RaycastHit hitData_u;
        hit_u = Physics.Raycast(ray_u, out hitData_u, rangeOfVision);
        Debug.DrawRay(transform.position, angle_u, hit_u ? Color.red : Color.green);

        RaycastHit hitData_d;
        hit_d = Physics.Raycast(ray_d, out hitData_d, rangeOfVision);
        Debug.DrawRay(transform.position, angle_d, hit_d ? Color.red : Color.green);
    }

    void CheckForHit()
    {
        if (hit_r || hit_l || hit_u || hit_d)
        {
            
            if (!hit_r)
            {
                evationPoint = ray_r.GetPoint(10);
                evade = true;  
            }
            else if (!hit_l)
            {
                evationPoint = ray_l.GetPoint(10);
                evade = true;
            }
            else if (!hit_u)
            {
                evationPoint = ray_u.GetPoint(10);
                evade = true;        
            }
            else if (!hit_d)
            {
                evationPoint = ray_d.GetPoint(10);
                evade = true;
            }
            else
            {
                ray_angle += 5f;
            }
            Debug.Log("Evade to: " + evationPoint.ToString());
        }
    }

    void GoToEvation(float radius)
    {
        if (evade)
        {
            if (Vector3.Distance(evationPoint, transform.localPosition) <= radius || 
                Vector3.Distance(evationPoint, transform.localPosition) >= radius * 2f)
            {
                evade =  false;
                ray_angle = 20f;
            }
        }
    }

    //Calculate flock steering Vector based on the Craig Reynold's algorithm (Cohesion, Alignment, Follow leader and Seperation)
    private Vector3 Steer() 
    { 
        Vector3 velocity = controller.flockVelocity - rigidbody.velocity;           // alignment
        center = controller.flockCenter - transform.localPosition;          // cohesion
        if (evade)
        {
            follow = evationPoint;
        }
        else
        {
            follow = controller.target.localPosition - transform.localPosition; // follow leader
        }
        
        Vector3 separation = Vector3.zero; // separation

        if (Vector3.Distance(controller.target.localPosition, transform.localPosition) <= controller.patrolRadius)
        {
            controller.currentTarget++;
            if (controller.currentTarget == controller.targetList.Count)
            {
                controller.currentTarget = 0;
            }
        }
        foreach (Flock flock in controller.flockList) 
        {
            if (flock != this) 
            {
                Vector3 relativePos = transform.localPosition - flock.transform.localPosition;
                separation += relativePos.normalized;
            }
        }


        // Randomize the direction every 2 seconds
        if(Time.time % 2==0)
        {
            randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);
            randomize.Normalize();
        }

        return (controller.centerWeight * center +
                controller.velocityWeight * velocity +
                controller.separationWeight * separation +
                controller.followWeight * follow +
                controller.randomizeWeight * randomize);
    }
}
