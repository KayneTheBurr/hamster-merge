using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class BallLauncher : MonoBehaviour
{
    public bool isInObject = false;
    public Collider2D playerCollider;
    public Rigidbody2D rb;
    public Vector2 mousePos;
    public LayerMask hamsterLayer;
    public GameObject hamsterPlaceholder;
    LineRenderer lineRend;

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        lineRend = GetComponentInChildren<LineRenderer>();
    }

    float start_x, start_y, end_x, end_y, force_x, force_y;
    GameObject clone;
    public int force = 100;

    void OnMouseDown()
    {
        start_x = transform.position.x;
        start_y = transform.position.y;

        //Create clone in the initial object position and make minor clone component adjustments
        clone = Instantiate(hamsterPlaceholder, transform.position, Quaternion.identity);
       
    }

    void OnMouseDrag()
    {
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        end_x = transform.position.x;
        end_y = transform.position.y;

        var lineRend = GetComponentInChildren<LineRenderer>();
        
        if ( lineRend != null)
        {
            lineRend.enabled = true;
            lineRend.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 1));
            lineRend.SetPosition(1, new Vector3(clone.transform.position.x, clone.transform.position.y, 1));
            
        }
    }

    void OnMouseUp()
    {
        force_x = start_x - end_x;
        force_y = start_y - end_y;

        rb.AddForce(Vector2.up * force_y * force);
        rb.AddForce(Vector2.right * force_x * force);

        Destroy(clone);

    }
}
