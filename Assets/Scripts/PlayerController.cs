using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask _isGround;

    private Animator _animator;

    private NavMeshAgent _agent;

    private bool _isMoving = false;

    private Vector3 _targetPositionGround;
    private float _distance;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            _animator.SetBool("isMoving", value);
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _targetPositionGround = transform.position;
    }
    private void Update()
    {
        Moving();
        DistanceDetection();
    }

    public void Moving()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray agentRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(agentRay, out hitInfo, 1000, _isGround))
            {
                _agent.SetDestination(hitInfo.point);
                _targetPositionGround = hitInfo.point;
            }
        }
    }

    private void DistanceDetection()
    {
        _distance = Vector3.Distance(_targetPositionGround, transform.position);
        IsMoving = _distance <= 0.5f ?false : true;
    }
}
