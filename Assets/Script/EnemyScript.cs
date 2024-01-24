using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject text;
    [SerializeField] float maxStabDistance, maxVisionDistance, life = 3;
    private bool canStab, canBeSeen;

    private void Update()
    {
        float Distance = Vector3.Distance(player.position, transform.position);

        if (Vector3.Dot(transform.forward, player.position - transform.position) > 0)
        {
            if(Distance < maxVisionDistance)
            {
                canBeSeen = true;
            }
            else
            {
                canBeSeen= false;
            }
        }
        else
        {
            canBeSeen = false;
        }

        if(Vector3.Dot(transform.forward, player.position - transform.position) < 0)
        {
            if (Vector3.Dot(transform.forward, player.forward) > 0)
            {
                if (Distance < maxStabDistance)
                {
                    CharacterMovement.instance.Stab(gameObject);
                }
            }
        }

        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public bool Vision()
    {
        return canBeSeen;
    }

    public void Health(float health)
    {
        life -= health;
        Debug.Log(life);
    }

    public float StabDistance()
    {
        return maxStabDistance;
    }

    //public float StabDistance()
    //{
    //    return maxStabDistance;
    //}
}
