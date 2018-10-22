using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoGanador : MonoBehaviour {

    public GameObject objeto_ganador;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            objeto_ganador.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
