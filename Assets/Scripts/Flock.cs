using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager; //Acessa o manager
    float speed; //Declara a velocidade
    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed); //A velocidade acessa a velocidade máxima e mínima
    }

    void Update()
    {
        ApplyRules();
        transform.Translate(0, 0, Time.deltaTime * speed); //Movimentação do peixe
    }

    void ApplyRules()
    {
        GameObject[] gos; //Lista dos peixes
        gos = myManager.allFish; //O manager acessa todos os peixes

        Vector3 vcentre = Vector3.zero; //Define o centro do cardume 
        Vector3 vavoid = Vector3.zero; //Define a dispersão do cardume

        float gSpeed = 0.01f; //Velocidade do Grupo
        float nDistance; //Distancia entre os vizinhos
        int groupSize = 0; //Tamanh0 do grupo

        //Para cada item na lista
        foreach(GameObject go in gos)
        {
            if(go != this.gameObject) //Se o que ta na lista não for um peixe
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
            vcentre = vcentre / groupSize;
            speed = gSpeed / groupSize;
            speed = Mathf.Clamp(speed, myManager.minSpeed, myManager.maxSpeed); //Impede que a velocidade vai aumentando infinitamente

            //Define o tamanho circunferência com base na velocidade de rotação do cardume 
            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }
    }
}
