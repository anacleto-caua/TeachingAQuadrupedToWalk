using System.Collections.Generic;
using UnityEngine;

public class IndividualMovement : MonoBehaviour
{

    public class Leg
    {
        public Transform LegIk;

        public List<Vector3> dirs;

        public float speed = 30f;
 
        // sweet move vars
        public float xSpeed = .1f;
        public float ySpeed = .3f;
        public float pico = 3;
        public Vector3 oldPosition;
        public bool isIntoSweetMove = false;

        public int step = 0;

        public Leg(Transform LegIk)
        {
            this.LegIk = LegIk;
        }

        public Leg(Transform LegIk, List<Vector3> dirs, float speed)
        {
            this.LegIk = LegIk;
            this.speed = speed;
            this.oldPosition = Vector3.zero;
            AddDirs(dirs);
        }

        public void AddDirs(List<Vector3> dirs)
        {
            this.dirs = dirs;
            NormalizeDirs();
        }

        public void NormalizeDirs()
        {
            foreach (var dir in dirs)
            {
                dir.Normalize();
            }
        }

        public void Move()
        {
            LegIk.position += dirs[step] * speed;
            step++;
            if(step >= dirs.Count)
            {
                step = 0;
            }
        }

        public void SweetMove()
        {
            if (!isIntoSweetMove && oldPosition == LegIk.position)
            {
                // Started another sweet move
                isIntoSweetMove = true;
                oldPosition = LegIk.position;
            }
            else if(isIntoSweetMove && oldPosition != LegIk.position && LegIk.position.y == 0)
            {
                // Just finished this sweet move
                isIntoSweetMove = false;
                return;
            }

            /**
             * i got get it from here, basically it wont move in the dirs direction, and wont reverse the y movement to keep the up and down.
             */

            Vector3 deslocamento = new Vector3(xSpeed, ySpeed, 0) * Time.deltaTime;

            if (LegIk.position.y + deslocamento.y > pico && ySpeed > 0)
            {
                deslocamento.y = pico - LegIk.position.y;
            }
            else if (LegIk.position.y + deslocamento.y < 0)
            {
                deslocamento.y = 0 - LegIk.position.y;
            }

            LegIk.position += deslocamento;

            // Muda o sinal de y se chegou no ponto mínimo ou no ponto máximo
            if (LegIk.position.y == 0 && ySpeed <= 0)
            {
                ySpeed *= -1;
            }
            else if (LegIk.position.y == pico && ySpeed >= 0)
            {
                ySpeed *= -1;
            }
        }
    }

    public Transform fr_ik;
    public Transform fl_ik;
    public Transform br_ik;
    public Transform bl_ik;

    public Transform bodyPivot;

    public List<Vector3> dirs_fr; // temporary pls

    public List<Leg> Legs;

    private float speed = .1f;

    public int stepIndex = 0; // Keeps counts on wich leg I'm using

    public bool shallMove = true;

    private void Start()
    {
        dirs_fr.Add(Vector3.forward);
        dirs_fr.Add(-Vector3.forward);
        
        Legs = new List<Leg>
        {
            new Leg(fr_ik, dirs_fr, speed),
            new Leg(fl_ik, dirs_fr, speed),
            new Leg(br_ik, dirs_fr, speed),
            new Leg(bl_ik, dirs_fr, speed)
        };
    }

    private void Update()
    {
        if(!shallMove )
        {
            return;
        }

        // Next leg moves
        Legs[stepIndex].SweetMove();

        // Advance for the next leg if this sweet move is finished
        if (!Legs[stepIndex].isIntoSweetMove)
        {
            stepIndex++;
            if(stepIndex >= Legs.Count)
            {
                stepIndex = 0;
            }
        }

        CenterBodyPivot();
    }

    private void CenterBodyPivot() {

        Vector3 NewCenter = Vector3.zero;

        NewCenter.x += fr_ik.position.x;
        NewCenter.x += fl_ik.position.x;
        NewCenter.x += br_ik.position.x;
        NewCenter.x += bl_ik.position.x;
        NewCenter.x /= 4;

        NewCenter.z += fr_ik.position.z;
        NewCenter.z += fl_ik.position.z;
        NewCenter.z += br_ik.position.z;
        NewCenter.z += bl_ik.position.z;
        NewCenter.z /= 4;

        // Y - maybe I got use it sometime
        NewCenter.y += fr_ik.position.y;
        NewCenter.y += fl_ik.position.y;
        NewCenter.y += br_ik.position.y;
        NewCenter.y += bl_ik.position.y;
        NewCenter.y /= 4;
        NewCenter.y = 0;

        bodyPivot.position = NewCenter;
    }

    /*
    void MoveTheLeg()
    {
        Vector3 deslocamento = new Vector3(xSpeed, ySpeed, 0) * Time.deltaTime;

        if (moveit.position.y + deslocamento.y > pico && ySpeed > 0)
        {
            deslocamento.y = pico - moveit.position.y;
        }
        else if (moveit.position.y + deslocamento.y < 0)
        {
            deslocamento.y = 0 - moveit.position.y;
        }

        moveit.position = moveit.position + deslocamento;

        // Muda o sinal de y se chegou no ponto mínimo ou no ponto máximo
        if (moveit.position.y == 0 && ySpeed <= 0)
        {
            ySpeed *= -1;
        }
        else if (moveit.position.y == pico && ySpeed >= 0)
        {
            ySpeed *= -1;
        }

    }

    */

}