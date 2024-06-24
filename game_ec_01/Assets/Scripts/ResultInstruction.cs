using UnityEngine;

//Scriptable Object for dialogue instructions 
[CreateAssetMenu(fileName = "New Result Instruction", menuName = "Result/Result Instruction")]
public class ResultInstruction : ScriptableObject {
    [TextArea(3, 10)]
    public string resultText;
}
