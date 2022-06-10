using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager; //Acessa o manager
    float speed; //Declara a velocidade
    bool turning = false; //Vari�vel que define a o ponto que os peixes ir�o rodear

    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed); //A velocidade acessa a velocidade m�xima e m�nima
    }

    void Update()
    {
        //Cria os limites para os peixes nadarem
        Bounds b = new Bounds(myManager.transform.position, myManager.swinLimits * 2);
        RaycastHit hit = new RaycastHit();
        Vector3 direction = myManager.transform.position - transform.position;

        if (!b.Contains(transform.position)) //Se o bounds/contains for diferente ele tornar� o turning verdadeiro
        {
            turning = true;
            direction = myManager.transform.position - transform.position;
        }
        else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit)) //Cria linha de detec��o para direcionar os peixes
        {
            turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        else //Deixa o turning falso caso as condi��es anteriores n�o acontecerem
        {
            turning = false;
        }
        if (turning) //Caso o turning aconte�a, ativa a rota��o do peixe
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            if(Random.Range(0, 100) < 10) //Altera a velocidade
            {
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            }
            if(Random.Range(0, 100) < 20) //Ativa o aplyRules caso aumente a velocidade
            {
                ApplyRules();
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed); //Movimenta��o do peixe  
    }

    void ApplyRules()
    {
        GameObject[] gos; //Lista dos peixes
        gos = myManager.allFish; //O manager acessa todos os peixes

        Vector3 vcentre = Vector3.zero; //Define o centro do cardume 
        Vector3 vavoid = Vector3.zero; //Define a dispers�o do cardume

        float gSpeed = 0.01f; //Velocidade do Grupo
        float nDistance; //Distancia entre os vizinhos
        int groupSize = 0; //Tamanh0 do grupo

        //Para cada item na lista
        foreach(GameObject go in gos)
        {
            if(go != this.gameObject) //Se o que ta na lista n�o for um peixe
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position); //Calcule a distancia
                if(nDistance <= myManager.neighborDistance) //Se a distancia for menor que a distancia do cardume
                {
                    vcentre += go.transform.position; //Aumenta o centro do cardume
                    groupSize++; //Aumenta o tamanho do grupo

                    if(nDistance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position); //Dispersa os peixes
                    }
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed; 
                }
            }
        }
        if(groupSize > 0)   
        {
            //Organiza o centro e a velocidade do cardume de acordo com a quantidade do cardume
            vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
            speed = gSpeed / groupSize;
            speed = Mathf.Clamp(speed, myManager.minSpeed, myManager.maxSpeed); //Impede que a velocidade vai aumentando infinitamente

            //Define o tamanho circunfer�ncia com base na velocidade de rota��o do cardume 
            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }
    }
}
