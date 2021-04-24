using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryChooser: MonoBehaviour 
{
    [SerializeField] private GameObject responsibleCategory;

    
public GameObject ResponsibleCategory { get => responsibleCategory; set => responsibleCategory = value; }
}
