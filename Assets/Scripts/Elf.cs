using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elf : MonoBehaviour
{
    [SerializeField]
    Transform[] targets;
    Vector3 destination;
    int targetIndex;
    NavMeshAgent agent;
    bool hitted;

    Animator anim;
    [SerializeField]
    GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        hitted = false;
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        targetIndex = 0;
        destination = targets[targetIndex].position;
        agent.SetDestination(destination);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance <= 0.5f) //Si el elfo está cerca del destino
        {
            if (targetIndex == targets.Length - 1) //si es el ultimo destino
            {
                targetIndex = 0;                   //vuelve a empezar en el primer destino
            }
            else
            {
                targetIndex++; //cambia al siguiente destino
            }
            destination = targets[targetIndex].position;
            agent.SetDestination(destination);

        }
    }

    //OnTriggerEnter se llama cuando un objeto con collider entra en el trigger de este objeto.
    private void OnTriggerEnter(Collider other)
    {
        if (!hitted) // Si hitted es falso/false
        {
            if (other.CompareTag("Ball"))  // SI el objeto que ha chocado con el elfo, tiene el tag "Ball"
            {
                Destroy(other.gameObject); //Se destruye la bola de nieve.
                hitted = true;             //cambia el balor de hitted a verdadero/true
                anim.SetTrigger("hitted"); //disparamos el trigger del animator para cambiar de animación
                agent.isStopped = true;    //paramos el movimiento del elfo
                DropBox();                 //LLama a la funcion DropBox para soltar la caja del elfo.
            }
        }
    }

   
    void DropBox()
    {
        box.transform.parent = null;                     //La caja deja de ser hijo del Elfo
        Rigidbody boxRB = box.GetComponent<Rigidbody>(); //Cogemos el rigidbody de la caja.
        boxRB.isKinematic = false;                       //El rigidbody deja de ser kinematic (ahora le afecta la gravedad)
        boxRB.AddForce(Vector3.up * 100);                //Le añadimos una fuerza hacia arriba para que de un saltito.
    }

}