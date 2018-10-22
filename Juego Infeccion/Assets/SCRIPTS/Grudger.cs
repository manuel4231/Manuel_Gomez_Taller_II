using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grudger : Actor {

    public AIMovimiento mi_movimiento;

    public Transform posicion_base;

    private Transform posicion_player;

    private bool persiguiendo;

    private void Start()
    {
        ActivarContagio();
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag=="Player")
        {
            posicion_player = other.gameObject.transform;
            mi_movimiento.Caminar(other.gameObject.transform);
            persiguiendo = true;
        }
    }

    // Update is called once per frame
    void Update () {
		if(persiguiendo)
        {
            mi_movimiento.Caminar(posicion_player);

            float distancia_jugador = Vector3.Distance(posicion_player.position, transform.position);
            if(distancia_jugador>20f)
            {
                persiguiendo = false;
                mi_movimiento.Caminar(posicion_base);
            }

            if (distancia_jugador <1f)
            {
                persiguiendo = false;
                mi_movimiento.Caminar(posicion_base);
            }
        }
	}
}
