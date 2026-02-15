using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BallLauncher : MonoBehaviour
{
    public bool isInObject = false;
    public Collider2D playerCollider;
    public Rigidbody2D rb;
    public Vector2 mousePos;
    public LayerMask hamsterLayer;
    public GameObject hamsterPlaceholder;
    public LineRenderer lineRend;

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        lineRend = GetComponentInChildren<LineRenderer>();
    }

    float start_x, start_y, end_x, end_y, force_x, force_y;
    public GameObject clone;
    public int force = 100;

    void OnMouseDown()
    {
        //start at the position of the hamster
        start_x = transform.position.x;
        start_y = transform.position.y;

        //Create clone in the initial object position
        clone = Instantiate(hamsterPlaceholder, transform.position, Quaternion.identity);

    }

    void OnMouseDrag()
    {
        clone.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, 0);

        end_x = clone.transform.position.x;
        end_y = clone.transform.position.y;

        var lineRend = GetComponentInChildren<LineRenderer>();

        if (lineRend != null)
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
        Destroy(lineRend);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var foodData = col.gameObject.GetComponent<EdibleObject>();
        if (foodData != null)

            Debug.Log("Collide check");
        {
            gameObject.transform.localScale = new Vector3(transform.localScale.x + foodData.sizeGainAmount / 10,
                transform.localScale.y + foodData.sizeGainAmount / 10,
                transform.localScale.z + foodData.sizeGainAmount / 10);


            rb.linearDamping -= foodData.speedGainAmount / 10;
            rb.AddForce(Vector2.one * foodData.speedGainAmount/10, ForceMode2D.Impulse);
            //rb.mass -= foodData.speedGainAmount/10;


            rb.AddTorque(foodData.torqueGainAMount * 10);

        }

        Destroy(col.gameObject);
    }
}
