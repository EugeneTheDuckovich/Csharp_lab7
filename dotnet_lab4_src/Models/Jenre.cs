using AudioLibrary.Utilities;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AudioLibrary.Models;

public class Jenre : NotifyPropertyChanged, IEditableObject
{
    struct JenreData 
    {
        internal int id;
        internal string name;
    }

    private JenreData currentData;
    private JenreData backupData;

    public int Id
    {
        get => currentData.id;
        set
        {
            currentData.id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    public string Name
    {
        get => currentData.name;
        set
        {
            if (currentData.name != value)
            {
                currentData.name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    
    [JsonConstructor]
    public Jenre()
    {
        currentData = new JenreData();
    }

    public Jenre(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }

    private bool _editInProcess;
    public void BeginEdit()
    {
        if (!_editInProcess)
        {
            backupData = currentData;
            _editInProcess = true;
        }
    }

    public void CancelEdit()
    {
        if (_editInProcess)
        {
            currentData = backupData;
            _editInProcess = false;
        }
    }

    public void EndEdit()
    {
        if (_editInProcess)
        {
            backupData = new JenreData();
            _editInProcess = false;
        }
    }
}
