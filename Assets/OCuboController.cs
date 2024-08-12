using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class OCuboController : MonoBehaviour
{
    public float xSpeed = 2;

    public float ySpeed = 1;

    public float pico = 3;

    public bool rerun = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(rerun == true)
        {
            transform.position = Vector3.zero;
            rerun = false;
        }
       
        Vector3 deslocamento = new Vector3 (xSpeed, ySpeed, 0) * Time.deltaTime;

        if(transform.position.y + deslocamento.y > pico && ySpeed > 0)
        {
            deslocamento.y = pico - transform.position.y;
        }
        else if(transform.position.y + deslocamento.y < 0)
        {
            deslocamento.y = 0 - transform.position.y;
        }

        transform.position = transform.position + deslocamento;

        // Muda o sinal de y se chegou no ponto mínimo ou no ponto máximo
        if(transform.position.y == 0 && ySpeed <= 0)
        {
            ySpeed *= -1;
        }
        else if(transform.position.y == pico && ySpeed >= 0){
            ySpeed *= -1;
        }
        
    }
}
