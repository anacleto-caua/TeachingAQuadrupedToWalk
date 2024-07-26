using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IndividualMovement : MonoBehaviour
{
    public Transform front_right_foot_ik;
    public Transform front_left_foot_ik;

    public Transform back_right_foot_ik;
    public Transform back_left_foot_ik;


    public Transform front_right_foot;
    public Transform front_left_foot;

    public Transform back_right_foot;
    public Transform back_left_foot;

    public Transform debugBall;

    public Transform bodyPivot;

    private float speed = .5f;

    private float frontFeetSpeed = .00f;
    private float backFeetSpeed = .3f;
    private float increase = 0.01f;
    private float cap = 0.3f;
    private bool frontUping = true;

    void Update()
    {
        if (frontUping)
        {
            frontFeetSpeed += increase;
            backFeetSpeed -= increase;
            if (frontFeetSpeed >= cap)
            {
                frontUping = false;
            }
        }
        else
        {
            frontFeetSpeed -= increase;
            backFeetSpeed += increase;
            if (backFeetSpeed >= cap)
            {
                frontUping = true;
            }
        }

        Debug.Log("aa F: " + frontFeetSpeed + " B: " + backFeetSpeed);

        front_right_foot_ik.position += front_right_foot_ik.forward * frontFeetSpeed * Time.deltaTime;
        front_left_foot_ik.position += front_left_foot_ik.forward * frontFeetSpeed * Time.deltaTime;

        back_right_foot_ik.position += back_right_foot_ik.forward * backFeetSpeed * Time.deltaTime;
        back_left_foot_ik.position += back_left_foot_ik.forward * backFeetSpeed * Time.deltaTime;

        Vector3 midpoint = front_right_foot_ik.position + front_left_foot_ik.position + back_right_foot_ik.position + back_left_foot_ik.position;
        midpoint /= 4;
        midpoint.y = bodyPivot.position.y;

        bodyPivot.position = midpoint;
    }

    float distanceFromFrontToBack()
    {
        Vector3 midpointfront = front_right_foot_ik.position + front_left_foot_ik.position / 2f;
        Vector3 midpointback = back_right_foot_ik.position + back_left_foot_ik.position / 2f;

        return Vector2.Distance(ignoreY(midpointfront), ignoreY(midpointback));
    }

    string directionToFall()
    {

        Vector3 midpointfront = front_right_foot_ik.position + front_left_foot_ik.position / 2f;
        Vector3 midpointback = back_right_foot_ik.position + back_left_foot_ik.position / 2f;

        if(Vector2.Distance(ignoreY(midpointfront), ignoreY(bodyPivot.position)) > Vector2.Distance(ignoreY(midpointback), ignoreY(bodyPivot.position)))
        {
            return "front";
        }
        return "back";
    }

    Vector2 ignoreY(Vector3 a)
    {
        return new Vector2(a.x, a.z);
    }
}
