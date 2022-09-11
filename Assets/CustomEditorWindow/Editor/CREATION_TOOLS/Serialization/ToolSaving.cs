using UnityEngine;
using UnityEditor;
using System.IO;

using CREATION_TOOLS;

public static class ToolSaving
{
    public static void SaveQuest(int tQuestCount, QUEST_DATA tQuest)
    {
        string filename = Application.dataPath + "\\OUTPUT\\QUEST_OUTPUT.hex";
        if (!File.Exists(filename))
        {
            File.Create(filename);
        }

        using FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Write);
        using BinaryWriter bW = new(fileStream);

        bW.Write(tQuestCount);

        for (int i = 0; i < tQuestCount; i++)
        {
            bW.Write(Serialization.ToByteArray((QUEST_DATA)tQuest));
        }
        fileStream.Close();
        bW.Close();
    }
}
  
