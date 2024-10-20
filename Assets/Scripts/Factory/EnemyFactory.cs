using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ~ 1�� 30�� ����0_0 Cryogonal ��������
1�� 30�� ~ �߰�����_0 ���������� ����0_0 + ����0_1 
2�� �߰�����_0  Regice

�߰�����_0 Kill�ϰ��� ~ 1�� ����1_0 Bergmite �Ǿ
1�е� ����1_0 ������
1�� ~ 2�� ����1_0 + ����1_1 Avalugg ũ�����̽�
2�� ~ 2�� 30�� ����1_1
2��30�� �߰�����_1 --- 5��30��  Glalie

�߰�����_1 Kill�ϰ��� ~ 1�� ����2_0
1�е� ����2_0 ������
1�� ~ 2�� 30�� ����2_0   Vanillite �ٴ���Ƽ
2��30�� �߰�����_2 --- 9�� Froslass

�߰�����_2 Kill�ϰ��� ~ 1�� ����3_0  Vanillish �ٴҸ�ġ
1�е� ����3_0 ������
1�� ~ 2�� ����3_0 + ����3_1   Vanilluxe ��ٴҶ�
2�� ~ 2�� 30�� ����3_1
2��30�� ��������--- 12�� 30�� Kyurem
*/
public class EnemyFactory : MonoBehaviour
{
    
    


    [Header("��ȯ ��Ÿ��"), SerializeField] float _spawnCoolTime = 1f;

    [Header("�Ϲ� ���� ���丮"), SerializeField] MobFactory _normalFactory;
    [Header("���� ���� ���丮"), SerializeField] MobFactory _bossFactory;
    private void Awake()
    {
        
    }
    void SpwanPatern_0()
    {
        StartCoroutine(CRT_Patern_0(_spawnCoolTime, 90f));
    }
    IEnumerator CRT_Patern_0(float gap, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            yield return new WaitForSeconds(gap);
            elapsedTime += gap;
        }

    }
}
