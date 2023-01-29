using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 _movement_vector;
    [SerializeField]
    [Range(1f, 10f)]
    private float _movement_speed;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private bool _active;
    [SerializeField]
    private float _weight;
    [SerializeField]
    private bool _bunker;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _active = true;
    }

    private void Update()
    {
        if(!_active)
            return;
        _movement_vector.x = Input.GetAxisRaw("Horizontal");
        _movement_vector.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (!_active)
            return;
        Debug.Log(_movement_vector);
        _rigidbody.MovePosition(transform.position + (_movement_vector * _movement_speed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
