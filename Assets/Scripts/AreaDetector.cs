using UnityEngine;

public class AreaDetector : MonoBehaviour
{
    public float snapDistance = 1f;

    public Transform snapRotationReference;

    // Essa fun��o verifica se o objeto grabbable (objeto peg�vel) est� dentro da dist�ncia permitida.
    public bool IsObjectInside(GameObject grabbable)
    {
        float distance = Vector3.Distance(grabbable.transform.position, transform.position);

        return distance <= snapDistance;
    }

    // O m[etodo OnTriggerEnter serve para quando temos um Collider com "isTrigger ativado, que detecta outro Collider na �rea.

    private void OnTriggerEnter(Collider other)
    {

        // Verifica se o objeto que entrou na �rea possui a tag indicada.
        if (other.CompareTag("Grabbable")) {
            GrabbableObject grabbable = other.GetComponent<GrabbableObject>();

            // Se o componente existir, notifica que o objeto est� dentro da �rea.
            if (grabbable != null)
            {
                grabbable.NotifyEnterArea(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        // Verifica se o objeto que est� fora da �rea possui a tag indicada.
        if (other.CompareTag("Grabbable"))
        {
            GrabbableObject grabbable = other.GetComponent<GrabbableObject>();

            // Se o componente existir, notifica que o objeto est� saiu da �rea.
            if (grabbable != null)
            {
                grabbable.NotifyExitArea(this);
            }

        }
    }

    // Este m�todo desenha uma caixa verde na Cena, para auxiliar a visualiza��o da �rea de detec��o.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

}
