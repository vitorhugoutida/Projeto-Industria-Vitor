using UnityEngine;

// Essa linha exige que o GameObject tenha um componente, no caso o RigidBody.
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    
    private Rigidbody rb;

    private Vector3 moveDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = transform.right * moveX + transform.forward * moveZ;
    }

    void FixedUpdate() {

        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        
    }
}
