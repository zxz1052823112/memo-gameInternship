using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    public int expValue;

    private bool movingToPlayer;
    public float moveSpeed;

    public float timeBetweenChecks = .2f;
    private float checkCounter;

    private PlayController player;

    public float pickupRange;
    public float pickupSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        //player = PlayerManager._instance.GetComponent<PlayController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToPlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerManager.Position, moveSpeed * Time.deltaTime);
        }
        else
        {
            checkCounter -= Time.deltaTime;
            if (checkCounter <= 0)
            {
                checkCounter = timeBetweenChecks;

                //Debug.Log(Vector3.Distance(transform.position, PlayerManager.Position));

                if (Vector3.Distance(transform.position, PlayerManager.Position) < pickupRange)
                {
                    movingToPlayer = true;
                    moveSpeed = pickupSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ExperienceLevelController.instance.GetExp(expValue);

            Destroy(gameObject);
        }
    }
}
