using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameButton : MonoBehaviour
{
    [Header("���� ��ų ��ư"), SerializeField] Button _skillButton;
    [Header("���� ��ų �г�"), SerializeField] GameObject _skillPanel;

    [Header("�Ͻ����� ��ư"), SerializeField] Button _pauseButton;
    [Header("�Ͻ����� �г�"), SerializeField] GameObject _pausePanel;

    [Header("���θ޴� ��ư"), SerializeField] Button _mainmenuButton;
    [Header("�÷��� ��ư"), SerializeField] Button _playButton;
    [Header("�ٽ��ϱ� ��ư"), SerializeField] Button _replaybutton;

    [Header("���� �������� ��ư"), SerializeField] Button _nextStageButton;
    private void Awake()
    {
        //_skillButton.onClick.AddListener(() => Click_Skill());
        _pauseButton.onClick.AddListener(() => Click_Pause());

        _mainmenuButton.onClick.AddListener(() => Click_MainMenu());
        _playButton.onClick.AddListener(() => Click_Play());
        _replaybutton.onClick.AddListener(() => Click_Replay());

        _nextStageButton.onClick.AddListener(() => Click_NextStage());
        //_skillPanel.SetActive(false);
        _pausePanel.SetActive(false);
    }
    void Click_Skill()
    {
        _skillPanel.SetActive(!_skillPanel.activeSelf);
    }
    void Click_Pause()
    {
        _pausePanel.SetActive(true);//�Ͻ������� ��Ÿ�� ���� https://www.inflearn.com/questions/1005470/%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%BF%A8%ED%83%80%EC%9E%84-%EC%BD%94%EB%A3%A8%ED%8B%B4%ED%99%9C%EC%9A%A9-%EC%A7%88%EB%AC%B8

        Time.timeScale = 0;
    }
    void Click_MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    void Click_Play()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
    }
    void Click_Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CharacterSelector.Instance.StartGame();
    }
    void Click_NextStage()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
         
    }
}
