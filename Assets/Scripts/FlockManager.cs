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

    [Header("Configura��es do Cardume")] //Declara��o da velocidade m�xima e m�nima
    [Range(0.0f, 5.0f)]
    public float minSpeed; 
    [Range(0.0f, 5.0f)]
    public float maxSpeed;

    void Start()
    {
        allFish = new GameObject[numFish]; //Coloca o n�mero de peixes de acordo do array
        for(int i = 0; i < numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),
                                                                Random.Range(-swinLimits.y, swinLimits.y),
                                                                Random.Range(-swinLimits.z, swinLimits.z)); //Define as dist�ncias dos peixes dentro do cardume
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity); //Instancia os peixes do cardume de acordo do array
        }
    }
}
