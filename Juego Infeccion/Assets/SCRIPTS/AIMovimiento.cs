using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovimiento : MonoBehaviour {

    public NavMeshAgent navegador;

    public Transform[] camino_patrulla;

    public bool patrullero;

    public float distancia_minima;

    public int indice_patrulla;

    private Transform objetivo_navegacion;

    public void Caminar(Transform objetivo)
    {

        objetivo_navegacion = objetivo;
        navegador.SetDestination(objetivo.position);

    }

    public void StarPatrulla()
    {
        Caminar(camino_patrulla[indice_patrulla]);
    }

    private void YaLLego()
    {
        float distancia_objetivo;
        distancia_objetivo = Vector3.Distance(objetivo_navegacion.position, transform.position);
        if(distancia_objetivo<distancia_minima)
        {
            indice_patrulla++;
            if (indice_patrulla >= camino_patrulla.Length) indice_patrulla = 0;
            Caminar(camino_patrulla[indice_patrulla]);

        }
    }
	
	// Update is called once per frame
	void Update () {
        if (patrullero)
            YaLLego();        
	}
}
