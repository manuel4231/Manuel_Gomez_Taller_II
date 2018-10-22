using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actor : MonoBehaviour {

    public Enfermedad mi_enfermedad;

    private float tiempo_contagiado;

    private float tiempo_restante_morir;

    private bool enfermo;

    public float virusA_probabilidad;

    public float virusS_probabilidad;

    public float blackDeath_probabilidad;

    public float velocidad_movimiento;

    private float tiempo_postramiento;

    private float probabilidad_postramiento;

    private float tiempo_postrado;

    private float velocidad_enfermedad;

    public Color color_sano;

    public NavMeshAgent navegador;

    public MeshRenderer render;
    

    private bool postramiento_posible;


    private void Start()
    {
    
    }


    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Actor" && !enfermo)
        {
           
            Actor humano_tocado = collision.gameObject.GetComponent<Actor>();
            if (humano_tocado.enfermo)
            {
                mi_enfermedad = humano_tocado.mi_enfermedad;
                ActivarContagio();

            }
        }
        if(collision.gameObject.tag == "Vacuna")
        {
            Debug.Log("curado");
            Vacuna vacuna_actual = collision.gameObject.GetComponent<Vacuna>();
            switch (vacuna_actual.enfermedad_cura)
            {
                case 1:
                    VacunaA(vacuna_actual.cantidad_inmunitaria);
                    break;
                case 2:
                    VacunaS(vacuna_actual.cantidad_inmunitaria);
                    break;
                case 3:
                    VacunaBD(vacuna_actual.cantidad_inmunitaria);
                    break;
                default:
                    break;

            }
        }
    }

    void VacunaA(float inmunidad)
    {
        StopAllCoroutines();
        navegador.speed = 3.5f;
        render.material.color = color_sano;
        
        virusA_probabilidad -= inmunidad;
        if (virusA_probabilidad < 0.5f)
            virusA_probabilidad = 0.5f;
    }

    void VacunaS(float inmunidad)
    {
        StopAllCoroutines();
        navegador.speed = 3.5f;
        render.material.color = color_sano;

        virusS_probabilidad -= inmunidad;
        if (virusS_probabilidad < 0.5f)
            virusS_probabilidad = 0.5f;
    }

    void VacunaBD(float inmunidad)
    {
        StopAllCoroutines();
        navegador.speed = 3.5f;
        render.material.color = color_sano;

        blackDeath_probabilidad -= inmunidad;
        if (blackDeath_probabilidad < 0.5f)
            blackDeath_probabilidad = 0.5f;
    }

    public void ActivarContagio()
    {
        switch (mi_enfermedad.tipo_enfermedad)
        {

            case 1:
                ContagioA();
                break;
            case 2:
                ContagioS();
                break;
            case 3:
                ContagioBlack();
                break;
            default:
                break;

        }
    }

    public void ContagioA()
    {
        float loteria_Muerte = Random.Range(0f, 1f);
        if (loteria_Muerte <= virusA_probabilidad)
        {
            velocidad_enfermedad = velocidad_movimiento * 0.9f;
            
            postramiento_posible = false;
            enfermo = true;
            InstallarEnfermedad();
            StartCoroutine(Incubacion());
        }
    }

    public void ContagioS()
    {
        float loteria_Muerte = Random.Range(0f, 1f);
        if (loteria_Muerte <= virusS_probabilidad)
        {
            velocidad_enfermedad = velocidad_movimiento * 0.8f;

            enfermo = true;
            tiempo_postramiento = 10f;
            probabilidad_postramiento = 0.05f;
            tiempo_postrado = 2f;
            InstallarEnfermedad();
            StartCoroutine(Incubacion());
        }

    }

    public void ContagioBlack()
    {
        float loteria_Muerte = Random.Range(0f, 1f);
        if (loteria_Muerte <= blackDeath_probabilidad)
        {
            velocidad_enfermedad = velocidad_movimiento * 0.6f;

            enfermo = true;
            tiempo_postramiento = 10f;
            probabilidad_postramiento = 0.15f;
            tiempo_postrado = 3f;
            InstallarEnfermedad();
            StartCoroutine(Incubacion());
        }
    }

    public void LoteriaPostramiento()
    {
        float loteria_postramiento = Random.Range(0f, 1f);

        if(loteria_postramiento<=probabilidad_postramiento)
        {
            navegador.speed = 0f;
            StartCoroutine(Despostrar());
        }

        
    }

    public void InstallarEnfermedad()
    {
        tiempo_contagiado = mi_enfermedad.tiempo_manifestacion;
        tiempo_restante_morir = mi_enfermedad.tiempo_mortal;
    }

    public IEnumerator Incubacion()
    {
        yield return new WaitForSeconds(tiempo_contagiado);
        navegador.speed = velocidad_enfermedad;
        render.material.color = mi_enfermedad.color_enfermedad;
        StartCoroutine(CaminoMuerte());
        if(postramiento_posible)
        StartCoroutine(Postramiento());
    }

    IEnumerator CaminoMuerte()
    {
        yield return new WaitForSeconds(tiempo_restante_morir);
        Debug.Log("Hapend");
        
        gameObject.SetActive(false);
    }

    IEnumerator Postramiento()
    {
        yield return new WaitForSeconds(tiempo_postramiento);
        LoteriaPostramiento();
        StartCoroutine(Postramiento());
    }

    IEnumerator Despostrar()
    {
        yield return new WaitForSeconds(tiempo_postrado);
        navegador.speed = velocidad_enfermedad;
    }
}
