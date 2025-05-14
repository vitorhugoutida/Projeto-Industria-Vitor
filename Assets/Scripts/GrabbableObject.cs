using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private Rigidbody rb;
    private Transform holdPoint;
    private bool isHold = false;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private AreaDetector currentArea;

    public float moveSpeed = 10f;
    public float snapSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;  
    }

    // Update is called once per frame
    void Update()
    {
        if (isHold && holdPoint != null)
        {
            transform.position = Vector3.Lerp(transform.position, holdPoint.position, Time.deltaTime * moveSpeed);
        }
    }
    // Função que auxilia o GrabSystem para pegar o objeto e customizar os valores dentro dele
    public void Grab(Transform newHoldPoint)
    {
        isHold = true;
        rb.useGravity = false;
        rb.isKinematic = true;
        holdPoint = newHoldPoint;
    }
    // Função que auxilia 
    public void Relase()
    {
        isHold = false;
        rb.useGravity = true;
        rb.isKinematic = false;
        holdPoint = null;

        if (currentArea != null && currentArea.IsObjectInside(gameObject))
        {
            //Auxiia a animação de encaixe do objeto no local correto
            StartCoroutine(SnapToArea(currentArea.transform));
        }
        else
        {
            // Se o objeto não estiver na área valida, volta para a posição/rotação original
            StartCoroutine(ResetPosition());
        }
    }
    // Função que suaviza o movimento do objeto quando ele for colocado na área válida
        private IEnumerator SnapToArea(Transform snapTarget)
    {
        float t = 0f;
        Vector3 startPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        while (t < 1f)
        {
            t += Time.deltaTime * snapSpeed;

            transform.position = Vector3.Lerp(startPosition, snapTarget.position, t);
            transform.rotation = currentRotation;

            yield return null;
        }

        transform.position = snapTarget.position;
        transform.rotation = currentRotation;

        rb.useGravity = false;
        rb.isKinematic = true;

    }
    private IEnumerator ResetPosition()
    {
        float t = 0f;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (t < 1f)
        {
            t += Time.deltaTime * snapSpeed;

            transform.position = Vector3.Lerp(startPosition, originalPosition, t);
            transform.rotation = Quaternion.Lerp(startRotation, originalRotation, t);

            yield return null;
        }

        transform.position = originalPosition;
        transform.rotation = originalRotation;

        rb.useGravity = false;
        rb.isKinematic = true;

    }

    public void NotifyEnterArea(AreaDetector area)
    {
        currentArea = area;

    }
    public void NotifyExitArea(AreaDetector area)
    {
        if (currentArea == area)
        {
            currentArea = null;
        }
    }
    
}
