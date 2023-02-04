using UnityEngine;
using Utility;

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
    private GameObject _near_object;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private RuntimeAnimatorController _bunker_animator_controller;
    [SerializeField]
    private RuntimeAnimatorController _outside_animator_controller;
    [SerializeField]
    private AudioClip _outside_footstep_sound_fx;
    [SerializeField]
    private AudioClip _bunker_footstep_sound_fx;
    [SerializeField]
    private AudioSource _audio_source;
    [SerializeField]
    private GameObject _pop_up;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _audio_source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _pop_up = transform.GetChild(0).gameObject;
        _active = true;
    }

    private void Update()
    {
        if (!_active)
        {
            ResetAnimator();
            return;
        }

        _movement_vector.x = Input.GetAxisRaw("Horizontal");
        _movement_vector.y = Input.GetAxisRaw("Vertical");
        SetAnimatorParams();
        if (_near_object != null)
            HandleNearObject();
    }

    private void FixedUpdate()
    {
        if (!_active)
            return;
        _rigidbody.velocity = _movement_vector * _movement_speed;
    }

    private void SetAnimatorParams()
    {
        if (_movement_vector.x > 0)
        {
            _animator.SetBool("right", true);
            _animator.SetBool("left", false);
        }
        if (_movement_vector.x < 0)
        {
            _animator.SetBool("left", true);
            _animator.SetBool("right", false);
        }
        if(_movement_vector.y > 0)
        {
            _animator.SetBool("up", true);
            _animator.SetBool("down", true);
        }
        if(_movement_vector.y < 0)
        {
            _animator.SetBool("down", true);
            _animator.SetBool("up", false);
        }
        if(_movement_vector.x == 0 && _movement_vector.y == 0)
        {
            ResetAnimator();
        }
    }

    private void ResetAnimator()
    {
        _animator.SetBool("left", false);
        _animator.SetBool("right", false);
        _animator.SetBool("down", false);
        _animator.SetBool("up", false);
    }

    private void HandleNearObject()
    {
        if(_near_object.tag.Equals(Tags.PICKUP_TAG) && Input.GetKeyDown(KeyCode.F))
        {
            DisableMovement();
            _near_object.GetComponent<Pickup>().Interact();
        }   
        else if(_near_object.tag.Equals(Tags.READABLE_TAG) && Input.GetKeyDown(KeyCode.F))
        {
            DisableMovement();
            _near_object.GetComponent<Readable>().Interact();
        }
        else if (_near_object.tag.Equals(Tags.DOOR_TAG) && Input.GetKeyDown(KeyCode.F))
        {
            DisableMovement();
            _near_object.GetComponent<Door>().Interact();
        }
        else if (_near_object.tag.Equals(Tags.ENERGY_MACHINE_TAG) && Input.GetKeyDown(KeyCode.F))
        {
            DisableMovement();
            _near_object.GetComponent<EnergyMachine>().Interact();
        }
        else if (_near_object.tag.Equals(Tags.WATER_MACHINE_TAG) && Input.GetKeyDown(KeyCode.F))
        {
            DisableMovement();
            _near_object.GetComponent<WaterMachine>().Interact();
        }
        else if (_near_object.tag.Equals(Tags.GROUND_TAG) && Input.GetKeyDown(KeyCode.F))
        {
            DisableMovement();
            _near_object.GetComponent<Ground>().Interact();
        }
    }

    public void PlayInteractAnimation()
    {
        _animator.SetBool("interact", true);
    }

    public void StopInteractAnimation()
    {
        _animator.SetBool("interact", false);
    }

    public void SetBunkerAnimatorController()
    {
        _animator.runtimeAnimatorController = _bunker_animator_controller;
    }

    public void SetOutsideAnimatorController()
    {
        _animator.runtimeAnimatorController = _outside_animator_controller;
    }

    public void Die()
    {
        DisableMovement();
        ResetAnimator();
        _animator.SetBool("dead", true);
    }

    public void DisableMovement()
    {
        _active = false;
    }

    public void EnableMovement()
    {
        _active = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag.Equals(Tags.WATER_MACHINE_TAG) ||
            collision.collider.tag.Equals(Tags.ENERGY_MACHINE_TAG) ||
            collision.collider.tag.Equals(Tags.PICKUP_TAG) ||
            collision.collider.tag.Equals(Tags.DOOR_TAG) ||
            collision.collider.tag.Equals(Tags.READABLE_TAG) ||
            collision.collider.tag.Equals(Tags.GROUND_TAG))
        {
            _pop_up.SetActive(true);
            _near_object = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _pop_up.SetActive(false);
        _near_object = null;
    }

    public void PlayBunkerFootstepSoundFx()
    {
        _audio_source.clip = _bunker_footstep_sound_fx;
        _audio_source.Play();
    }

    public void PlayOutsideFootstepSoundFx()
    {
        _audio_source.clip = _outside_footstep_sound_fx;
        _audio_source.Play();
    }
}
