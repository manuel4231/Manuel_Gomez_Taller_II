using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sruvivor : Actor {

    public AIMovimiento mi_movimiento;

    public float tiempo_buscar_cura;

    public Transform posicion_vacuna;

	// Use this for initialization
	void Start () {
        ActivarContagio();
        mi_movimiento.StarPatrulla();
        StartCoroutine(BuscarCura());
    }
	
	// Update is called once per frame
	void Update () {
		if(posicion_vacuna!=null)
        {
            float distancia_vacuna = Vector3.Distance(posicion_vacuna.position, transform.position);
            if(distancia_vacuna<1.5f)
            {
                posicion_vacuna.gameObject.SetActive(false);
                posicion_vacuna = null;
                mi_movimiento.patrullero = true;
                mi_movimiento.StarPatrulla();
                StartCoroutine(BuscarCura());
            }
        }
	}

    IEnumerator BuscarCura()
    {
        yield return new WaitForSeconds(tiempo_buscar_cura);
        if(!Mirar())
        {
            StartCoroutine(BuscarCura());
        }
    }

    public bool Mirar()
    {
        RaycastHit hit;

        Vector3 p1 = transform.position;

        if(Physics.SphereCast(p1,20f,transform.forward,out hit))
        {
            Debug.Log("GolpeVacuna");
            if (hit.collider.gameObject.tag == "Vacuna")
            {
                
                mi_movimiento.patrullero = false;
                posicion_vacuna = hit.collider.gameObject.transform;
                mi_movimiento.Caminar(posicion_vacuna.transform);
                return true;
            }
        }
        return false;
    }
}
