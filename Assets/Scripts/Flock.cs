using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager; //Acessa o manager
    float speed; //Declara a velocidade
    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed); //A velocidade acessa a velocidade m�xima e m�nima
    }

    void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * speed); //Movimenta��o do peixe
    }
}
