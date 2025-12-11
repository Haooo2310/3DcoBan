using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator ani;
    public Rigidbody rb;

    public bool jump = false;
    public bool stop = false;
    public bool isDead = false;  // ← CỜ DỪNG MỌI THỨ KHI CHẾT

    float speed;
    bool isSitting = false;
    [SerializeField] private float walkSpeed = 0.2f;
    [SerializeField] private float Runspeed = 2f;
    public CapsuleCollider cp;
    public int hp = 1;

    void Start()
    {
        cp = GetComponent<CapsuleCollider>();
        if (ani == null) ani = GetComponent<Animator>();
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Nếu chết thì không cho làm gì nữa
        if (isDead) return;

        Run();

        StartCoroutine(Dropping());
    }

    public void Run()
    {
        if (stop) return;

        float ipHorizontal = Input.GetAxis("Horizontal");
        float ipVertical = Input.GetAxis("Vertical");

        Vector3 movementInput = new Vector3(ipHorizontal, 0, ipVertical);
        if (movementInput.magnitude > 1)
            movementInput.Normalize();

        Vector3 finalMovement =
            transform.forward * movementInput.z +
            transform.right * movementInput.x;

        finalMovement.y = 0;

        rb.MovePosition(rb.position + finalMovement * speed * Time.fixedDeltaTime);

        ani.SetBool("Walk", ipHorizontal != 0 || ipVertical != 0);

        if (Input.GetKeyDown(KeyCode.Space) && jump)
        {
            ani.SetTrigger("jump");
            rb.AddForce(Vector3.up * 45, ForceMode.Impulse);
            jump = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) && (ipHorizontal != 0 || ipVertical != 0))
        {
            ani.SetBool("Run", true);
            speed = Runspeed;
        }
        else
        {
            ani.SetBool("Run", false);
            speed = walkSpeed;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                isSitting = !isSitting;
                ani.SetBool("Ngoi", isSitting);
                StartCoroutine(tatcolider());
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jump = true;
        }
    }

    public IEnumerator Dropping()
    {
        if (Input.GetKeyDown(KeyCode.F) && !stop)
        {
            ani.SetTrigger("Drop");
            stop = true;
            yield return new WaitForSeconds(0.3f);
            stop = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            hp -= 1;

            if (hp <= 0)
            {
                ani.SetTrigger("Dead");
                isDead = true;  // ← BẬT CỜ "CHẾT"
            }
        }
    }
        IEnumerator tatcolider()
        {
            cp.enabled = false;    
            yield return new WaitForSeconds(0.5f);
            cp.enabled = true;  
        }
}
