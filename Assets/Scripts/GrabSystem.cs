using UnityEngine;

public class GrabSystem : MonoBehaviour
{
    public Transform cameraTransform;

    public float grabDistance = 3f;

    public KeyCode grabKey = KeyCode.F;
    public KeyCode rotateLeftKey = KeyCode.Q;
    public KeyCode rotateRightKey = KeyCode.E;

    public float rotationSpeed = 500f;

    public Transform holdPoint;

    private GameObject holdObject;

    private GrabbableObject holdScript;


    void Update()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        //Se a tecla de pegar (F) for pressionada
        if (Input.GetKeyDown(grabKey))
        {
            // Se não estiver segurando nenhum objeto
            if (holdObject == null)
            {
                if (Physics.Raycast(ray, out hit, grabDistance))
                {
                    if (hit.collider.CompareTag("Grabbable"))
                    {
                        holdObject = hit.collider.gameObject;

                        holdScript = holdObject.GetComponent<GrabbableObject>();
                    }

                    if (holdScript != null)
                    {
                        //holdScript.Grab(holdPoint);
                    }
                }
            }

        }

        else
        {
            //holdScript.Release();

            holdObject = null;
            holdScript = null;
        }

        if (holdObject != null )
        {
            float rotation = 0f;

            // Se a tecla Q for pressionada , adiciona rotação negativa (girar para a direita)
            if (Input.GetKey(rotateLeftKey))
            {
                rotation -= rotationSpeed * Time.deltaTime;
            }

            // Se a tecla E for pressionada, adiciona rotação positiva (girar para a direita)
            if(Input.GetKey(rotateRightKey))
            {
                rotation += rotationSpeed * Time.deltaTime;
            }

            holdObject.transform.Rotate(Vector3.up, rotation, Space.Self);
        }

    }

}

