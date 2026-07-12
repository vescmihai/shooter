using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemigo : MonoBehaviour
{
    [Header("Persecucion")]
    public float distanciaAtaque = 8f;   // a esta distancia se detiene y dispara

    [Header("Ataque")]
    public int dano = 1;
    public float cadencia = 1.5f;        // segundos entre disparos
    public AudioClip sonidoDisparo;
    public GameObject muzzle;            // destello opcional

    private NavMeshAgent agente;
    private Transform jugador;
    private AudioSource fuente;
    private float proximoDisparo = 0f;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        fuente = GetComponent<AudioSource>();

        // Busca al jugador por su Tag 
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null) jugador = obj.transform;

        if (muzzle != null) muzzle.SetActive(false);
    }

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia > distanciaAtaque)
        {
            // Lejos: perseguir al jugador por el NavMesh
            agente.isStopped = false;
            agente.SetDestination(jugador.position);
        }
        else
        {
            // Cerca: detenerse y atacar
            agente.isStopped = true;

            if (Time.time >= proximoDisparo)
            {
                proximoDisparo = Time.time + cadencia;
                Disparo();
            }
        }
    }

    void Disparo()
    {
        // Direccion hacia el jugador (a la altura del enemigo)
        Vector3 origen = transform.position + Vector3.up * 1f;
        Vector3 direccion = (jugador.position - origen).normalized;

        // Solo hace dano si tiene linea de vision (no dispara a traves de paredes)
        if (Physics.Raycast(origen, direccion, out RaycastHit hit, distanciaAtaque + 2f))
        {
            Vida v = hit.collider.GetComponentInParent<Vida>();
            if (v != null && v.esJugador)
            {
                v.RecibirDano(dano);

                if (sonidoDisparo != null && fuente != null)
                    fuente.PlayOneShot(sonidoDisparo);

                if (muzzle != null)
                {
                    muzzle.SetActive(true);
                    Invoke(nameof(ApagarMuzzle), 0.05f);
                }
            }
        }
    }

    void ApagarMuzzle()
    {
        if (muzzle != null) muzzle.SetActive(false);
    }
}
