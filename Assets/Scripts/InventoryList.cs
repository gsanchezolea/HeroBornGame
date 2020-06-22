using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryList <T> where T: class
{
    //Variables
    private T _item;
    public T item
    {
        get { return _item; }
        set { _item = value; }
    }
    
   
    //Constructor
    public InventoryList()
    {
        Debug.Log("Generic list initialized...");
    }

    public void SetItem(T newItem)
    {
        _item = newItem;
        Debug.Log("New item added...");
    }
    //Methods
}
