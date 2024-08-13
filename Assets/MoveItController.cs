using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class MoveItController : MonoBehaviour
{
    public float xSpeed = 2;

    public float ySpeed = 1;

    public float pico = 3;

    public bool rerun = false;

    public Transform moveit;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(rerun == true)
        {
            moveit.position = Vector3.zero;
            rerun = false;
        }
       
        Vector3 deslocamento = new Vector3 (xSpeed, ySpeed, 0) * Time.deltaTime;

        if(moveit.position.y + deslocamento.y > pico && ySpeed > 0)
        {
            deslocamento.y = pico - moveit.position.y;
        }
        else if(moveit.position.y + deslocamento.y < 0)
        {
            deslocamento.y = 0 - moveit.position.y;
        }

        moveit.position = moveit.position + deslocamento;

        // Muda o sinal de y se chegou no ponto mínimo ou no ponto máximo
        if(moveit.position.y == 0 && ySpeed <= 0)
        {
            ySpeed *= -1;
        }
        else if(moveit.position.y == pico && ySpeed >= 0){
            ySpeed *= -1;
        }
        
    }
}
