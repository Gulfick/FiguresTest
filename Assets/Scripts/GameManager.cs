using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using VContainer.Unity;

public class GameManager: IStartable, ITickable
{
    private LineClick _lineClick;
    private FigureChecker _figureChecker;
    private MainUI _mainUI;
    private TargetCheck _targetCheck;

    private List<string> _names = new List<string>();
    private List<string> _namesClicked = new List<string>();
    private bool _notFirst;

    public GameManager(LineClick lineClick, FigureChecker figureChecker, MainUI mainUI, TargetCheck targetCheck)
    {
        _lineClick = lineClick;
        _figureChecker = figureChecker;
        _mainUI = mainUI;
        _targetCheck = targetCheck;
    }

    public void Start()
    {
        var figures = GameObject.FindGameObjectsWithTag("Figure");
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < figures.Length; i++)
        {
            builder.AppendLine($"{figures[i].name}");
        }

        _mainUI.NamesList.GetComponent<TextMeshProUGUI>().text = builder.ToString();
        _lineClick.OnLineClick += CompareNames;
    }
    
    public async void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _targetCheck.IsTriggered)
        {
            await NewNames();
        }
    }

    private async UniTask NewNames()
    {
        if (_notFirst)
            _lineClick.Restart();
        else
            _notFirst = true;
        
        _namesClicked.Clear();
        var id = await SendJson(_figureChecker.Names);
        _mainUI.NamesList.gameObject.SetActive(true);
        var json = await Requests.Get($"http://158.160.3.255:8021/exercises/get_exercise_data?record_id={id}");
        _names = JsonConvert.DeserializeObject<List<string>>(json);
    }
    
    private async UniTask<string> SendJson(List<string> names)
    {
        var json = JsonConvert.SerializeObject(names);
        return await Requests.SendJson("http://158.160.3.255:8021/exercises/set_exercise_data", json);
    }

    private void CompareNames(string clickedName, int lineIndex)
    {
        if (_names.Contains(clickedName))
        {
            if(_namesClicked.Contains(clickedName))
                return;
            
            _namesClicked.Add(clickedName);
            _lineClick.ColorizeLine(lineIndex, Color.green);
            if(_names.Count == _namesClicked.Count)
                _mainUI.WinMenu.SetActive(true);
        }
        else
        {
            _lineClick.ColorizeLine(lineIndex, Color.red);
        }
    }
}
