using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject hitbox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(attack());
        }
    }
    IEnumerator attack()
    {
        yield return new WaitForSeconds(0.1f);

        hitbox.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        hitbox.SetActive(false);
    }
}
