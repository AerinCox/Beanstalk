using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    private float groundForceMagnifier = 11;
    private float airForceMagnifier = 1.1f;
    private float topVelocity = 20;
    private float groundFriction = 0.13f;
    private int collisionCount = 0;

    void Update()
    {
        handleUserInput();
        if (collisionCount > 0)
        {
            applyFriction();
            handleMaxVelocity();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (ObjectTag.groundObjects.Contains(other.gameObject))
        {
            collisionCount++;
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (ObjectTag.groundObjects.Contains(other.gameObject))
        {
            collisionCount--;
        }
    }

    void applyFriction()
    {
        // TODO: Check if absolute value of velocity is less than groundfriction
        if (rb.velocity.x > 0)
        {
            setVelocityX(rb.velocity.x - groundFriction);
        }
        if (rb.velocity.x < 0)
        {
            setVelocityX(rb.velocity.x + groundFriction);
        }
        if (rb.velocity.z > 0)
        {
            setVelocityZ(rb.velocity.z - groundFriction);
        }
        if (rb.velocity.z < 0)
        {
            setVelocityZ(rb.velocity.z + groundFriction);
        }
    }

    void handleMaxVelocity()
    {
        if (rb.velocity.x >= topVelocity)
        {
            //TODO make this a gradual thing
            setVelocityX(Mathf.Lerp(rb.velocity.x, topVelocity, 0.4f));
        }
        if (rb.velocity.x <= -topVelocity)
        {
            //TODO make this a gradual thing
            setVelocityX(Mathf.Lerp(rb.velocity.x, -topVelocity, 0.4f));
        }
        if (rb.velocity.z >= topVelocity)
        {
            //TODO make this a gradual thing
            setVelocityZ(Mathf.Lerp(rb.velocity.z, topVelocity, 0.4f));
        }
        if (rb.velocity.z <= -topVelocity)
        {
            //TODO make this a gradual thing
            setVelocityZ(Mathf.Lerp(rb.velocity.z, -topVelocity, 0.4f));
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
     }

    void handleUserInput()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
        Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 5, Color.green);
        if(collisionCount == 0){
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddRelativeForce(Vector3.left * airForceMagnifier);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddRelativeForce(Vector3.right * airForceMagnifier);
            }
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddRelativeForce(Vector3.forward * airForceMagnifier);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddRelativeForce(Vector3.back * airForceMagnifier);
            }
            return;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * groundForceMagnifier);
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddRelativeForce(Vector3.left * groundForceMagnifier);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddRelativeForce(Vector3.right * groundForceMagnifier);
            }
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddRelativeForce(Vector3.forward * groundForceMagnifier);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddRelativeForce(Vector3.back * groundForceMagnifier);
            }
        }
    }

    /// Helper Functions
    void setVelocityX(float x)
    {
        rb.velocity = new Vector3(x, rb.velocity.y, rb.velocity.z);
    }
    void setVelocityZ(float z)
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, z);
    }
}
