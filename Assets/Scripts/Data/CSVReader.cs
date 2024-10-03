using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CSVReader : MonoBehaviour
{
    [Header("�Ϲ� ��ų"), SerializeField] TextAsset _skillDataFile;
    [Header("���� ��ų"), SerializeField] TextAsset _charSkillDataFile;
    [Header("ĳ����"), SerializeField] TextAsset _charDataFile;
    SkillData _skillData;
    [SerializeField] SkillManager _skillManager;

    List<string> _rowSkillDatas = new List<string>();
    List<string> _rowCharSkillDatas = new List<string>();
    List<string> _rowCharDatas = new List<string>();
    private void Awake()
    {
        _skillData = new SkillData();
        ReadSkillCSV();
        ReadCharSkillCSV();
        ReadCharCSV();


        CharacterSelector.Instance.SetData(_skillData);
        //���Ѿ�� ����
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
