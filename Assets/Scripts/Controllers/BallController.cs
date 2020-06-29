
using UnityEngine;

using UnityEngine.EventSystems;

public class BallController : MonoBehaviour
{
    [Header("Values for force applied and Max drag")]
   
    [SerializeField] float forceApplied = 100f;
    [SerializeField] float maxDrag = 5f;

    //controling launch cycle, resets when new ball is spawned
    bool hasLaunched = false;
    
    Vector3 force;
    Rigidbody rb;
    LineRenderer lr;
    Touch _touch;
    Vector3 dragStartPos;

    ArrowPointer arrow;

    void Awake()
    {
        //grabbing the components references
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        arrow = GetComponentInChildren<ArrowPointer>();
        arrow.gameObject.SetActive(false);
        
        
       //TODO may not be needed,because ball is destroyed on colliding with target
       // GameHandler.OnReset += ResetBall;// subscribing to Reset Lvl event
    }

    
    
    void Update()
    {

        if (isClickingUi())// trying to stop launching ball is UI is pressed
        { return; }

        if (Input.touchCount > 0 && hasLaunched==false)// proccesing launch/line draw cycle
        {
            _touch = Input.GetTouch(0);
            ;
            if (_touch.phase == TouchPhase.Began)
            {
                StartDrag();
            }
            if (_touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            if (_touch.phase == TouchPhase.Ended)
            {
                DragEnding();
            }
            
        }
    }

    

    void StartDrag()//proccesing start of the dragging
    {

            Vector3 dragStartPos = GetRayHitPosition();
            
            
            lr.positionCount = 1;
            lr.SetPosition(0, dragStartPos);

        arrow.gameObject.SetActive(true);
        arrow.transform.LookAt(dragStartPos);
    }

    void Dragging()//dragging in progress
    {

            Vector3 draggingPos = GetRayHitPosition();
          
                          
                     
            lr.positionCount = 2;
            lr.SetPosition(0, draggingPos);

        arrow.transform.LookAt(draggingPos);
    }

    void DragEnding()//dragging finished
    {
        lr.positionCount = 0;
        Vector3 dragEnd= GetRayHitPosition();

        force = dragStartPos - dragEnd;//direction
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * forceApplied;

        arrow.transform.LookAt(dragEnd);
        arrow.gameObject.SetActive(false);
        rb.AddForce(clampedForce, ForceMode.Impulse);
        hasLaunched = true;
    }

    private Vector3 GetRayHitPosition()//registering position wich ray has hit
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        Plane plane = new Plane(Vector3.up, transform.position);
        float distance ; // returns distance
        if (plane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            hitPoint.y = 0.5f;
            return hitPoint;
        }
        else return Vector3.zero;
    }
  /* public void ResetBall()// destroy the ball, so the Game handler can respawn new
    {
        Destroy(this.gameObject);
    }*/
   
     bool isClickingUi()//checking if we clicking UI
     {
        return      EventSystem.current.IsPointerOverGameObject(0) && Input.GetTouch(0).phase == TouchPhase.Began
                 || EventSystem.current.IsPointerOverGameObject(0) && Input.GetTouch(0).phase == TouchPhase.Moved;
     }

    
}