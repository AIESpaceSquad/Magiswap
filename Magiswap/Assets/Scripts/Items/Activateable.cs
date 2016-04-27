using UnityEngine;
using System.Collections;

public class Activateable : MonoBehaviour {

    [SerializeField]
    protected bool showLogs = false;

    [SerializeField]//-1 requires no item, If an item's key is -1 then it activates nothing;
    protected int itemKey = -1;

    public bool isActive
    {
        get;
        protected set;
    }

    //null is valid input
    public bool AttemptActivate(Item in_itemUsed = null)
    {
        if (CanActivate(in_itemUsed))
        {
            if (showLogs)
                Debug.Log("Activate Attempt Passed");
            isActive = true;
            OnActivateImmediate(in_itemUsed);
            return true;
        }
        if (showLogs)
            Debug.Log("Activate Attempt Failed");
        return false;
    }

    public bool CanActivate(Item in_itemUsed = null)
    {
        if (isActive)
        {
            if (showLogs)
                Debug.Log("Activate will Fail, Item is already active");
            return false;
        }
        if (itemKey == -1)
        {
            if (showLogs)
                Debug.Log("Activate will Pass, Activateable requires no key");
            return true;
        }
        if (in_itemUsed == null)
        {
            if (showLogs)
                Debug.Log("Activate may Fail, Activateable requires a key but was not provided with an item");
            return false;
        }
        if (in_itemUsed.activatorKey == itemKey)
        {
            if (showLogs)
                Debug.Log("Activate will Pass, Key provided was valid");
            return true;
        }
        if (showLogs)
            Debug.Log("Activate will Fail, misc. failure, provided key may be invalid");
        return false;
    }

    protected virtual void  Reset()
    {
        isActive = false;
    }

    protected virtual void OnActivateImmediate(Item in_itemUsed)
    {

    }

}
