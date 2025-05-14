using UnityEngine;

public class AreaDetector : MonoBehaviour
{
    public float snapDistance = 1f;

    public Transform snapRotationReference;

    // Essa função verifica se o objeto grabbable (objeto pegável) está dentro da distância permitida.
    public bool IsObjectInside(GameObject grabbable)
    {
        float distance = Vector3.Distance(grabbable.transform.position, transform.position);

        return distance <= snapDistance;
    }

    // O m[etodo OnTriggerEnter serve para quando temos um Collider com "isTrigger ativado, que detecta outro Collider na área.

    private void OnTriggerEnter(Collider other)
    {

        // Verifica se o objeto que entrou na área possui a tag indicada.
        if (other.CompareTag("Grabbable")) {
            GrabbableObject grabbable = other.GetComponent<GrabbableObject>();

            // Se o componente existir, notifica que o objeto está dentro da área.
            if (grabbable != null)
            {
                grabbable.NotifyEnterArea(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        // Verifica se o objeto que está fora da área possui a tag indicada.
        if (other.CompareTag("Grabbable"))
        {
            GrabbableObject grabbable = other.GetComponent<GrabbableObject>();

            // Se o componente existir, notifica que o objeto está saiu da área.
            if (grabbable != null)
            {
                grabbable.NotifyExitArea(this);
            }

        }
    }

    // Este método desenha uma caixa verde na Cena, para auxiliar a visualização da área de detecção.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

}
