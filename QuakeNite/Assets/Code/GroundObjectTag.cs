using UnityEngine;

public class GroundObjectTag : MonoBehaviour
{
    void Start()
    {
        if(ObjectTag.groundObjects == null){
            ObjectTag.groundObjects = new System.Collections.Generic.List<GameObject>();
        }
        
        ObjectTag.groundObjects.Add(this.gameObject);
    }
}
