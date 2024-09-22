using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CSVReader : MonoBehaviour
{
    [Header("일반 스킬"), SerializeField] TextAsset _skillDataFile;
    [Header("전용 스킬"), SerializeField] TextAsset _charSkillDataFile;
    [Header("캐릭터"), SerializeField] TextAsset _charDataFile;
    SkillData _skillData;
    [SerializeField] SkillManager _skillManager;
    [SerializeField] CharacterSelector _characterSelector;

    List<string> _rowSkillDatas = new List<string>();
    List<string> _rowCharSkillDatas = new List<string>();
    List<string> _rowCharDatas = new List<string>();
    private void Awake()
    {
        _skillData = new SkillData();
        ReadSkillCSV();
        ReadCharSkillCSV();
        ReadCharCSV();

        
        _characterSelector.SetData(_skillData);
        //씬넘어갈때 시전
        //_skillManager.SetSkillAwake(_skillData);
    }
    void ReadSkillCSV()
    {
        string[] lines = System.Text.Encoding.UTF8.GetString(_skillDataFile.bytes).Split('\n');
        bool isTitle = true;
        foreach (string line in lines)
        {
            if (isTitle)
                isTitle = false;
            else
                _rowSkillDatas.Add(line);
        }
        _skillData.ConvertSkillCSVToDictionary(_rowSkillDatas);
    }
    void ReadCharSkillCSV()
    {
        string[] lines = System.Text.Encoding.UTF8.GetString(_charSkillDataFile.bytes).Split('\n');
        bool isTitle = true;
        foreach (string line in lines)
        {
            if (isTitle)
                isTitle = false;
            else
                _rowCharSkillDatas.Add(line);
        }
        _skillData.ConvertCharSkillCSVToDictionary(_rowCharSkillDatas);
    }
    void ReadCharCSV()
    {
        string[] lines = System.Text.Encoding.UTF8.GetString(_charDataFile.bytes).Split('\n');
        bool isTitle = true;
        foreach (string line in lines)
        {
            if (isTitle)
                isTitle = false;
            else
                _rowCharDatas.Add(line);
        }
        _skillData.ConvertCharacterCSVToDictionary(_rowCharDatas);
    }
}
