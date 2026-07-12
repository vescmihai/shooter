using UnityEngine;

public class PrimeraPersona : MonoBehaviour
{

    public float velocidad = 5f;
    public float sensibilidad = 2f;
    public float gravedad = -9.81f;
    public Transform camara;

    [Header("Efecto de lentitud al recibir dano")]
    public float factorLentitud = 0.5f;   //mitad de velocidad
    public float duracionLentitud = 5f;   //segundos que dura el efecto

    private CharacterController cc;
    private float pitch = 0f;
    private Vector3 velY;
    private float finLentitud = 0f;      

    void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Llamado desde Vida.cs cuando el jugador recibe dano
    public void Ralentizar()
    {
        finLentitud = Time.time + duracionLentitud;
    }

    void Update()
    {
        //Mirar con el raton
        float mx = Input.GetAxis("Mouse X") * sensibilidad;
        float my = Input.GetAxis("Mouse Y") * sensibilidad;
        transform.Rotate(0, mx, 0); // girar el cuerpo
        pitch = Mathf.Clamp(pitch - my, -80f, 80f); // mirar arriba y abajo
        camara.localEulerAngles = new Vector3(pitch, 0, 0);

        // Mientras dure el efecto, la velocidad se multiplica por el factor
        float velocidadActual = velocidad;
        if (Time.time < finLentitud) velocidadActual *= factorLentitud;

        // Caminar (WASD o Flechas)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 mov = (transform.right * h + transform.forward * v).normalized * velocidadActual;

        // Gravedad simple
        if (cc.isGrounded && velY.y < 0) velY.y = -2f;
        velY.y += gravedad * Time.deltaTime;

        cc.Move((mov + velY) * Time.deltaTime);

    }
}