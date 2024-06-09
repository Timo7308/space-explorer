using UnityEngine;

//Scriptable Object for dialogue instructions 
[CreateAssetMenu(fileName = "New Dialogue Instruction", menuName = "Dialogue/Dialogue Instruction")]
public class DialogueInstruction : ScriptableObject
{
    [TextArea(3, 10)]
    public string instructionText;
}
