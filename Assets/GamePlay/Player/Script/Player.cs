using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator ani;
    public Rigidbody rb;
    public bool jump = false;
    float speed = 1.2f;

    void Start()
    {
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        Run();
        
    }
    public void Run()
    {

        float ipHorizontal = Input.GetAxis(axisName: "Horizontal");
        float ipVertical = Input.GetAxis(axisName: "Vertical");

        Vector3 movement = new Vector3(ipHorizontal, y: 0, ipVertical);

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }


        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);

    ani.SetBool("Walk", ipHorizontal != 0 || ipVertical != 0);
        if (Input.GetKeyDown(KeyCode.Space) && jump)
        {
            ani.SetTrigger("jump");
            rb.AddForce(new Vector3(0, 9, 0) * 5, ForceMode.Impulse);
            jump = false;
        }
        XoaytheoPlayer(movement);
        if (Input.GetKey(KeyCode.LeftShift) && (ipHorizontal != 0 || ipVertical != 0))
        {
            ani.SetBool("Run",true);
            speed = 3f;
        }
        else
        {
            speed = 1.2f;
            ani.SetBool("Run", false);
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jump = true;
        }
    }
    public void XoaytheoPlayer(Vector3 playerMovementInput)
    {
        Vector3 lookDirection = playerMovementInput;
        lookDirection.y = 0;

        if (lookDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = rotation;
        }
    }

}
