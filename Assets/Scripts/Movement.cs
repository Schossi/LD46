using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public GameObject ImpactTemplate;

    public Transform DashTarget;
    public Transform Head;
    public Rigidbody Rigidbody;
    public float Speed;
    public float Gravity;
    public float Jump;
    public float Dash;

    public float DashHeight;
    public float DashDistance;

    private bool _jump = false;

    private Vector3? _currentDashTarget;
    private float _currentDashHeight;

    private static Vector3 _spawnPosition;

    // Use this for initialization
    void Start()
    {
        ImpactTemplate.SetActive(false);
        _spawnPosition = transform.position;
    }

    public void Respawn()
    {
        transform.position = _spawnPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
            Respawn();

        if (transform.position.y < -10f)
            Respawn();

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _jump = true;
        }

        DashTarget.position = new Vector3(0f, -1000f, 0f);

        Ray ray = new Ray(Head.position, Head.forward);
        RaycastHit hit;
        if (MouseLock.IsLocked && Physics.Raycast(ray, out hit))
        {
            float dashHeight = transform.position.y - hit.point.y;
            float dashDistance = Vector3.Distance(new Vector3(hit.point.x, 0f, hit.point.z), new Vector3(transform.position.x, 0f, transform.position.z));

            if (!IsGrounded() && dashHeight > DashHeight && dashDistance < DashDistance)
            {
                DashTarget.position = hit.point;

                var size = (Camera.main.transform.position - DashTarget.position).magnitude / 50f;
                DashTarget.localScale = new Vector3(size, size, size);

                if (Input.GetButtonDown("Fire1"))
                {
                    _currentDashTarget = hit.point;
                    _currentDashHeight = dashHeight;
                }
            }
        }

        if (MouseLock.IsLocked && Input.GetButton("Fire2"))
        {
            RaycastHit poundHit;
            if (Physics.Raycast(new Ray(transform.position, Vector3.down), out poundHit, float.MaxValue, LayerMask.GetMask("Terrain")) && poundHit.distance > 1f)
            {
                _currentDashTarget = poundHit.point;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (_currentDashTarget.HasValue)
        //{
        //    var impact = Instantiate(ImpactTemplate, collision.contacts[0].point, ImpactTemplate.transform.rotation);
        //    impact.SetActive(true);

        //    _currentDashTarget = null;
        //}
    }

    private void FixedUpdate()
    {
        if (_currentDashTarget.HasValue)
        {
            var waytogo = _currentDashTarget.Value - transform.position;
            var velocity = waytogo.normalized * Dash;

            if (waytogo.magnitude < 10f)
            {
                transform.position = _currentDashTarget.Value;
                Rigidbody.velocity = Vector3.zero;

                var impact = Instantiate(ImpactTemplate, transform.position, ImpactTemplate.transform.rotation);
                impact.SetActive(true);

                _currentDashTarget = null;
            }
            else
            {
                Rigidbody.velocity = velocity;
                return;
            }
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        var horizontalVelocity = new Vector3(Head.right.x, 0f, Head.right.z).normalized * moveHorizontal * Speed;
        var verticalVelocity = new Vector3(Head.forward.x, 0f, Head.forward.z).normalized * moveVertical * Speed;
        var gravity = Vector3.down * Gravity;

        Rigidbody.AddForce(horizontalVelocity + verticalVelocity + gravity, ForceMode.VelocityChange);
        //Rigidbody.AddForce(verticalVelocity, ForceMode.VelocityChange);

        if (_jump)
        {
            Rigidbody.AddForce(Vector3.up * Jump, ForceMode.Impulse);
            _jump = false;
        }

        //Rigidbody.velocity = new Vector3(horizontalVelocity.x + verticalVelocity.x, Rigidbody.velocity.z, horizontalVelocity.z + verticalVelocity.z);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.5f, LayerMask.GetMask("Terrain","BoundsBottom"));
    }
}
