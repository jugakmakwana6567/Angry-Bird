using System.Collections;
using UnityEngine;

public class playermovement : MonoBehaviour
{
     Rigidbody2D rb;
    bool ispressed;
    public LineRenderer[] line;
    public Transform[] stripPositions;
    public Transform centerPos;
    public GameObject birdPrefab;
    public float birdPositionOffset;
    Collider2D birdCollider;
    public float maxLength;
    public float bottomBoundary;
  
    public Transform idlePosition;
    public float force;
    public Vector3 currentPosition;


    // Start is called before the first frame update
    void Start()
    {
        line[0].positionCount = 2;
        line[1].positionCount = 2;
        line[0].SetPosition(0, stripPositions[0].position);
        line[1].SetPosition(0, stripPositions[1].position);
         CreateBird();
       
    }
    void CreateBird()
    {
        rb = Instantiate(birdPrefab).GetComponent<Rigidbody2D>();
        birdCollider = rb.GetComponent<Collider2D>();
        birdCollider.enabled = false;

        rb.isKinematic = true;

        ResetStrips();
        
    }


    // Update is called once per frame
    
    
        void Update()
        {
            if (ispressed)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10;

                currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                currentPosition = centerPos.position + Vector3.ClampMagnitude(currentPosition
                    - centerPos.position, maxLength);

                currentPosition = ClampBoundary(currentPosition);

                SetStrips(currentPosition);

                if (birdCollider)
                {
                    birdCollider.enabled = true;
                }
            }
            else
            {
                ResetStrips();
            }
        }
    
    private void OnMouseDown()
    {
        ispressed = true;

    }
    private void OnMouseUp()
    {
        ispressed = false;
        Shoot();
        currentPosition =idlePosition.position;
    }

    void Shoot()
    {
        rb.isKinematic = false;
        Vector3 birdForce = (currentPosition - centerPos.position) * force * -1;
        rb.velocity = birdForce;
        rb = null;
        birdCollider = null;
        
        Invoke("CreateBird", 2);

    }


    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
        
    }

    void SetStrips(Vector3 position)
    {
        line[0].SetPosition(1, position);
        line[1].SetPosition(1, position);

        if (rb)
        {
            Vector3 dir = position - centerPos.position;
            rb.transform.position = position + dir.normalized * birdPositionOffset;
       
        }
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }
 


}
