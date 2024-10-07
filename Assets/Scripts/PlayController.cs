using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    //public float pickupRange = 1.5f;

    // Start is called before the first frame update
    void Start()
    {   
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x > 0)
        {
            transform.eulerAngles = new Vector3(x: 0f, y: 0f, z: 0f);
            _animator.SetBool(name: "run", value: true);
        }

        if (x < 0)
        {
            transform.eulerAngles = new Vector3(x: 0f, y: 180f, z: 0f);
            _animator.SetBool(name: "run", value: true);
        }

        if (y < 0)
        {
            _animator.SetBool(name: "run", value: true);
        }

        if (y > 0)
        {
            _animator.SetBool(name: "run", value: true);
        }

        if ((x < 0.001f && x > -0.001f) && (y < 0.001f && y > -0.001f))
        {
            _animator.SetBool(name: "run", value: false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("鼠标右键被按下");
            _animator.SetBool("Skill", true);
        }
        else
        {
            _animator.SetBool("Skill", false);
        }

        Vector2  position = transform.position;
        position.x += x * speed * Time.deltaTime;
        position.y += y * speed * Time.deltaTime;
        transform.position = position;
    }
}
