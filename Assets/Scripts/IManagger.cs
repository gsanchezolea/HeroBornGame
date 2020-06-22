using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManagger 
{
  string State { get; set; }
    void Initialize();   
}
