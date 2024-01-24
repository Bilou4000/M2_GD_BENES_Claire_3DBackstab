using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement instance;

    [SerializeField] GameObject canBeSeenText, canBackStabText;
    [SerializeField] private bool canStab, stabing;
    [SerializeField] private float speed = 5f, stabDistance;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] Animator animator;
    private GameObject theEnemy;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        //rotation
        float directionY = Input.GetAxis("Horizontal");
        transform.Rotate(0, rotationSpeed * directionY * Time.deltaTime, 0);

        //movement
        float directionZ = Input.GetAxis("Vertical");
        Vector3 movement = directionZ * transform.forward * speed * Time.deltaTime;
        transform.position = transform.position + movement;

        if(Input.GetAxis("Vertical") != 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < allEnemies.Length; i++)
        {
            if (allEnemies[i].GetComponent<EnemyScript>().Vision() == true)
            {
                canStab = false;
                canBeSeenText.SetActive(true);
                canBackStabText.SetActive(false);
                return;
            }
            else
            {
                canStab = true;
                canBeSeenText.SetActive(false);
            }
        }

        if (theEnemy != null)
        {
            stabDistance = theEnemy.GetComponent<EnemyScript>().StabDistance();

            if (Vector3.Distance(theEnemy.transform.position, transform.position) > stabDistance)
            {
                stabing = false;
                canBackStabText.SetActive(false);
            }

            if (Vector3.Dot(transform.forward, theEnemy.transform.forward) < 0)
            {
                stabing = false;
                canBackStabText.SetActive(false);
            }
        }
        else
        {
            stabing = false;
            canBackStabText.SetActive(false);
        }

        if (stabing)
        {
            if (Input.GetMouseButtonUp(0))
            {
                animator.SetTrigger("attack");
                theEnemy.GetComponent<EnemyScript>().Health(1f);
                stabing = false;
            }
        }
    }

    public void Stab(GameObject enemy)
    {
        if(canStab)
        {
            stabing = true;
            theEnemy = enemy;
            canBackStabText.SetActive(true);
        }
        else
        {
            stabing = false;
            canBackStabText.SetActive(false);
        }
    }
}
