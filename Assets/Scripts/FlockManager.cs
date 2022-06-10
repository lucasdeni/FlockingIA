using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    //Declara��o de vari�veis
    public GameObject fishPrefab;
    public int numFish = 20; //Define o n�mero de peixes
    public GameObject[] allFish; //Array dos peixes
    public Vector3 swinLimits = new Vector3(5, 5, 5); //Declara o espa�o entre os peixes do cardume
    public Vector3 goalPos; //Ponto dos peixes

    [Header("Configura��es do Cardume")] //Declara��o da velocidade m�xima e m�nima
    [Range(0.0f, 5.0f)]
    public float minSpeed; 
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 50.0f)]
    public float neighborDistance;
    [Range(0.5f, 5.0f)]
    public float rotationSpeed;

    void Start()
    {
        allFish = new GameObject[numFish]; //Coloca o n�mero de peixes de acordo do array
        for(int i = 0; i < numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),
                                                                Random.Range(-swinLimits.y, swinLimits.y),
                                                                Random.Range(-swinLimits.z, swinLimits.z)); //Define as dist�ncias dos peixes dentro do cardume
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity); //Instancia os peixes do cardume de acordo do array
            allFish[i].GetComponent<Flock>().myManager = this; //Pega os peixes com o c�gido do Flock
        }
        goalPos = this.transform.position; //O destino � o mesmo que est� no jogo
    }
    void Update()
    {
        goalPos = this.transform.position;
        if(Random.Range(0, 100) < 10) //Muda a posi��o do ponto dos peixes
        {
            goalPos = this.transform.position + new Vector3(Random.Range(-swinLimits.x,swinLimits.x),
                                                            Random.Range(-swinLimits.y,swinLimits.y),
                                                            Random.Range(-swinLimits.z,swinLimits.z));
        }
    }
}
