using UnityEngine;
using System.Collections;

public class EntityBase : MonoBehaviour
{
 
    public GameObject liveAnimationPrefab;




    protected LimbsHPProperty Limbs = new LimbsHPProperty();
   // protected EntityAC AnimationController = new EntityAC();

    public virtual string GetName()
    {
        return "";
    }


	// Use this for initialization
	void Start()
    {
          
        GameObject go;
        go = (GameObject)Instantiate(liveAnimationPrefab, this.transform.position, Quaternion.identity);
        go.transform.parent = gameObject.transform;
    

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
