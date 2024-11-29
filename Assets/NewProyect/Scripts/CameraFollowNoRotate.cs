using UnityEngine;

public class CameraFollow2_5D : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    private Vector3 offset; // Desplazamiento fijo de la cámara respecto al jugador

    void Start()
    {
        // Calcular el desplazamiento inicial
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Actualizar la posición de la cámara manteniendo el desplazamiento
        transform.position = player.position + offset;

        // Mantener la rotación original de la cámara
        transform.rotation = Quaternion.Euler(60, 0, 0); // Ajusta los ángulos según sea necesario
    }
}
