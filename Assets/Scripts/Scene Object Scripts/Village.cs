
using UnityEngine;

public class Village : BaseSceneObject
{
    
    public override bool WasSelected()
    { 
        Debug.Log("village: "+ Utils.WasSelected(this));
        return Utils.WasSelected(this);
    }
}
