using UnityEngine;

public class Disparar : MonoBehaviour
{
    public Camera camara;
    public int dano = 2;
    public float alcance = 100f;
    public float cadencia = 0.5f;
    public AudioClip sonidoDisparo;
    public GameObject muzzle;

    [Header("Municion")]
    public int balasMax = 3;           // capacidad del cargador
    public int balasReserva = 6;      // balas de reserva 
    public float tiempoRecarga = 1.5f; // espera al recargar (segundos)

    private AudioSource fuente;
    private float proximo = 0f;
    private int balasActuales;
    private bool recargando = false;

    void Start()
    {
        fuente = GetComponent<AudioSource>();
        if (muzzle != null) muzzle.SetActive(false);
        balasActuales = balasMax;
    }

    void Update()
    {
        // Disparar: solo si hay balas y no esta recargando
        if (Input.GetMouseButtonDown(0) && Time.time >= proximo
            && balasActuales > 0 && !recargando)
        {
            proximo = Time.time + cadencia;
            Disparo();
        }

        // Recargar con R (si no esta lleno, hay reserva y no esta ya recargando)
        if (Input.GetKeyDown(KeyCode.R) && balasActuales < balasMax
            && balasReserva > 0 && !recargando)
        {
            recargando = true;
            Invoke(nameof(TerminarRecarga), tiempoRecarga);
        }
    }

    void Disparo()
    {
        balasActuales--;

        if (sonidoDisparo != null) fuente.PlayOneShot(sonidoDisparo);
        if (muzzle != null)
        {
            muzzle.SetActive(true);
            Invoke(nameof(ApagarMuzzle), 0.05f);
        }

        Ray ray = camara.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, alcance))
        {
            Vida v = hit.collider.GetComponentInParent<Vida>();
            if (v != null) v.RecibirDano(dano);
        }
    }

    void TerminarRecarga()
    {
        // Pasa balas de la reserva al cargador (solo las que hay)
        int faltan = balasMax - balasActuales;
        int transferir = Mathf.Min(faltan, balasReserva);

        balasActuales += transferir;
        balasReserva -= transferir;
        recargando = false;
    }

    void ApagarMuzzle()
    {
        if (muzzle != null) muzzle.SetActive(false);
    }

    // Metodos para que el HUD lea el estado de la municion
    public int BalasActuales() { return balasActuales; }
    public int BalasMax() { return balasMax; }
    public int BalasReserva() { return balasReserva; }
    public bool Recargando() { return recargando; }
}