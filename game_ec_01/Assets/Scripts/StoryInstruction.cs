using UnityEngine;

//Scriptable Object for dialogue instructions 
[CreateAssetMenu(fileName = "New Dialogue Instruction", menuName = "Story/Story Instruction")]
public class StoryInstruction : ScriptableObject {
    [TextArea(3, 10)]
    public string instructionText;
}
