using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  #region 
  public static Transform instance;
  private void Awake()
  {
    instance = this.transform;
  }
  #endregion
}
