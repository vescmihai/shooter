using UnityEngine;

public class Camara : MonoBehaviour
{
    private Transform camara;

    void Start()
    {
        camara = UnityEngine.Camera.main.transform;
    }

    void LateUpdate()
    {
        if (camara == null) return;

        Vector3 direccion = transform.position - camara.position;
        direccion.y = 0f;
        if (direccion.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(direccion);
    }
}