using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Denizen : Actor {

    public AIMovimiento mi_movimiento;

	// Use this for initialization
	void Start () {
        mi_movimiento.StarPatrulla();
        ActivarContagio();
        
    }

}
