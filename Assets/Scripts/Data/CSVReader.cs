using System.Collections.Generic;
using UnityEngine;



// CSVReader Ŭ����
public class CSVReader : MonoBehaviour
{
    
    [Header("�Ϲ� ��ų"), SerializeField] TextAsset _skillDataFile;
    [Header("���� ��ų"), SerializeField] TextAsset _charSkillDataFile;
    [Header("ĳ����"), SerializeField] TextAsset _charDataFile;
    [Header("����"), SerializeField] TextAsset _mobDataFile;

    ICSVDataConverter _skillData = new SkillData();
    ICSVDataConverter _charSkillData = new CharSkillData();
    ICSVDataConverter _charData = new CharacterData();
    ICSVDataConverter _mobData = new EnemyData();

    private void Awake()
    {
        ReadCSV(_skillDataFile, _skillData);
        ReadCSV(_charSkillDataFile, _charSkillData);
        ReadCSV(_charDataFile, _charData);
        ReadCSV(_mobDataFile, _mobData);

        CharacterSelector.Instance.SetData(_skillData as SkillData);
        CharacterSelector.Instance.SetData(_charSkillData as CharSkillData);
        CharacterSelector.Instance.SetData(_charData as CharacterData);
        DataHandler.Instance.SetData(_mobData as EnemyData);
        //CharacterSelector.Instance.SetData(_mobData as EnemyData);
    }

    void ReadCSV(TextAsset dataFile, ICSVDataConverter converter)
    {
        string[] lines = System.Text.Encoding.UTF8.GetString(dataFile.bytes).Split('\n');
        List<string> rows = new List<string>(lines);
        converter.ConvertToDictionary(rows);
    }
}