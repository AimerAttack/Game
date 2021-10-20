using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;
using Node = UnityEditor.Experimental.GraphView.Node;

public class DialogueNode : Node
{
    public string GUID;
    public string DialogueText;
    public bool EntryPoint = false;
}
